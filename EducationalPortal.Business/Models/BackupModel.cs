using EducationalPortal.Business.Abstractions;

namespace EducationalPortal.Business.Models
{
    public class BackupModel : BaseModel
    {
        public Guid? FileId { get; set; }
        public FileModel? File { get; set; }
    }
}
