using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChainingEngine.Interfaces;
using ChainingEngine.Models.Adelante;
using ChainingEngine.Views;

namespace ChainingEngine.Models.Atras
{
    public class Comportamiento : ObjectBase, IQuestion
    {
        public string Question { get; set; }
        public Conclusion Conclusion { get; set; }

        public Comportamiento(string question, Conclusion conclusion)
        {
            this.Question = question;
            this.Conclusion = conclusion;
        }

        public bool Answer { get; set; }
        public void Ask(MainView main)
        {
        }
    }
}
