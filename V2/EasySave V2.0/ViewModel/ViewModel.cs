using System.Diagnostics;
using EasySaveApp.model;
using System.Collections.Generic;
using System.Windows;

namespace EasySaveApp.viewmodel
{
    public class ViewModel
    {

        public bool JailAppsStop { get; set; }
        private Model model;
        string[] jail_apps = Model.GetJailApps();
        public string[] BlackListApp { get => jail_apps; set => jail_apps = value; }

        public ViewModel()
        {
            model = new Model();
        }


        public void AddSaveModel(string type, string saveName, string sourceDir, string targetDir, string mirrorDir)//Function that allows you to add a backup
        {
            model.SaveName = saveName;
            Backup backup = new Backup(model.SaveName, sourceDir, targetDir, type, mirrorDir);
            model.AddSave(backup); // Calling the function to add a backup job
        }


        public List<string> ListBackup()//Function that lets you know the lists of the names of the backups.
        {

            List<string> nameslist = new List<string>();
            foreach (var obj in model.NameList())
            {
                nameslist.Add(obj.SaveName);
            }
            return nameslist;
        }

        public void LoadBackup(string backupname)//Function that allows you to load the backups that were selected by the user.
        {
            {
                JailAppsStop = true;

                if (!Model.CheckSoftware(BlackListApp))//If a program is in the blacklist we do not start the backup.
                {
                    JailAppsStop = false;
                  
                }
                else
                {
                    model.LoadSave(backupname);//Function that launches backups
                }
            }

        }

        public void DeleteBackup(string backupname)//Function that allows you to delete the backups that were selected by the user.
        {
            model.DeleteSave(backupname);
        }

        public void DontSave() //Function that prevent EasySave from saving while a third party app is running
        {
            List<string> BL = new List<string>();

            foreach (string bl in jail_apps)
            {
                BL.Add(bl);

                Process[] i = Process.GetProcessesByName(bl);

                if (i.Length > 0 == true)
                {
                    foreach (Process proc in i)
                    {
                        proc.WaitForExit();

                        if (proc.HasExited)
                        {
                            proc.CloseMainWindow();

                            proc.Close();
                        }
                    }
                }
            }
        }

        public void TransitFormat(bool format)
        {
            model.TransitFormat(format);
        }
    }
}
