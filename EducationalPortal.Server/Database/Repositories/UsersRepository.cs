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
    public class UsersRepository : BaseRepository<UserModel>
    {
        private readonly AppDbContext _context;
        public UsersRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<UserModel> CreateAsync(UserModel entity)
        {
            if (!string.IsNullOrEmpty(entity.Email))
            {
                List<UserModel> checkUniqeUserEmail = Get(e => e.Email == entity.Email).ToList();
                if (checkUniqeUserEmail.Count > 0)
                    throw new Exception("Користувач з введеним Email уже існує");
            }
            
            List<UserModel> checkUniqeUserLogin = Get(e => e.Login == entity.Login).ToList();
            if (checkUniqeUserLogin.Count > 0)
                throw new Exception("Користувач з введеним Логіном уже існує");

            await base.CreateAsync(entity);
            return entity;
        }
        
        public override async Task<UserModel> UpdateAsync(UserModel entity)
        {
            if (!string.IsNullOrEmpty(entity.Email))
            {
                List<UserModel> checkUniqeUserEmail = Get(e => e.Email == entity.Email).ToList();
                if (checkUniqeUserEmail.Count > 0)
                    throw new Exception("Користувач з введеним Email уже існує");
            }
            
            List<UserModel> checkUniqeUserLogin = Get(e => e.Login == entity.Login).ToList();
            if (checkUniqeUserLogin.Count > 0)
                throw new Exception("Користувач з введеним Логіном уже існує");

            await base.UpdateAsync(entity);
            return entity;
        }

        public UserModel GetByLogin(string login)
        {
            List<UserModel> users = Get(e => e.Login == login).ToList();
            if (users.Count == 0)
                throw new Exception("Користувач з введеним Логіном не існує");
            return users[0];
        }
        
        public UserModel? GetByLoginOrDefault(string login)
        {
            List<UserModel> users = Get(e => e.Login == login).ToList();
            if (users.Count == 0)
                return null;
            return users[0];
        }
    }
}
