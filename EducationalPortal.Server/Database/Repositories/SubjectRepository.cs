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

        public override GetEntitiesResponse<SubjectModel> Get(Func<SubjectModel, object> predicate, bool descending, int page, Func<SubjectModel, bool>? condition = null)
        {

            IEnumerable<SubjectModel> entities = descending
                ? _context.Set<SubjectModel>().Include(s => s.TeachersHaveAccessCreatePosts).Include(s => s.GradesHaveAccessRead).AsNoTracking().OrderByDescending(predicate)
                : _context.Set<SubjectModel>().Include(s => s.TeachersHaveAccessCreatePosts).Include(s => s.GradesHaveAccessRead).AsNoTracking().OrderBy(predicate);

            if (condition != null)
                entities = entities.Where(condition);

            int total = entities.Count();

            int take = 20;
            int skip = (page - 1) * take;
            entities = entities.Skip(skip).Take(take);

            return new GetEntitiesResponse<SubjectModel>
            {
                Entities = entities,
                Total = total,
            };
        }

        public override async Task<SubjectModel> CreateAsync(SubjectModel subject)
        { 
            Guid currentEducationalYearId = _educationalYearRepository.GetCurrent().Id;
            subject.EducationalYearId = currentEducationalYearId;
            await base.CreateAsync(subject);
            if (subject.GradesHaveAccessReadIds != null)
            {
                SubjectModel addedSubject = GetByIdWithGradesHaveAccessRead(subject.Id);
                foreach (var gradeId in subject.GradesHaveAccessReadIds.Distinct())
                {
                    GradeModel grade = _gradeRepository.GetById(gradeId);
                    addedSubject.GradesHaveAccessRead.Add(grade);
                }
                await UpdateAsync(addedSubject);
            }
            if (subject.TeachersHaveAccessCreatePostsIds != null)
            {
                SubjectModel addedSubject = GetByIdWithTeachersHaveAccessCreatePosts(subject.Id);
                foreach (var teacherId in subject.TeachersHaveAccessCreatePostsIds.Distinct())
                {
                    UserModel teacher = _userRepository.GetById(teacherId);
                    addedSubject.TeachersHaveAccessCreatePosts.Add(teacher);
                }
                await UpdateAsync(addedSubject);
            }
            return subject;
        }

        public override async Task<SubjectModel> UpdateAsync(SubjectModel newSubject)
        {
            SubjectModel oldSubject = GetById(newSubject.Id);
            newSubject.TeacherId = oldSubject.TeacherId;
            newSubject.EducationalYearId = oldSubject.EducationalYearId;
            newSubject.CreatedAt = oldSubject.CreatedAt;
            await base.UpdateAsync(newSubject);
            SubjectModel subjectWithGradesHaveAccessRead = GetByIdWithGradesHaveAccessRead(newSubject.Id);
            if (newSubject.GradesHaveAccessReadIds != null)
            {
                subjectWithGradesHaveAccessRead.GradesHaveAccessRead = subjectWithGradesHaveAccessRead.GradesHaveAccessRead.Where(g => newSubject.GradesHaveAccessReadIds.Any(gId => gId == g.Id)).ToList();
                foreach (var gradeId in newSubject.GradesHaveAccessReadIds.Distinct())
                {
                    if (!subjectWithGradesHaveAccessRead.GradesHaveAccessRead.Any(g => g.Id == gradeId))
                    {
                        GradeModel grade = _gradeRepository.GetById(gradeId);
                        subjectWithGradesHaveAccessRead.GradesHaveAccessRead.Add(grade);
                    }
                }
                await _context.SaveChangesAsync();
            }
            
            SubjectModel subjectWithTeachersHaveAccessCreatePosts = GetByIdWithTeachersHaveAccessCreatePosts(newSubject.Id);
            if (newSubject.TeachersHaveAccessCreatePostsIds != null)
            {
                subjectWithTeachersHaveAccessCreatePosts.TeachersHaveAccessCreatePosts = subjectWithTeachersHaveAccessCreatePosts.TeachersHaveAccessCreatePosts.Where(t => newSubject.TeachersHaveAccessCreatePostsIds.Any(tId => tId == t.Id)).ToList();
                foreach (var teacherId in newSubject.TeachersHaveAccessCreatePostsIds.Distinct())
                {
                    if (!subjectWithTeachersHaveAccessCreatePosts.TeachersHaveAccessCreatePosts.Any(t => t.Id == teacherId))
                    {
                        UserModel teacher = _userRepository.GetById(teacherId);
                        subjectWithTeachersHaveAccessCreatePosts.TeachersHaveAccessCreatePosts.Add(teacher);
                    }
                }
                await _context.SaveChangesAsync();
            }
            return newSubject;
        }

        public SubjectModel GetByIdWithGradesHaveAccessRead(Guid? id)
        {
            SubjectModel? subject = _context.Subjects.Include(s => s.GradesHaveAccessRead).SingleOrDefault(s => s.Id == id);
            if (subject == null)
                throw new Exception("Не знайдено предмет");
            return subject;
        }
        
        public SubjectModel GetByIdWithTeachersHaveAccessCreatePosts(Guid? id)
        {
            SubjectModel? subject = _context.Subjects.Include(s => s.TeachersHaveAccessCreatePosts).SingleOrDefault(s => s.Id == id);
            if (subject == null)
                throw new Exception("Не знайдено предмет");
            return subject;
        }
    }
}
