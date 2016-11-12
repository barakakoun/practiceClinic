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

        [Required]
        public String Name { get; set; }
        

        public Type Type { get; set; }


        public Boolean Anesthesia { get; set; }

        [Required]
        [Display(Name = "Recommanded price")]
        [DataType(DataType.Currency)]
        [Range(1, Int32.MaxValue, ErrorMessage = "Price is a positive number")]
        public int Price { get; set; }

        [Required]
        [DataType(DataType.Duration)]
        [Range(1, Int32.MaxValue, ErrorMessage = "Price is a positive number")]
        public int Duration { get; set; }


        public virtual ICollection<Procedure> Procedures { get; set; }
    }

    
}
