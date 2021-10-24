using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChainingEngine.ViewModels;
using ChainingEngine.Views;
using Kit.Sql.Attributes;
using Kit.Sql.Interfaces;

namespace ChainingEngine.Models.Adelante
{
    [Serializable]
    public class Conclusion : IGuid, IComparable<Conclusion>, IEquatable<Conclusion>, IBranch
    {
        public string Descripcion { get; set; }
        [PrimaryKey]
        public Guid Guid { get; set; }
        public bool Side { get; set; }
        public Guid ParentId { get; set; }
        public Conclusion()
        {

        }
        public Conclusion(string descripcion)
        {
            Descripcion = descripcion;
        }
        public static Conclusion New(string descripcion) => new Conclusion(descripcion);

        public void Run(MainView window)
        {
            var view = new ConclusionView();
            var model = new ConclusionViewModel(window, this);
            view.DataContext = model;
            window.Content = view;
        }

        public void Delete()
        {
            App.SqLite.Delete(this);
        }
        public void Save()
        {
            App.SqLite.InsertOrReplace(this);
        }

        public void Save(Guid Guid, bool answer)
        {
            ParentId = Guid;
            Side = answer;
            Save();
        }

        public void Load()
        {
            //nada que cargar :)
        }

        public bool Equals(Conclusion other)
        {
            return other?.Guid == this.Guid;
        }

        public override int GetHashCode()
        {
            var a=this.Guid.GetHashCode();
            return a;
        }

        public override string ToString() => Descripcion;

        public int CompareTo([AllowNull] Conclusion other) => this.Guid.CompareTo(other?.Guid ?? Guid.Empty);
    }
}
