namespace EducationalPortal.Server.Database.Abstractions
{
    public class GetEntitiesResponse<T> where T : class
    {
        public IEnumerable<T> Entities { get; set; }
        public int Total { get; set; }
    }
}
