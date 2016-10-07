using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models
{
    public class Comment
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string SiteAddress { get; set; }
        public string Content { get; set; }

        public int PostID { get; set; }
        public virtual Post Post { get; set; }
    }
}
