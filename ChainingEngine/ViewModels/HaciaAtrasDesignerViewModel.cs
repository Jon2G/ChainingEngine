using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Xml;
using ChainingEngine.Models;
using ChainingEngine.Models.Adelante;
using ChainingEngine.Models.Atras;
using ChainingEngine.Views;
using Kit;
using Kit.Extensions;
using Kit.Model;

namespace ChainingEngine.ViewModels
{
    public class HaciaAtrasDesignerViewModel : ModelBase
    {
        public string Hipotesis { get; set; }
        public ObservableCollection<HaciaAtrasRow> Rows { get; set; }
        public ObservableCollection<StringObject> Conclusiones { get; set; }
        public ICommand RemoveConclusionCommand { get; }
        public ICommand RemoveEvidenciaCommand { get; }
        public ICommand AddConclusionCommand { get; }
        public ICommand AddEvidenciaCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        private readonly MainView MainView;
        private readonly HaciaAtras HaciaAtras;
        public HaciaAtrasDesignerViewModel(MainView mainView, HaciaAtras encadenamiento=null)
        {
            this.HaciaAtras = encadenamiento;
            this.MainView = mainView;
            this.AddConclusionCommand = new Command(AddConclusion);
            this.AddEvidenciaCommand = new Command(AddEvidencia);
            this.RemoveConclusionCommand = new Command(RemoveConclusion);
            this.RemoveEvidenciaCommand = new Command<HaciaAtrasRow>(RemoveEvidencia);
            this.SaveCommand = new Command(Save);
            this.CancelCommand = new Command(Cancel);
            this.Rows = new ObservableCollection<HaciaAtrasRow>();
            this.Conclusiones = new ObservableCollection<StringObject>();
            if (encadenamiento is not null)
            {
                this.Hipotesis = encadenamiento.Title;
                encadenamiento.GetConclusiones(this.Conclusiones);
                encadenamiento.GetEvidencias(this.Rows);
            }
        }

        private void Cancel()
        {
            this.MainView.Content = new PrincipalPage(this.MainView);
        }
        private void Save()
        {
            HaciaAtras?.Delete();
            
            Conclusion[] conclusiones = new Conclusion[Conclusiones.Count];
            for (int i = 0; i < Conclusiones.Count; i++)
            {
                string conclusion = Conclusiones[i];
                conclusiones[i] = new Conclusion(conclusion);
            }

            Evidencia[] evidencias = new Evidencia[Rows.Count];
            for (var index = 0; index < Rows.Count; index++)
            {
                HaciaAtrasRow row = Rows[index];
                Comportamiento[] comportamientos = new Comportamiento[Conclusiones.Count];
                for (int i = 0; i < Conclusiones.Count; i++)
                {
                    string hecho = row.Hechos[i];
                    comportamientos[i] = new Comportamiento(hecho, conclusiones[i]);
                }
                Evidencia evidencia = new Evidencia(row.Evidencia, comportamientos);
                evidencias[index] = evidencia;
            }
            Models.Atras.Hipotesis hipotesis = new Models.Atras.Hipotesis(Hipotesis, evidencias);
            HaciaAtras haciaAtras = new HaciaAtras(hipotesis);
            haciaAtras.Save();
            Cancel();
        }
        private void RemoveConclusion()
        {
            if (!Conclusiones.Any())
            {
                return;
            }

            int index = this.Conclusiones.Count - 1;
            this.Conclusiones.RemoveAt(index);
            foreach (HaciaAtrasRow row in Rows)
            {
                row.Hechos.RemoveAt(index);
            }

        }
        private void AddConclusion()
        {
            int count = Conclusiones.Count + 1;
            this.Conclusiones.Add((StringObject)$"Conclusión #{count}");
            foreach (HaciaAtrasRow row in Rows)
            {
                row.Hechos.Add((StringObject)"");
            }
        }

        private void RemoveEvidencia(HaciaAtrasRow row)
        {
            if (row is null)
                return;
            this.Rows.Remove(row);
        }
        private void AddEvidencia()
        {
            int count = this.Rows.Count + 1;
            var row = new HaciaAtrasRow() { Evidencia = $"Evidencia #{count}" };
            for (int i = 0; i < Conclusiones.Count; i++)
            {
                row.Hechos.Add((StringObject)String.Empty);
            }
            this.Rows.Add(row);
        }
        private DataGridTextColumn Create()
        {
            StringReader stringReader = new StringReader(
                @"<DataGridTextColumn xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"" IsReadOnly=""False"">
                        <DataGridTextColumn.HeaderTemplate> 
                            <DataTemplate> 
                                <TextBox/> 
                            </DataTemplate>
                        </DataGridTextColumn.HeaderTemplate>
                   </DataGridTextColumn>");
            XmlReader xmlReader = XmlReader.Create(stringReader);
            return XamlReader.Load(xmlReader) as DataGridTextColumn;
        }

    }
}
