using System.Xml;
using Sources.Common.Serialization.Extensions;

namespace Sources.Common.Serialization {
    public sealed class XMLSerializationProvider : ISerializationProvider<XmlDocument> {
        public XmlDocument Serialize(object config) {
            return null;
        }

        public T Deserialize<T>(XmlDocument xmlDoc) where T : class {
            return xmlDoc.DeserializeNode<T>();
        }
    }
}