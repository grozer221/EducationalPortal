﻿using EducationalPortal.Database.Abstractions;
using EducationalPortal.Database.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EducationalPortal.Database.Models
{
    public class UserModel : BaseModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public string Login { get; set; }
        public string? Email { get; set; }
        public bool IsEmailConfirmed { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public UserRoleEnum Role { get; set; }

        public Guid? GradeId { get; set; }
        public virtual GradeModel? Grade { get; set; }
        public virtual IEnumerable<SubjectModel>? Subjects { get; set; }
        //public virtual IEnumerable<PermisionTeacherEditSubjectModel> PermisionTeachersEditSubject { get; set; }
    }
}
