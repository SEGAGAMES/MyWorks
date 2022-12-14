using PlanetLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AdditionalLib
{
    static public class Serializers
    {
        public static void SerializeXML(string path, Planets p)
        {
            XmlSerializer xml = new XmlSerializer(typeof(Planets));
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                xml.Serialize(fs, p);
            }
        }
       public static Planets DeserializeXML(string path)
        {
            XmlSerializer xml = new XmlSerializer(typeof(Planets));
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                return (Planets)xml.Deserialize(fs);
            }
        }
       public static void SerializeXML(string path, Users u)
        {
            XmlSerializer xml = new XmlSerializer(typeof(Users));
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                xml.Serialize(fs, u);
            }
        }
       public  static Users DeserializeXMLUser(string path)
        {
            XmlSerializer xml = new XmlSerializer(typeof(Users));
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                return (Users)xml.Deserialize(fs);
            }
        }
        public static void SerializeJSON(string path, Planets p)
        {
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                JsonSerializer.SerializeAsync(fs, p);
            }
        }
        public static Planets DeserializeJSON(string path)
        {
            using (StreamReader fs = new StreamReader(path))
            {
                Planets ps = JsonSerializer.Deserialize<Planets>(fs.ReadToEnd());
                return ps;
            }
        }
    }
}
