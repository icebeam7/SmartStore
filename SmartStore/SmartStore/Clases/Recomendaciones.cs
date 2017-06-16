using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace SmartStore.Clases
{
    public class Recomendaciones
    {
        const string RecomendacionesKey = "f96a4e0e5e3f47d1b8dbc56c307453fd";
        const string BaseUri = "https://westus.api.cognitive.microsoft.com/recommendations/v4.0";
        HttpClient _httpClient;
        const string idModelo = "a3cfb4d6-49e4-4f70-a13d-ec58058dbf81";
        const long buildId = 1577305;

        public Recomendaciones()
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(BaseUri),
                Timeout = TimeSpan.FromMinutes(5),
                DefaultRequestHeaders =
                {
                    {"Ocp-Apim-Subscription-Key", RecomendacionesKey }
                }
            };
        }

        public async Task<RecommendedItemSetInfoList> ObtenerRecomendaciones(string itemIds, int numberOfResults)
        {
            string uri = $"{BaseUri}/models/{idModelo}/recommend/item?itemIds={itemIds}&numberOfResults={numberOfResults}&minimalScore=0";
            var response = await _httpClient.GetAsync(uri);

            var jsonString = await response.Content.ReadAsStringAsync();
            var recommendedItemSetInfoList = JsonConvert.DeserializeObject<RecommendedItemSetInfoList>(jsonString);
            return recommendedItemSetInfoList;
        }
    }
}