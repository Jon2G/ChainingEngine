using System;
using System.Collections.Generic;
using System.Text;
using Kit.Model;

namespace ChainingEngine.Models
{
    public class StringObject : ModelBase
    {
        public static implicit operator string(StringObject o) => o.Value;
        public static explicit operator StringObject(string b) => new StringObject() { Value = b };
        public string Value { get; set; }
        public override string ToString() => Value;
    }
}
