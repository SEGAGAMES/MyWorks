using Microsoft.VisualBasic.Logging;
using System.Security.Policy;
using System.Text;

namespace ВолкиП
{
    public partial class Form1 : Form
    {
        Game game;
        delegate string check(Wolf w);
        delegate string check1(Hunter hunter);
        event check Check;
        event check1 Check1;
        public Form1()
        {
            InitializeComponent();
            foreach (Button b in groupBox1.Controls)
            {
                b.Text = string.Empty;
            }
            timer1.Interval = 500;
            timer1.Start();
            game = new Game(4, groupBox1);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            game.Generate();
            log.Text += StartEvent().ToString();
            Stop();
        }
        void Stop()
        {
            foreach (Button b in groupBox1.Controls)
            {
                if (b.Text == "W")
                    return;
            }
            log.Text += ("Игра окончена!");
            timer1.Stop();
        }
        StringBuilder StartEvent()
        {
            Check = null;
            Check1 = null;
            StringBuilder builder = new StringBuilder();
            check[] checkWolfs = new check[game.animals.Count - 2];
            int count = 0;
            foreach (Animal wolf in game.animals)
            {
                if (wolf.GetType() == typeof(Wolf))
                {
                    checkWolfs[count] = new check(game.CheckW);
                    count++;
                }
            }
            check1[] checkHunters = new check1[2];
            int countH = 0;
            foreach (Animal hunter in game.animals)
            {
                if (hunter.GetType() == typeof(Hunter))
                {
                    checkHunters[countH] = new check1(game.Check);
                    countH++;
                }
            }
            foreach (check ch in checkWolfs)
                Check += ch;
            for (int i = 0; i < 2; i++)
            {
                Check1 += checkHunters[i];
            }
            foreach (Animal h in game.animals)
                if (h.GetType() == typeof(Hunter))
                {
                    if (Check1((Hunter)h) != null)
                    {
                        builder.Append(Check1((Hunter)h));
                        builder.Append("\n");
                    }
                }
            foreach (Animal h in game.animals)
                if (h.GetType() == typeof(Wolf))
                {
                    if (Check((Wolf)h) != null)
                    {
                        builder.Append(Check((Wolf)h));
                        builder.Append("\n");
                    }
                }
            return builder;
        }
    }
}