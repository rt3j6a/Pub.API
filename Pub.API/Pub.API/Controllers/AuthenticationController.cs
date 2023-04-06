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
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await authenticationManager.LoginAsync(request.LoginName,request.Password);

            if (result.success) {
                var token = authenticationService.GenerateJSONWebToken(request.LoginName);
                return Ok(token);
            }

            return BadRequest(new ErrorResponse { ErrorCode= (int)System.Net.HttpStatusCode.BadRequest, Message=result.message });
        }

        [Route(Router.VersionOne + "/" +Router.Admin+"/"+ Router.Authentication.PrefixName + "/" + Router.Authentication.AddAccount)]
        [HttpPost]
        public async Task<IActionResult> AddAccount([FromBody] AddAccountRequest request)
        {
            var result=await authenticationManager.AddAccountAsync(request.EmailAddress,request.LoginName,request.Password);

            if (result.success) {
                return Ok(result.message);
            }

            return Unauthorized(new ErrorResponse { ErrorCode=(int)System.Net.HttpStatusCode.Unauthorized, Message=result.message});
        }

        [Route(Router.VersionOne + "/" + Router.Admin + "/" + Router.Authentication.PrefixName + "/" + Router.Authentication.TestToken)]
        [HttpPost]
        public  IActionResult TestToken([FromBody] string token) {
            var result= authenticationService.ValidateJSONWebToken(token);

            if (result) {
                return Ok();
            }

            return Unauthorized();
        }
    }
}
