using EducationalPortal.Server.Database.Abstractions;
using EducationalPortal.Server.Database.Enums;
using EducationalPortal.Server.Database.Models;

namespace EducationalPortal.Server.Database.Repositories
{
    public class HomeworkRepository : BaseRepository<HomeworkModel>
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
            HomeworkModel addedHomework = GetById(newHomework.Id);
            addedHomework.Mark = newHomework.Mark;
            addedHomework.ReviewResult = newHomework.ReviewResult;
            addedHomework.Status = newHomework.Status;
            await _context.SaveChangesAsync();
            return addedHomework;
        }

    }
}
