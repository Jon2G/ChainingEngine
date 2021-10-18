using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChainingEngine.Models.Adelante;
using ChainingEngine.Views;

namespace ChainingEngine.Models.Atras
{
    public class Hipotesis : List<Evidencia>
    {
        public string Question { get; set; }
        public Hipotesis(string question, params Evidencia[] evidencias)
        {
            Question = question;
            this.AddRange(evidencias);
        }

        public static Hipotesis New(string question, params Evidencia[] evidencias) => new Hipotesis(question, evidencias);

        public void Run(MainView window)
        {
            //? y la ventana la de la primera hipotesis seria la starup no?
            throw new NotImplementedException();
        }
    }
}
