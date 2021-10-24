using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using AsyncAwaitBestPractices;
using ChainingEngine.Models;
using ChainingEngine.Models.Adelante;
using ChainingEngine.Models.Atras;
using HandyControl.Data;
using HandyControl.Themes;
using HandyControl.Tools;
using Kit;
using Kit.Sql.Sqlite;
using Kit.WPF.Dialogs.ICustomMessageBox;
using Prism.Ioc;

namespace ChainingEngine
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public static readonly SQLiteConnection SqLite;

        static App()
        {
            Kit.WPF.Tools.Init();
            FileInfo dbFileInfo = new FileInfo($"{Tools.Instance.LibraryPath}\\ChangingEngine.db");
            SqLite = new SQLiteConnection(dbFileInfo, 100);
            SqLite.CheckTables(new[]
            {
                typeof(HaciaAtras),
                typeof(HaciaAdelante),
                typeof(Models.Atras.Hipotesis),
                typeof(Models.Adelante.Hipotesis),
                typeof(Hecho),
                typeof(Comportamiento),
                typeof(Conclusion),
                typeof(Evidencia),
            });
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
        }

        internal void UpdateSkin(SkinType skin)
        {

            SharedResourceDictionary.SharedDictionaries.Clear();
            Resources.MergedDictionaries.Add(ResourceHelper.GetSkin(skin));
            Resources.MergedDictionaries.Add(new ResourceDictionary
            {
                Source = new Uri("pack://application:,,,/HandyControl;component/Themes/Theme.xaml")
            });
            Current.MainWindow?.OnApplyTemplate();
        }
    }
}
