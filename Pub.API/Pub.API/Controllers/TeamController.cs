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
    public class TeamController : ControllerBase {
        private readonly AuthenticationService authenticationService;
        private readonly ITeamManager teamManager;
        public TeamController(IConfiguration configuration, IServiceScopeFactory scopeFactory)
        {
            authenticationService = new AuthenticationService(configuration);
            teamManager=new TeamManager(configuration, scopeFactory);
        }

        [Route(Router.VersionOne+"/"+Router.Admin+"/"+Router.Team.GetAllTeamAssignments)]
        [HttpGet]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAllTeamAssignments([FromQuery] string token) {
            var auth = authenticationService.ValidateJSONWebToken(token);

            if (auth == false) return Unauthorized();

            var result = await teamManager.GetAllTeamAssignmentsAsync();

            if (result.Count() == 0) {
                return NoContent();
            }

            return Ok(result);
        }

        [Route(Router.VersionOne + "/" + Router.Admin + "/" + Router.Team.GetActiveTeamAssignments)]
        [HttpGet]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetActiveTeamAssignments([FromQuery] string token) {
            var auth = authenticationService.ValidateJSONWebToken(token);

            if (auth == false) return Unauthorized();

            var result=await teamManager.GetActiveTeamAssignmentsAsync();

            if (result.Count() == 0) {
                return NoContent();
            }

            return Ok(result);
        }

        [Route(Router.VersionOne + "/" + Router.Admin + "/" + Router.Team.AddTeamAssignment)]
        [HttpPost]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK,type:typeof(SuccessResponse))]
        [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest,type:typeof(ErrorResponse))]

        public async Task<IActionResult> AddTeamAssignment([FromBody] AddTeamAssignmentRequest request) {
            var result = await teamManager.AddTeamAssignmentAsync(request.TeamName,request.TeamMemberCount,request.SourceEmailAddress,request.EventId);

            if (!result.success) {
                return BadRequest(new ErrorResponse {
                    ErrorCode=StatusCodes.Status400BadRequest,
                    Message=result.message
                });
            }

            return Ok(new SuccessResponse {Message=result.message});
        }

        [Route(Router.VersionOne + "/" + Router.Admin + "/" + Router.Team.UpdateTeamAssignmentStatus)]
        [HttpPatch]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(SuccessResponse))]
        [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ErrorResponse))]
        public async Task<IActionResult> UpdateTeamAssignmentStatus([FromQuery] string token,[FromBody] UpdateTeamAssignmentStatusRequest request) {
            var auth = authenticationService.ValidateJSONWebToken(token);

            if (auth == false) return Unauthorized();

            var result = await teamManager.UpdateTeamAssignmentStatusAsync(request.AssignmentId,(Core.Common.TeamAssignmentStatus)request.TeamAssignmentStatusId);

            if (!result.success) {
                return BadRequest(new ErrorResponse {
                    ErrorCode = StatusCodes.Status400BadRequest,
                    Message = result.message
                });
            }

            return Ok(new SuccessResponse { Message = result.message });
        }


    }
}
