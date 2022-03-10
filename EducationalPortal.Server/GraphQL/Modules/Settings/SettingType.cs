using EducationalPortal.Server.Database.Models;
using EducationalPortal.Server.GraphQL.Abstraction;
using GraphQL.Types;
using Newtonsoft.Json;

namespace EducationalPortal.Server.GraphQL.Modules.Settings
{
    public class SettingType : BaseType<SettingModel>
    {
        public SettingType() : base()
        {
            Field<NonNullGraphType<StringGraphType>, string>()
               .Name("Name")
               .Resolve(context => context.Source.Name);
            
            Field<StringGraphType, string?>()
               .Name("Value")
               .Resolve(context => context.Source.Value);
        }
    }
}
