using Newtonsoft.Json;
using System.Runtime.Serialization;
namespace SmartStore.Clases
{
    [DataContract]
    public class SentimentRequestInfo
    {
        [DataMember]
        [JsonProperty("documents")]
        public Document[] Documents { get; set; }
    }
}
