using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AuthSystem.Models
{
    public class Article
    {   [Key]
        public int ArticleId { get; set; }
        [Column(TypeName ="nvarchar(100)")]
        [DisplayName("Tytuł")]  
        public string Title { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [DisplayName("Data")]
        public string Date { get; set; }


        [Column(TypeName = "varchar(MAX)")]
        [DisplayName("Treść")]
        public string Content { get; set; }
    }
}
