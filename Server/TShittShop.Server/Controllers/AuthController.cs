using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TShirtShop.Server.Security;
using TShirtShop.Services.Auth;
using TShirtShop.Services.Users;

namespace TShirtShop.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly JwtTokenProvider tokenProvider;
        private readonly IUsersService userService;

        public AuthController(JwtTokenProvider tokenProvider, IUsersService userService)
        {
            this.tokenProvider = tokenProvider;
            this.userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseModel>> Login([FromBody]LoginRequestModel model)
        {
            var result = await userService.PasswordSignInAsync(model.Email, model.Password);
            if (result.Succeeded)
            {
                var user = result.Data;
                var token = tokenProvider.GenerateToken(model.Email, user.Id, new[] { user.Role });

                return new LoginResponseModel()
                {
                    Token = token
                };
            }

            return Unauthorized(result.Errors);
        }
    }
}