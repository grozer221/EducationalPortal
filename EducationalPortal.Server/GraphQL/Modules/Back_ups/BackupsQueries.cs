using EducationalPortal.Business.Abstractions;
using EducationalPortal.Business.Models;
using EducationalPortal.Business.Repositories;
using EducationalPortal.Server.GraphQL.Abstraction;
using EducationalPortal.Server.GraphQL.Modules.Auth;
using GraphQL;
using GraphQL.Types;

namespace EducationalPortal.Server.GraphQL.Modules.Back_ups
{
    public class BackupsQueries : ObjectGraphType, IQueryMarker
    {
        public BackupsQueries(IBackupRepository backupRepository)
        {
            Field<NonNullGraphType<GetEntitiesResponseType<BackupType, BackupModel>>, GetEntitiesResponse<BackupModel>>()
                .Name("GetBackups")
                .Argument<NonNullGraphType<IntGraphType>, int>("Page", "Argument for get Backups")
                .Argument<NonNullGraphType<StringGraphType>, string>("Like", "Argument for get Backups")
                .ResolveAsync(async context =>
                {
                    int page = context.GetArgument<int>("Page");
                    string like = context.GetArgument<string>("Like");
                    return await backupRepository.WhereOrDefaultAsync(y => y.CreatedAt, Order.Descend, page, y => y.File.Name.ToLower().Contains(like.ToLower()), b => b.File);
                })
               .AuthorizeWith(AuthPolicies.Teacher);

            //Field<NonNullGraphType<EducationalYearType>, EducationalYearModel>()
            //    .Name("GetEducationalYear")
            //    .Argument<NonNullGraphType<IdGraphType>, Guid>("Id", "Argument for get Educational year")
            //    .ResolveAsync(async context =>
            //    {
            //        Guid id = context.GetArgument<Guid>("Id");
            //        return await educationalYearRepository.GetByIdAsync(id);
            //    })
            //    .AuthorizeWith(AuthPolicies.Teacher);
        }
    }
}
