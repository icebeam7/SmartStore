using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace SmartStore.Clases
{
    public class Emociones
    {
        private string _id;

        public string Foto { get; set; }
        public string Emocion { get; set; }
        public string Libro { get; set; }
        public float Score { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Id
        {
            get { return _id; }
            set
            {
                _id = value;
            }
        }

        [Version]
        public string Version { get; set; }

    }
}