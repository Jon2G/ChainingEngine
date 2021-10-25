using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using ChainingEngine.Models;
using ChainingEngine.Models.Adelante;
using ChainingEngine.Views;
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

        public ICommand CancelCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand NuevoCuestionamientoCommand { get; set; }
        public ICommand NuevaConclusionCommand { get; set; }
        public ICommand EliminarConclusionCommand { get; set; }
        public ICommand EliminarCuestionamientoCommand { get; set; }
        private readonly MainView MainView;
        private readonly HaciaAdelante Adelante;
        public HaciaAdelanteDesignerViewModel(MainView mainView, HaciaAdelante adelante = null)
        {
            this.Adelante = adelante;
            this.MainView = mainView;
            Instance = this;
            CancelCommand = new Command(Cancel);
            SaveCommand = new Command(Save);
            NuevaConclusionCommand = new Command(NuevaConclusion);
            EliminarConclusionCommand = new Command<StringObject>(EliminarConclusion);
            NuevoCuestionamientoCommand = new Command(NuevoCuestionamiento);
            EliminarCuestionamientoCommand = new Command<HechoModel>(EliminarCuestionamiento);
            Conclusiones = new ObservableCollection<StringObject>();
            Hechos = new ObservableCollection<HechoModel>();
            if (adelante is not null)
            {
                this.Cuestionamiento = adelante.Title;
                adelante.GetConclusiones(this.Conclusiones, adelante.Hecho.Hipotesis);
                adelante.GetHechos(this.Hechos, adelante.Hecho.Hipotesis);
            }
        }

        private void Cancel()
        {
            MainView.Content = new PrincipalPage(MainView);
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
            Adelante?.Delete();
               Conclusion[] conclusiones = new Conclusion[Conclusiones.Count];
            for (int i = 0; i < Conclusiones.Count; i++)
            {
                conclusiones[i] = new Conclusion(this.Conclusiones[i]);
            }
            HechoModel hecho = Hechos.First();
            Hipotesis inferido = new Hipotesis(hecho.Descripcion);
            BuildHechos(inferido, hecho, conclusiones);
            new HaciaAdelante(new Hecho(Cuestionamiento).SetHipotesis(inferido)).Save();
            Cancel();
        }

        private void BuildHechos(Hipotesis hipotesis, HechoModel hecho, Conclusion[] conclusiones)
        {
            Conclusion conclusion;
            HechoModel nhecho;
            if (this.Conclusiones.IndexOf(hecho.Si) is int cindex && cindex >= 0)
            {
                conclusion = conclusiones[cindex];
                hipotesis.Verdadero = conclusion;
            }
            else
            {
                nhecho = Hechos.First(x => x.Descripcion == hecho.Si);
                Hipotesis siHipotesis = new Hipotesis(nhecho.Descripcion);
                hipotesis.Verdadero = siHipotesis;
                BuildHechos(siHipotesis, nhecho, conclusiones);
            }

            if (this.Conclusiones.IndexOf(hecho.No) is int nindex && nindex >= 0)
            {
                conclusion = conclusiones[nindex];
                hipotesis.Falso = conclusion;
            }
            else
            {
                nhecho = Hechos.First(x => x.Descripcion == hecho.No);
                Hipotesis siHipotesis = new Hipotesis(nhecho.Descripcion);
                hipotesis.Falso = siHipotesis;
                BuildHechos(siHipotesis, nhecho, conclusiones);
            }
        }
    }
}
