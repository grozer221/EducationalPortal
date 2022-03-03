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

        public async Task<SubjectModel> CreateAsync(SubjectModel subject, Guid currentTeacherId)
        {
            UserModel currentTeacher = await _usersRepository.GetByIdAsync(currentTeacherId);
            if (currentTeacher?.Role == UserRoleEnum.Student)
                throw new Exception("Ви не маєте прав на створення данного предмету");
            subject.TeacherId = currentTeacherId;
            Guid currentEducationalYearId = _educationalYearRepository.GetCurrent().Id;
            subject.EducationalYearId = currentEducationalYearId;
            await base.CreateAsync(subject);
            return subject;
        }
        
        public override Task<SubjectModel> UpdateAsync(SubjectModel entity)
        {
            throw new NotImplementedException();
        }

        public async Task<SubjectModel> UpdateAsync(SubjectModel subject, Guid currentTeacherId)
        {
            UserModel? currentTeacher = await _usersRepository.GetByIdAsync(currentTeacherId);
            if (currentTeacher?.Id != subject.TeacherId || currentTeacher?.Role == UserRoleEnum.Student)
                if(currentTeacher?.Role != UserRoleEnum.Administrator)
                    throw new Exception("Ви не маєте прав на редагування данного предмету");
            await base.UpdateAsync(subject);
            return subject;
        }

        public override Task RemoveAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<SubjectModel> RemoveAsync(Guid subjectId, Guid currentTeacherId)
        {
            SubjectModel subject = await GetByIdAsync(subjectId);
            UserModel currentTeacher = await _usersRepository.GetByIdAsync(currentTeacherId);
            if (currentTeacher?.Id != subject.TeacherId || currentTeacher?.Role == UserRoleEnum.Student)
                if(currentTeacher?.Role != UserRoleEnum.Administrator)
                    throw new Exception("Ви не маєте прав на видалення данного предмету");
            await base.RemoveAsync(subjectId);
            return subject;
        }
    }
}
