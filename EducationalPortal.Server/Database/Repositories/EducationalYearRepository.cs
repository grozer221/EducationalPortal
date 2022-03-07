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
            List<EducationalYearModel> checkUniqueYear = GetOrDefault(e => e.Name == entity.Name).ToList();
            if (checkUniqueYear.Count > 0)
                throw new Exception("Навчальний рік з данним ім'ям уже існує");
            await base.CreateAsync(entity);
            return entity;
        }
        
        public override async Task<EducationalYearModel> UpdateAsync(EducationalYearModel newEducationalYear)
        {
            List<EducationalYearModel>? checkUniqeYear = GetOrDefault(e => e.Name == newEducationalYear.Name && e.Id != newEducationalYear.Id).ToList();
            if (checkUniqeYear.Count > 0 && checkUniqeYear[0].Id != newEducationalYear.Id)
                throw new Exception("Навчальний рік з данним ім'ям уже існує");

            EducationalYearModel oldEducationalYear = GetById(newEducationalYear.Id);
            newEducationalYear.CreatedAt = oldEducationalYear.CreatedAt;
            if (newEducationalYear.IsCurrent)
            {
                List<EducationalYearModel>? currentYears = GetOrDefault(y => y.IsCurrent == true && y.Id != newEducationalYear.Id).ToList();
                foreach(var currentYear in currentYears)
                {
                    currentYear.IsCurrent = false;
                    await base.UpdateAsync(currentYear);
                }
            }
            await base.UpdateAsync(newEducationalYear);
            return newEducationalYear;
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
