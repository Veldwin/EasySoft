using EasySoft.controler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasySoft.model;

namespace EasySoft.view
{
    class View
    {
        /// <summary>
        /// Show this panel when the console open
        /// </summary>
        public void ShowStart()
        {
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine("|||            Welcome to EasySave               |||");
            Console.WriteLine("----------------------------------------------------");
        }
        public void ShowMenu()
        {
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine("|||                   Menu                       |||");
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine("|||  0. Exit                                     |||");
            Console.WriteLine("|||  1. Open a backup job                        |||");
            Console.WriteLine("|||  2. Create a backup job                      |||");
            Console.WriteLine("----------------------------------------------------");
            Console.Write("Please enter the menu number : ");
        }
    }
}

