using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models
{
    public class Procedure
    {
        public int ID { get; set; }
        
        [Display(Name = "Procedure Type")]
        public int ProcedureTypeID { get; set; }
        
        [Display(Name = "Patient")]
        public int PatientID { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime Time { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Range(1, Int32.MaxValue, ErrorMessage = "Price is a positive number")]
        public int Price { get; set; }

        [Display(Name = "Was Paid?")]
        public Boolean IsPaid { get; set; }


        [Display(Name = "Procedure Type")]
        public virtual ProcedureType ProcedureType { get; set; }


        [Display(Name = "Patient")]
        public virtual Patient Patient { get; set; }
    }
}
