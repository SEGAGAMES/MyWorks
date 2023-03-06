using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observer
{
    internal class Article
    {
        readonly public string Title;
        readonly public string Text;
        public Article(string title, string text)
        {
            Title = title;
            Text = text;
        }
    }
}
