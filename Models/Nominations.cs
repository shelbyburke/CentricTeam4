using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CentricTeam4.Models
{
    public class Nominations
    {
        [Key]
        public int recId { get; set; }
        public Guid userId { get; set; }
        public DateTime dateOfRecognition { get; set; }
        [Display(Name = "Date Recognized")]
        public string comments { get; set; }
        [Display(Name = "Comment")]
        public int award { get; set; }
        [Display(Name = "Award")]
        public int UserProfile_userId { get; set; }
        [ForeignKey("userId")]
        public virtual userData Awardee { get; set; }
    }
}