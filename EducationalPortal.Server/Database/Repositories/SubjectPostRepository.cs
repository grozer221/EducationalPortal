using EducationalPortal.Server.Database.Abstractions;
using EducationalPortal.Server.Database.Enums;
using EducationalPortal.Server.Database.Models;

namespace EducationalPortal.Server.Database.Repositories
{
    public class SubjectPostRepository : BaseRepository<SubjectPostModel>
    {
        private readonly AppDbContext _context;
        private readonly SubjectRepository _subjectRepository;
        private readonly UserRepository _usersRepository;
        public SubjectPostRepository(AppDbContext context, SubjectRepository subjectRepository, UserRepository usersRepository) : base(context)
        {
            _context = context;
            _subjectRepository = subjectRepository;
            _usersRepository = usersRepository;
        }
        
        public async Task<SubjectPostModel> UpdateAsync(SubjectPostModel subjectPost)
        {
            SubjectPostModel oldSubjectPost = GetById(subjectPost.Id);
            subjectPost.SubjectId = oldSubjectPost.SubjectId;
            subjectPost.TeacherId = oldSubjectPost.TeacherId;
            subjectPost.CreatedAt = oldSubjectPost.CreatedAt;
            await base.UpdateAsync(subjectPost);
            return subjectPost;
        }
    }
}
