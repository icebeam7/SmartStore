using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SmartStore.Clases
{
    [DataContract]
    public class RecommendedItemSetInfo
    {
        public RecommendedItemSetInfo()
        {
            Items = new List<RecommendedItemInfo>();
        }

        [DataMember]
        [JsonProperty("items")]
        public IEnumerable<RecommendedItemInfo> Items { get; set; }

        [DataMember]
        [JsonProperty("rating")]
        public double Rating { get; set; }

        [DataMember]
        [JsonProperty("reasoning")]
        public IEnumerable<string> Reasoning { get; set; }
    }
}