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
        public ICommand EditCommand { get; }
        public ICommand EncadenamientoCommand { get; }
        public ObservableCollection<IEncadenamiento> AtrasEncadenamientos { get; }
        public ObservableCollection<IEncadenamiento> AdelanteEncadenamientos { get; }
        public MainView MainView { get; }
        private IEncadenamiento _Selected;

        public IEncadenamiento Selected
        {
            get => _Selected;
            set
            {
                _Selected = value;
                Raise(() => Selected);
                Raise(() => IsItemSelected);
            }
        }

        public bool IsItemSelected => Selected is not null;

        public PrincipalPageViewModel(MainView mainView)
        {
            this.AtrasEncadenamientos = new ObservableCollection<IEncadenamiento>(Models.Atras.HaciaAtras.GetAll());
            this.AdelanteEncadenamientos = new ObservableCollection<IEncadenamiento>(Models.Adelante.HaciaAdelante.GetAll());
            this.EncadenamientoCommand = new Command<IEncadenamiento>(Encadenamiento);
            this.HaciaAdelanteCommand = new Command(HaciaAdelante);
            this.HaciaAtrasCommand = new Command(HaciaAtras);
            this.PageClickCommand = new Command(PageClick);
            this.StartCommand = new Command<IEncadenamiento>(Start);
            this.EditCommand = new Command<IEncadenamiento>(Edit);
            this.MainView = mainView;
        }

        private void PageClick()
        {
            this.Selected = null;
        }

        private void Edit(IEncadenamiento encadenamiento)
        {
            switch (encadenamiento)
            {
                case Models.Adelante.HaciaAdelante adelante:
                    MainView.Content = new HaciaAdelanteDesigner(MainView,adelante);
                    break;
                case Models.Atras.HaciaAtras atras:
                    MainView.Content = new HaciaAtrasDesigner(MainView, atras);
                    break;
            }
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
            MainView.Content = new HaciaAtrasDesigner(this.MainView);
        }

        private void HaciaAdelante()
        {
            MainView.Content = new HaciaAdelanteDesigner(this.MainView);
        }
    }
}
