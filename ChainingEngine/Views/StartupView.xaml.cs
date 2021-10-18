using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ChainingEngine.ViewModels;

namespace ChainingEngine.Views
{
    /// <summary>
    /// Interaction logic for StartupView.xaml
    /// </summary>
    public partial class StartupView 
    {
        public StartupViewModel Model { get; }
        public StartupView(StartupViewModel model)
        {
            InitializeComponent();
            this.Model = model;
            this.DataContext = this.Model;
        }
    }
}
