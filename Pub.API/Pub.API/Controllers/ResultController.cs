using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pub.API.Common;
using Pub.API.Model.Request;
using Pub.API.Model.Response;
using Pub.API.Service.Authentication;
using Pub.Core.Interface;
using Pub.Core.Manager;

namespace Pub.API.Controllers {
    [ApiController]
    public class ResultController : ControllerBase {
        private readonly IResultManager resultManager;
        private readonly AuthenticationService authenticationService;
        public ResultController(IConfiguration configuration,IServiceScopeFactory scopeFactory)
        {
            resultManager=new ResultManager(configuration,scopeFactory);
            authenticationService=new AuthenticationService(configuration);
        }

        [Route(Router.VersionOne+"/"+Router.Admin+"/"+Router.Result.PrefixName+"/"+Router.Result.AddResult)]
        [HttpPost]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(SuccessResponse))]
        [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ErrorResponse))]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> AddResult([FromQuery]string token,[FromBody]AddResultRequest request) {
            var auth=authenticationService.ValidateJSONWebToken(token);

            if (!auth) {
                return Unauthorized();
            }

            var result = await resultManager.AddResultAsync(request.TeamName,request.TeamScore,request.EventId);

            if (!result.success) {
                return BadRequest(new ErrorResponse { ErrorCode = (int)StatusCodes.Status400BadRequest, Message = result.message });
            }

            return Ok(new SuccessResponse { Message = result.message });
            
        }
        [Route(Router.VersionOne + "/" + Router.Admin + "/" + Router.Result.PrefixName + "/" + Router.Result.RemoveResult)]
        [HttpDelete]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(SuccessResponse))]
        [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ErrorResponse))]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> RemoveResult([FromQuery] string token,[FromQuery]RemoveResultRequest request) {
            var auth = authenticationService.ValidateJSONWebToken(token);

            if (!auth) {
                return Unauthorized();
            }

            var result = await resultManager.RemoveResultAsync(request.ResultId);

            if (!result.success) {
                return BadRequest(new ErrorResponse { ErrorCode = (int)StatusCodes.Status400BadRequest, Message = result.message });
            }

            return Ok(new SuccessResponse { Message = result.message });
        }
        [Route(Router.VersionOne + "/" + Router.Admin + "/" + Router.Result.PrefixName + "/" + Router.Result.UpdateResultScore)]
        [HttpPatch]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(SuccessResponse))]
        [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ErrorResponse))]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> UpdateResultScore([FromQuery] string token, [FromQuery]UpdateResultRequest request) {
            var auth = authenticationService.ValidateJSONWebToken(token);
            
            if (!auth) {
                return Unauthorized();
            }

            var result = await resultManager.UpdateResultScoreAsync(request.ResultId,request.TeamScore);

            if (!result.success) {
                return BadRequest(new ErrorResponse { ErrorCode = (int)StatusCodes.Status400BadRequest, Message = result.messsage });
            }

            return Ok(new SuccessResponse { Message = result.messsage });
        }
        [Route(Router.VersionOne + "/" + Router.User + "/" + Router.Result.PrefixName + "/" + Router.Result.GetAllResultsByEvent)]
        [HttpGet]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> GetAllResultsByEvent([FromQuery] GetAllResultsByEventRequest request) {
            var result = await resultManager.GetResultsByEventAsync(request.EventId);

            if (result.Count() == 0) {
                return NoContent();
            }

            return Ok(result);
        }

        [Route(Router.VersionOne + "/" + Router.User + "/" + Router.Result.PrefixName + "/" + Router.Result.GetAllResults)]
        [HttpGet]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> GetAllResults() {
            var result=await resultManager.GetAllResultsAsync();

            if (result.Count() == 0) {
                return NoContent();
            }

            return Ok(result);
        }

        [Route(Router.VersionOne + "/" + Router.User + "/" + Router.Result.PrefixName + "/" + Router.Result.GetResult)]
        [HttpGet]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> GetResult([FromQuery]GetResultRequest request) {
            var result = await resultManager.GetResultAsync(request.ResultId);

            if (result == null) {
                return NoContent();
            }

            return Ok(result);
        }
    }
}
