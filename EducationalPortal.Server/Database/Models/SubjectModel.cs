using EducationalPortal.Server.Database.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPortal.Server.Database.Models
{
    public class SubjectModel : BaseModel
    {
        public string Name { get; set; }
        public string? Link { get; set; }
        public virtual IEnumerable<SubjectPostModel>? Posts { get; set; }
        public virtual List<GradeModel>? GradesHaveAccessRead { get; set; }
        [NotMapped]
        public virtual IEnumerable<Guid>? GradesHaveAccessReadIds { get; set; }
        public virtual IEnumerable<UserModel>? TeachersHaveAccessCreatePosts { get; set; }
        public Guid? TeacherId { get; set; }
        public virtual UserModel? Teacher { get; set; }
        public Guid? EducationalYearId { get; set; }
        public virtual EducationalYearModel? EducationalYear { get; set; }
    }
}
