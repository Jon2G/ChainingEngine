using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Documents;
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
    }
}
