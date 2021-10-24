using System;
using ChainingEngine.Interfaces;
using ChainingEngine.Views;
using Kit.Sql.Attributes;
using Kit.Sql.Interfaces;

namespace ChainingEngine.Models.Adelante
{
    public class Hecho : IGuid
    {
        [PrimaryKey]
        public Guid Guid { get; set; }
        public string Descripcion { get; set; }
        public BaseHipotesis Hipotesis { get; set; }

        public Hecho(string descripcion)
        {
            this.Descripcion = descripcion;
        }

        public Hecho()
        {
            
        }
        public static Hecho New(string descripcion) => new Hecho(descripcion);

        public Hecho SetHipotesis(BaseHipotesis hipotesis)
        {
            Hipotesis = hipotesis;
            return this;
        }

        public virtual void Run(MainView window) => Hipotesis.Ask(window);

    }
}
