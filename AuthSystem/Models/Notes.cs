using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AuthSystem.Models
{
    public class Notes
    {
        [Key]
        public int NoteId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string FirstName { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string LastName { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Date { get; set; }
        [Column(TypeName = "nvarchar(500)")]
        public string Note { get; set; }
    }
}
