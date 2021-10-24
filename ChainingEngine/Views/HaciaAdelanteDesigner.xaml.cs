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
using ChainingEngine.ViewModels;

namespace ChainingEngine.Views
{
    /// <summary>
    /// Interaction logic for HaciaAdelanteDesigner.xaml
    /// </summary>
    public partial class HaciaAdelanteDesigner : UserControl
    {
        public HaciaAdelanteDesignerViewModel Model { get; }
        public HaciaAdelanteDesigner()
        {
            this.Model = new HaciaAdelanteDesignerViewModel();
            this.DataContext = this.Model;
            InitializeComponent();
            
        }
    }
}
