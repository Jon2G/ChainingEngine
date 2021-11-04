using System;
using System.Collections.Generic;
using System.Text;
using ChainingEngine.Models.Adelante;
using ChainingEngine.Models.Atras;
using Newtonsoft.Json;

namespace ChainingEngine.Interfaces
{
    public interface IEncadenamiento
    {
        [JsonIgnore]
        public string Title { get; }

        [JsonIgnore]

        public string Type { get; }

    }
}
