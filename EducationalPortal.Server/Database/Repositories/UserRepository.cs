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
    public class UserRepository : BaseRepository<UserModel>
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<UserModel> CreateAsync(UserModel entity)
        {
            if (!string.IsNullOrEmpty(entity.Email))
            {
                List<UserModel> checkUniqeUserEmail = GetOrDefault(e => e.Email == entity.Email).ToList();
                if (checkUniqeUserEmail.Count > 0)
                    throw new Exception("Користувач з введеним Email уже існує");
            }
            
            List<UserModel> checkUniqeUserLogin = GetOrDefault(e => e.Login == entity.Login).ToList();
            if (checkUniqeUserLogin.Count > 0)
                throw new Exception("Користувач з введеним Логіном уже існує");

            await base.CreateAsync(entity);
            return entity;
        }
        
        public override async Task<UserModel> UpdateAsync(UserModel newUser)
        {
            if (!string.IsNullOrEmpty(newUser.Email))
            {
                List<UserModel> checkUniqeUserEmail = GetOrDefault(e => e.Email == newUser.Email && e.Id != newUser.Id).ToList();
                if (checkUniqeUserEmail.Count > 0)
                    throw new Exception("Користувач з введеним Email уже існує");
            }
            
            List<UserModel> checkUniqeUserLogin = GetOrDefault(e => e.Login == newUser.Login && e.Id != newUser.Id).ToList();
            if (checkUniqeUserLogin.Count > 0)
                throw new Exception("Користувач з введеним Логіном уже існує");

            UserModel addedUser = GetById(newUser.Id);
            addedUser.FirstName = newUser.FirstName;
            addedUser.LastName = newUser.LastName;
            addedUser.MiddleName = newUser.MiddleName;
            addedUser.Email = newUser.Email;
            addedUser.Login = newUser.Login;
            addedUser.DateOfBirth = newUser.DateOfBirth;
            addedUser.Role = newUser.Role;
            addedUser.GradeId = newUser.GradeId;
            await _context.SaveChangesAsync();
            return newUser;
        }

        public UserModel GetByLogin(string login)
        {
            UserModel? user = GetByLoginOrDefault(login);
            if (user == null)
                throw new Exception("Користувач з введеним Логіном не існує");
            return user;
        }
        
        public UserModel? GetByLoginOrDefault(string login)
        {
            List<UserModel> users = GetOrDefault(e => e.Login == login).ToList();
            if (users == null || users.Count == 0)
                return null;
            return users[0];
        }

        public async Task<UserModel> UpdateProfileAsync(UserModel newUser)
        {
            UserModel oldUser = GetById(newUser.Id);
            newUser.Role = oldUser.Role;
            await UpdateAsync(newUser);
            return newUser;
        }
    }
}
