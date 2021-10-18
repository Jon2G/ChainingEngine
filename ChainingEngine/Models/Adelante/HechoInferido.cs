using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChainingEngine.Views;

namespace ChainingEngine.Models.Adelante
{
    public class HechoInferido : Hecho
    {
        public Conclusion Conclusion { get; set; }
        public HechoInferido(string descripcion) : base(descripcion)
        {

        }
        public static new HechoInferido New(string descripcion) => new HechoInferido(descripcion);
        public HechoInferido SetConclusion(Conclusion conclusion)
        {
            Conclusion = conclusion;
            return this;
        }

        public override void Run(MainView window)
        {
            if (Conclusion is null)
            {
                base.Run(window);
                return;
            }
            Conclusion.Run(window);
        }
    }
}
