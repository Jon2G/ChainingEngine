using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Documents;
using ChainingEngine.ViewModels;
using ChainingEngine.Views;
using ChainingEngine.Views.Atras;
using Kit;

namespace ChainingEngine.Models.Atras
{
    public class Evidencia : List<Comportamiento>
    {
        public string Descripcion { get; set; }
        public Evidencia(string descripcion, params Comportamiento[] comportamientos)
        {
            this.Descripcion = descripcion;
            this.AddRange(comportamientos.Shuffle(new Random()));
        }

        public async Task<Comportamiento> Run(MainView window)
        {
            var view = new HipotesisView();
            var model = new HipotesisViewModel(this);
            view.DataContext = model;
            window.Content = view;
            await Task.Run(() => model.ResetEvent.WaitOne());
            return model.Comportamiento; 
        }
    }
}
