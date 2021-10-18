using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using ChainingEngine.Models.Atras;
using Kit.Extensions;

namespace ChainingEngine.ViewModels
{
    public class HipotesisViewModel
    {
        public Evidencia Evidencia { get; set; }
        public Comportamiento Comportamiento { get; set; }
        public Command<Comportamiento> ActionCommand { get; set; }
        public ManualResetEvent ResetEvent { get; }
        public HipotesisViewModel(Evidencia evidencia)
        {
            this.ResetEvent = new ManualResetEvent(false);
            this.Evidencia = evidencia;
            this.ActionCommand = new Command<Comportamiento>(Accion);
        }

        private void Accion(Comportamiento comportamiento)
        {
            comportamiento.Answer = true;
            Comportamiento = comportamiento;
            ResetEvent.Set();
        }
    }
}
