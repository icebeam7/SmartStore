using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace SmartStore.Clases
{
    [DataContract]
    public class Document
    {
        [DataMember]
        [JsonProperty("language")]
        public string language { get; set; }

        [DataMember]
        [JsonProperty("id")]
        public string id { get; set; }

        [DataMember]
        [JsonProperty("text")]
        public string text { get; set; }

        [DataMember]
        [JsonProperty("score")]
        public string score { get; set; }
    }
}
