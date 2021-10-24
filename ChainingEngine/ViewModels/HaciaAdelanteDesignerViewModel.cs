using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using ChainingEngine.Models;
using ChainingEngine.Models.Adelante;
using Kit.Extensions;
using Kit.Model;

namespace ChainingEngine.ViewModels
{
    public class HaciaAdelanteDesignerViewModel : ModelBase
    {
        public static HaciaAdelanteDesignerViewModel Instance;
        public string Cuestionamiento { get; set; }
        public ObservableCollection<StringObject> Conclusiones { get; set; }
        public ObservableCollection<HechoModel> Hechos { get; set; }

        public StringObject[] Elementos
        {
            get
            {
                List<StringObject> items = new List<StringObject>();
                items.AddRange(Conclusiones);
                items.AddRange(Hechos.Select(x => x.Descripcion));
                return items.ToArray();
            }
        }

        public ICommand SaveCommand { get; set; }
        public ICommand NuevoCuestionamientoCommand { get; set; }
        public ICommand NuevaConclusionCommand { get; set; }
        public ICommand EliminarConclusionCommand { get; set; }
        public ICommand EliminarCuestionamientoCommand { get; set; }

        public HaciaAdelanteDesignerViewModel()
        {
            Instance = this;
            SaveCommand = new Command(Save);
            NuevaConclusionCommand = new Command(NuevaConclusion);
            EliminarConclusionCommand = new Command<StringObject>(EliminarConclusion);
            NuevoCuestionamientoCommand = new Command(NuevoCuestionamiento);
            EliminarCuestionamientoCommand = new Command<HechoModel>(EliminarCuestionamiento);
            Conclusiones = new ObservableCollection<StringObject>();
            Hechos = new ObservableCollection<HechoModel>();
        }

        public void Refresh() => Raise(() => Elementos);

        private void EliminarCuestionamiento(HechoModel obj)
        {
            this.Hechos.Remove(obj);
            Raise(() => Elementos);
        }

        private void NuevoCuestionamiento()
        {
            this.Hechos.Add(new HechoModel());
            Raise(() => Elementos);
        }

        private void EliminarConclusion(StringObject c)
        {
            this.Conclusiones.Remove(c);
            Raise(() => Elementos);
        }
        private void NuevaConclusion()
        {
            this.Conclusiones.Add(new StringObject());
            Raise(() => Elementos);
        }

        private void Save()
        {
            Conclusion[] conclusiones = new Conclusion[Conclusiones.Count];
            for (int i = 0; i < Conclusiones.Count; i++)
            {
                conclusiones[i] = new Conclusion(this.Conclusiones[i]);
            }

            BaseHipotesis[] hipotesis = new BaseHipotesis[Hechos.Count];
            for (int i = 0; i < Hechos.Count; i++)
            {
                var hecho = Hechos[i];
                var inferido = new Hipotesis<HechoInferido>(hecho.Descripcion);
                BuildHechos(inferido, hecho, conclusiones);
            }




            //Hecho hecho = new Hecho(Cuestionamiento);




            //Hecho.New("Es una CocaCola")
            //      .SetHipotesis(Hipotesis<HechoInferido>.New("Es color marrón")
            //          .Set(
            //              verdadero: (HechoInferido)HechoInferido.New("Es color marrón").SetHipotesis(Hipotesis<Conclusion>.New("La etiqueta es roja").Set(Conclusion.New("Cumple con los estándares"), Conclusion.New("No cumple con los estándares"))),
            //              falso: HechoInferido.New("No es color marrón").SetConclusion(Conclusion.New("No es CocaCola"))));
        }

        private void BuildHechos(BaseHipotesis hipotesis, HechoModel hecho, Conclusion[] conclusiones)
        {
            Conclusion conclusion;
            HechoModel nhecho;
            if (this.Conclusiones.IndexOf(hecho.Si) is int cindex && cindex > 0)
            {
                conclusion = conclusiones[cindex];
                
            }
            else
            {
                nhecho = Hechos.First(x => x.Descripcion == hecho.Si);



            }

            if (this.Conclusiones.IndexOf(hecho.No) is int nindex && nindex > 0)
            {
                conclusion = conclusiones[nindex];

            }
            else
            {
                nhecho = Hechos.First(x => x.Descripcion == hecho.No);

            }
        }
    }
}
