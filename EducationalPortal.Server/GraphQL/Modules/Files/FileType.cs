using EducationalPortal.Business.Models;
using EducationalPortal.Server.GraphQL.Abstraction;
using EducationalPortal.Server.GraphQL.Modules.Homeworks;
using GraphQL.Types;

namespace EducationalPortal.Server.GraphQL.Modules.Files
{
    public class FileType : BaseType<FileModel>
    {
        public FileType() : base()
        {
            Field<NonNullGraphType<StringGraphType>, string>()
                .Name("Name")
                .Resolve(context => context.Source.Name);
            
            Field<NonNullGraphType<StringGraphType>, string>()
                .Name("Path")
                .Resolve(context => context.Source.Path);
            
            Field<GuidGraphType, Guid?>()
                .Name("HomeworkId")
                .Resolve(context => context.Source.HomeworkId);
            
            Field<HomeworkType, HomeworkModel>()
                .Name("Homework")
                .Resolve(context => context.Source.Homework);
        }
    }
}
