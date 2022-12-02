using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using System.Diagnostics;
using System.Xml.Linq;
using System.Xml;
using System.Windows;

namespace EasySaveApp.model
{
    class Model
    {

        //Declaration of all variables and properties
        public int checkDataBackup;
        private string serializeObj;
        public string backupListFile = System.Environment.CurrentDirectory + @"\Works\";
        public string stateFile = System.Environment.CurrentDirectory + @"\State\";
        public string logDir = @"..\..\..\Logs\";
        public DataState DataState { get; set; }
        public string NameStateFile { get; set; }
        public string BackupNameState { get; set; }
        public string Resource { get; set; }
        public int Nbfilesmax { get; set; }
        public int Nbfiles { get; set; }
        public long Size { get; set; }
        public float Progs { get; set; }
        public string TargetResource { get; set; }
        public string SaveName { get; set; }
        public int Type { get; set; }
        public string SourceFile { get; set; }
        public string TypeString { get; set; }
        public long TotalSize { get; set; }
        public int NbFileMmax { get; private set; }
        public TimeSpan TimeTransfert { get; set; }
        public string UserMenuInput { get; set; }
        public string MirrorResource { get; set; }
        public bool Format { get; set; }

        public Model()
        {
            UserMenuInput = " ";

            if (!Directory.Exists(backupListFile)) //Check if the folder is created
            {
                DirectoryInfo di = Directory.CreateDirectory(backupListFile); //Function that creates the folder
            }
            backupListFile += @"backupList.json"; //Create a JSON file

            if (!Directory.Exists(stateFile))//Check if the folder is created
            {
                DirectoryInfo di = Directory.CreateDirectory(stateFile); //Function that creates the folder
            }
            stateFile += @"state.json"; //Create a JSON file

            if (!Directory.Exists(logDir)) //Check if the folder is created
            {
                DirectoryInfo di = Directory.CreateDirectory(logDir); //Function that creates the folder
            }


        }

        public void CompleteSave(string inputpathsave, string inputDestToSave, bool copyDir, bool verif)
        {
            DataState = new DataState(NameStateFile);
            DataState.SaveState = true;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            TotalSize = 0;
            NbFileMmax = 0;
            Size = 0;
            Nbfiles = 0;
            Progs = 0;

            DirectoryInfo resource = new DirectoryInfo(inputpathsave);

            if (!resource.Exists)
            {
                throw new DirectoryNotFoundException("ERROR: Directory Not Found ! " + inputpathsave);
            }

            DirectoryInfo[] Resource = resource.GetDirectories();
            Directory.CreateDirectory(inputDestToSave); // if already exist do nothing

            FileInfo[] files = resource.GetFiles();

            if (!verif)
            {
                foreach (FileInfo file in files) // Calcul resource size
                {
                    TotalSize += file.Length;
                    NbFileMmax++;
                }
                foreach (DirectoryInfo subresource in Resource) // Calcul subresource size
                {
                    FileInfo[] Maxfiles = subresource.GetFiles();
                    foreach (FileInfo file in Maxfiles)
                    {
                        TotalSize += file.Length;
                        NbFileMmax++;
                    }
                }

            }


            foreach (FileInfo file in files)
            {
                string tempPath = Path.Combine(inputDestToSave, file.Name);

                if (Size > 0)
                {
                    Progs = ((float)Size / TotalSize) * 100;
                }

                //Systems which allows to insert the values ​​of each file in the report file.
                DataState.SourceFileState = Path.Combine(inputpathsave, file.Name);
                DataState.TargetFileState = tempPath;
                DataState.TotalSizeState = NbFileMmax;
                DataState.TotalFileState = TotalSize;
                DataState.TotalSizeRestState = TotalSize - Size;
                DataState.FileRestState = NbFileMmax - Nbfiles;
                DataState.ProgressState = Progs;

                UpdateStateFile();

                file.CopyTo(tempPath, true);
                Nbfiles++;
                Size += file.Length;

            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copyDir)
            {
                foreach (DirectoryInfo subdir in Resource)
                {
                    string tempPath = Path.Combine(inputDestToSave, subdir.Name);
                    CompleteSave(subdir.FullName, tempPath, copyDir, true);
                }
            }
            ResetValue();
            UpdateStateFile();
            stopwatch.Stop();
            TimeTransfert = stopwatch.Elapsed; // Note the time passed
        }

        /// <summary>
        /// function called when differential backup is selected
        /// </summary>
        /// <param name="pathA"></param>
        /// <param name="pathB"></param>
        /// <param name="pathC"></param>
        public void DifferentialSave(string pathA, string pathB, string pathC)
        {
            DataState = new DataState(NameStateFile);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            DataState.SaveState = true;
            TotalSize = 0;
            NbFileMmax = 0;

            System.IO.DirectoryInfo resource1 = new System.IO.DirectoryInfo(pathA);
            System.IO.DirectoryInfo resource2 = new System.IO.DirectoryInfo(pathB);

            // Take a snapshot of the file system.  
            IEnumerable<System.IO.FileInfo> list1 = resource1.GetFiles("*.*", System.IO.SearchOption.AllDirectories);
            IEnumerable<System.IO.FileInfo> list2 = resource2.GetFiles("*.*", System.IO.SearchOption.AllDirectories);

            //A custom file comparer defined below  
            FileCompare myFileCompare = new FileCompare();

            var queryList1Only = (from file in list1 select file).Except(list2, myFileCompare);
            Size = 0;
            Nbfiles = 0;
            Progs = 0;

            foreach (var v in queryList1Only)
            {
                TotalSize += v.Length;
                NbFileMmax++;

            }


            foreach (var v in queryList1Only)
            {
                string tempPath = Path.Combine(pathC, v.Name);
                DataState.SourceFileState = Path.Combine(pathA, v.Name);
                DataState.TargetFileState = tempPath;
                DataState.TotalSizeState = NbFileMmax;
                DataState.TotalFileState = TotalSize;
                DataState.TotalSizeRestState = TotalSize - Size;
                DataState.FileRestState = NbFileMmax - Nbfiles;
                DataState.ProgressState = Progs;
                UpdateStateFile();
                v.CopyTo(tempPath, true);
                Size += v.Length;
                Nbfiles++;
            }

            ResetValue();
            UpdateStateFile();
            stopwatch.Stop();
            TimeTransfert = stopwatch.Elapsed;
        }


