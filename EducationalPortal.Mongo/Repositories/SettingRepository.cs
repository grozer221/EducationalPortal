using EducationalPortal.Business.Models;
using EducationalPortal.Business.Repositories;
using EducationalPortal.Mongo.Abstractions;

namespace EducationalPortal.Mongo.Repositories
{
    public class SettingRepository : BaseRepository<SettingModel>, ISettingRepository
    {
        public async Task<SettingModel> GetByNameAsync(string name)
        {
            SettingModel? setting = await GetByNameOrDefaultAsync(name);
            if (setting == null)
                throw new Exception("Налаштування з введеною назвою не знайдено");
            return setting;
        }
        
        public async Task<SettingModel?> GetByNameOrDefaultAsync(string name)
        {
            List<SettingModel> settings = await GetOrDefaultAsync(s => s.Name == name);
            return settings.Count() == 0 ? null : settings[0];
        }
        
        public async Task<SettingModel> CreateOrUpdateAsync(SettingModel newSetting)
        {
            throw new NotImplementedException();
            List<SettingModel> checkUniqueSettingName = await GetOrDefaultAsync(s => s.Name == newSetting.Name);
            if (checkUniqueSettingName.Count == 0)
            {
                await base.CreateAsync(newSetting);
                return newSetting;
            }
            else
            {
                checkUniqueSettingName[0].Name = newSetting.Name;
                checkUniqueSettingName[0].Value = newSetting.Value;
                await base.UpdateAsync(checkUniqueSettingName[0]);
                return checkUniqueSettingName[0];
            }
        }
    }
}
