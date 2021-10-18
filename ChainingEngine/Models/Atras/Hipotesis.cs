using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChainingEngine.Models.Adelante;

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
    }
}
