﻿using Infrastructure.Entities.Abstracts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrganisationManagement.Model
{
#nullable disable
    public class Faculty: IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public List<Department> Departments { get; set; } = new List<Department>();
        public List<Lecture> Lectures { get; set; } = new List<Lecture>();
        public List<Classroom> Classrooms { get; set; } = new List<Classroom>();

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedAt { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; }
    }
}