        private void ResetValue()
        {
            DataState.SourceFileState = null;
            DataState.TargetFileState = null;
            DataState.TotalFileState = 0;
            DataState.TotalSizeState = 0;
            DataState.TotalSizeRestState = 0;
            DataState.FileRestState = 0;
            DataState.ProgressState = 0;
            DataState.SaveState = false;
        }


        private void UpdateStateFile()//Function that updates the status file.
        {
            List<DataState> stateList = new List<DataState>();
            this.serializeObj = null;
            if (!File.Exists(stateFile)) //Checking if the file exists
            {
                File.Create(stateFile).Close();
            }

            string jsonString = File.ReadAllText(stateFile);  //Reading the json file

            if (jsonString.Length != 0) //Checking the contents of the json file is empty or not
            {
                DataState[] list = JsonConvert.DeserializeObject<DataState[]>(jsonString); //Derialization of the json file

                foreach (var obj in list) // Loop to allow filling of the JSON file
                {
                    if (obj.SaveNameState == this.NameStateFile) //Verification so that the name in the json is the same as that of the backup
                    {
                        obj.SourceFileState = this.DataState.SourceFileState;
                        obj.TargetFileState = this.DataState.TargetFileState;
                        obj.TotalFileState = this.DataState.TotalFileState;
                        obj.TotalSizeState = this.DataState.TotalSizeState;
                        obj.FileRestState = this.DataState.FileRestState;
                        obj.TotalSizeRestState = this.DataState.TotalSizeRestState;
                        obj.ProgressState = this.DataState.ProgressState;
                        obj.BackupDateState = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                        obj.SaveState = this.DataState.SaveState;
                    }

                    stateList.Add(obj); //Allows you to prepare the objects for the json filling

                }

                this.serializeObj = JsonConvert.SerializeObject(stateList.ToArray(), Newtonsoft.Json.Formatting.Indented) + Environment.NewLine; //Serialization for writing to json file

                File.WriteAllText(stateFile, this.serializeObj); //Function to write to JSON file
            }
        }



        /// <summary>
        /// update log file on: \EasySoft\bin
        /// </summary>
        /// <param name="savename"></param>
        /// <param name="sourcedir"></param>
        /// <param name="targetdir"></param>
        public void UpdateLogFile(string savename, string sourcelog, string targetlog)
        {
            string Time = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", TimeTransfert.Hours, TimeTransfert.Minutes, TimeTransfert.Seconds, TimeTransfert.Milliseconds / 10);
            DataLogs datalogs = new DataLogs
            {
                SaveNameLog = savename,
                SourceLog = sourcelog,
                TargetLog = targetlog,
                BackupDateLog = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                TotalSizeLog = TotalSize,
                TransactionTimeLog = Time
            };
            string path = System.Environment.CurrentDirectory;
            var directory = System.IO.Path.GetDirectoryName(path);
            string pathfile = directory + @"DailyLogs_" + DateTime.Now.ToString("dd-MM-yyyy") + ".json";
            string pathfiles = directory + @"DailyLogs_" + DateTime.Now.ToString("dd-MM-yyyy") + ".xml";
            string serializeObj = JsonConvert.SerializeObject(datalogs, Newtonsoft.Json.Formatting.Indented) + Environment.NewLine;

            if (Format == true)
            {
                XmlDocument xdoc = new XmlDocument();

                try
                {
                    xdoc.Load(pathfiles);
                }
                catch
                {
                    XElement Logs = new XElement("Logs");
                    StreamWriter sr = new StreamWriter(pathfiles);
                    sr.WriteLine(Logs);
                    sr.Close();
                    xdoc.Load(pathfiles);

                }
                XmlNode Log = xdoc.CreateElement("log");

                XmlNode Name = xdoc.CreateElement("name");
                XmlNode SourceFile = xdoc.CreateElement("sourceFile");
                XmlNode TargetFile = xdoc.CreateElement("TargetFile");
                XmlNode Date = xdoc.CreateElement("date");
                XmlNode SizeOctet = xdoc.CreateElement("size");
                XmlNode TransfertTime = xdoc.CreateElement("transfertTime");

                Name.InnerText = datalogs.SaveNameLog;
                SourceFile.InnerText = datalogs.SourceLog;
                TargetFile.InnerText = datalogs.TargetLog;
                Date.InnerText = datalogs.BackupDateLog;
                SizeOctet.InnerText = datalogs.TotalSizeLog.ToString();
                TransfertTime.InnerText = datalogs.TransactionTimeLog;

                Log.AppendChild(Name);
                Log.AppendChild(SourceFile);
                Log.AppendChild(TargetFile);
                Log.AppendChild(Date);
                Log.AppendChild(SizeOctet);
                Log.AppendChild(TransfertTime);

                xdoc.DocumentElement.PrependChild(Log);
                xdoc.Save(pathfiles);
            }
            else
            {
                File.AppendAllText(pathfile, serializeObj);
            }
        }
    }
}
