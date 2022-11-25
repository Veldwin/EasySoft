﻿using System;
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

        /// <summary>
        /// open this when 2 is enter
        /// </summary> 
        public void show_sub_menu()
        {
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("|||           Backup Type / Type de Sauvergarde        |||");
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("|||  0. Exit / Quitter                                 |||");
            Console.WriteLine("|||  1. Complete Save / Sauvegarde complète            |||");
            Console.WriteLine("|||  2. Differential Save / Sauvegarde Différentielle  |||");
            Console.WriteLine("----------------------------------------------------------");
            Console.Write("Please enter the menu number.\n" +
            "Entrez le numéro du menu : ");
        }

        /// <summary>
        /// name your backup
        /// </summary>
        public void show_name()
        {
            Console.WriteLine("Please enter the name of your backup.\n" +
                "Entrez le nom de votre sauvegarde : ");
        }
        
        /// <summary>
        /// Enter the path of the ressource you wish to backup
        /// </summary>
        public void show_ressource()
        {
            Console.WriteLine("Please enter the path of the ressource you want to back up.\n" +
            "you can drag and drop your ressource : \n" +
            "Entrez le chemin du dossier que vous souhaitez sauvegarder.\n" +
            "Vous pouvez glisser-déposer votre dossier");
        }

        /// <summary>
        ///  select the destination of your backup
        /// </summary>
        public void show_target_ressource()
        {
            Console.WriteLine("Please enter the destination path for the backup. \n " +
            "you can drag and drop your ressource: \n" +
            "Entrez le chemin de destination de la sauvegarde. \n" +
            "Vous pouvez glisser-déposer votre ressource :");
        }

    }
}
