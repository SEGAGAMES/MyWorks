using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ВолкиП
{
    internal class Game
    {
        public List<Animal> animals = new List<Animal>();
        Random random = new Random();
        const int dx = 69;
        const int dy = 61;
        const int xMax = 620;
        const int xMin = 135;
        const int yMin = 99;
        const int yMax = 528;
        GroupBox groupBox1;
        Button? Spawn()
        {
            int count = 0;
            int check = random.Next(0, groupBox1.Controls.Count);
            foreach (Button b in groupBox1.Controls)
            {
                if (count == check)
                    return b;
                count++;
            }
            return null;
        }
        public Game(int count, GroupBox gp)
        {
            groupBox1 = gp;
            for (int i = 0; i < count; i++)
            {
                animals.Add(new Wolf(Spawn()));
            }
            animals.Add(new Hunter(Spawn()));
            animals.Add(new Hunter(Spawn()));
        }
        public Button FindButton(int x, int y)
        {
            foreach (Button b in groupBox1.Controls)
            {
                if (b.Location.X == x && b.Location.Y == y) return b;
            }
            return null;
        }
        public void Generate()
        {
            for (int i = 0; i < animals.Count; i++)
            {
                if (animals[i].Position == null) continue;
            mbox:
                int dir = random.Next(0, 4);
                switch (dir)
                {
                    case 0:
                        if (animals[i].Position.Location.X + dx < xMax)
                            animals[i].Move(FindButton(animals[i].Position.Location.X + dx, animals[i].Position.Location.Y));
                        else
                        {
                            if (animals[i].GetType() == typeof(Wolf))
                            {
                                ((Wolf)animals[i]).Life = false;
                            }
                            goto mbox;
                        }
                        break;
                    case 1:
                        if (animals[i].Position.Location.X - dx > xMin)
                            animals[i].Move(FindButton(animals[i].Position.Location.X - dx, animals[i].Position.Location.Y));
                        else
                        {
                            if (animals[i].GetType() == typeof(Wolf))
                            {
                                ((Wolf)animals[i]).Life = false;
                            }
                            goto mbox;
                        }
                        break;
                    case 2:
                        if (animals[i].Position.Location.Y - dy > yMin)
                            animals[i].Move(FindButton(animals[i].Position.Location.X, animals[i].Position.Location.Y - dy));
                        else
                        {
                            if (animals[i].GetType() == typeof(Wolf))
                            {
                                ((Wolf)animals[i]).Life = false;
                            }
                            goto mbox;
                        }
                        break;
                    case 3:
                        if (animals[i].Position.Location.Y + dy < yMax)
                            animals[i].Move(FindButton(animals[i].Position.Location.X, animals[i].Position.Location.Y + dy));
                        else
                        {
                            if (animals[i].GetType() == typeof(Wolf))
                            {
                                ((Wolf)animals[i]).Life = false;
                            }
                            goto mbox;
                        }
                        break;
                }
            }
        }
        public string Check(Hunter hunter)
        {
            foreach (Animal wolf in animals)
            {
                if (wolf.GetType() == typeof(Wolf))
                {

                    if (hunter.Position == wolf.Position)
                    {
                        //if (((Wolf)wolf).Life == true)
                        //{ 
                            ((Wolf)wolf).Life = false;
                            return "Волк пойман!"; 
                       // }
                    }
                }
            }
            return null;
        }
        public string CheckW(Wolf wolf)
        {
            for (int i = 0; i < animals.Count; i++)
            {
                if (animals[i] != wolf)
                    if (animals[i].GetType() == wolf.GetType() && animals[i].Position == wolf.Position && animals[i].Position.Text != string.Empty)
                        return $"Два волка встретились в {animals[i].Position.Location}";
            }
            return null;
        }
    }
}
