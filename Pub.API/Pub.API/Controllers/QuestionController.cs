using Microsoft.AspNetCore.Mvc;
using Pub.API.Common;
using Pub.API.Model.Request;
using Pub.API.Model.Response;
using Pub.API.Service.Authentication;
using Pub.Core.Interface;
using Pub.Core.Manager;

namespace Pub.API.Controllers {

    [ApiController]
    public class QuestionController : ControllerBase {

        private readonly IQuestionManager questionManager;
        private readonly AuthenticationService authenticationService;

        public QuestionController(IConfiguration configuration, IServiceScopeFactory scopeFactory)
        {
            questionManager=new QuestionManager(configuration, scopeFactory);
            authenticationService = new AuthenticationService(configuration);
        }

        [HttpPost]
        [Route(Router.VersionOne + "/" + Router.Admin + "/" + Router.Question.AddQuestion)]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(SuccessResponse))]
        [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ErrorResponse))]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> AddQuestion([FromQuery] string token,[FromBody] AddQuestionRequest request) {
            var auth = authenticationService.ValidateJSONWebToken(token);

            if (auth == false) return Unauthorized();

            var result = await questionManager.AddQuestionAsync(request.Content, request.EventId);

            if (!result.success) {
                return BadRequest(new ErrorResponse { ErrorCode=StatusCodes.Status400BadRequest,Message=result.message});
            }

            return Ok(new SuccessResponse { Message=result.message});
        }
        [HttpPatch]
        [Route(Router.VersionOne + "/" + Router.Admin + "/" + Router.Question.UpdateQuestion)]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(SuccessResponse))]
        [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ErrorResponse))]
        [Consumes("application/json")]
        [Produces("application/json")]

        public async Task<IActionResult> UpdateQuestion([FromQuery] string token, [FromBody] UpdateQuestionRequest request) {
            var auth = authenticationService.ValidateJSONWebToken(token);

            if (auth == false) return Unauthorized();

            var result = await questionManager.UpdateQuestionAsync(request.QuestionId,request.Content);

            if (!result.success) {
                return BadRequest(new ErrorResponse { ErrorCode = StatusCodes.Status400BadRequest, Message = result.message });
            }

            return Ok(new SuccessResponse { Message = result.message });

        }
        [HttpDelete]
        [Route(Router.VersionOne + "/" + Router.Admin + "/" + Router.Question.RemoveQuestion)]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(SuccessResponse))]
        [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ErrorResponse))]
        [Consumes("application/json")]
        [Produces("application/json")]

        public async Task<IActionResult> RemoveQuestion([FromQuery] string token, [FromBody] RemoveQuestionRequest request) {
            var auth = authenticationService.ValidateJSONWebToken(token);

            if (auth == false) return Unauthorized();

            var result = await questionManager.RemoveQuestionAsync(request.QuestionId);

            if (!result.success) {
                return BadRequest(new ErrorResponse { ErrorCode = StatusCodes.Status400BadRequest, Message = result.message });
            }

            return Ok(new SuccessResponse { Message = result.message });
        }
        [HttpGet]
        [Route(Router.VersionOne + "/" + Router.User + "/" + Router.Question.GetAllQuestions)]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [Consumes("application/json")]
        [Produces("application/json")]

        public async Task<IActionResult> GetAllQuestions() {
            var result=await questionManager.GetAllQuestionsAsync();

            if (result.Count() == 0) {
                return NoContent();
            }

            return Ok(result);
        }
        [HttpGet]
        [Route(Router.VersionOne + "/" + Router.User + "/" + Router.Question.GetAllQuestionsForEvent)]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [Consumes("application/json")]
        [Produces("application/json")]

        public async Task<IActionResult> GetAllQuestionsForEvent([FromBody]GetAllQuestionsForEventRequest request) {
            var result = await questionManager.GetAllQuestionsForEventAsync(request.EventId);

            if (result.Count() == 0) {
                return NoContent();
            }

            return Ok(result);
        }
        [HttpGet]
        [Route(Router.VersionOne + "/" + Router.User + "/" + Router.Question.GetQuestion)]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [Consumes("application/json")]
        [Produces("application/json")]

        public async Task<IActionResult> GetQuestion([FromBody] GetQuestionRequest request) {
            var result = await questionManager.GetQuestionAsync(request.QuestionId);

            if (result==null) {
                return NoContent();
            }

            return Ok(result);
        }
    }
}





