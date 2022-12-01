﻿
namespace EasySoft.model
{
    public class Backup
    {

        public string ResourceBackup { get; set; }
        public string TargetBackup { get; set; }
        public string SaveName { get; set; }
        public string Type { get; set; }
        public string MirrorBackup { get; set; }


        public Backup(string saveName, string source, string target, string type, string mirror)
        {
            SaveName = saveName;
            ResourceBackup = source;
            TargetBackup = target;
            Type = type;
            MirrorBackup = mirror;
        }
    }
}
