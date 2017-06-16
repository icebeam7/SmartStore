using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.ProjectOxford.Emotion;

namespace SmartStore.Servicios
{
    public class ServicioEmocion
    {
        static string key = "34acc186fbd349b6a9c51f2dd580e721";
        public static async Task<KeyValuePair<string, float>> GetEmotions(Stream stream)
        {
            EmotionServiceClient cliente = new EmotionServiceClient(key);
            var emotions = await cliente.RecognizeAsync(stream);
            if (emotions == null || emotions.Count() == 0)
                return default(KeyValuePair<string, float>);

            return emotions[0].Scores.ToRankedList().ToDictionary(x => x.Key, x => x.Value).First(); ;
        }
    }
}
