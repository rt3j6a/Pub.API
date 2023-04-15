using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pub.API.Model.Request;
using Pub.API.Service.Ftp;
using Pub.API.Service.Smtp;
using System.Buffers.Text;
using System.ComponentModel.DataAnnotations;

namespace Pub.API.Controllers {

    [ApiController]


    public class TestController : ControllerBase {

        private readonly SmtpService smtpService;
        private readonly FtpHandler ftpHandler;

        public TestController(IConfiguration configuration)
        {
            smtpService = new SmtpService(configuration);
            ftpHandler = new FtpHandler(configuration);
        }

        [HttpGet]
        [Route("Test/TestSMTP")]
        public async Task<IActionResult> TestStmp() {

            await smtpService.SendAssignmentHandledEmail("csigahunter@gmail.com", "Teszt Csapat", true) ;

            await smtpService.SendAssignmentAfterEmail("csigahunter@gmail.com", "Teszt Csapat");

            return Ok();
        }


        [HttpPost]
        [Route("Test/TestFTP")]
        public async Task<IActionResult> TestFTP([FromBody] TestFtpRequest request) {

            //byte[] data = Convert.FromBase64String(request.data);
            //await ftpHandler.UploadFileAsync(data, "ftp://sftp.nethely.hu/test/", "testimage.jpeg");

            //var result = await ftpHandler.DownloadFileAsync("ftp://sftp.nethely.hu/test/", "testimage.jpeg");

            await ftpHandler.ListDirectoryAsync(null);

            return Ok();
        }
    }
}
