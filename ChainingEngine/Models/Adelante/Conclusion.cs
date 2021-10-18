using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
          
        }
    }
}
