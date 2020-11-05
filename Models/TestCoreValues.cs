using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CentricTeam4.Models
{
    public class TestCoreValues
    {
        public int ID { get; set; }
        [Display(Name = "Core value recognized")]
        public CoreValue award { get; set; }
        [Display(Name = "ID of Person giving the recognition")]
        public Guid recognizor { get; set; }
        [Display(Name = "ID of Person receiving the recognition")]
        public Guid recognized { get; set; }
        [Display(Name = "Date recognition given")]
        public DateTime recognitionDate { get; set; }
        [Display(Name = "Phone number of person giving recognition")]
        public string phone { get; set; }
        [Display(Name = "Title of person giving recognition")]
        public string title { get; set; }
        [Display(Name = "Why should this person be receiving this award?")]
        public string description { get; set; }
        public enum CoreValue
        {
            Excellence = 1,
            Integrity = 2,
            Stewardship = 3,
            Culture = 4,
            Commitment = 5,
            Innovation = 6,
            Balance = 7,
        }
    }
}