using EducationalPortal.Server.Database.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPortal.Server.Database.Models
{
    public class EducationalYearModel : BaseModel
    {
        public string Name { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public bool IsCurrent { get; set; }
        public virtual List<SubjectModel>? Subjects { get; set; }
    }
}
