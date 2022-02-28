using EducationalPortal.Database.Abstractions;
using EducationalPortal.Database.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPortal.Database.Models
{
    public class SubjectPostModel : BaseModel
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public PostType Type { get; set; }

        public Guid? SubjectId { get; set; }
        public virtual SubjectModel? Subject { get; set; }
    }
}
