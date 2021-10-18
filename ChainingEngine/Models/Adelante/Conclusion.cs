using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChainingEngine.ViewModels;
using ChainingEngine.Views;

namespace ChainingEngine.Models.Adelante
{
    public class Conclusion : ObjectBase
    {
        public string Descripcion { get; set; }
        public Conclusion(string descripcion)
        {
            Descripcion = descripcion;
        }
        public static Conclusion New(string descripcion) => new Conclusion(descripcion);

        public void Run(MainView window)
        {
            var view = new ConclusionView();
            var model = new ConclusionViewModel(window, this);
            view.DataContext = model;
            window.Content = view;
        }
    }
}
