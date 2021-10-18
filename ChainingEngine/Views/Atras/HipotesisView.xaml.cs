using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using ChainingEngine.Annotations;
using ChainingEngine.Models.Atras;
using ChainingEngine.ViewModels;

namespace ChainingEngine.Views.Atras
{
    /// <summary>
    /// Interaction logic for HipotesisView.xaml
    /// </summary>
    public partial class HipotesisView : INotifyPropertyChanged
    {
        public HipotesisView()
        {
            InitializeComponent();
        }

        public double MaxSize { get; set; }
        private void FrameworkElement_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Height > MaxSize)
            {
                var max = e.NewSize.Height;
                if (max > MaxSize)
                {
                    MaxSize = max;
                    OnPropertyChanged(nameof(MaxSize));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void UIElement_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            (this.DataContext as HipotesisViewModel).ActionCommand.Execute((sender as Grid).DataContext as Comportamiento);
        }
    }
}
