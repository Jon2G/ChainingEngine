using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChainingEngine.Interfaces;
using ChainingEngine.Models.Adelante;
using ChainingEngine.Views;
using Kit.Sql.Attributes;
using Kit.Sql.Interfaces;

namespace ChainingEngine.Models.Atras
{
    public class Comportamiento : IQuestion, IGuid, IComparable<Comportamiento>
    {
        public Guid EvidenciaGuid { get; set; }
        public Guid ConclusionGuid { get; set; }
        [PrimaryKey]
        public Guid Guid { get; set; }
        public string Question { get; set; }
        [Ignore]
        public Conclusion Conclusion { get; set; }
        [Ignore]
        public bool Answer { get; set; }

        public Comportamiento() : this(string.Empty, new Conclusion())
        {

        }
        public Comportamiento(string question, Conclusion conclusion)
        {
            this.Question = question;
            this.Conclusion = conclusion;
        }

        public void Ask(MainView main)
        {
        }

        public override string ToString()
        {
            return $"Comportamiento {Question}->{Conclusion}";
        }
        public void Delete()
        {
            Conclusion.Delete();
            App.SqLite.Delete(this);
        }
        internal void Save(Guid evidenciaGuid, Conclusion conclusion)
        {
            EvidenciaGuid = evidenciaGuid;
            ConclusionGuid = conclusion.Guid;
            App.SqLite.InsertOrReplace(this);

        }
        public void Load()
        {
            this.Conclusion = App.SqLite.Table<Conclusion>().FirstOrDefault(x => x.Guid == this.ConclusionGuid);
        }
        public int CompareTo([AllowNull] Comportamiento other) => this.Guid.CompareTo(other?.Guid ?? Guid.Empty);
        public override bool Equals(object? obj)
        {
            if (obj is Comportamiento comportamiento)
                return comportamiento.Guid == this.Guid;
            return false;
        }
    }
}
