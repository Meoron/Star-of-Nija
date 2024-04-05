namespace Sources.Common.Serialization {
    public interface ISerializationProvider<L> {
        L Serialize(object config);
        T Deserialize<T>(L input) where T : class;
    }
    
    public sealed class Serialization {
        public static T Deserialize<T, L, K>(K input) where L : ISerializationProvider<K>, new() where T : class {
            var provider = new L();
            return provider.Deserialize<T>(input);
        }
        
        public static object Deserialize<L, K>(K input) where L : ISerializationProvider<K>, new() {
            return Deserialize<object, L, K>(input);
        }

        public static K Serialize<L, K>(object config) where L : ISerializationProvider<K>, new() {
            var provider = new L();
            return provider.Serialize(config);
        }
    }
}