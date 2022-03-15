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

            EducationalYearModel addedEducationalYear = GetById(newEducationalYear.Id);
            addedEducationalYear.Name = newEducationalYear.Name;
            addedEducationalYear.DateStart = newEducationalYear.DateStart;
            addedEducationalYear.DateEnd = newEducationalYear.DateEnd;
            addedEducationalYear.IsCurrent = newEducationalYear.IsCurrent;
            if (newEducationalYear.IsCurrent)
            {
                List<EducationalYearModel>? currentYears = GetOrDefault(y => y.IsCurrent == true && y.Id != newEducationalYear.Id).ToList();
                foreach(var currentYear in currentYears)
                {
                    currentYear.IsCurrent = false;
                }
                await _context.SaveChangesAsync();
            }
            await _context.SaveChangesAsync();
            return addedEducationalYear;
        }

        public EducationalYearModel GetCurrent()
        {
            List<EducationalYearModel> currentYears = GetOrDefault(y => y.IsCurrent == true).ToList();
            if (currentYears.Count == 0)
                throw new Exception("Ви не можете створити коли немає поточного навчального року");
            return currentYears[0];
        }
    }
}
