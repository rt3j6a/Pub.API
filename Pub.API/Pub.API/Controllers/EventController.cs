using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pub.API.Common;
using Pub.API.Model.Request;
using Pub.API.Model.Response;
using Pub.API.Service.Authentication;
using Pub.Core.Interface;
using Pub.Core.Manager;
using System.Net;

namespace Pub.API.Controllers {

    [ApiController]
    public class EventController : ControllerBase {

        private readonly AuthenticationService authenticationService;
        private readonly IEventManager eventManager;
        public EventController(IServiceScopeFactory scopeFactory, IConfiguration configuration)
        {
            authenticationService = new AuthenticationService(configuration);
            eventManager = new EventManager(configuration, scopeFactory);
        }

        [Route(Router.VersionOne+"/"+Router.Admin+"/"+Router.Event.PrefixName+"/"+Router.Event.GetAllEvents)]
        [HttpGet]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(statusCode:StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAllEvents([FromHeader] string token) {

            var auth = authenticationService.ValidateJSONWebToken(token);

            if (auth == false) return Unauthorized();

            var result =await eventManager.GetAllEventsAsync();

            if (result.Count() == 0) {
                return NoContent();
            }

            return Ok(result);
        }

        [Route(Router.VersionOne + "/" + Router.Admin + "/" + Router.Event.PrefixName + "/" + Router.Event.GetActiveEvents)]
        [HttpGet]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetActiveEventsAsync([FromHeader] string token) {

            var auth = authenticationService.ValidateJSONWebToken(token);

            if (auth == false) return Unauthorized();

            var result = await eventManager.GetActiveEventsAsync();

            if (result.Count() == 0) {
                return NoContent();
            }

            return Ok(result);
        }

        [Route(Router.VersionOne + "/" + Router.Admin + "/" + Router.Event.PrefixName + "/" + Router.Event.AddEvent)]
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK,type:typeof(SuccessResponse))]
        [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent,type:typeof(ErrorResponse))]

        public async Task<IActionResult> AddEvent([FromHeader] string token,[FromBody] AddEventRequest request) {
            var auth = authenticationService.ValidateJSONWebToken(token);

            if (auth == false) return Unauthorized();

            var result = await eventManager.AddEventAsync(request.EventName,request.EventDescription,request.EventPinnedDateTime);

            if (result.success) {
                return Ok(new SuccessResponse { Message = result.message });
            }

            return BadRequest(new ErrorResponse { ErrorCode=(int)StatusCodes.Status400BadRequest,Message=result.message});
        }

        [Route(Router.VersionOne + "/" + Router.Admin + "/" + Router.Event.PrefixName + "/" + Router.Event.GetEvent)]
        [HttpGet]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetEvent([FromHeader] string token, [FromQuery] GetEventRequest request) {
            var auth = authenticationService.ValidateJSONWebToken(token);

            if (auth == false) return Unauthorized();

            var result = await eventManager.GetEventAsync(request.eventId);

            if (result == null) {
                return NoContent();
            }

            return Ok(result);
        }

        [Route(Router.VersionOne + "/" + Router.Admin + "/" + Router.Event.PrefixName + "/" + Router.Event.UpdateEventStatus)]
        [HttpPatch]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(SuccessResponse))]
        [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent, type: typeof(ErrorResponse))]
        public async Task<IActionResult> UpdateEventStatus([FromHeader] string token, [FromBody] UpdateEventStatusRequest request) {
            var auth = authenticationService.ValidateJSONWebToken(token);

            if (auth == false) return Unauthorized();

            var result = await eventManager.UpdateEventStatusAsync(request.eventId, (Core.Common.EventStatus)request.eventStatusValue);

            if (result.success) {
                return Ok(new SuccessResponse { Message = result.message });
            }

            return BadRequest(new ErrorResponse { ErrorCode = (int)StatusCodes.Status400BadRequest, Message = result.message });
        }


    }
}
