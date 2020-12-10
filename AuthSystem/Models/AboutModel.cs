using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AuthSystem.Models
{
    public class AboutModel
    {
        [Key]
        public int AboutId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Title { get; set; }

        [Column(TypeName = "nvarchar(1000)")]
        [DisplayName("Name")]
        public string AboutContent { get; set; }
    }
}
