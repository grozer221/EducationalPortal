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
        private readonly UserRepository _usersRepository;
        public SubjectRepository(AppDbContext context, EducationalYearRepository educationalYearRepository, UserRepository usersRepository) : base(context)
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
            UserModel currentTeacher = _usersRepository.GetById(currentTeacherId);
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

        public async Task<SubjectModel> UpdateAsync(SubjectModel newSubject, Guid currentTeacherId)
        {
            UserModel? currentTeacher = _usersRepository.GetById(currentTeacherId);
            SubjectModel oldSubject = GetById(newSubject.Id);
            newSubject.TeacherId = oldSubject.TeacherId;
            newSubject.EducationalYearId = oldSubject.EducationalYearId;
            newSubject.CreatedAt = oldSubject.CreatedAt;
            if (currentTeacher?.Id != newSubject.TeacherId || currentTeacher?.Role == UserRoleEnum.Student)
                if(currentTeacher?.Role != UserRoleEnum.Administrator)
                    throw new Exception("Ви не маєте прав на редагування данного предмету");
            await base.UpdateAsync(newSubject);
            return newSubject;
        }

        public override Task RemoveAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<SubjectModel> RemoveAsync(Guid subjectId, Guid currentTeacherId)
        {
            SubjectModel subject = GetById(subjectId);
            UserModel currentTeacher = _usersRepository.GetById(currentTeacherId);
            if (currentTeacher?.Id != subject.TeacherId || currentTeacher?.Role == UserRoleEnum.Student)
                if(currentTeacher?.Role != UserRoleEnum.Administrator)
                    throw new Exception("Ви не маєте прав на видалення данного предмету");
            await base.RemoveAsync(subjectId);
            return subject;
        }
    }
}
