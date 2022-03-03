using EducationalPortal.Server.Database.Abstractions;
using EducationalPortal.Server.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPortal.Server.Database.Repositories
{
    public class EducationalYearRepository : BaseRepository<EducationalYearModel>
    {
        private readonly AppDbContext _context;

        public EducationalYearRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<EducationalYearModel> CreateAsync(EducationalYearModel entity)
        {
            List<EducationalYearModel> checkUniqueYear = Get(e => e.Name == entity.Name).ToList();
            if (checkUniqueYear.Count > 0)
                throw new Exception("Навчальний рік з данним ім'ям уже існує");
            await base.CreateAsync(entity);
            return entity;
        }
        
        public override async Task<EducationalYearModel> UpdateAsync(EducationalYearModel entity)
        {
            List<EducationalYearModel>? checkUniqeYear = Get(e => e.Name == entity.Name && e.Id != entity.Id).ToList();
            if (checkUniqeYear.Count > 0 && checkUniqeYear[0].Id != entity.Id)
                throw new Exception("Навчальний рік з данним ім'ям уже існує");
            if (entity.IsCurrent)
            {
                List<EducationalYearModel>? currentYears = Get(y => y.IsCurrent == true && y.Id != entity.Id).ToList();
                foreach(var currentYear in currentYears)
                {
                    currentYear.IsCurrent = false;
                    await base.UpdateAsync(currentYear);
                }
            }
            await base.UpdateAsync(entity);
            return entity;
        }

        public async Task<EducationalYearModel> SetCurrentEducationalYearAsync(Guid yearId)
        {
            EducationalYearModel year = await GetByIdAsync(yearId);
            if (year == null)
                throw new Exception("Навчального року за даним Id не існує");
            List<EducationalYearModel> educationalYears = Get(y => y.IsCurrent == true).ToList();
            foreach(var educationalYear in educationalYears)
            {
                educationalYear.IsCurrent = false;
                await UpdateAsync(educationalYear);
            }
            year.IsCurrent = true;
            await UpdateAsync(year);
            return year;
        }
        
        public EducationalYearModel GetCurrent()
        {
            List<EducationalYearModel> currentYears = Get(y => y.IsCurrent == true).ToList();
            if (currentYears.Count == 0)
                throw new Exception("На даний момент немає навчального року");
            return currentYears[0];
        }
    }
}
