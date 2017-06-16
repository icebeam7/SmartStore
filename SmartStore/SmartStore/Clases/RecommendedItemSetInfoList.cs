using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SmartStore.Clases
{
    [DataContract]
    public class RecommendedItemSetInfoList
    {
        [DataMember]
        [JsonProperty("recommendedItems")]
        public IEnumerable<RecommendedItemSetInfo> RecommendedItemSetInfo { get; set; }
    }
}
