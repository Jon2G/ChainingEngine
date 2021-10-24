using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChainingEngine.Interfaces;
using ChainingEngine.ViewModels;
using ChainingEngine.Views;
using Kit.Sql.Attributes;
using Kit.Sql.Interfaces;

namespace ChainingEngine.Models.Adelante
{
    [Table("HipotesisHaciaAdelante")]
    public class Hipotesis : IQuestion, IBranch, IGuid
    {
        [PrimaryKey]
        public Guid Guid { get; set; }
        [Ignore]
        public bool Answer { get; set; }
        public string Question { get; set; }
        [Ignore]
        public IBranch Verdadero { get; set; }
        [Ignore]
        public IBranch Falso { get; set; }
        public bool Side { get; set; }
        public Guid ParentId { get; set; }

        public new static Hipotesis New(string descripcion) => new Hipotesis(descripcion);
        public Hipotesis(string question)
        {
            Question = question;
        }

        public Hipotesis()
        {
            
        }

        public Hipotesis Set(IBranch verdadero, IBranch falso)
        {
            Verdadero = verdadero;
            Falso = falso;
            return this;
        }

        public void Run(MainView main) => Ask(main);
        public void Ask(MainView main)
        {
            var view = new QuestionView();
            var model = new QuestionViewModel(main, this);
            view.DataContext = model;
            main.Content = view;
        }

        public void Save() => Save(Guid.Empty, false);

        public void Save(Guid pGuid, bool answer)
        {
            ParentId = pGuid;
            Side = answer;
            App.SqLite.InsertOrReplace(this);
            this.Verdadero.Save(Guid, true);
            this.Falso.Save(Guid, false);
        }

        public void Load()
        {

            this.Verdadero=(IBranch)
                App.SqLite.Table<Hipotesis>().FirstOrDefault(x => x.ParentId == this.Guid && x.Side)
                ?? App.SqLite.Table<Conclusion>().First(x => x.ParentId == this.Guid && x.Side);

            this.Falso = (IBranch)
                             App.SqLite.Table<Hipotesis>().FirstOrDefault(x => x.ParentId == this.Guid && !x.Side)
                             ?? App.SqLite.Table<Conclusion>().First(x => x.ParentId == this.Guid && !x.Side);
            this.Verdadero.Load();
            this.Falso.Load();

        }
    }
}
