using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models.DomainModels
{
    public class Comment
    {
        public Comment(DateTime time, string text)
        {
            Time = time;
            Text = text;
        }

        public DateTime Time { get; set; }
        public string Text { get; set; }
    }
}
