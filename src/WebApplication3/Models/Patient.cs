using Microsoft.AspNet.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models
{
    public enum CommonDrugs
    {
        Moxypen = 1,
        Acamol = 2,
        Narocin = 3,
        Parodontax = 4
    }
    public class Patient
    {
        //public Patient()
        //{
        //    this.PatientDrugs = new HashSet<PatientDrug>();
        //}

        public Patient()
        {
            MedicineAllergies = new List<Medicine>();
        }

        public int ID { get; set; }
        
        [Display(Name = "First Name")]
        public String FirstName { get; set; }

        [Display(Name = "Last Name")]
        public String LastName { get; set; }

        [DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        public DateTime Birthday { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Email address invalid")]
        public string Email { get; set; }

        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(10, ErrorMessage = "ID must be 9 digits number", MinimumLength = 10)]
        public string Phone { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Required")]
        [StringLength(100, ErrorMessage = "Password should be at least 6 letters", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Prucedures")]
        public virtual ICollection<Procedure> Prucedures { get; set; }

        public List<Medicine> MedicineAllergies { get; set; }

    }

    public class Medicine
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
