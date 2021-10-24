using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChainingEngine.Interfaces;
using ChainingEngine.Models.Adelante;
using ChainingEngine.Models.Atras;
using ChainingEngine.ViewModels;
using ChainingEngine.Views;
using Kit;

namespace ChainingEngine.Models
{
    public static class Engine
    {
        public static void Run(MainView mainView, IEncadenamiento encadenamiento)
        {
            switch (encadenamiento)
            {
                case HaciaAtras atras:
                    Run(mainView,atras.Hipotesis);
                    return;
                case HaciaAdelante adelante:
                    //Run(adelante);
                    return;
            }
        }
        public static void Run(MainView mainView, Hipotesis hipotesis)
        {
            mainView.Content = new StartupView(new StartupViewModel(mainView, hipotesis));
        }
        public static void Run(MainView mainView, Hecho hecho)
        {
            mainView.Content = new StartupView(new StartupViewModel(mainView, hecho));
        }
    }
}
