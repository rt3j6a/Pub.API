using Microsoft.AspNetCore.Mvc;
using Pub.API.Common;
using Pub.API.Model.Request;
using Pub.API.Model.Response;
using Pub.API.Service.Authentication;
using Pub.Core.Interface;
using Pub.Core.Manager;

namespace Pub.API.Controllers
{
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationManager authenticationManager;
        private readonly AuthenticationService authenticationService;
        public AuthenticationController(IServiceScopeFactory scopeFactory, IConfiguration configuration)
        {
            authenticationManager = new AuthenticationManager(configuration, scopeFactory);
            authenticationService=new AuthenticationService(configuration);
        }

        [Route(Router.VersionOne + "/" +Router.Admin+"/"+ Router.Authentication.PrefixName + "/" + Router.Authentication.Login)]
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK,type:typeof(LoginResponse))]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest,type:typeof(ErrorResponse))]
        public async Task<IActionResult> Login([FromBody] LoginRequest request) {
            var result = await authenticationManager.LoginAsync(request.LoginName,request.Password);

            if (result.success) {
                var token = authenticationService.GenerateJSONWebToken(request.LoginName);
                return Ok(new LoginResponse { Message = result.message ,AccessToken=token});

            }

            return BadRequest(new ErrorResponse { ErrorCode= (int)System.Net.HttpStatusCode.BadRequest, Message=result.message });
        }

        [Route(Router.VersionOne + "/" +Router.Admin+"/"+ Router.Authentication.PrefixName + "/" + Router.Authentication.AddAccount)]
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(SuccessResponse))]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ErrorResponse))]
        public async Task<IActionResult> AddAccount([FromQuery]string token,[FromBody] AddAccountRequest request)
        {
            var auth = authenticationService.ValidateJSONWebToken(token);

            if (!auth) return Unauthorized();

            var result=await authenticationManager.AddAccountAsync(request.EmailAddress,request.LoginName,request.Password);

            if (result.success) {
                return Ok(new SuccessResponse { Message = result.message });
            }

            return BadRequest(new ErrorResponse { ErrorCode=(int)System.Net.HttpStatusCode.Unauthorized, Message=result.message});
        }

        [Route(Router.VersionOne + "/" + Router.Admin + "/" + Router.Authentication.PrefixName + "/" + Router.Authentication.TestToken)]
        [HttpGet]
        public  IActionResult TestToken([FromBody] string token) {
            var result= authenticationService.ValidateJSONWebToken(token);

            if (result) {
                return Ok();
            }

            return Unauthorized();
        }
    }
}
