using Newtonsoft.Json;

namespace Sources.Common.Serialization {
    public sealed class JsonSerializationProvider : ISerializationProvider<string> {
        public string Serialize(object config) {
            return JsonConvert.SerializeObject(config);
        }

        public T Deserialize<T>(string config) where T : class {
            return JsonConvert.DeserializeObject<T>(config);
        }
    }
}