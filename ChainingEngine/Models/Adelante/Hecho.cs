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
        public Guid AdelanteGuid { get; set; }
        public string Descripcion { get; set; }
        [Ignore]
        public Hipotesis Hipotesis { get; set; }

        public Hecho(string descripcion)
        {
            this.Descripcion = descripcion;
        }

        public Hecho()
        {

        }
        public static Hecho New(string descripcion) => new Hecho(descripcion);

        public Hecho SetHipotesis(Hipotesis hipotesis)
        {
            Hipotesis = hipotesis;
            return this;
        }

        public virtual void Run(MainView window) => Hipotesis.Ask(window);

        public void Delete()
        {
            App.SqLite.Delete(this);
            Hipotesis.Delete();
        }
        public void Save(Guid AdelanteGuid)
        {
            this.AdelanteGuid = AdelanteGuid;
            App.SqLite.InsertOrReplace(this);
            Hipotesis.Save(this.Guid, false);
        }

        public void Load()
        {
            Hipotesis hipotesis = App.SqLite.Table<Hipotesis>().
                First(x => x.ParentId == this.Guid);
            hipotesis.Load();
            this.Hipotesis = hipotesis;
        }
    }
}
