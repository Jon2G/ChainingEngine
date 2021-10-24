using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChainingEngine.Interfaces;
using ChainingEngine.ViewModels;
using ChainingEngine.Views;
using Kit.Sql.Interfaces;

namespace ChainingEngine.Models.Adelante
{
    public  class BaseHipotesis : ObjectBase, IQuestion
    {
        public bool Answer { get; set; }
        public string Question { get; set; }

        public BaseHipotesis(string question)
        {
            Question = question;
        }
        public static BaseHipotesis New(string descripcion) => new BaseHipotesis(descripcion);
        public virtual void Ask(MainView main){}
    }
    public class Hipotesis<T> : BaseHipotesis where T : IGuid
    {
       
        public T Verdadero { get; set; }
        public T Falso { get; set; }
        public new static Hipotesis<T> New(string descripcion) => new Hipotesis<T>(descripcion);
        public Hipotesis(string question) : base(question)
        {
        }

        public Hipotesis<T> Set(T verdadero, T falso)
        {
            Verdadero = verdadero;
            Falso = falso;
            return this;
        }

        public override void Ask(MainView main)
        {
            var view = new QuestionView();
            var model = new QuestionViewModel(main, this);
            view.DataContext = model;
            main.Content = view;
        }
    }
}
