using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySoft.Model
{
    class Backup
    {

        public string RessourceBackup { get; set; }
        public string TargetBackup { get; set; }
        public string SaveName { get; set; }
        public int Type { get; set; }
        public string MirrorBackup { get; set; }

        public Backup(string saveName, string source, string target, int type, string mirror)
        {
            SaveName = saveName;
            RessourceBackup = source;
            TargetBackup = target;
            Type = type;
            MirrorBackup = mirror;
        }
    }
}
