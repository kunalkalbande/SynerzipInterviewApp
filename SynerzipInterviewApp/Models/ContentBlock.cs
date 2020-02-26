using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SynerzipInterviewApp.Models
{
    public class ContentBlock
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ContentBlockId { get; set; }
        public string Name { get; set; }
        public string Query { get; set; }
        public string VisualizationType { get; set; }
    }
}
