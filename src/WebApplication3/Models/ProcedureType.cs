using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models
{
    public enum Type
    {
        Medical,
        Cosmetic
    }
    public class ProcedureType
    {
        public int ID { get; set; }


        public String Name { get; set; }


        public Type Type { get; set; }


        public Boolean Anesthesia { get; set; }

        [Display(Name = "Recommanded price")]
        [DataType(DataType.Currency)]
        public int Price { get; set; }

        [DataType(DataType.Duration)]
        public int Duration { get; set; }


        public virtual ICollection<Procedure> Procedures { get; set; }
    }
}
