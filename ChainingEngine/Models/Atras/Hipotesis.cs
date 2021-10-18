using System;
using ChainingEngine.Views;
using System.Collections.Generic;
using System.Linq;
using ChainingEngine.Models.Adelante;
using ChainingEngine.ViewModels;
using Kit;

namespace ChainingEngine.Models.Atras
{
    public class Hipotesis : Queue<Evidencia>
    {
        public string Question { get; set; }
        public List<Comportamiento> Comportamientos { get; }
        public Hipotesis(string question, params Evidencia[] evidencias)
        {
            this.Comportamientos = new List<Comportamiento>();
            Question = question;
            foreach (Evidencia evidencia in evidencias.Shuffle(new Random()))
            {
                this.Enqueue(evidencia);
            }
        }

        public static Hipotesis New(string question, params Evidencia[] evidencias) => new Hipotesis(question, evidencias);

        public async void Run(MainView window)
        {
            while (this.Any())
            {
                var evidencia = this.Dequeue();
                Comportamiento comportamiento = await evidencia.Run(window);
                this.Comportamientos.Add(comportamiento);
            }

            List<Tuple<Conclusion, int>> resultados = this.Comportamientos.GroupBy(x => x.Conclusion)
                 .Select(g => new Tuple<Conclusion, int>(g.Key, g.Select(z => z.Answer).Count())).ToList();

            Tuple<Conclusion, int>? Tconclusion = resultados.OrderBy(x => x.Item2).OrderByDescending(x => x.Item2).FirstOrDefault();

            Conclusion conclusion = Tconclusion.Item1;
            if (resultados.Any(x => x.Item2 == Tconclusion.Item2 && x.Item1 != conclusion)) 
            {
                conclusion = new Conclusion("Resultado inconcluso");
            }
            var view = new ConclusionView();
            var model = new ConclusionViewModel(window, conclusion);
            view.DataContext = model;
            window.Content = view;
        }
    }
}
