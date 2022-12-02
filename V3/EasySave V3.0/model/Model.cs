﻿using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using System.Diagnostics;
using System.Xml.Linq;
using System.Xml;
using System.Windows;
using System.DirectoryServices.ActiveDirectory;

namespace EasySaveApp.model
{
    class Model
    {

        //Declaration of all variables and properties
        public int checkDataBackup;
        private string serializeObj;
        public string backupListFile = System.Environment.CurrentDirectory + @"\Works\";
        public string stateFile = System.Environment.CurrentDirectory + @"\State\";
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
                List<object> jsonContent = new List<object>();
                if (File.Exists(pathfile))
                {
                    string oldJsonFileContent = File.ReadAllText(pathfile);
                    jsonContent = JsonConvert.DeserializeObject<List<object>>(oldJsonFileContent);
                }
                jsonContent.Add(datalogs);
                string serializedObj = JsonConvert.SerializeObject(jsonContent, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(pathfile, serializedObj);
            }
        }


        public void AddSave(Backup backup) //Function that creates a backup job
        {
            List<Backup> backupList = new List<Backup>();
            this.serializeObj = null;

            if (!File.Exists(backupListFile)) //Checking if the file exists
            {
                File.WriteAllText(backupListFile, this.serializeObj);
            }

            string jsonString = File.ReadAllText(backupListFile); //Reading the json file

            if (jsonString.Length != 0) //Checking the contents of the json file is empty or not
            {
                Backup[] list = JsonConvert.DeserializeObject<Backup[]>(jsonString); //Derialization of the json file
                foreach (var obj in list) //Loop to add the information in the json
                {
                    backupList.Add(obj);
                }
            }
            backupList.Add(backup); //Allows you to prepare the objects for the json filling

            this.serializeObj = JsonConvert.SerializeObject(backupList.ToArray(), Newtonsoft.Json.Formatting.Indented) + Environment.NewLine; //Serialization for writing to json file
            File.WriteAllText(backupListFile, this.serializeObj); // Writing to the json file

            DataState = new DataState(this.SaveName);//Class initiation

            DataState.BackupDateState = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"); //Adding the time in the variable
            AddState(); //Call of the function to add the backup in the report file.
        }

        public void AddState() //Function that allows you to add a backup job to the report file.
        {
            List<DataState> stateList = new List<DataState>();
            this.serializeObj = null;

            if (!File.Exists(stateFile)) //Checking if the file exists
            {
                File.Create(stateFile).Close();
            }

            string jsonString = File.ReadAllText(stateFile); //Reading the json file

            if (jsonString.Length != 0)
            {
                DataState[] list = JsonConvert.DeserializeObject<DataState[]>(jsonString); //Derialization of the json file
                foreach (var obj in list) //Loop to add the information in the json
                {
                    stateList.Add(obj);
                }
            }
            this.DataState.SaveState = false;
            stateList.Add(this.DataState); //Allows you to prepare the objects for the json filling

            this.serializeObj = JsonConvert.SerializeObject(stateList.ToArray(), Newtonsoft.Json.Formatting.Indented) + Environment.NewLine; //Serialization for writing to json file
            File.WriteAllText(stateFile, this.serializeObj);// Writing to the json file


        }

        public void LoadSave(string backupname) //Function that allows you to load backup jobs
        {
            Backup selectedBackup = null;
            this.TotalSize = 0;
            BackupNameState = backupname;

            string jsonString = File.ReadAllText(backupListFile); //Reading the json file


            if (jsonString.Length != 0) //Checking the contents of the json file is empty or not
            {
                Backup[] list = JsonConvert.DeserializeObject<Backup[]>(jsonString);  //Derialization of the json file
                foreach (var obj in list)
                {
                    if (obj.SaveName == backupname) //Check to have the correct name of the backup
                    {
                        selectedBackup = new Backup(obj.SaveName, obj.ResourceBackup, obj.TargetBackup, obj.Type, obj.MirrorBackup); //Function that allows you to retrieve information about the backup
                    }
                }
            }

            if (selectedBackup != null)
            {
                NameStateFile = selectedBackup.SaveName;

                if (selectedBackup.Type == "full") //If the type is 1, it means it's a full backup
                {
                    CompleteSave(selectedBackup.ResourceBackup, selectedBackup.TargetBackup, true, false); //Calling the function to run the full backup
                }
                else //If this is the wrong guy then, it means it's a differential backup
                {
                    DifferentialSave(selectedBackup.ResourceBackup, selectedBackup.MirrorBackup, selectedBackup.TargetBackup); //Calling the function to start the differential backup
                }

                UpdateLogFile(selectedBackup.SaveName, selectedBackup.ResourceBackup, selectedBackup.TargetBackup); //Call of the function to start the modifications of the log file
            }
        }

