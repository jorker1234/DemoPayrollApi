using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayrollApi.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required, Column(TypeName="nvarchar(100)")]
        public string Firstname { get; set; }

        [Required, Column(TypeName = "nvarchar(100)")]
        public string Lastname { get; set; }

        public ICollection<Subject> Subjects { get; set; }

        public Student()
        {
            Subjects = new HashSet<Subject>();
        }
    }
}
