using System;
using System.Xml.Serialization;
using System.IO;

namespace TutorialRPG
{
    public class XmlManager<T>
    {
        /// <summary>
        /// Type to be desirialized. Only set 
        /// If loading a class derieved from T
        /// </summary>
        public Type Type { get; set; }

        public XmlManager()
        {
            Type = typeof(T);
        }

        public T Load(string path)
        {
            T instance;
            using (TextReader reader = new StreamReader(path))
            {
                XmlSerializer xml = new XmlSerializer(Type);
                instance = (T)xml.Deserialize(reader);
            }

            return instance;
        }

        public void Save(string path, object obj)
        {
            using (TextWriter writer = new StreamWriter(path))
            {
                XmlSerializer xml = new XmlSerializer(Type);
                xml.Serialize(writer, obj);
            }
        }
    }
}
