using AdditionalLib;
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
        CommandBot bot = new CommandBot(planets);
        static int count = 0;

        public string Execute(string[] command)
        {
            users = Serializers.DeserializeXMLUser("users.xml");
            answer.Clear();
            switch (command[0])
            {
                case "SaveXML":
                    return bot.SaveXML(checkUser);
                case "LoadXML":
                    return bot.LoadXML(checkUser, count, out count, out planets);
                case "SaveJSON":
                    return bot.SaveJSON(checkUser);
                case "LoadJSON":
                    return bot.LoadJSON(checkUser, count, out count, out planets);
                case "Info":
                    return bot.Info(command);
                case "Div":
                    return bot.Div(command);
                case "AllInfo":
                    return bot.AllInfo();
                case "Help":
                    return bot.Help();
                case "Add":
                    return bot.Add(command, out planets);
                case "Del":
                    return bot.Del(command, out planets);
                default:
                    {
                        return ("Ошибка!");
                    }
            }
        }
        public string ID(string[] command)
        {
            if (checkUser == null && command[0] == "login" && command.Length > 2)
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
    }
}