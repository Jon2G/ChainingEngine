using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ChainingEngine.Interfaces;
using Kit.Sql.Attributes;
using Kit.Sql.Interfaces;
using Microsoft.VisualBasic;

namespace ChainingEngine.Models.Adelante
{
    public class HaciaAdelante : IEncadenamiento, IGuid
    {
        [PrimaryKey]
        public Guid Guid { get; set; }
        [Ignore]
        public Hecho Hecho { get; private set; }

        public string Title
        {
            get
            {
                if (Hecho is null)
                    Hecho = new Hecho();
                return Hecho.Descripcion;
            }
            set
            {
                if (Hecho is null)
                    Hecho = new Hecho();
                Hecho.Descripcion = value;
            }
        }
        [Ignore]
        public string Type => "Hacia adelante";

        public HaciaAdelante(Hecho Hecho)
        {
            this.Hecho = Hecho;
        }

        public HaciaAdelante()
        {

        }

        public void Delete()
        {
            App.SqLite.Delete(this);
            Hecho.Delete();
        }
        public void Save()
        {
            App.SqLite.InsertOrReplace(this);
            Hecho.Save(this.Guid);
        }

        internal static IEnumerable<HaciaAdelante> GetAll()
        {
            HaciaAdelante[] HaciaAdelante = App.SqLite.Table<HaciaAdelante>().ToArray();
            for (int i = 0; i < HaciaAdelante.Length; i++)
            {
                HaciaAdelante[i].Load();
            }
            return HaciaAdelante;
        }

        private void Load()
        {
            Hecho hecho = App.SqLite.Table<Hecho>().First(x => x.AdelanteGuid == this.Guid);
            hecho.Load();
            this.Hecho = hecho;
        }

        public void GetConclusiones(Collection<StringObject> list, IBranch branch)
        {
            switch (branch)
            {
                case Conclusion c:
                    list.Add((StringObject)c.Descripcion);
                    break;
                case Hipotesis h:
                    GetConclusiones(list, h.Verdadero);
                    GetConclusiones(list, h.Falso);
                    break;
            }
        }


        public void GetHechos(ObservableCollection<HechoModel> list, IBranch branch)
        {
            switch (branch)
            {
                case Hipotesis h:
                    list.Add(new HechoModel()
                    {
                        Descripcion = (StringObject)h.Question,
                        Si = (StringObject)h.Verdadero.Descripcion,
                        No = (StringObject)h.Falso.Descripcion
                    });
                    GetHechos(list,h.Verdadero);
                    GetHechos(list,h.Falso);
                    break;
            }
        }
    }
}
