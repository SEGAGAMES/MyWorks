using PlanetLib;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdditionalLib
{
    internal class CommandBot
    {
        public CommandBot( Planets ps)
        {
            this.ps = ps;
        }

        Planets ps { get; set; }


        public string SaveXML(User checkUser)
        {
            if (checkUser.Login == "admin")
            {
                Serializers.SerializeXML("f.xml", ps);
                return "Успех";
            }
            return "Вы должны быть администратором!";
        }
        public string LoadXML(User checkUser, int count, out int cnt, out Planets pas)
        {
            if (checkUser.Login == "admin")
            {
                StringBuilder answer = new StringBuilder();
                if (count < 3)
                {
                    Serializers.SerializeXML($"save{count++}.xml", ps);
                }
                else
                {
                    count = 0;
                    Serializers.SerializeXML($"save{count++}.xml", ps);
                }
                ps.planetlist.Clear();
                ps = Serializers.DeserializeXML("f.xml");
                foreach (Planet p1 in ps.planetlist)
                {
                    answer.Append(p1.Info());
                    answer.Append("\n");
                }
                cnt = count;
                pas = ps;
                return answer.ToString();
            }
            cnt = count;
            pas = ps;
            return "Вы должны быть администратором!";
        }
        public string SaveJSON(User checkUser)
        {
            if (checkUser.Login == "admin")
            {
                Serializers.SerializeJSON("f.json", ps);
                return "Успех!";
            }
            return "Вы должны быть администратором!";
        }
        public string LoadJSON(User checkUser, int count, out int cnt, out Planets pas)
        {
            if (checkUser.Login == "admin")
            {
                StringBuilder answer = new StringBuilder();
                if (count < 3)
                {
                    Serializers.SerializeJSON($"save{count++}.json", ps);
                }
                else
                {
                    count = 0;
                    Serializers.SerializeJSON($"save{count++}.json", ps);
                }
                ps.planetlist.Clear();
                ps = Serializers.DeserializeJSON("f.json");
                foreach (Planet p1 in ps.planetlist)
                {
                    answer.Append(p1.Info());
                    answer.Append("\n");
                }
                cnt = count;
                pas = ps;
                return answer.ToString();
            }
            cnt = count;
            pas = ps;
            return "Вы должны быть администратором!";
        }
        public string Info(string[] command)
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
            return (ps.planetlist[numb - 1].Info());
        }
        public string Div(string[] command)
        {
            int numb1;
            int numb2;
            if (!int.TryParse(command[1], out numb1) || !int.TryParse(command[2], out numb2) || numb1 < 1 || numb1 > 8 || numb2 < 1 || numb2 > 8)
            {
                return "Ошибка!";
            };
            return Planet.Masses(ps.planetlist[numb1 - 1], ps.planetlist[numb2 - 1]);
        }
        public string AllInfo()
        {
            StringBuilder answer = new StringBuilder();
            foreach (Planet planet in ps.planetlist)
            {
                answer.Append(planet.Info());
                answer.Append("\n");
            }
            return answer.ToString();
        }
        public string Help()
        {
            StringBuilder answer = new StringBuilder();
            answer.Append("\nВсе команды:");
            answer.Append("\nend - выйти");
            answer.Append("\nDiv arg1 arg2 - отношение диаметра планета arg 1 к планете arg2");
            answer.Append("\nIndo arg - информация о планете arg(arg - номер планеты в Солнечной системе)");
            answer.Append("\nAllInfo - информация о всех планетах");
            answer.Append("\nAdd arg1 arg2 - добавить планету с названием arg1 и диаметром arg2");
            answer.Append("\nSaveXML - сохраняет базу в XML файл");
            answer.Append("\nLoadXML - загружает базу из XML файла");
            return answer.ToString();
        }
        public string Add(string[] command, out Planets pas)
        {
            double diam;
            if (command.Length != 3 || !double.TryParse(command[2], out diam) || diam < 0)
            {
                pas = ps;
                return "Ошибка!";
            }
            ps.planetlist.Add(new Planet(ps.planetlist[ps.planetlist.Count - 1].Number + 1, command[1], diam));
            pas = ps;
            return ($"Планета {command[1]} успешно добавлена");
        }
        public string Del(string[] command, out Planets pas)
        {
            int numb;
            if (!int.TryParse(command[1], out numb) || numb < 0)
            {
                pas = ps;
                return "Ошибка!";
            }
            for (int i = 0; i < ps.planetlist.Count; i++)
            {
                if (ps.planetlist[i].Number == numb) ps.planetlist.RemoveAt(i);
            }
            pas = ps;
            return ($"Планета успешно удалена");
        }
    }
}
