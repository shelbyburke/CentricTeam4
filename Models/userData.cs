using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CentricTeam4.Models
{
    public class userData
    {
        [Required] 
        public Guid ID { get; set; } 

        [Required]
        [EmailAddress(ErrorMessage = "Enter your most frequently used email address")]
        [Display(Name = "Email")]
        public string Email { get; set; }


        [Required] 
        [Display(Name = "Last Name")]
        public string lastName { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string firstName { get; set; }

        [Display(Name = "Primary Phone")]
        [Phone]
        [RegularExpression(@"^(\(\d{3}\) |\d{3}-)\d{3}-\d{4}$",
            ErrorMessage = "Phone numbers must be in the format (xxx) xxx-xxxx or xxx-xxx-xxxx")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Current Position")]
        public string Position { get; set; }
        public LocationList Location { get; set; }

        [Display(Name = "Current Business Unit")]
        public string BusinessUnit { get; set; }

        [Display(Name = "Hire Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime hireDate { get; set; }

        [Display(Name = "Bio")]
        public string bio { get; set; }



        public enum LocationList
        {
            Boston = 1,
            Charlotte = 2,
            Chicago = 3,
            Cincinnati = 4,
            Cleveland = 5,
            Columbus = 6,
            India = 7,
            Indianapolis = 8,
            Louisville = 9,
            Miami = 10,
            Seattle = 11,
            StLouis = 12,
            Tampa = 13,
       
        }


    }
}