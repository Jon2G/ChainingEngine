using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChainingEngine.Models.Adelante;
using ChainingEngine.ViewModels;

namespace ChainingEngine.Views
{
    /// <summary>
    /// Interaction logic for HaciaAdelanteDesigner.xaml
    /// </summary>
    public partial class HaciaAdelanteDesigner : UserControl
    {
        public HaciaAdelanteDesignerViewModel Model { get; }
        public HaciaAdelanteDesigner(MainView mainView,HaciaAdelante adelante = null)
        {
            this.Model = new HaciaAdelanteDesignerViewModel(mainView,adelante);
            this.DataContext = this.Model;
            InitializeComponent();

        }
    }
}
