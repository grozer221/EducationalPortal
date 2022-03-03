using EducationalPortal.Server.Database.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPortal.Server.Database.Models
{
    public class GradeModel : BaseModel
    {
        public string Name { get; set; }
        public virtual IEnumerable<UserModel>? Students { get; set; }
        //public virtual IEnumerable<SubjectModel>? SubjectHaveAccess { get; set; }
    }
}
