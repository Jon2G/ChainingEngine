using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChainingEngine.Models.Adelante;
using ChainingEngine.Models.Atras;
using ChainingEngine.ViewModels;
using ChainingEngine.Views;
using Kit;

namespace ChainingEngine.Models
{
    public static class Engine
    {
        public static void Run(Hipotesis hipotesis)
        {
            MainView mainView = new MainView();
            mainView.Content = new StartupView(new StartupViewModel(mainView, hipotesis));
            mainView.ShowDialog();
        }
        public static void Run(Hecho hecho)
        {
            MainView mainView = new MainView();
            mainView.Content = new StartupView(new StartupViewModel(mainView, hecho));
            mainView.ShowDialog();
        }
    }
}
