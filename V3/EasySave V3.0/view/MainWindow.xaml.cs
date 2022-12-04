using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Windows;
using EasySaveApp.viewmodel;

namespace EasySaveApp.view
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModel viewmodel;

        private string language = "fr";
        private bool formatmw = false;


        public MainWindow()
        {
            viewmodel = new ViewModel();
            InitializeComponent();
            ShowListBox();
        }



        private void ButtonClickFr(object sender, RoutedEventArgs e)//Function to translate the software into French
        {
            result.Text = "";
            language = "fr";
            ChooseLanguage(language);
        }

        private void ButtonClickEn(object sender, RoutedEventArgs e)//Function to translate the software into English
        {
            result.Text = "";
            language = "en";
            ChooseLanguage(language);
        }

        private void ChooseLanguage(string language)//Function that allows you to choose the language of the software
        {
            ResourceDictionary dict = new ResourceDictionary();
            if (language == "fr")
            {
                dict.Source = new Uri("Resources\\fr-FR.xaml", UriKind.Relative);
            }
            else
            {
                dict.Source = new Uri("Resources\\en-GB.xaml", UriKind.Relative);
            }
            this.Resources.MergedDictionaries.Add(dict);
        }
        private void ButtonAddSaveClick(object sender, RoutedEventArgs e)//Function that allows the button to add a backup
        {
            string saveName = "";
            string sourceDir = "";
            string targetDir = "";
            string mirrorDir = "";

            saveName = name_save.Text;
            sourceDir = SoureDir.Text;
            targetDir = TargetDir.Text;
            mirrorDir = MirrorDir.Text;

            if (mirror_button.IsChecked.Value) //If the button of the full backup is selected
            {
                if (name_save.Text.Length.Equals(0) || SoureDir.Text.Length.Equals(0) || TargetDir.Text.Length.Equals(0))
                {
                    result.Text = (string)FindResource("msg_emptyfield");
                    /*if (language == "fr")
                    {
                        result.Text = " Veuillez remplir tous les champs ! ";
                    }
                    else
                    {
                        result.Text = " Please complete all fields ! ";
                    }*/
                }
                else
                {
                    string type = "full";

                    viewmodel.AddSaveModel(type, saveName, sourceDir, targetDir, ""); //Function to add the backup

                    result.Text = (string)FindResource("msg_saveaddedfull");

                    ShowListBox();//Function to update the list.
                }

            }
            else if (diff_button.IsChecked.Value)//If the button of the full backup is selected
            {
                if (name_save.Text.Length.Equals(0) || SoureDir.Text.Length.Equals(0) || TargetDir.Text.Length.Equals(0) || MirrorDir.Text.Length.Equals(0))
                {
                    result.Text = (string)FindResource("msg_emptyfield");
                }
                else
                {
                    string type = "diff";
                    viewmodel.AddSaveModel(type, saveName, sourceDir, targetDir, mirrorDir);//Function to add the backup

                    result.Text = (string)FindResource("msg_saveaddeddiff");

                    ShowListBox();//Function to update the list.
                }

            }

        }
        private void XmlLog(object sender, RoutedEventArgs e)
        {
            if (xml_button.IsChecked.Value)
            {
                formatmw = true;
                viewmodel.ModelFormat(formatmw);
            }
        }

        private void JsonLog(object sender, RoutedEventArgs e)
        {
            if ((Json_button.IsChecked.Value))
            {
                formatmw = false;
                viewmodel.ModelFormat(formatmw);
            }
        }


        

        private void SourceResourceClick(object sender, RoutedEventArgs e)//Function to retrieve the path to the source folder
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog(); //Declaration of the method to open the window to choose the folder path.
            dialog.IsFolderPicker = true;

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                SoureDir.Text = dialog.FileName; //Displays the path in the window text.
            }

        }

        private void TargetResourceClick(object sender, RoutedEventArgs e)//Function to retrieve the path to the destination folder
        {

            CommonOpenFileDialog dialog = new CommonOpenFileDialog(); //Declaration of the method to open the window to choose the folder path.
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                TargetDir.Text = dialog.FileName; //Displays the path in the window text.
            }

        }

        private void MirrorResourceClick(object sender, RoutedEventArgs e)//Function to retrieve the folder path of the mirror backup.
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog(); //Declaration of the method to open the window to choose the folder path.
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                MirrorDir.Text = dialog.FileName; //Displays the path in the window text.
            }

        }

        private void ShowListBox() //Function that displays the names of the backups in the list.
        {

            Save_work.Items.Clear();

            List<string> names = viewmodel.ListBackup();
            foreach (string name in names)//Loop that allows you to manage the names in the list.
            {
                Save_work.Items.Add(name); //Function that allows you to insert the names of the backups in the list.
            }
        }

        private void MirrorButtonChecked(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonExit(object sender, RoutedEventArgs e)//Function of the button to close the software
        {
            Application.Current.Shutdown();//Function that turns off the software
        }

        private void GridMenuMouseDown(object sender, RoutedEventArgs e)//Function that allows you to move the software window.
        {
            DragMove();//Function that allows movement
        }

        private void ButtonStartSaveClick(object sender, RoutedEventArgs e)//Function that launches the backup
        {
            if (Save_work.SelectedItem != null) //Condition that allows to check if the user has selected a backup.
            {
                foreach (string item in Save_work.SelectedItems)//Loop that allows you to select multiple saves
                {
                    string saveName = item.ToString();

                    bool backupSucceeded = viewmodel.LoadBackup(saveName);

                    if (!backupSucceeded) //Check for message display if blacklisted software was detected.
                    {
                        result.Text = (string)FindResource("msg_failsave");
                    }
                    else
                    {
                        result.Text = (string)FindResource("msg_successave");
                    }
                }
            }
        }

        private void OpenBlacklist(object sender, RoutedEventArgs e)//Function that allows the button to open the file of blacklisted software
        {
            System.Diagnostics.Process.Start("notepad.exe", @"..\..\..\Resources\JailApps.json");
        }

        private void OpenCryptExt(object sender, RoutedEventArgs e)//Function that allows the button to open the file of blacklisted software
        {
            System.Diagnostics.Process.Start("notepad.exe", @"..\..\..\Resources\CryptExtension.json");
        }


        private void ButtonSelectAll(object sender, RoutedEventArgs e)
        {
            Save_work.SelectAll();
        }

        private void ButtonUnselectAll(object sender, RoutedEventArgs e)
        {
            Save_work.UnselectAll();
        }

        private void ButtonDeleteSave(object sender, RoutedEventArgs e)//Function that allows the deletion of a backup
        {
            string saveName = "";

            if (Save_work.SelectedItem != null) //Condition that allows to check if the user has selected a backup.
            {
                foreach (string item in Save_work.SelectedItems)//Loop that allows you to select multiple saves
                {
                    saveName = item.ToString();
                    viewmodel.DeleteBackup(saveName);
                }

                ShowListBox();//Function to update the list.
            }
        }
    }
}
