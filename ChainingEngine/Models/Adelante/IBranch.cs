using System;
using System.Collections.Generic;
using System.Text;
using ChainingEngine.Interfaces;
using ChainingEngine.Views;

namespace ChainingEngine.Models.Adelante
{
    public interface IBranch 
    {
        public string Descripcion { get;  }
        public void Save(Guid Guid, bool answer);
        public void Delete();
        public void Load();
        public void Run(MainView view);
    }
}
