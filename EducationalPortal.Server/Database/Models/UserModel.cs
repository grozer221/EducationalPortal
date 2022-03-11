using EducationalPortal.Server.Database.Abstractions;
using EducationalPortal.Server.Database.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EducationalPortal.Server.Database.Models
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
        public virtual List<SubjectModel>? Subjects { get; set; }
        public virtual List<SubjectModel>? SubjectHaveAccessCreatePosts { get; set; }
    }
}
