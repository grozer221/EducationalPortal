using EducationalPortal.Database.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPortal.Database.Models
{
    public class GradeModel : BaseModel
    {
        public string Name { get; set; }
        public virtual IEnumerable<UserModel>? Students { get; set; }
        //public virtual IEnumerable<SubjectModel>? SubjectHaveAccess { get; set; }
    }
}
