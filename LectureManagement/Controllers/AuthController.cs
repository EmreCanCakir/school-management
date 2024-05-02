using Contracts;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using LectureManagement.Consumer;

namespace LectureManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController: ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public AuthController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromQuery] String userName, [FromQuery] String password)
        {
            await _publishEndpoint.Publish(
                new AuthTokenCreateEvent 
                { 
                    UserName = userName, Password = password 
                }, new CancellationToken());
            var token = AuthTokenGeneratedConsumer.Token;

            return Ok(new { Token = token });
        }
    }
}
