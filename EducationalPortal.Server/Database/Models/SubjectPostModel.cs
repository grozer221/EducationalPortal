using EducationalPortal.Server.Database.Abstractions;
using EducationalPortal.Server.Database.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPortal.Server.Database.Models
{
    public class SubjectPostModel : BaseModel
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public PostType Type { get; set; }

        public Guid? SubjectId { get; set; }
        public virtual SubjectModel? Subject { get; set; }
        public Guid? TeacherId { get; set; }
        public virtual UserModel? Teacher { get; set; }
        public virtual List<HomeworkModel>? Homeworks { get; set; }
    }
}
