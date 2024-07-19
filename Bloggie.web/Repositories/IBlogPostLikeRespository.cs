using Bloggie.web.Models.Domain;

namespace Bloggie.web.Repositories
{
    public interface IBlogPostLikeRespository
    {
        Task<int> GetTotalLikes(Guid blogPostId);

        Task<IEnumerable<BlogPostLike>> GetLikesForBlog(Guid blogPostId);

        Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike);
    } 
}
