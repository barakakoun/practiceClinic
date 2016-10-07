using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models
{
    public class Patient
    {
        public int ID { get; set; }
        
        [Display(Name = "First Name")]
        public String FirstName { get; set; }

        [Display(Name = "Last Name")]
        public String LastName { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
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

        [Display(Name = "Drug Allergies")]
        public virtual ICollection<Drug> DrugAllergies { get; set; }

        [Display(Name = "Prucedures")]
        public virtual ICollection<Procedure> Prucedures { get; set; }

    }
}
