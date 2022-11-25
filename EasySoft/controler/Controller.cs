using EasySoft.view;
using EasySoft.model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
                                view.ErrorMenu("You already have 5 backups to create.");
                            }
                            break;
                    }

                }
                catch
                {
                    Console.Clear();//Console cleaning
                }
                return "";
            }
        }
    }
}
