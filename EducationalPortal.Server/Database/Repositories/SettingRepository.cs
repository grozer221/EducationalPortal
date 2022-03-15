using EducationalPortal.Server.Database.Abstractions;
using EducationalPortal.Server.Database.Enums;
using EducationalPortal.Server.Database.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPortal.Server.Database.Repositories
{
    public class SettingRepository : BaseRepository<SettingModel>
    {
        private readonly AppDbContext _context;
        public SettingRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public SettingModel GetByName(string name)
        {
            SettingModel? setting = GetByNameOrDefault(name);
            if (setting == null)
                throw new Exception("Налаштування з введеною назвою не знайдено");
            return setting;
        }
        
        public SettingModel? GetByNameOrDefault(string name)
        {
            List<SettingModel> settings = GetOrDefault(s => s.Name == name).ToList();
            return settings.Count() == 0 ? null : settings[0];
        }
        
        public async Task<SettingModel> CreateOrUpdateAsync(SettingModel newSetting)
        {
            List<SettingModel> checkUniqueSettingName = GetOrDefault(s => s.Name == newSetting.Name).ToList();
            if (checkUniqueSettingName.Count == 0)
            {
                await base.CreateAsync(newSetting);
            }
            else
            {
                checkUniqueSettingName[0].Id = newSetting.Id;
                checkUniqueSettingName[0].CreatedAt = newSetting.CreatedAt;
                await _context.SaveChangesAsync();
            }
            return checkUniqueSettingName[0];
        }
    }
}
