using System;
using System.Collections.Generic;
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
       
        public string UserMenuInput { get; set; }
        public int CheckDataBackup;
        public string BackupListFile = System.Environment.CurrentDirectory + @"\Works\";
        public Model()
        {
            UserMenuInput = " ";
        }


        /// <summary>
        /// count the number of backup in json file
        /// </summary>
        public void check_data_file()
        {
            CheckDataBackup = 0;
             
            if (File.Exists(BackupListFile))
            {
                string jsonstring = File.ReadAllText(BackupListFile);
                if (jsonstring.Length != 0)
                {
                    Backup[]? list = JsonConvert.DeserializeObject<Backup[]>(jsonstring);
                    CheckDataBackup = list.Length;
                }
            }
        }
    }
}
