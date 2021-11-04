using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using ChainingEngine.ViewModels;
using Kit.Model;
using Kit.Sql.Interfaces;

namespace ChainingEngine.Models
{
    public class StringObject : ModelBase, IEquatable<StringObject>, IComparable<StringObject>
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

        public bool Equals(StringObject other)
        {
            if (other is null)
            {
                return false;
            }
            return this.Value switch
            {
                null when other.Value is not null => false,
                null when other.Value is null => true,
                _ => this.Value.CompareTo(other.Value) == 0
            };
        }

        public int CompareTo([AllowNull] StringObject other) => this.Value.CompareTo(other.Value);

        public override bool Equals(object? obj)
        {
            if (obj is StringObject s)
            {
                return s.Equals(this);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return this.Value?.GetHashCode(StringComparison.InvariantCultureIgnoreCase)??-1;
        }

        public override string ToString() => Value??"[NULL]";

        public static bool operator ==(StringObject obj1, StringObject obj2)
        {
            if (ReferenceEquals(obj1.Value, obj2.Value))
            {
                return true;
            }
            if (ReferenceEquals(obj1.Value, null))
            {
                return false;
            }
            if (ReferenceEquals(obj2.Value, null))
            {
                return false;
            }

            return obj1.Value.Equals(obj2.Value);
        }

        public static bool operator !=(StringObject obj1, StringObject obj2)
        {
            if (obj1?.Value is null && obj2?.Value is null)
            {
                return true;
            }
            return obj1?.Value != obj2?.Value;
        }
    }
}
