using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayrollApi.Models
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }

        [Required, Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        [Required, Column(TypeName ="tinyint")]
        public int Unit { get; set; }

        [Required, Column(TypeName = "decimal(6,2)")]
        public double Price { get; set; }

        public ICollection<Student> Students { get; set; }

        public Subject()
        {
            Students = new HashSet<Student>();
        }
    }
}
