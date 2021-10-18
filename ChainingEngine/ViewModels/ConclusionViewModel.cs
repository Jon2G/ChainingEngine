using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ChainingEngine.Models.Adelante;
using ChainingEngine.Views;
using Kit.Extensions;

namespace ChainingEngine.ViewModels
{
   public class ConclusionViewModel
    {
        public string FinalConclusion { get; set; }
        public ICommand EndCommand { get; }

        public ConclusionViewModel(MainView window,Conclusion conclusion)
        {
            this.FinalConclusion = conclusion.Descripcion;
            this.EndCommand = new Command(()=>End(window));
        }

        private void End(MainView window)
        {
            window.Restart();
        }
    }
}
