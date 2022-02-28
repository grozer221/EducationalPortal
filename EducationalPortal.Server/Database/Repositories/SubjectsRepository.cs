using EducationalPortal.Database.Abstractions;
using EducationalPortal.Database.Enums;
using EducationalPortal.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPortal.Database.Repositories
{
    public class SubjectsRepository : BaseRepository<SubjectModel>
    {
        private readonly AppDbContext _context;
        private readonly EducationalYearRepository _educationalYearRepository;
        private readonly UsersRepository _usersRepository;
        public SubjectsRepository(AppDbContext context, EducationalYearRepository educationalYearRepository, UsersRepository usersRepository) : base(context)
        {
            _context = context;
            _educationalYearRepository = educationalYearRepository;
            _usersRepository = usersRepository;
        }

        public override Task<SubjectModel> CreateAsync(SubjectModel entity)
        {
            throw new NotImplementedException();
        }

        public async Task<SubjectModel> CreateAsync(SubjectModel subject, Guid teacherId, Guid educationalYearId)
        {
            subject.TeacherId = teacherId;
            subject.EducationalYearId = educationalYearId;
            await base.UpdateAsync(subject);
            return subject;
        }
        
        public override Task<SubjectModel> UpdateAsync(SubjectModel entity)
        {
            throw new NotImplementedException();
        }

        public async Task<SubjectModel> UpdateAsync(SubjectModel subject, Guid teacherId)
        {
            UserModel teacher = await _usersRepository.GetByIdAsync(teacherId);
            if (subject.TeacherId != teacher.Id && teacher.Role != UserRoleEnum.Administrator)
                throw new Exception("Ви не маєте прав на редагування данного предмету");
            await base.UpdateAsync(subject);
            return subject;
        }

        public override Task RemoveAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<SubjectModel> RemoveAsync(Guid subjectId, Guid teacherId)
        {
            SubjectModel subject = await GetByIdAsync(subjectId);
            UserModel teacher = await _usersRepository.GetByIdAsync(teacherId);
            if (subject.TeacherId != teacher.Id && teacher.Role != UserRoleEnum.Administrator)
                throw new Exception("Ви не маєте прав на редагування данного предмету");
            await base.RemoveAsync(subjectId);
            return subject;
        }
    }
}
