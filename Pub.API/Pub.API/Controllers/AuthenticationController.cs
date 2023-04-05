using Microsoft.AspNetCore.Mvc;
using Pub.API.Common;

namespace Pub.API.Controllers
{
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        [Route(Router.VersionOne + "/" +Router.Admin+"/"+ Router.Authentication.PrefixName + "/" + Router.Authentication.Login)]
        [HttpPost]
        public async Task<IActionResult> Login()
        {
            return Ok();
        }

        [Route(Router.VersionOne + "/" + Router.Authentication.PrefixName + "/" + Router.Authentication.TestToken)]
        [HttpPost]
        public IActionResult TestToken()
        {
            return Ok();

        }
        [Route(Router.VersionOne + "/" + Router.Authentication.PrefixName + "/" + Router.Authentication.AddAccount)]
        [HttpPost]
        public async Task<IActionResult> AddAccount()
        {
            return Ok();
        }
    }
}
