using EasySoft.model;
using EasySoft.view;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySoft.controler
{
   class Controler
    {

        private Model model;
        private View view;
        private int inputMenu;

        public Controler()
        {
            model = new Model();
            view = new View();
            view.ShowStart(); //Function call

        }
    }
}
