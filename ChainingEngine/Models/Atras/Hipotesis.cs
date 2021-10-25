using System;
using ChainingEngine.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ChainingEngine.Interfaces;
using ChainingEngine.Models.Adelante;
using ChainingEngine.ViewModels;
using Kit;
using Kit.Sql.Attributes;
using Kit.Sql.Interfaces;
using Newtonsoft.Json;

namespace ChainingEngine.Models.Atras
{
    [Table("HipotesisHaciaAtras")]
    public class Hipotesis : IGuid
    {
        [Ignore]
        public ObservableCollection<Evidencia> Evidencias { get; private set; }
        [Ignore]
        public string Title => Question;
        public string Question { get; set; }
        [Ignore]
        public List<Comportamiento> Comportamientos { get; set; }
        [PrimaryKey]
        public Guid Guid { get; set; }

        public Hipotesis()
        {
            Comportamientos = new List<Comportamiento>();
            Evidencias = new ObservableCollection<Evidencia>();
        }
        public Hipotesis(string question, params Evidencia[] evidencias)
        {
            this.Comportamientos = new List<Comportamiento>();
            Question = question;
            Evidencias = new ObservableCollection<Evidencia>(evidencias);
        }

        public static Hipotesis New(string question, params Evidencia[] evidencias) => new Hipotesis(question, evidencias);

        public async void Run(MainView window)
        {
            Evidencias = new ObservableCollection<Evidencia>(Evidencias.Shuffle(new Random()));
            while (Evidencias.Any())
            {
                var evidencia = this.Evidencias.First();
                Evidencias.Remove(evidencia);
                Comportamiento comportamiento = await evidencia.Run(window);
                this.Comportamientos.Add(comportamiento);
            }
            //totales

            List<Tuple<Conclusion, int>> resultados = this.Comportamientos.GroupBy(x => x.Conclusion)
                 .Select(g => new Tuple<Conclusion, int>(g.Key, g.Select(z => z.Answer).Count())).ToList();

            Tuple<Conclusion, int>? Tconclusion = resultados.OrderBy(x => x.Item2).OrderByDescending(x => x.Item2).FirstOrDefault();

            Conclusion conclusion = Tconclusion.Item1;
            if (resultados.Any(x => x.Item2 == Tconclusion.Item2 && x.Item1 != conclusion)) //EFECTIVAMENTE 50/50,tambien
            {
                conclusion = new Conclusion("Resultado inconcluso");
            }
            //si sirve?
            var view = new ConclusionView();
            var model = new ConclusionViewModel(window, conclusion);
            view.DataContext = model;
            window.Content = view;
        }

        public void Delete()
        {
            foreach (Evidencia evidencia in Evidencias)
            {
                evidencia.Delete();
            }
            App.SqLite.Delete(this);
        }
        public void Save()
        {
            Guid = Guid.NewGuid();
            App.SqLite.InsertOrReplace(this);
            for (var i = 0; i < Evidencias.Count; i++)
            {
                Evidencia evidencia = Evidencias[i];
                evidencia.Save(i + 1, Guid);
            }
        }

        public void Load()
        {
            IEnumerable<Evidencia> evidencias = App.SqLite.Table<Evidencia>()
                .Where(x => x.HipotesisGuid == this.Guid).OrderBy(x => x.Id).ToArray();
            for (int i = 0; i < evidencias.Count(); i++)
            {
                evidencias.ElementAt(i).Load();
            }
            this.Evidencias.AddRange(evidencias);
        }
    }
}
