using System;
using System.Collections.Generic;
using System.Text;
using ChainingEngine.Views;

namespace ChainingEngine.Models.Adelante
{
    public interface IBranch
    {
        public void Save(Guid Guid, bool answer);
        public void Load();
        public void Run(MainView view);
    }
}
