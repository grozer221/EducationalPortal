using EducationalPortal.Server.Database.Abstractions;
using EducationalPortal.Server.Database.Enums;

namespace EducationalPortal.Server.Database.Models
{
    public class HomeworkModel : BaseModel
    {
        public string? Text { get; set; }
        public string? Mark { get; set; }
        public string? ReviewResult { get; set; }
        public HomeworkStatus Status { get; set; }
        public Guid? StudentId { get; set; }
        public virtual UserModel? Student { get; set; }
        public Guid? SubjectPostId { get; set; }
        public virtual SubjectPostModel? SubjectPost { get; set; }
        public virtual List<FileModel>? Files { get; set; }
    }
}
