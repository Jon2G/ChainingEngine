using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ChainingEngine.Models.Adelante;
using ChainingEngine.Models.Atras;
using ChainingEngine.Views;
using Kit.Extensions;
using Kit.Model;

namespace ChainingEngine.ViewModels
{
    public class StartupViewModel : ModelBase
    {
        public string Hipotesis { get; }
        public ICommand IniciarCommand { get; }

        public StartupViewModel(MainView window,ChainingEngine.Models.Atras.Hipotesis hipotesis)
        {
            this.Hipotesis = hipotesis.Question;
            this.IniciarCommand = new Command(() => Iniciar(window, hipotesis));

        }
        public StartupViewModel(MainView window,Hecho hecho)
        {
            this.Hipotesis = hecho.Descripcion;
            this.IniciarCommand = new Command(() =>Iniciar(window, hecho));
        }

        private void Iniciar(MainView window, ChainingEngine.Models.Atras.Hipotesis hipotesis)
        {
            hipotesis.Run(window);

        }
        private void Iniciar(MainView window, Hecho hecho)
        {
            hecho.Run(window);
        }
    }
}
