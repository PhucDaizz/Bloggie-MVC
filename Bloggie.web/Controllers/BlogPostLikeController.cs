using Bloggie.web.Models.Domain;
using Bloggie.web.Models.ViewModels;
using Bloggie.web.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostLikeController : ControllerBase
    {
        private readonly IBlogPostLikeRespository blogPostLikeRespository;

        public BlogPostLikeController(IBlogPostLikeRespository blogPostLikeRespository)
        {
            this.blogPostLikeRespository = blogPostLikeRespository;
        }


        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Addlike([FromBody] AddlLikeRequest addlLikeRequest)
        {
            var model = new BlogPostLike
            {
                BlogPostId = addlLikeRequest.BlogPostId,
                UserId = addlLikeRequest.UserId,
            };

            await blogPostLikeRespository.AddLikeForBlog(model);

            return Ok();
        }

        [HttpGet]
        [Route("{blogPostId:Guid}/totalLikes")]
        public async Task<IActionResult> GetTotalLikesForBlog([FromRoute] Guid blogPostId)
        {
            var totalLikes = await blogPostLikeRespository.GetTotalLikes(blogPostId);
            
            return Ok(totalLikes);
        } 

    }
}
