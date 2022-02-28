using EducationalPortal.Database.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPortal.Database.Models
{
    public class EducationalYearModel : BaseModel
    {
        public string Name { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public bool IsCurrent { get; set; }
        public virtual IEnumerable<SubjectModel>? Subjects { get; set; }
    }
}
