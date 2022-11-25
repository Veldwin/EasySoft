using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasySoft.controller;
using EasySoft.model;

namespace EasySoft.view
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
    }
}
