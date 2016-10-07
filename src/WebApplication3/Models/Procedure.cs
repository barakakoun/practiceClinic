﻿using System;
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

        public int ProcedureTypeID { get; set; }

        public int PatientID { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Time { get; set; }


        public int Price { get; set; }

        [Display(Name = "Was Paid?")]
        public Boolean IsPaid { get; set; }


        [Display(Name = "Procedure Type")]
        public virtual ProcedureType ProcedureType { get; set; }


        [Display(Name = "Patient")]
        public virtual Patient Patient { get; set; }
    }
}
