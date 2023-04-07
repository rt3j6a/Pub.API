using Azure.Core;
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
    public class TableController : ControllerBase {
        private readonly AuthenticationService authenticationService;
        private readonly ITableManager tableManager;

        public TableController(IConfiguration configuration, IServiceScopeFactory scopeFactory)
        {
            authenticationService = new AuthenticationService(configuration);
            tableManager = new TableManager(configuration, scopeFactory);
        }

        [Route(Router.VersionOne+"/"+Router.Admin+"/"+Router.Table.PrefixName+"/"+Router.Table.GetAllTables)]
        [HttpGet]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized)]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> GetAllTables([FromQuery] string token) {
            var auth = authenticationService.ValidateJSONWebToken(token);

            if (!auth) {
                return Unauthorized();
            }

            var result = await tableManager.GetAllTablesAsync();

            if (result.Count() == 0) {
                return NoContent();
            }

            return Ok(result);
        }
        [Route(Router.VersionOne + "/" + Router.Admin + "/" + Router.Table.PrefixName + "/" + Router.Table.AddTable)]
        [HttpPost]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(SuccessResponse))]
        [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ErrorResponse))]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> AddTable([FromQuery] string token,[FromBody]AddTableRequest request) {
            var auth = authenticationService.ValidateJSONWebToken(token);

            if (!auth) {
                return Unauthorized();
            }

            var result = await tableManager.AddTableAsync(request.TableName,request.MaxSeatNumber);

            if (!result.success) {
                return BadRequest(new ErrorResponse { Message=result.message,ErrorCode=(int)HttpStatusCode.BadRequest });
            }

            return Ok(new SuccessResponse { Message=result.message});
        }
        [Route(Router.VersionOne + "/" + Router.Admin + "/" + Router.Table.PrefixName + "/" + Router.Table.DeleteTable)]
        [HttpDelete]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(SuccessResponse))]
        [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ErrorResponse))]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteTable([FromQuery] string token, [FromQuery] DeleteTableRequest request) {
            var auth = authenticationService.ValidateJSONWebToken(token);

            if (!auth) {
                return Unauthorized();
            }

            var result = await tableManager.DeleteTableAsync(request.TableId);

            if (!result.success) {
                return BadRequest(new ErrorResponse { Message = result.message, ErrorCode = (int)HttpStatusCode.BadRequest});
            }

            return Ok(new SuccessResponse { Message = result.message });

        }
        [Route(Router.VersionOne + "/" + Router.Admin + "/" + Router.Table.PrefixName + "/" + Router.Table.AddTableReservation)]
        [HttpPost]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(SuccessResponse))]
        [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ErrorResponse))]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> AddTableReservation([FromQuery] string token,[FromBody]AddTableReservationRequest request) {
            var auth = authenticationService.ValidateJSONWebToken(token);

            if (!auth) {
                return Unauthorized();
            }

            var result = await tableManager.AddTableReservationAsync(request.TeamName,request.Comment,request.TableId,request.EventId);
           
            if (!result.success) {
                return BadRequest(new ErrorResponse { Message = result.message, ErrorCode = (int)HttpStatusCode.BadRequest });
            }

            return Ok(new SuccessResponse { Message = result.message });
        }
        [Route(Router.VersionOne + "/" + Router.Admin + "/" + Router.Table.PrefixName + "/" + Router.Table.GetTableReservation)]
        [HttpGet]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized)]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> GetTableReservation([FromQuery] string token, [FromQuery]GetTableReservationRequest request) {
            var auth = authenticationService.ValidateJSONWebToken(token);

            if (!auth) {
                return Unauthorized();
            }

            var result = await tableManager.GetTableReservationAsync(request.ReservationId);

            if (result == null) {
                return NoContent();
            }

            return Ok(result);
        }
        [Route(Router.VersionOne + "/" + Router.Admin + "/" + Router.Table.PrefixName + "/" + Router.Table.UpdateTableReservationComment)]
        [HttpPatch]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(SuccessResponse))]
        [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ErrorResponse))]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> UpdateTableReservationComment([FromQuery] string token,[FromBody]UpdateTableReservationCommentRequest request) {

            var auth = authenticationService.ValidateJSONWebToken(token);

            if (!auth) {
                return Unauthorized();
            }

            var result = await tableManager.UpdateTableReservationCommentAsync(request.ReservationId,request.Comment);

            if (!result.success) {
                return BadRequest(new ErrorResponse { Message = result.message, ErrorCode = (int)HttpStatusCode.BadRequest });
            }

            return Ok(new SuccessResponse { Message = result.message });
        }
        [Route(Router.VersionOne + "/" + Router.Admin + "/" + Router.Table.PrefixName + "/" + Router.Table.DeleteAllTableReservationForEvent)]
        [HttpDelete]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(SuccessResponse))]
        [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ErrorResponse))]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteAllTableReservationsForEvent([FromQuery] string token, [FromQuery] DeleteAllTableReservationsForEventRequest request) {
            var auth = authenticationService.ValidateJSONWebToken(token);

            if (!auth) {
                return Unauthorized();
            }
            var result = await tableManager.DeleteAllTableReservationForEventAsync(request.EventId);

            if (!result.success) {
                return BadRequest(new ErrorResponse { Message = result.message, ErrorCode = (int)HttpStatusCode.BadRequest });
            }

            return Ok(new SuccessResponse { Message = result.message });
        }

        [Route(Router.VersionOne + "/" + Router.Admin + "/" + Router.Table.PrefixName + "/" + Router.Table.GetallTableReservations)]
        [HttpGet]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized)]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> GetAllTableReservations([FromQuery] string token) {
            var auth = authenticationService.ValidateJSONWebToken(token);

            if (!auth) {
                return Unauthorized();
            }

            var result = await tableManager.GetAllTableReservationsAsync();

            if (result.Count() == 0) {
                return NoContent();
            }

            return Ok(result);
        }


    }
}
