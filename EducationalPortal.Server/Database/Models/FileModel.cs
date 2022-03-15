using EducationalPortal.Server.Database.Abstractions;

namespace EducationalPortal.Server.Database.Models
{
    public class FileModel : BaseModel
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public Guid? HomeworkId { get; set; }
        public virtual HomeworkModel? Homework { get; set; }
    }
}
