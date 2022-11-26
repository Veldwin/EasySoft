﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasySoft.controller;
using EasySoft.view;
using Newtonsoft.Json;


namespace EasySoft.model
{
    class Model
    {

        public int CheckDataBackup;
        private string SerializeObj;
        public string BackupListFile = System.Environment.CurrentDirectory + @"\Works\";
        public string StateFile = System.Environment.CurrentDirectory + @"\State\";
        public DataState DataState { get; set; }
        public string NameStateFile { get; set; }
        public string BackupNameState { get; set; }
        public string Resource { get; set; }
        public int NbFileMmax { get; set; }
        public int Nbfiles { get; set; }
        public long Size { get; set; }
        public float Progs { get; set; }
        public string Targetresource { get; set; }
        public string SaveName { get; set; }
        public int Type { get; set; }
        public string SourceFile { get; set; }
        public string TypeString { get; set; }
        public long TotalSize { get; set; }
        public TimeSpan TimeTransfert { get; set; }
        public string UserMenuInput { get; set; }
        public string Mirrorresource { get; set; }
        
        
        public Model()
        {
            UserMenuInput = " ";

            if (!Directory.Exists(BackupListFile))
            {
                DirectoryInfo resource = Directory.CreateDirectory(BackupListFile);
            }
            BackupListFile += @"backupList.json";

            if (!Directory.Exists(StateFile))
            {
                DirectoryInfo resource = Directory.CreateDirectory(StateFile);
            }
            StateFile += @"state.json";
        }

        /// <summary>
        /// function called when full backup is selected
        /// </summary>
        /// <param name="inputpathsave"></param>
        /// <param name="inputDestToSave"></param>
        /// <param name="copyDir"></param>
        /// <param name="verif"></param>
        /// <exception cref="DirectoryNotFoundException"></exception>


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


            // reset value 
            DataState.TotalSizeState = TotalSize;
            DataState.SourceFileState = null;
            DataState.TargetFileState = null;
            DataState.TotalFileState = 0;
            DataState.TotalSizeState = 0;
            DataState.TotalSizeRestState = 0;
            DataState.FileRestState = 0;
            DataState.ProgressState = 0;
            DataState.SaveState = false;

             UpdateStateFile();

            stopwatch.Stop();
            TimeTransfert = stopwatch.Elapsed; // Note the time passed
        }

            // DifferentialSave() TODO

            // UpdateLogFile() TODO



            public void AddSave(Backup backup)
        {
            List<Backup> backupList = new List<Backup>();
            SerializeObj = null;

            if (!File.Exists(BackupListFile))
            {
                File.WriteAllText(BackupListFile, SerializeObj);
            }

            string jsonString = File.ReadAllText(BackupListFile);

            if (jsonString.Length != 0)
            {
                Backup[] list = JsonConvert.DeserializeObject<Backup[]>(jsonString);
                foreach (var obj in list)
                {
                    backupList.Add(obj);
                }
            }
            backupList.Add(backup);

            SerializeObj = JsonConvert.SerializeObject(backupList.ToArray(), Formatting.Indented) + Environment.NewLine;
            File.WriteAllText(BackupListFile, SerializeObj);

            DataState = new DataState(SaveName)
            {
                BackupDateState = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")
            };
            //// AddState();
        }

        // AddState() TODO

        /// <summary>
        /// load backup registered
        /// </summary>
        /// <param name="backupname"></param>
        public void LoadSave(string BackupName)
        {
            Backup backup = null;
            TotalSize = 0;
            BackupNameState = BackupName;

            string jsonString = File.ReadAllText(BackupListFile);


            if (jsonString.Length != 0)
            {
                Backup[] list = JsonConvert.DeserializeObject<Backup[]>(jsonString);
                foreach (var obj in list)
                {
                    if (obj.SaveName == BackupName)
                    {
                        backup = new Backup(obj.SaveName, obj.ResourceBackup, obj.TargetBackup, obj.Type, obj.MirrorBackup);
                    }
                }
            }

            if (backup.Type == 1)
            {
                NameStateFile = backup.SaveName;
                CompleteSave(backup.ResourceBackup, backup.TargetBackup, true, false);
                //// UpdateLogFile(backup.SaveName, backup.ResourceBackup, backup.TargetBackup);
                Console.WriteLine("Saved Successfull !");
            }
            else
            {
                NameStateFile = backup.SaveName;
                //// DifferentialSave(backup.ResourceBackup, backup.MirrorBackup, backup.TargetBackup);
                //// UpdateLogFile(backup.SaveName, backup.ResourceBackup, backup.TargetBackup);
                Console.WriteLine("Saved Successfull !");
            }

        }

        /// <summary>
        /// update the status file
        /// </summary>
        private void UpdateStateFile()
        {
            List<DataState> stateList = new List<DataState>();
            SerializeObj = null;
            if (!File.Exists(StateFile))
            {
                File.Create(StateFile).Close();
            }

            string jsonString = File.ReadAllText(StateFile);

            if (jsonString.Length != 0)
            {
                DataState[]? list = JsonConvert.DeserializeObject<DataState[]>(jsonString);

                foreach (var obj in list)
                {
                    if (obj.SaveNameState == NameStateFile)
                    {
                        obj.SourceFileState = DataState.SourceFileState;
                        obj.TargetFileState = DataState.TargetFileState;
                        obj.TotalFileState = DataState.TotalFileState;
                        obj.TotalSizeState = DataState.TotalSizeState;
                        obj.FileRestState = DataState.FileRestState;
                        obj.TotalSizeRestState = DataState.TotalSizeRestState;
                        obj.ProgressState = DataState.ProgressState;
                        obj.BackupDateState = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                        obj.SaveState = DataState.SaveState;
                    }

                    stateList.Add(obj);

                }

                SerializeObj = JsonConvert.SerializeObject(stateList.ToArray(), Formatting.Indented) + Environment.NewLine;

                File.WriteAllText(StateFile, SerializeObj);
            }
        }

        // CheckDataFile() TODO
    }
}
