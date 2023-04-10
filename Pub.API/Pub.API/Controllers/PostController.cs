using Microsoft.AspNetCore.Mvc;
using Pub.API.Common;
using Pub.API.Model.Request;
using Pub.API.Model.Response;
using Pub.API.Service.Authentication;
using Pub.Core.Interface;
using Pub.Core.Manager;

namespace Pub.API.Controllers {

    [ApiController]
    public class PostController : ControllerBase {
        private readonly AuthenticationService authenticationService;
        private readonly IPostManager postManager;

        public PostController(IConfiguration configuration, IServiceScopeFactory scopeFactory)
        {
            authenticationService=new AuthenticationService(configuration);
            postManager=new PostManager(configuration, scopeFactory);
        }

        [Route(Router.VersionOne+"/"+Router.Admin+"/"+Router.Post.PrefixName+"/"+Router.Post.AddPost)]
        [HttpPost]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(SuccessResponse))]
        [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ErrorResponse))]
        public async Task<IActionResult> AddPost([FromQuery] string token, [FromBody] AddPostRequest request) {
            var auth = authenticationService.ValidateJSONWebToken(token);

            if (!auth) {
                return Unauthorized();
            }

            var result = await postManager.AddPostAsync(request.PostHeader,request.PostBody,request.PicturesSourceRoute,request.EventId,request.AccountId);

            if (result.success) {
                return Ok(new SuccessResponse { Message = result.message }); 
            }

            return BadRequest(new ErrorResponse { ErrorCode = (int)StatusCodes.Status400BadRequest, Message = result.message });

        }

        [Route(Router.VersionOne + "/" + Router.Admin + "/" + Router.Post.PrefixName + "/" + Router.Post.DeletePost)]
        [HttpDelete]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(SuccessResponse))]
        [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ErrorResponse))]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> DeletePost([FromQuery] string token, [FromBody] DeletePostRequest request) {

            var auth = authenticationService.ValidateJSONWebToken(token);

            if (!auth) {
                return Unauthorized();
            }

            var result = await postManager.DeletePostAsync(request.PostId);


            if (result.success) {
                return Ok(new SuccessResponse { Message = result.message });
            }

            return BadRequest(new ErrorResponse { ErrorCode = (int)StatusCodes.Status400BadRequest, Message = result.message });

        }
        [Route(Router.VersionOne + "/" + Router.Admin + "/" + Router.Post.PrefixName + "/" + Router.Post.UpdatePostContent)]
        [HttpPatch]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(SuccessResponse))]
        [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ErrorResponse))]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> UpdatePostContent([FromQuery] string token, [FromBody] UpdatePostRequest request) {

            var auth = authenticationService.ValidateJSONWebToken(token);

            if (!auth) {
                return Unauthorized();
            }

            var result = await postManager.UpdatePostContentAsync(request.PostId, request.PostHeader, request.PostBody);
            
            if (result.success) {
                return Ok(new SuccessResponse { Message = result.message });
            }

            return BadRequest(new ErrorResponse { ErrorCode = (int)StatusCodes.Status400BadRequest, Message = result.message });

        }
        [Route(Router.VersionOne + "/" + Router.User + "/" + Router.Post.PrefixName + "/" + Router.Post.GetAllPosts)]
        [HttpGet]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> GetAllPosts() {
            var result = await postManager.GetAllPostsAsync();

            if (result.Count() == 0) {
                return NoContent();
            }

            return Ok(result);
        }
        [Route(Router.VersionOne + "/" + Router.User + "/" + Router.Post.PrefixName + "/" + Router.Post.GetAllPostsByEvent)]
        [HttpGet]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> GetAllPostsByEvent([FromQuery] GetAllPostsByEventRequest request) {
            var result = await postManager.GetAllPostsByEventAsync(request.EventId);

            if (result.Count() == 0) {
                return NoContent();
            }

            return Ok(result);
        }
        [Route(Router.VersionOne + "/" + Router.User + "/" + Router.Post.PrefixName + "/" + Router.Post.GetPost)]
        [HttpGet]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> GetPost([FromQuery] GetPostRequest request) {
            var result = await postManager.GetPostAsync(request.PostId);

            if (result == null) {
                return NoContent();
            }

            return Ok(result);
        }
    }
}
