using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using ChainingEngine.Models.Atras;
using ChainingEngine.ViewModels;

namespace ChainingEngine.Views
{
    /// <summary>
    /// Interaction logic for HaciaAtrasDesigner.xaml
    /// </summary>
    public partial class HaciaAtrasDesigner : UserControl
    {
        public HaciaAtrasDesignerViewModel Model { get; }
        public HaciaAtrasDesigner()
        {
            InitializeComponent();
            this.Model = new HaciaAtrasDesignerViewModel();
            this.DataContext = this.Model;
        }

        private void UIElement_OnGotMouseCapture(object sender, MouseEventArgs e)
        {
            Keyboard.Focus(sender as IInputElement);
        }

        #region SyncScrollViewers

        private readonly HashSet<ScrollViewer> Scroll = new HashSet<ScrollViewer>();
        private void ScrollViewer_OnScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            foreach (ScrollViewer scrollViewer in Scroll)
            {
                scrollViewer.ScrollToHorizontalOffset(e.HorizontalOffset);
            }
        }

        private void FrameworkElement_OnInitialized(object? sender, EventArgs e)
        {
            Scroll.Add(sender as ScrollViewer);
        }

        #endregion


        private void UIElement_OnGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            (sender as TextBox)?.SelectAll();
        }
    }
}
