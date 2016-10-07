using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models
{
    public class Supplier
    {
        public int ID { get; set; }

        public String Description { get; set; }
        public String City { get; set; }
        public String Street { get; set; }
        public int HouseNumber { get; set; }


        [Display(Name = "X Coordination")]
        public double CoorX { get; set; }

        [Display(Name = "Y Coordination")]
        public double CoorY { get; set; }
    }
}
