using System;
using System.Collections.Generic;
using System.Text;
using ChainingEngine.Interfaces;

namespace ChainingEngine.Models.Adelante
{
    public class HaciaAdelante : IEncadenamiento
    {
        private readonly Hecho Hecho;
        public string Title => Hecho.Descripcion;

        public string Type => "Hacia adelante";

        public HaciaAdelante(Hecho Hecho)
        {
            this.Hecho = Hecho;
        }

    }
}
