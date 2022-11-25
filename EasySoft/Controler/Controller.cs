using EasySoft.view;
using EasySoft.model;
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

        public Controller()
        {
            model = new Model();
            view = new View();
            view.show_start();
            model.UserMenuInput = first_menu();
        }


        /// <summary>
        /// launch the console and react to the input of the user if registered
        /// </summary>
        /// <returns></returns>
        private string first_menu()
        {
            bool menu = true;
            while (menu)
            {
                model.check_data_file();
                try
                {
                    view.show_menu();
                    InputMenu = int.Parse(Console.ReadLine());
                    switch (InputMenu)
                    {
                        case 0:
                            Environment.Exit(0);
                            break;

                        case 1:
                            view.show_name_file();
                            string JsonString = File.ReadAllText(model.BackupListFile);
                            Backup[] list = JsonConvert.DeserializeObject<Backup[]>(JsonString);
                            foreach (var Obj in list)
                            {
                                Console.WriteLine(" -- " + Obj.SaveName);
                            }
                            view.show_file();
                            string input_name_backup = Console.ReadLine();
                            model.load_save(input_name_backup);
                            break;

                        case 2:
                            if (model.CheckDataBackup < 5)
                            {
                                Console.Clear();
                                view.show_sub_menu();
                                sub_menu();
                            }
                            else
                            {
                                Console.Clear();
                                view.error_menu("You already have 5 backups to create.");
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
    }
}
