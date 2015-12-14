using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Logic
{
    public static class Serializer
    {
        private static readonly string FileName = "EmploymentCenter.xml";
        private static FileStream _fileStream;
        private static XmlSerializer _serializer;

        static Serializer()
        {
            _fileStream = new FileStream(FileName, FileMode.OpenOrCreate);
        }

        public static bool SerializeAll<T>(params T[] serializableObject)
        {
            _serializer = new XmlSerializer(typeof(T[]));
            try
            {
                _serializer.Serialize(_fileStream, serializableObject);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
           
        }

        public static object DeserializeAll()
        {
            _serializer = new XmlSerializer(typeof(object));
            return _serializer.Deserialize(_fileStream);
        }


    }
}
