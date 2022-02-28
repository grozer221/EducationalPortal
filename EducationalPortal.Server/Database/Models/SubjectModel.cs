using EducationalPortal.Database.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPortal.Database.Models
{
    public class SubjectModel : BaseModel
    {
        public string Name { get; set; }
        public string? Link { get; set; }
        public virtual IEnumerable<SubjectPostModel>? Posts { get; set; }
        //public virtual IEnumerable<GradeModel>? GradesHaveAccessRead { get; set; }
        //public virtual IEnumerable<PermisionTeacherEditSubjectModel> PermisionTeachersEditSubject { get; set; }
        public Guid? TeacherId { get; set; }
        public virtual UserModel? Teacher { get; set; }
        public Guid? EducationalYearId { get; set; }
        public virtual EducationalYearModel? EducationalYear { get; set; }
    }
}
