using EducationalPortal.Server.Database.Abstractions;
using EducationalPortal.Server.Database.Enums;
using EducationalPortal.Server.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPortal.Server.Database.Repositories
{
    public class SubjectRepository : BaseRepository<SubjectModel>
    {
        private readonly AppDbContext _context;
        private readonly EducationalYearRepository _educationalYearRepository;
        private readonly GradeRepository _gradeRepository;
        public SubjectRepository(AppDbContext context, EducationalYearRepository educationalYearRepository, GradeRepository gradeRepository) : base(context)
        {
            _context = context;
            _educationalYearRepository = educationalYearRepository;
            _gradeRepository = gradeRepository;
        }

        public override async Task<SubjectModel> CreateAsync(SubjectModel subject)
        { 
            Guid currentEducationalYearId = _educationalYearRepository.GetCurrent().Id;
            subject.EducationalYearId = currentEducationalYearId;
            await base.CreateAsync(subject);
            SubjectModel addedSubject = GetByIdWithGradesHaveAccessRead(subject.Id);
            if (subject.GradesHaveAccessReadIds != null)
            {
                List<GradeModel> gradesHaveAccessRead = new List<GradeModel>();
                foreach (var gradeId in subject.GradesHaveAccessReadIds)
                {
                    GradeModel grade = _gradeRepository.GetById(gradeId);
                    gradesHaveAccessRead.Add(grade);
                }
                addedSubject.GradesHaveAccessRead = gradesHaveAccessRead;
            }
            await UpdateAsync(addedSubject);
            return subject;
        }

        public override async Task<SubjectModel> UpdateAsync(SubjectModel newSubject)
        {
            SubjectModel oldSubject = GetById(newSubject.Id);
            newSubject.TeacherId = oldSubject.TeacherId;
            newSubject.EducationalYearId = oldSubject.EducationalYearId;
            newSubject.CreatedAt = oldSubject.CreatedAt;
            await base.UpdateAsync(newSubject);
            SubjectModel addedSubject = GetByIdWithGradesHaveAccessRead(newSubject.Id);
            if (newSubject.GradesHaveAccessReadIds != null)
            {
                addedSubject.GradesHaveAccessRead = addedSubject.GradesHaveAccessRead.Where(g => newSubject.GradesHaveAccessReadIds.Any(gId => gId == g.Id)).ToList();
                foreach (var gradeId in newSubject.GradesHaveAccessReadIds)
                {
                    if (!addedSubject.GradesHaveAccessRead.Any(g => g.Id == gradeId))
                    {
                        GradeModel grade = _gradeRepository.GetById(gradeId);
                        addedSubject.GradesHaveAccessRead.Add(grade);
                    }
                }
                await _context.SaveChangesAsync();
            }
            return addedSubject;
        }

        public SubjectModel GetByIdWithGradesHaveAccessRead(Guid id)
        {
            SubjectModel? subject = _context.Subjects.Include(s => s.GradesHaveAccessRead).SingleOrDefault(s => s.Id == id);
            if (subject == null)
                throw new Exception("Не знайдено предмет");
            return subject;
        }
    }
}
