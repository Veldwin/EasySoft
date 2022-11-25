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
            
        }
    }
}
