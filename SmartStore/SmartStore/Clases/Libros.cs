using System;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace SmartStore.Clases
{
    public class Libros
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Categoria { get; set; }
        public string Autor { get; set; }
        public string Editorial { get; set; }
        public string Ano { get; set; }
        public string Score { get; set; }

        [Version]
        public string Version { get; set; }

    }
}
