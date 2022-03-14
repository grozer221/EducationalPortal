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
        private readonly UserRepository _userRepository;
        public SubjectRepository(AppDbContext context, EducationalYearRepository educationalYearRepository, GradeRepository gradeRepository, UserRepository userRepository) : base(context)
        {
            _context = context;
            _educationalYearRepository = educationalYearRepository;
            _gradeRepository = gradeRepository;
            _userRepository = userRepository;
        }

        public override async Task<SubjectModel> CreateAsync(SubjectModel subject)
        { 
            Guid currentEducationalYearId = _educationalYearRepository.GetCurrent().Id;
            subject.EducationalYearId = currentEducationalYearId;
            await base.CreateAsync(subject);
            _context.Entry(subject).State = EntityState.Detached;

            SubjectModel addedSubject = GetById(subject.Id, s => s.GradesHaveAccessRead, s => s.TeachersHaveAccessCreatePosts);
            foreach (var gradeId in subject.GradesHaveAccessReadIds.Distinct())
            {
                GradeModel grade = _gradeRepository.GetById(gradeId);
                addedSubject.GradesHaveAccessRead.Add(grade);
            }
            foreach (var teacherId in subject.TeachersHaveAccessCreatePostsIds.Distinct())
            {
                UserModel teacher = _userRepository.GetById(teacherId);
                addedSubject.TeachersHaveAccessCreatePosts.Add(teacher);
            }
            await base.UpdateAsync(addedSubject);
            return addedSubject;
        }

        public override async Task<SubjectModel> UpdateAsync(SubjectModel newSubject)
        {
            SubjectModel addedSubject = GetById(newSubject.Id, s => s.GradesHaveAccessRead, s => s.TeachersHaveAccessCreatePosts);
            addedSubject.Name = newSubject.Name;
            addedSubject.Link = newSubject.Link;

            addedSubject.GradesHaveAccessRead = addedSubject.GradesHaveAccessRead.Where(g => newSubject.GradesHaveAccessReadIds.Any(gId => gId == g.Id)).ToList();
            foreach (var gradeId in newSubject.GradesHaveAccessReadIds.Distinct())
            {
                if (!addedSubject.GradesHaveAccessRead.Any(g => g.Id == gradeId))
                {
                    GradeModel grade = _gradeRepository.GetById(gradeId);
                    addedSubject.GradesHaveAccessRead.Add(grade);
                }
            }

            addedSubject.TeachersHaveAccessCreatePosts = addedSubject.TeachersHaveAccessCreatePosts.Where(t => newSubject.TeachersHaveAccessCreatePostsIds.Any(tId => tId == t.Id)).ToList();
            foreach (var teacherId in newSubject.TeachersHaveAccessCreatePostsIds.Distinct())
            {
                if (!addedSubject.TeachersHaveAccessCreatePosts.Any(t => t.Id == teacherId))
                {
                    UserModel teacher = _userRepository.GetById(teacherId);
                    addedSubject.TeachersHaveAccessCreatePosts.Add(teacher);
                }
            }

            await _context.SaveChangesAsync();
            return addedSubject;
        }
    }
}
