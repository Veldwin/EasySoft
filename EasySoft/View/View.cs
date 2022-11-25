using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySoft.View
{
    class View
    {

        /// <summary>
        /// Show this panel when the console open
        /// </summary>
        public void show_start()
        {
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("|||                   EasySave v1.0                    |||");
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("     _____    _    ______   ______    ___     _______ ");
            Console.WriteLine("    | ____|  / \\  / ___\\ \\ / / ___|  / \\ \\   / / ____|");
            Console.WriteLine("    |  _|   / _ \\ \\___ \\\\ V /\\___ \\ / _ \\ \\ / /|  _|  ");
            Console.WriteLine("    | |___ / ___ \\ ___) || |  ___) / ___ \\ V / | |___ ");
            Console.WriteLine("    |_____/_/   \\_\\____/ |_| |____/_/   \\_\\_/  |_____|");
            Console.WriteLine("                                                      ");
            Console.WriteLine("----------------------------------------------------------");
        }

        /// <summary>
        /// open this in the same time as show_start
        /// </summary>
        /// 
        public void show_menu()
        {
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("|||                          Menu                      |||");
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("|||  0. Exit / Quitter                                 |||");
            Console.WriteLine("|||  1. Open a backup job / Ouvrir une sauvegarde      |||");
            Console.WriteLine("|||  2. Create a backup job / Créer une sauvegarde     |||");
            Console.WriteLine("----------------------------------------------------------");
            Console.Write("Please enter the menu number you want to use : \n" +
            "Veuillez entrer le numéro du menu que vous souhaitez utiliser : ");
        }
    }
}
