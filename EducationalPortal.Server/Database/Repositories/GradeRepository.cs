using EducationalPortal.Server.Database.Abstractions;
using EducationalPortal.Server.Database.Models;

namespace EducationalPortal.Server.Database.Repositories
{
    public class GradeRepository : BaseRepository<GradeModel>
    {
        private readonly AppDbContext _context;

        public GradeRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<GradeModel> CreateAsync(GradeModel grade)
        {
            List<GradeModel> checkUniqueGradeName = GetOrDefault(e => e.Name == grade.Name).ToList();
            if (checkUniqueGradeName.Count > 0)
                throw new Exception("Клас з данною назвою уже існує");
            await base.CreateAsync(grade);
            return grade;
        }

        public override async Task<GradeModel> UpdateAsync(GradeModel newGrade)
        {
            List<GradeModel>? checkUniqueGradeName = GetOrDefault(e => e.Name == newGrade.Name && e.Id != newGrade.Id).ToList();
            if (checkUniqueGradeName.Count > 0 && checkUniqueGradeName[0].Id != newGrade.Id)
                throw new Exception("Клас з даною назвою уже існує");
            GradeModel addedGrade = GetById(newGrade.Id);
            addedGrade.Name = newGrade.Name;
            await _context.SaveChangesAsync();
            return addedGrade;
        }
    }
}