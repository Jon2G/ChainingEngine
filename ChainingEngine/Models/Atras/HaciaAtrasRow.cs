using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Kit.Model;

namespace ChainingEngine.Models.Atras
{
    public class HaciaAtrasRow : ModelBase
    {
        public string Evidencia { get; set; }
        public ObservableCollection<StringObject> Hechos { get; set; }
        public HaciaAtrasRow()
        {
            Hechos = new ObservableCollection<StringObject>();
        }

    }
}
