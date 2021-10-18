using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using AsyncAwaitBestPractices;
using ChainingEngine.Models;
using ChainingEngine.Models.Adelante;
using ChainingEngine.Models.Atras;
using Kit;

namespace ChainingEngine
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            //Engine.Run(Hecho.New("Es una CocaCola")
            //    .SetHipotesis(Hipotesis<HechoInferido>.New("Es color marrón")
            //        .Set(
            //            verdadero: (HechoInferido)HechoInferido.New("Es color marrón").SetHipotesis(Hipotesis<Conclusion>.New("La etiqueta es roja").Set(Conclusion.New("Cumple con los estándares"), Conclusion.New("No cumple con los estándares"))),
            //            falso: HechoInferido.New("No es color marrón").SetConclusion(Conclusion.New("No es CocaCola")))));


            //Atras
            string no = "No tengo ese síntoma";
            var rf = Conclusion.New("Resfriado");
            var ifz = Conclusion.New("Influenza");
            var none = Conclusion.New("No tiene resfriado ni influenza");

            Engine.Run(Hipotesis.New("Podría estar resfriado",
                new Evidencia("Fiebre",
                    new Comportamiento("Poco frecuente", rf),
                    new Comportamiento("Frecuente", ifz),
                    new Comportamiento(no, none)),
                new Evidencia("Dolor muscular",
                    new Comportamiento("Leve", rf),
                    new Comportamiento("Frecuente, dolores fuertes", ifz),
                    new Comportamiento(no, none)),
                new Evidencia("Escalofríos",
                    new Comportamiento("Poco común", rf),
                    new Comportamiento("Bastante común", rf),
                    new Comportamiento(no, none))));
        }

        public App()
        {
            Kit.WPF.Tools.Init();
        }
    }
}
