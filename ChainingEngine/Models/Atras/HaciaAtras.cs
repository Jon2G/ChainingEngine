﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using ChainingEngine.Interfaces;
using Kit.Sql.Attributes;
using Kit.Sql.Interfaces;

namespace ChainingEngine.Models.Atras
{
    [Serializable]
    public class HaciaAtras : IEncadenamiento, IGuid
    {
        [Ignore]
        public Hipotesis Hipotesis { get; set; }
        public Guid HipotesisGuid
        {
            get => Hipotesis.Guid;
            set
            {
                if (Hipotesis is null)
                {
                    Hipotesis = new Hipotesis();
                }
                Hipotesis.Guid = value;
            }
        }

        public string Title
        {
            get => Hipotesis.Question;
            set
            {
                if (Hipotesis is null)
                {
                    Hipotesis = new Hipotesis();
                }
                Hipotesis.Question = value;
            }
        }
        [Ignore]
        public string Type => "Hacia atras";
        [PrimaryKey]
        public Guid Guid { get; set; }

        public HaciaAtras() : this(new Hipotesis())
        {

        }
        public HaciaAtras(Hipotesis Hipotesis)
        {
            this.Hipotesis = Hipotesis;
        }

        public void Delete()
        {
            Hipotesis.Delete();
            App.SqLite.Delete(this);
        }
        public void Save()
        {
            Delete();
            Hipotesis.Save();
            this.HipotesisGuid = Hipotesis.Guid;
            App.SqLite.InsertOrReplace(this);
        }

        internal static IEnumerable<HaciaAtras> GetAll()
        {
            HaciaAtras[] HaciaAtras = App.SqLite.Table<HaciaAtras>().ToArray();
            for (int i = 0; i < HaciaAtras.Length; i++)
            {
                HaciaAtras[i].Load();
            }
            return HaciaAtras;

        }

        private void Load()
        {
            Hipotesis = App.SqLite.Find<Hipotesis>(this.HipotesisGuid);
            Hipotesis.Load();
        }

        public void GetConclusiones(ObservableCollection<StringObject> conclusiones)
        {
            conclusiones.AddRange(Hipotesis.Evidencias.SelectMany(x => x.Comportamientos.Select(x => x.Conclusion))
                .Distinct().Select(x => (StringObject)x.Descripcion));
        }

        public void GetEvidencias(ObservableCollection<HaciaAtrasRow> rows)
        {
            for (int j = 0; j < Hipotesis.Evidencias.Count; j++)
            {
                var evidencia = Hipotesis.Evidencias.ElementAt(j);

                var row = new HaciaAtrasRow()
                {
                    Evidencia = evidencia.Descripcion,
                    Hechos = new ObservableCollection<StringObject>(evidencia.Comportamientos.Select(x => (StringObject)x.Question).ToArray())
                };
                rows.Add(row);
            }
        }
    }
}
