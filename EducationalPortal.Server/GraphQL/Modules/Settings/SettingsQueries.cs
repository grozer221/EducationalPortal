using EducationalPortal.Server.Database.Abstractions;
using EducationalPortal.Server.Database.Models;
using EducationalPortal.Server.Database.Repositories;
using EducationalPortal.Server.GraphQL.Abstraction;
using EducationalPortal.Server.GraphQL.Modules.Auth;
using GraphQL;
using GraphQL.Types;

namespace EducationalPortal.Server.GraphQL.Modules.Settings
{
    public class SettingsQueries : ObjectGraphType, IQueryMarker
    {
        public SettingsQueries(SettingRepository settingRepository)
        {
            Field<NonNullGraphType<SettingType>, SettingModel>()
                .Name("GetSetting")
                .Argument<NonNullGraphType<StringGraphType>, string>("Name", "Argument for get Setting")
                .Resolve(context =>
                {
                    string name = context.GetArgument<string>("Name");
                    return settingRepository.GetByName(name);
                });

            Field<NonNullGraphType<ListGraphType<SettingType>>, List<SettingModel>>()
                .Name("GetSettings")
                .Resolve(context => settingRepository.Get());
        }
    }
}
