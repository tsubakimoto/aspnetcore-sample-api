using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace AspNetCoreSampleApi.Controllers
{
    [Route("[controller]")] // caution!!
    [ApiController]
    public class ClaimsController : ControllerBase
    {
        private readonly ILogger<ClaimsController> logger;

        public ClaimsController(ILogger<ClaimsController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var headers = HttpContext.Request.Headers;
            var authorizationHeader = headers["Authorization"];
            var tokenString = authorizationHeader.ToString().Replace("Bearer ", string.Empty);
            var token = new JwtSecurityTokenHandler().ReadJwtToken(tokenString);
            var name = token.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
            var surname = token.Claims.FirstOrDefault(c => c.Type == "family_name")?.Value;
            var givenName = token.Claims.FirstOrDefault(c => c.Type == "given_name")?.Value;

            return Ok(new
            {
                name,
                surname,
                givenName
            });
        }
    }
}