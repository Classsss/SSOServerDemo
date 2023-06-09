using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace ClassHubSSO.Server.Controllers
{
    [ApiController]
    [Route("api/token/refresh")]
    public class TokenRefreshController : ControllerBase
    {
        [HttpGet]
        public Task<IActionResult> RefreshToken([FromQuery] int user_id, [FromQuery] string refreshToken)
        {
            string cacheConnection = "classhub-sso-chache-cheap.redis.cache.windows.net:6380,password=67jYcaIgYIAFqYLuyeOaNxarFsLNZUO74AzCaDSl6uo=,ssl=True,abortConnect=False";
            ConnectionMultiplexer connection = ConnectionMultiplexer.Connect(cacheConnection);
            IDatabase cache = connection.GetDatabase();
            string savedToken = cache.StringGet(user_id + "_rtoken");

            if (refreshToken == savedToken) {
                Console.WriteLine("토큰 갱신");
            }

            return Task.FromResult<IActionResult>(Ok(false));
        }
    };
}