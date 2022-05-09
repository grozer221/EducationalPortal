using EducationalPortal.Business.Enums;
using EducationalPortal.Business.Models;
using EducationalPortal.Business.Repositories;
using EducationalPortal.PostgreSql.Abstractions;

namespace EducationalPortal.PostgreSql.Repositories
{
    public class HomeworkRepository : BaseRepository<HomeworkModel>, IHomeworkRepository
    {
        private readonly AppDbContext _context;
        public HomeworkRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<HomeworkModel> CreateAsync(HomeworkModel homework)
        {
            homework.Status = HomeworkStatus.New;
            await base.CreateAsync(homework);
            return homework;
        }
        
        public override async Task<HomeworkModel> UpdateAsync(HomeworkModel newHomework)
        {
            HomeworkModel addedHomework = await GetByIdAsync(newHomework.Id);
            addedHomework.Mark = newHomework.Mark;
            addedHomework.ReviewResult = newHomework.ReviewResult;
            addedHomework.Status = newHomework.Status;
            await _context.SaveChangesAsync();
            return addedHomework;
        }

    }
}
