using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ChainingEngine.Interfaces;
using ChainingEngine.Models;
using ChainingEngine.Models.Adelante;
using ChainingEngine.Models.Atras;
using ChainingEngine.Views;
using Kit.Extensions;
using Kit.Model;

namespace ChainingEngine.ViewModels
{
    public class PrincipalPageViewModel : ModelBase
    {
        public ICommand PageClickCommand { get; }
        public ICommand HaciaAtrasCommand { get; }
        public ICommand HaciaAdelanteCommand { get; }
        public ICommand StartCommand { get; }
        public ICommand EncadenamientoCommand { get; }
        public ObservableCollection<IEncadenamiento> Encadenamientos { get; }
        public MainView MainView { get; }
        private IEncadenamiento _Selected;

        public IEncadenamiento Selected
        {
            get => _Selected;
            set
            {
                _Selected = value;
                Raise(() => Selected);
                Raise(()=>IsItemSelected);
            }
        }

        public bool IsItemSelected => Selected is not null;

        public PrincipalPageViewModel(MainView mainView)
        {
            //var rf = Conclusion.New("Resfriado");
            //var ifz = Conclusion.New("Influenza");
            //var none = Conclusion.New("No tiene resfriado ni influenza");
            //var noc = new Comportamiento("No tengo ese síntoma", none);
            //this.Encadenamientos = new ObservableCollection<IEncadenamiento>()
            //{
            //    new HaciaAtras(Hipotesis.New("Podría estar resfriado",
            //        new Evidencia("Comportamiento de los síntomas",new Comportamiento("Gradual",rf),
            //            new Comportamiento("Repentino",ifz), noc),
            //        new Evidencia("Fatiga, debilidad", new Comportamiento("A veces", rf), new Comportamiento("Normal",ifz), noc),
            //        new Evidencia("Estornudos", new Comportamiento("Común",rf), new Comportamiento("A veces", ifz), noc),
            //        new Evidencia("Incomodidad en el pecho, tos", new Comportamiento("Leve o moderado",rf), new Comportamiento("Común, incomodidad intensa",ifz), noc),
            //        new Evidencia("Naríz tapada", new Comportamiento("Común", rf), new Comportamiento("A veces",ifz), noc),
            //        new Evidencia("Dolor de garganta", new Comportamiento("Común",rf), new Comportamiento("A veces", ifz), noc),
            //        new Evidencia("Dolor de cabeza", new Comportamiento("Poco frecuente",rf), new Comportamiento("Común",ifz),noc),
            //        new Evidencia("Fiebre", new Comportamiento("Poco frecuente", rf), new Comportamiento("Frecuente", ifz), noc),
            //        new Evidencia("Dolor muscular", new Comportamiento("Leve", rf), new Comportamiento("Frecuente, dolores fuertes", ifz), noc),
            //        new Evidencia("Escalofríos", new Comportamiento("Poco común", rf), new Comportamiento("Bastante común", rf), noc))),



            //    new HaciaAdelante(Hecho.New("Es una CocaCola")
            //        .SetHipotesis(Hipotesis<HechoInferido>.New("Es color marrón")
            //            .Set(
            //                verdadero: (HechoInferido)HechoInferido.New("Es color marrón").SetHipotesis(Hipotesis<Conclusion>.New("La etiqueta es roja").Set(Conclusion.New("Cumple con los estándares"), Conclusion.New("No cumple con los estándares"))),
            //                falso: HechoInferido.New("No es color marrón").SetConclusion(Conclusion.New("No es CocaCola")))))
            //};
            this.Encadenamientos = new ObservableCollection<IEncadenamiento>(Models.Atras.HaciaAtras.GetAll());
            this.EncadenamientoCommand = new Command<IEncadenamiento>(Encadenamiento);
            this.HaciaAdelanteCommand = new Command(HaciaAdelante);
            this.HaciaAtrasCommand = new Command(HaciaAtras);
            this.PageClickCommand = new Command(PageClick);
            this.StartCommand = new Command<IEncadenamiento>(Start);
            this.MainView = mainView;
        }

        private void PageClick()
        {
            this.Selected = null;
        }

        private void Start(IEncadenamiento encadenamiento)
        {
            Engine.Run(MainView, encadenamiento);
        }
        private void Encadenamiento(IEncadenamiento encadenamiento)
        {
            this.Selected = encadenamiento;
        }

        private void HaciaAtras()
        {
            MainView.Content = new HaciaAtrasDesigner();


            return;
            var rf = Conclusion.New("Resfriado");
            var ifz = Conclusion.New("Influenza");
            var none = Conclusion.New("No tiene resfriado ni influenza");
            var noc = new Comportamiento("No tengo ese síntoma", none);

            Engine.Run(MainView, Hipotesis.New("Podría estar resfriado",
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

        private void HaciaAdelante()
        {
            MainView.Content = new HaciaAdelanteDesigner();
            return;
            Engine.Run(MainView, Hecho.New("Es una CocaCola")
                .SetHipotesis(Hipotesis<HechoInferido>.New("Es color marrón")
                    .Set(
                        verdadero: (HechoInferido)HechoInferido.New("Es color marrón").SetHipotesis(Hipotesis<Conclusion>.New("La etiqueta es roja").Set(Conclusion.New("Cumple con los estándares"), Conclusion.New("No cumple con los estándares"))),
                        falso: HechoInferido.New("No es color marrón").SetConclusion(Conclusion.New("No es CocaCola")))));
        }
    }
}
