using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

namespace Sources.Common.Serialization.Extensions {
    public static class XMLNodeExtensions {
        public static T DeserializeNode<T>(this XmlNode node) where T : class{
            var serializer = new XmlSerializer(typeof(T));
            var reader = new StringReader(node.OuterXml);
            return serializer.Deserialize(reader) as T;
        }
        
        public static object DeserializeNode(this XmlNode node, Type type) {
            var serializer = new XmlSerializer(type);
            var reader = new StringReader(node.OuterXml);
            return serializer.Deserialize(reader);
        }

        public static T Deserialize<T>(string path) {
            var serializer = new XmlSerializer(typeof(T));
            var reader = new StreamReader(path);
            T deserialized = (T)serializer.Deserialize(reader.BaseStream);
            reader.Close();
            return deserialized;
        }
        
        public static float ParseToFloat(this XmlNode node) {
            return node != null ? float.Parse(node.InnerText) : 0f;
        }
        
        public static int ParseToInt(this XmlNode node) {
            return node != null ? int.Parse(node.InnerText) : 0;
        }
        
        public static string ParseToString(this XmlNode node) {
            return node != null ? node.InnerText : string.Empty;
        }

        public static bool ParseToBool(this XmlNode node) {
            var text = node != null ? node.InnerText : string.Empty;
            return !string.IsNullOrEmpty(text) && (text == "true" || text == "True");
        }
        
        public static T ParseToEnum<T>(this XmlNode node) where T : Enum {
            return (T)Enum.Parse(typeof(T), node.InnerText);
        }

        public static Vector3 ParseToVector3(this XmlNode node) {
            var vector = new Vector3();
            if (node == null) return vector;
            
            vector.x = node.SelectSingleNode("X").ParseToFloat();
            vector.y = node.SelectSingleNode("Y").ParseToFloat();
            vector.z = node.SelectSingleNode("Z").ParseToFloat();
            return vector;
        }

        public static Vector3 ParseToVector2(this XmlNode node) {
            var vector = new Vector2();
            if (node == null) return vector;

            vector.x = node.SelectSingleNode("X").ParseToFloat();
            vector.y = node.SelectSingleNode("Y").ParseToFloat();
            return vector;
        }

        /*public static Vector3 ParseToVector3(this Vector3DTO node) {
            return new Vector3(node.X, node.Y, node.Z);
        }*/
    }
}