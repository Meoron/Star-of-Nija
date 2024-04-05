using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Sources.Common.Serialization {
    public sealed class BinarySerializationProvider : ISerializationProvider<byte[]> {
        public T Deserialize<T>(byte[] data) where T : class {
            if(data == null)
                return default(T);

            BinaryFormatter bf = new BinaryFormatter();
            using(MemoryStream ms = new MemoryStream(data))
            {
                object obj = bf.Deserialize(ms);
                return (T)obj;
            }
        }
        
        public byte[] Serialize(object data) {
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, data);
                return ms.ToArray();
            }
        }
    }
}