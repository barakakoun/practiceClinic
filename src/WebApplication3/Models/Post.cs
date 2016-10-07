using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class Post
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        [Display(Name = "Website")]
        public string SiteAddress { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Publish Date")]
        public DateTime PublishDate { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public string Video { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        
    }
}
