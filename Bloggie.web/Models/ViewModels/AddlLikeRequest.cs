namespace Bloggie.web.Models.ViewModels
{
    public class AddlLikeRequest
    {
        public Guid BlogPostId { get; set; }

        public Guid UserId { get; set; }
    }
}
