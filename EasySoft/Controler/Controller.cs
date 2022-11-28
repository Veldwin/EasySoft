using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasySoft.view;
using EasySoft.model;
using Newtonsoft.Json;



namespace EasySoft.controller
{
    class Controller
    {
        private Model model;
        private View view;
        private int InputMenu;

        public Controller()
        {
            model = new Model();
            view = new View();
            view.ShowStart();
            model.UserMenuInput = FirstMenu();
        }




        /// <summary>
        /// launch the console and react to the input of the user if registered
        /// </summary>
        /// <returns></returns>
        private string FirstMenu()
        {
            bool menu = true;
            while (menu)
            {
                model.CheckDataFile();
                try
                {
                    view.ShowMenu();
                    InputMenu = int.Parse(Console.ReadLine());
                    switch (InputMenu)
                    {
                        case 0:
                            Environment.Exit(0);
                            break;

                        case 1:
                            view.ShowNameFile();
                            string JsonString = File.ReadAllText(model.BackupListFile);
                            Backup[] list = JsonConvert.DeserializeObject<Backup[]>(JsonString);
                            foreach (var Obj in list)
                            {
                                Console.WriteLine(" -- " + Obj.SaveName);
                            }
                            view.ShowFile();
                            string input_name_backup = Console.ReadLine();
                            model.LoadSave(input_name_backup);
                            break;

                        case 2:
                            if (model.CheckDataBackup < 5)
                            {
                                Console.Clear();
                                view.ShowSubMenu();
                                SubMenu();
                            }
                            else
                            {
                                Console.Clear();
                                view.ErrorMenu("You already have 5 backups to create.\n" +
                                "Vous avez déjà 5 Sauvegardes Enregistrées");
                            }
                            break;
                    }

                }
                catch
                {
                    Console.Clear();//Console cleaning
                }
            }
            return "";
        }

        /// <summary>
        /// launch the SubMenu when 2 is selected in the menu
        /// </summary>
        private void SubMenu()
        {
            bool SubMenu = true;
            while (SubMenu)
            {
                try
                {
                    int inputMenuSub = int.Parse(Console.ReadLine());
                    switch (inputMenuSub)
                    {
                        case 0:
                            Console.Clear();
                            FirstMenu();
                            break;
                        case 1:   // full 
                            model.Type = "full";
                            view.ShowName();
                            model.SaveName = Console.ReadLine();
                            view.ShowResource();
                            model.Resource = GetResourceInput();
                            view.ShowTargetResource();
                            model.Targetresource = GetTargetResource();
                            Backup backup = new Backup(model.SaveName, model.Resource, model.Targetresource, model.Type, "");
                            model.AddSave(backup);
                            break;

                        case 2: // differential
                            model.Type = "differential";
                            view.ShowName();
                            model.SaveName = Console.ReadLine();
                            view.ShowResource();
                            model.Resource = GetResourceInput();
                            view.ShowMirrorResource();
                            model.MirrorResource = GetMirrorResource();
                            view.ShowTargetResource();
                            model.Targetresource = GetTargetResource();
                            Backup backup2 = new Backup(model.SaveName, model.Resource, model.Targetresource, model.Type, model.MirrorResource);
                            model.AddSave(backup2);
                            break;
                    }

                }
                catch
                {
                    Console.Clear();
                    FirstMenu();
                }

            }

        }

        /// <summary>
        /// get the file to backup
        /// </summary>
        /// <returns></returns>
        private string GetResourceInput()
        {
            string resourceInput = "";
            bool IsValid = false;

            while (!IsValid)
            {
                resourceInput = Console.ReadLine();
                if (Directory.Exists(resourceInput.Replace("\"", "")))
                {
                    IsValid = true;
                }
                else
                {
                    view.ErrorMenu("Incorrect Path.\n" +
                    "Chemin Incorrect");
                }

            }
            return resourceInput;
        }

        /// <summary>
        /// Obtain the resource when called
        /// </summary>
        /// <returns></returns>
        private string GetTargetResource()
        {
            string Targetresource = "";
            bool IsValid = false;

            do
            {
                Targetresource = Console.ReadLine();
                if (Directory.Exists(Targetresource.Replace("\"", "")))
                {
                    IsValid = true;
                }
                else
                {
                    view.ErrorMenu("Incorrect Path.\n" +
                    "Chemin Incorrect");
                }
            } while (!IsValid);


            return Targetresource;
        }

        private string GetMirrorResource()
        {
            string Mirrorresource = "";
            bool IsValid = false;

            do
            {
                Mirrorresource = Console.ReadLine();
                if (Directory.Exists(Mirrorresource.Replace("\"", "")))
                {
                    IsValid = true;
                }
                else
                {
                    view.ErrorMenu("Incorect Path.\n" +
                    "Chemin Incorrect");
                }
            } while (!IsValid);


            return Mirrorresource;
        }
    }
}
