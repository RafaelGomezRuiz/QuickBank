using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using QuickBank.Core.Application.Interfaces.Helpers;

namespace QuickBank.Core.Application.Helpers
{
    public class JsonHelper : IJsonHelper
    {
        public string Serialize<T>(T value)
        {
            var settings = new JsonSerializerSettings {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            return JsonConvert.SerializeObject(value, settings);
        }

        public T? Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json ?? string.Empty);
        }
    }
}
