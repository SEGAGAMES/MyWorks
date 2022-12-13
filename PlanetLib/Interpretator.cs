using System.Data;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;

namespace PlanetLib
{
    public class Interpretator
    {
        static List<Planet> ps = new List<Planet>()
        {
            new Planet(1, "Венера", 4.87), new Planet(2, "Меркурий", 12.1), new Planet(3, "Земля", 12.756),
            new Planet(4, "Марс", 6.67), new Planet(5, "Юпитер", 143.76), new Planet(6, "Сатурн", 120.42),
            new Planet(7, "Уран", 51.3), new  Planet(8, "Нептун", 49.5),
        };
        static Planets planets = new Planets(ps);

        StringBuilder answer = new StringBuilder();

        static List<User> us = new List<User>()
        {
            new User(1111, "admin"), new User(1234, "user"),
        };
        User checkUser = null;
        Users users = new Users(us);

        public string Execute(string[] command)
        {
            //try { SerializeXML("file.xml", users); }
            //catch { return "Error"; }
            //users = DeserializeXMLUser("file.xml");
            answer.Clear();
            switch (command[0])
            {
                case "SaveXML":
                    if (checkUser.Login == "admin")
                    {
                        SerializeXML("f.xml", planets);
                        return "Успех!";
                    }
                    return "Вы должны быть администратором!";
                case "LoadXML":
                    if (checkUser.Login == "admin")
                    {
                        SerializeXML("f1.xml", planets);
                        planets.planetlist.Clear();
                        planets = DeserializeXML("f.xml");
                        foreach (Planet p1 in planets.planetlist)
                        {
                            answer.Append(p1.Info());
                            answer.Append("\n");
                        }
                        return answer.ToString();
                    }
                    return "Вы должны быть администратором!";
                case "SaveJSON":
                    if (checkUser.Login == "admin")
                    {
                        SerializeJSON("f.json", planets);
                        return "Успех!";
                    }
                    return "Вы должны быть администратором!";
                case "LoadJSON":
                    if (checkUser.Login == "admin")
                    {
                        SerializeJSON("f1.json", planets);
                        planets.planetlist.Clear();
                        planets = DeserializeJSON("f.json");
                        foreach (Planet p1 in planets.planetlist)
                        {
                            answer.Append(p1.Info());
                            answer.Append("\n");
                        }
                        return answer.ToString();
                    }
                    return "Вы должны быть администратором!";
                case "Info":
                    {
                        int numb;

                        if (command.Length < 2)
                        {
                            return ("Ошибка!");
                        }
                        if (!int.TryParse(command[1], out numb) || numb < 1 || numb > 8)
                        {
                            return ("Ошибка!");
                        }
                        return (planets.planetlist[numb - 1].Info());
                    }
                case "Div":
                    {
                        int numb1;
                        int numb2;
                        if (!int.TryParse(command[1], out numb1) || !int.TryParse(command[2], out numb2) || numb1 < 1 || numb1 > 8 || numb2 < 1 || numb2 > 8)
                        {
                            return "Ошибка!";
                        };
                        return Planet.Masses(planets.planetlist[numb1 - 1], planets.planetlist[numb2 - 1]);
                    }
                case "AllInfo":
                    {
                        foreach (Planet planet in planets.planetlist)
                        {
                            answer.Append(planet.Info());
                            answer.Append("\n");
                        }
                        return answer.ToString();
                    }
                case "Help":
                    {
                        answer.Append("\nВсе команды:");
                        answer.Append("\nExit - выйти");
                        answer.Append("\nDiv arg1 arg2 - отношение диаметра планета arg 1 к планете arg2");
                        answer.Append("\nIndo arg - информация о планете arg(arg - номер планеты в Солнечной системе)");
                        answer.Append("\nAllInfo - информация о всех планетах");
                        answer.Append("\nAdd arg1 arg2 - добавить планету с названием arg1 и диаметром arg2");
                        return answer.ToString();
                    }
                case "Add":
                    {
                        double diam;
                        if (command.Length != 3 || !double.TryParse(command[2], out diam) || diam < 0)
                        {
                            return "Ошибка!";
                        }
                        planets.planetlist.Add(new Planet(planets.planetlist[planets.planetlist.Count - 1].Number + 1, command[1], diam));
                        return ($"Планета {command[1]} успешно добавлена");
                    }
                case "Del":
                    {
                        int numb;
                        if (!int.TryParse(command[1], out numb) || numb < 0)
                        {
                            return "Ошибка!";
                        }
                        for (int i = 0; i < planets.planetlist.Count; i++)
                        {
                            if (planets.planetlist[i].Number == numb) planets.planetlist.RemoveAt(i);
                        }
                        return ($"Планета успешно удалена");
                    }
                default:
                    {
                        return ("Ошибка!");
                    }
            }
        }
        public string ID(string[] command)
        {
            if (checkUser == null && command[0] == "login" && command.Length >2)
            {
                int pin;
                if (command[0] == "login" && command.Length > 2 && int.TryParse(command[2], out pin))
                {
                    foreach (User u in users.users)
                    {
                        if (u.Login == command[1] && u.Pin == pin)
                        {
                            checkUser = new User(pin, command[1]);
                            return "Вы успешно вошли.";
                        }
                    }
                    return $"Неправильный логин или пароль! {command[0]} {command[1]} {command[2]}";
                }
                else return $"Неправильный формат данных";
            }
            else return "Пройдите аутентификацию \nlogin [логин] [пароль]";
        }
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
        static void SerializeXML(string path, Users u)
        {
            XmlSerializer xml = new XmlSerializer(typeof(Users));
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                xml.Serialize(fs, u);
            }
        }
        static Users DeserializeXMLUser(string path)
        {
            XmlSerializer xml = new XmlSerializer(typeof(Users));
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                return (Users)xml.Deserialize(fs);
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
    }
}