        public void CheckDataFile()  // Function that allows to count the number of backups in the json file of backup jobs
        {
            checkDataBackup = 0;

            if (File.Exists(backupListFile)) //Check on file exists
            {
                string jsonString = File.ReadAllText(backupListFile);//Reading the json file
                if (jsonString.Length != 0)//Checking the contents of the json file is empty or not
                {
                    Backup[] list = JsonConvert.DeserializeObject<Backup[]>(jsonString); //Derialization of the json file
                    checkDataBackup = list.Length; //Allows to count the number of backups
                }
            }
        }

        public List<Backup> NameList()//Function that lets you know the names of the backups.
        {
            List<Backup> backupList = new List<Backup>();

            if (!File.Exists(backupListFile)) //Checking if the file exists
            {
                File.WriteAllText(backupListFile, this.serializeObj);
            }

            List<Backup> names = new List<Backup>();
            string jsonString = File.ReadAllText(backupListFile); //Function to read json file
            Backup[] list = JsonConvert.DeserializeObject<Backup[]>(jsonString); // Function to dezerialize the json file

            if (jsonString.Length != 0)
            {
                foreach (var obj in list) //Loop to display the names of the backups
                {
                    names.Add(obj);
                }

            }

            return names;

        }

        // Function Delete a backup
        public void DeleteSave(string backupname)
        {
            List<Backup> backupList = new List<Backup>();
            this.serializeObj = null;

            if (!File.Exists(backupListFile)) //Checking if the file exists
            {
                File.WriteAllText(backupListFile, this.serializeObj);
            }

            string jsonString = File.ReadAllText(backupListFile); //Reading the json file

            if (jsonString.Length != 0) //Checking the contents of the json file is empty or not
            {
                Backup[] list = JsonConvert.DeserializeObject<Backup[]>(jsonString); //Derialization of the json file
                foreach (var obj in list) //Loop to add the information in the json
                {
                    if (obj.SaveName != backupname) //Check to have the correct name of the backup
                    {
                        backupList.Add(obj);
                    }
                }
            }

            this.serializeObj = JsonConvert.SerializeObject(backupList.ToArray(), Newtonsoft.Json.Formatting.Indented) + Environment.NewLine; //Serialization for writing to json file
            File.WriteAllText(backupListFile, this.serializeObj); // Writing to the json file
        }

        public static string[] GetJailApps()//Function that allows to recover software that is blacklisted.
        {
            using StreamReader reader = new StreamReader(@"..\..\..\Resources\JailApps.json");//Function to read the json file
            JailAppsFormat[] item_jailapps;
            string[] jailapps_array;
            string json = reader.ReadToEnd();
            List<JailAppsFormat> items = JsonConvert.DeserializeObject<List<JailAppsFormat>>(json);
            item_jailapps = items.ToArray();
            jailapps_array = item_jailapps[0].jailed_apps.Split(',');

            return jailapps_array;//We return the names of the softwares which are in the list of the json file.
        }


        public static bool CheckSoftware(string[] blacklist_app)//Function that allows you to compare a program that is in the list is running.
        {
            bool abortSave = false;
            foreach (string App in blacklist_app)
            {
                Process[] ps = Process.GetProcessesByName(App);
                if (ps.Length > 0)
                {
                    abortSave = true;
                }
            }
            return abortSave;
        }

        public void ModelFormat(bool extension)
        {
            Format = extension;
        }
    }
}



