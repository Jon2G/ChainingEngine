using System;
using System.Collections.Generic;
using System.Text;
using Kit.Model;

namespace ChainingEngine.Models.Adelante
{
    public class HechoModel : ModelBase
    {
        private StringObject _Descripcion;

        public StringObject Descripcion
        {
            get => _Descripcion;
            set
            {
                _Descripcion = value;
                Raise(() => Descripcion);
            }
        }

        private StringObject _Si;
        public StringObject Si
        {
            get => _Si;
            set
            {
                if (value != Descripcion)
                {
                    _Si = value;
                }
                else
                {
                    Si = new StringObject();
                    Raise(() => Si);
                    Si.Value = string.Empty;
                }
                Raise(() => Si);
            }
        }

        private StringObject _No;
        public StringObject No
        {
            get => _No;
            set
            {
                if (value != Descripcion)
                {
                    _No = value;
                }
                else
                {
                    No = new StringObject();
                    Raise(() => No);
                    No.Value = string.Empty;
                }
                Raise(() => No);
            }
        }

        public HechoModel()
        {
            Descripcion = new StringObject();
            Si = new StringObject();
            No = new StringObject();
        }
    }
}
