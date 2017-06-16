using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace SmartStore.Clases
{
    [DataContract]
    public class RecommendedItemInfo
    {
        [DataMember]
        [JsonProperty("id")]
        public string Id { get; set; }

        [DataMember]
        [JsonProperty("name")]
        public string Name { get; set; }

        [DataMember]
        [JsonProperty("metadata")]
        public string Metadata { get; set; }
    }
}