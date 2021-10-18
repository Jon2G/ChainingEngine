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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChainingEngine.ViewModels;

namespace ChainingEngine.Views
{
    /// <summary>
    /// Interaction logic for PrincipalPage.xaml
    /// </summary>
    public partial class PrincipalPage
    {
        private readonly MainView MainView;
        public PrincipalPage(MainView mainView)
        {
            InitializeComponent();
            this.MainView = mainView;
            this.DataContext = new PrincipalPageViewModel(mainView);
        }
    }
}
