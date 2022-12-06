using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EasySaveApp.model
{
    public class Backup
    {
        // Declaration of properties that are used for saving backup information for the backup job file
        public string ResourceBackup { get; set; }
        public string TargetBackup { get; set; }
        public string SaveName { get; set; }
        public string Type { get; set; }
        public string MirrorBackup { get; set; }
        public bool IsSelected { get; set; }
        public float Progress { get; set; }

        public Backup(string saveName, string source, string target, string type, string mirror, float progress = 0)
        {
            SaveName = saveName;
            ResourceBackup = source;
            TargetBackup = target;
            Type = type;
            MirrorBackup = mirror;
            Progress = progress;
        }
    }

    public class BackupWithProgress : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string _SaveName;
        private float _Progress;

        public string SaveName { get { return _SaveName; } set { _SaveName = value; OnPropertyChanged(); } }
        public float Progress { get { return _Progress; } set { _Progress = value; OnPropertyChanged(); } }

        public BackupWithProgress(string saveName, float progress)
        {
            _SaveName = saveName;
            _Progress = progress;
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

}