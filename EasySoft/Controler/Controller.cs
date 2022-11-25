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
