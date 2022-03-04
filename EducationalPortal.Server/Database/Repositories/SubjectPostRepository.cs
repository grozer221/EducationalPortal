using EducationalPortal.Server.Database.Abstractions;
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

        public override Task<SubjectPostModel> CreateAsync(SubjectPostModel subjectPost)
        {
            throw new NotImplementedException();
        }
        
        public async Task<SubjectPostModel> CreateAsync(SubjectPostModel subjectPost, Guid currentTeacherId)
        {
            SubjectModel subject = _subjectRepository.GetById(subjectPost.SubjectId);
            if (subject.TeacherId != currentTeacherId)
                throw new Exception($"Ви не маєте прав на додавання посту для предмета {subject.Name}");
            subjectPost.TeacherId = currentTeacherId;
            await base.CreateAsync(subjectPost);
            return subjectPost;
        }
        
        public override Task<SubjectPostModel> UpdateAsync(SubjectPostModel subjectPost)
        {
            throw new NotImplementedException();
        }
        
        public async Task<SubjectPostModel> UpdateAsync(SubjectPostModel subjectPost, Guid currentTeacherId)
        {
            SubjectPostModel oldSubjectPost = GetById(subjectPost.Id);
            subjectPost.SubjectId = oldSubjectPost.SubjectId;
            subjectPost.TeacherId = oldSubjectPost.TeacherId;
            subjectPost.CreatedAt = oldSubjectPost.CreatedAt;
            if (subjectPost.TeacherId != currentTeacherId)
                throw new Exception($"Ви не маєте прав на редагування посту {oldSubjectPost.Title}");
            await base.UpdateAsync(subjectPost);
            return subjectPost;
        }

        public override Task RemoveAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        public async Task RemoveAsync(Guid postId, Guid currentTeacherId)
        {
            SubjectPostModel subjectPost = GetById(postId);
            if (subjectPost.TeacherId != currentTeacherId)
                throw new Exception($"Ви не маєте прав на видалення посту {subjectPost.Title}");
            await base.RemoveAsync(postId);
        }
    }
}
