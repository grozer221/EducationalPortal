using EducationalPortal.Business.Models;
using EducationalPortal.Business.Repositories;
using EducationalPortal.Mongo.Abstractions;

namespace EducationalPortal.Mongo.Repositories
{
    public class SubjectPostRepository : BaseRepository<SubjectPostModel>, ISubjectPostRepository
    {
        public async Task<SubjectPostModel> UpdateAsync(SubjectPostModel newSubjectPost)
        {
            SubjectPostModel addedSubjectPost = await GetByIdAsync(newSubjectPost.Id);
            addedSubjectPost.Title = newSubjectPost.Title;
            addedSubjectPost.Text = newSubjectPost.Text;
            addedSubjectPost.Type = newSubjectPost.Type;
            await base.UpdateAsync(addedSubjectPost);
            return addedSubjectPost;
        }
    }
}
