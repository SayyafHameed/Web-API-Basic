using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace WebAPIBasic.Controllers
{
    [ApiController]
    [Route("controller")]
    public class ConfigController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IOptions<AttachmentsOptions> attachmentsOptions;

        public ConfigController(IConfiguration configuration, IOptions<AttachmentsOptions> attachmentsOptions)
        {
            _configuration = configuration;
            this.attachmentsOptions = attachmentsOptions;
        }

        [HttpGet]
        [Route("")]
        public ActionResult GetConfig()
        {
            var config = new
            {
                AllowedHosts = _configuration["AllowedHosts"],
                AttachmentsOptions = this.attachmentsOptions.Value
            };
            return Ok(config);
        }
    }
}
