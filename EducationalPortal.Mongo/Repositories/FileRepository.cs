using EducationalPortal.Business.Abstractions;
using EducationalPortal.Business.Models;
using EducationalPortal.Business.Repositories;
using EducationalPortal.Mongo.Abstractions;

namespace EducationalPortal.Mongo.Repositories
{
    public class FileRepository : BaseRepository<FileModel>, IFileRepository
    {
    }
}
