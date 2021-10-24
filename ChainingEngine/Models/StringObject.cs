using System;
using System.Collections.Generic;
using System.Text;
using ChainingEngine.ViewModels;
using Kit.Model;

namespace ChainingEngine.Models
{
    public class StringObject : ModelBase
    {
        public static implicit operator string(StringObject o) => o.Value;
        public static explicit operator StringObject(string b) => new StringObject() { Value = b };
        private string _Value;

        public string Value
        {
            get => _Value;
            set
            {
                _Value = value;
                Raise(() => Value);
                HaciaAdelanteDesignerViewModel.Instance?.Refresh();
            }
        }

        public override string ToString() => Value;
    }
}
