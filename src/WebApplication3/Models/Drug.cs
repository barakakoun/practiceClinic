using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models;

namespace WebApplication3.Models
{
    public class Drug
    {
        //public Drug()
        //{
        //    this.PatientDrugs = new HashSet<PatientDrug>();
        //}

        public int ID { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }

        //[Display(Name = "Allergic Patients")]
        //public virtual ICollection<Patient> Patients { get; set; }
        //public virtual ICollection<PatientDrug> PatientDrugs { get; set; }
    }
}
