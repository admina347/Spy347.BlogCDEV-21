using Spy347.BlogCDEV_21.Infrastructure.Models;

namespace Spy347.BlogCDEV_21.Infrastructure.Repositories
{
    public interface ITagRepository
    {
        Task AddTag(Tag tag);
        HashSet<Tag> GetAllTags();
        Tag GetTag(int id);
        Task RemoveTag(int id);
        Task<bool> SaveChangesAsync();
        Task UpdateTag(Tag tag);
    }
}