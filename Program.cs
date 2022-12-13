using System;
using Lib1;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Xml.Linq;

namespace ConsoleApp1
{
    internal class Program
    {
        static void SerializeXML(string path, Planets p)
        {
            XmlSerializer xml = new XmlSerializer(typeof(Planets));
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                xml.Serialize(fs, p);
            }
        }
        static Planets DeserializeXML(string path)
        {
            XmlSerializer xml = new XmlSerializer(typeof(Planets));
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                return (Planets)xml.Deserialize(fs);
            }
        }

        static void SerializeJSON(string path, Planets p)
        {
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                JsonSerializer.SerializeAsync(fs, p);
            }
        }
        static Planets DeserializeJSON(string path)
        {
            using (StreamReader fs = new StreamReader(path))
            {
                Planets ps = JsonSerializer.Deserialize<Planets>(fs.ReadToEnd());
                return ps;
            }
        }
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            string[] command;

            Planets p = new Planets();
            p.planetlist.Add(new Planet(1, "Венера", 4.87));
            p.planetlist.Add(new Planet(2, "Меркурий", 12.1));
            p.planetlist.Add(new Planet(3, "Земля", 12.756));
            p.planetlist.Add(new Planet(4, "Марс", 6.67));
            p.planetlist.Add(new Planet(5, "Юпитер", 143.76));
            p.planetlist.Add(new Planet(6, "Сатурн", 120.42));
            p.planetlist.Add(new Planet(7, "Уран", 51.3));
            p.planetlist.Add(new Planet(8, "Нептун", 49.5));
            foreach (Planet pl in p.planetlist) pl.Info();
            static void Error()
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Команда введена неверно");
                Console.WriteLine("Повторите попытку!");
                Console.ForegroundColor = ConsoleColor.White;
            }
            while (true)
            {
                m:
                Console.Write(">");
                command = Console.ReadLine().Split();
                switch (command[0])
                {
                    case "SaveXML":
                        SerializeXML("f.xml", p);
                        break;
                    case "LoadXML":
                        p.planetlist.Clear();
                        p = DeserializeXML("f.xml");
                        foreach (Planet p1 in p.planetlist) p1.Info();
                        break;
                    case "SaveJSON":
                        SerializeJSON("f.json", p);
                        break;
                    case "LoadJSON":
                        p.planetlist.Clear();
                        p = DeserializeJSON("f.json");
                        foreach (Planet p1 in p.planetlist) p1.Info();
                        break;
                    case "Exit": Environment.Exit(0); break;
                    case "Info":
                        {
                            int numb;

                            if (command.Length < 2)
                            {
                                Error();
                                goto m;
                            }
                            if (!int.TryParse(command[1], out numb) || numb< 1 || numb > 8)
                            {
                                Error();
                                goto m;
                            }

                            p.planetlist[numb-1].Info();
                            break;
                        }
                    case "Div":
                        {
                            int numb1;
                            int numb2;

                            if (!int.TryParse(command[1], out numb1) || !int.TryParse(command[2], out numb2) || numb1 < 1 || numb1 > 8 || numb2 < 1 || numb2 > 8)
                            {
                                Error();
                                goto m;
                            };

                            Planet.Masses(p.planetlist[numb1-1], p.planetlist[numb2-1]);
                            break;
                        }
                    case "AllInfo":
                        {
                            foreach (Planet planet in p.planetlist) planet.Info();
                            break;
                        }
                    case "Help":
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\nВсе команды:");
                            Console.WriteLine("Exit - выйти");
                            Console.WriteLine("Div arg1 arg2 - отношение диаметра планета arg 1 к планете arg2");
                            Console.WriteLine("Indo arg - информация о планете arg(arg - номер планеты в Солнечной системе)");
                            Console.WriteLine("AllInfo - информация о всех планетах");
                            Console.WriteLine("Add arg1 arg2 - добавить планету с названием arg1 и диаметром arg2\n");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        }
                    case "Add":
                        {
                            double diam;
                            if (command.Length!= 3 || !double.TryParse(command[2], out diam)
                                || diam < 0) { Error(); goto m; }
                            p.planetlist.Add(new Planet(p.planetlist[p.planetlist.Count-1].Number+1, command[1], diam));
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Планета {command[1]} успешно добавлена");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        }
                    case "Del":
                        {
                            int numb;
                            if (!int.TryParse(command[1], out numb)|| numb < 0) { Error(); goto m; }
                            for (int i = 0; i <p.planetlist.Count; i++)
                            {
                                if (p.planetlist[i].Number == numb) p.planetlist.RemoveAt(i);
                            }
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Планета успешно удалена");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        }
                   default:
                        {
                            Error();
                            goto m;
                        }
                }
            } 
        }
    }
}
