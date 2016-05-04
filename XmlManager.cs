using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;
using System.IO;

namespace Breakout
{
    public class XmlManager <T>
    {
        public Type Type;
        public XmlManager()
            {
                Type = typeof(T);
            }
        public T Load(string path, object obj) 
        {
            T instance;

            using (StreamReader streamreader = new StreamReader(path)) 
            {
                XmlSerializer xmlSerializer = new XmlSerializer(Type);
                instance = (T)xmlSerializer.Deserialize(streamreader);
            }

            return instance;
        }
    }
}
