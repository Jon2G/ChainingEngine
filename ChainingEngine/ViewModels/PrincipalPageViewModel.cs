using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ChainingEngine.Models;
using ChainingEngine.Models.Adelante;
using ChainingEngine.Models.Atras;
using ChainingEngine.Views;
using Kit.Extensions;

namespace ChainingEngine.ViewModels
{
    public class PrincipalPageViewModel
    {
        public ICommand AdelanteCommand { get; set; }
        public ICommand AtrasCommand { get; set; }
        public MainView MainView { get; }
        public PrincipalPageViewModel(MainView mainView)
        {
            AdelanteCommand = new Command(Adelante);
            AtrasCommand = new Command(Atras);
            MainView = mainView;
        }

        private void Atras()
        {
            var rf = Conclusion.New("Resfriado");
            var ifz = Conclusion.New("Influenza");
            var none = Conclusion.New("No tiene resfriado ni influenza");
            var noc = new Comportamiento("No tengo ese síntoma", none);

            Engine.Run(MainView,Hipotesis.New("Podría estar resfriado",
                new Evidencia("Comportamiento de los síntomas", new Comportamiento("Gradual", rf),
                new Comportamiento("Repentino", ifz), noc),
                new Evidencia("Fatiga, debilidad", new Comportamiento("A veces", rf), new Comportamiento("Normal", ifz), noc),
                new Evidencia("Estornudos", new Comportamiento("Común", rf), new Comportamiento("A veces", ifz), noc),
                new Evidencia("Incomodidad en el pecho, tos", new Comportamiento("Leve o moderado", rf), new Comportamiento("Común, incomodidad intensa", ifz), noc),
                new Evidencia("Naríz tapada", new Comportamiento("Común", rf), new Comportamiento("A veces", ifz), noc),
                new Evidencia("Dolor de garganta", new Comportamiento("Común", rf), new Comportamiento("A veces", ifz), noc),
                new Evidencia("Dolor de cabeza", new Comportamiento("Poco frecuente", rf), new Comportamiento("Común", ifz), noc),
                new Evidencia("Fiebre", new Comportamiento("Poco frecuente", rf), new Comportamiento("Frecuente", ifz), noc),
                new Evidencia("Dolor muscular", new Comportamiento("Leve", rf), new Comportamiento("Frecuente, dolores fuertes", ifz), noc),
                new Evidencia("Escalofríos", new Comportamiento("Poco común", rf), new Comportamiento("Bastante común", rf), noc)));
        }

        private void Adelante()
        {
            Engine.Run(MainView,Hecho.New("Es una CocaCola")
                .SetHipotesis(Hipotesis<HechoInferido>.New("Es color marrón")
                    .Set(
                        verdadero: (HechoInferido)HechoInferido.New("Es color marrón").SetHipotesis(Hipotesis<Conclusion>.New("La etiqueta es roja").Set(Conclusion.New("Cumple con los estándares"), Conclusion.New("No cumple con los estándares"))),
                        falso: HechoInferido.New("No es color marrón").SetConclusion(Conclusion.New("No es CocaCola")))));
        }
    }
}
