using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pub.API.Common;
using Pub.API.Model.Request;
using Pub.API.Model.Response;
using Pub.API.Service.Authentication;
using Pub.API.Service.Ftp;
using Pub.Core.Interface;
using Pub.Core.Manager;

namespace Pub.API.Controllers {

    [ApiController]
    public class PictureController : ControllerBase {

        private readonly AuthenticationService authenticationService;
        private readonly IPictureManager pictureManager;
        private readonly IFtpService ftpService;

        public PictureController(IConfiguration configuration, IServiceScopeFactory scopeFactory)
        {
            authenticationService = new AuthenticationService(configuration);
            pictureManager = new PictureManager(configuration,scopeFactory);
            ftpService = new FtpHandler(configuration);
        }

        [HttpPost]
        [Route(Router.VersionOne + "/" + Router.Admin + "/" + Router.Picture.PrefixName + "/" + Router.Picture.UploadPicture)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ErrorResponse))]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UploadPicture([FromQuery] string token, [FromBody] UploadPicutreRequest request) {
            var auth = authenticationService.ValidateJSONWebToken(token);

            if (!auth) {
                return Unauthorized();
            }

            var linkResult = await pictureManager.AddPictureEventLinkAsync(request.EventId);

            if (!linkResult.success) {
                return BadRequest(new ErrorResponse { ErrorCode=StatusCodes.Status400BadRequest,Message=linkResult.message});
            }

            //a path a message-ben lesz
            string path = linkResult.message;

            await ftpService.UploadFileAsync(request.PictureDataBase64, path, request.FileName);

            return Ok();
        }

        [HttpPost]
        [Route(Router.VersionOne + "/" + Router.User + "/" + Router.Picture.PrefixName + "/" + Router.Picture.DownloadPicture)]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DownloadPicture([FromBody] DownloadPictureRequest request) {
            var route = await pictureManager.GetSoruceRouteForEventAsync(request.EventId);

            if (route == null) {
                return NoContent();
            }

            var result = ftpService.DownloadFile(route, request.FileName);

            if (result == null) {
                return NoContent();
            }
            
            return Ok(result);
        }

        [HttpGet]
        [Route(Router.VersionOne + "/" + Router.User + "/" + Router.Picture.PrefixName + "/" + Router.Picture.GetPictureNamesForEvent)]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetPictureNamesForEvent([FromQuery] int EventId) {

            var route = await pictureManager.GetSoruceRouteForEventAsync(EventId);

            if (route == null) {
                return NoContent();
            }

            var result = await ftpService.ListDirectoryAsync(route);

            if (result == null) {
                return NoContent();
            }

            return Ok(result);
        }
    }
}
