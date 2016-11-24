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
        public Patient()
        {
            MedicineAllergies = new List<Medicine_Patient>();
        }

        public int ID { get; set; }

        [Required]
        [Display(Name = "ID")]
        [StringLength(9, ErrorMessage = "ID is a 9 digits number", MinimumLength = 9)]
        public String Identifier { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public String FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public String LastName { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        public DateTime Birthday { get; set; }

        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Email address invalid")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(10, ErrorMessage = "Phone number must be 9-10 digits", MinimumLength = 9)]
        public string Phone { get; set; }
        
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Required")]
        [StringLength(100, ErrorMessage = "Password should be at least 6 letters", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Prucedures")]
        public virtual ICollection<Procedure> Prucedures { get; set; }

        public List<Medicine_Patient> MedicineAllergies { get; set; }

    }

    public class Medicine
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }


        public List<Medicine_Patient> PatientAllergic { get; set; }
    }

    public class Medicine_Patient
    {
        public int ID { get; set; }
        public int MedicineID { get; set; }
        public int PatientID { get; set; }
    }
}
