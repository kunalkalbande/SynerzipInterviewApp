using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SynerzipInterviewApp.Models
{
    public class Interview
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long InterviewId { get; set; }
        public string CandidateName { get; set; }
        public string ContactPerson { get; set; }
        public DateTime? dateofInterview { get; set; }
        public string MobileNo { get; set; }
    }
}
