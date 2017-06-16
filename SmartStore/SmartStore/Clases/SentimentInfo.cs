using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace SmartStore.Clases
{
    [DataContract]
    public class SentimentInfo
    {
        [DataMember]
        [JsonProperty("documents")]
        public Document[] documents { get; set; }
    }
}
