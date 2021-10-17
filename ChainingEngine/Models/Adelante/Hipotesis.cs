using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChainingEngine.Interfaces;

namespace ChainingEngine.Models.Adelante
{
    public abstract class BaseHipotesis : ObjectBase, IQuestion
    {
        public bool Answer { get; set; }
        public string Question { get; set; }
        public abstract void Ask();
    }
    public class Hipotesis<T> : BaseHipotesis where T : ObjectBase
    {
        public bool Answer { get; set; }
        public string Question { get; set; }
        public T Verdadero { get; set; }
        public T Falso { get; set; }
        public static Hipotesis<T> New(string descripcion) => new Hipotesis<T>(descripcion);
        public Hipotesis(string question)
        {
            Question = question;
        }

        public Hipotesis<T> Set(T verdadero, T falso)
        {
            Verdadero = verdadero;
            Falso = falso;
            return this;
        }

        public override void Ask()
        {
            throw new NotImplementedException();
        }
    }
}
