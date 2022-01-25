using GoldShop.Infrastructure.Dtos;
using GoldShop.Infrastructure.IShopServices;
using GoldShop.Infrastructure.ShopServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace GoldShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersController : ControllerBase
    {
        private readonly IUsersServices _IUsersServices;
        private IConfiguration _config;

        public UsersController(IUsersServices IUsersServices, IConfiguration config)
        {
            _IUsersServices = IUsersServices;
            _config = config;
        }

        // User Login Method()
        [AllowAnonymous]
        [HttpPost(nameof(Login))]
        [ProducesResponseType(typeof(UsersDto), (int)System.Net.HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)System.Net.HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Username or password is incorrect" });
            var userDetails = await _IUsersServices.LoginUser(dto);
            if (userDetails != null)
            {
                var genrateToken = new JwtToken();
                var tokenString = genrateToken.BuildToken(_config);
                return Ok(new { userDetails, token = tokenString });
            }
            else
                return BadRequest(new { message = "Username or password is incorrect" });

        }

        // User Gold Discount calculator Method()
        [HttpPost(nameof(DiscountCalculator))]
        [ProducesResponseType(typeof(int), (int)System.Net.HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)System.Net.HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DiscountCalculator([FromBody] GoldCalculatorDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Gold Price and Gold Weight must be greater then 0" });
            
            var calculatedValue = await _IUsersServices.GoldDiscountCalculator(dto);
            if (calculatedValue > 0)
                return Ok(calculatedValue);
            return BadRequest(new { message = "Gold Price and  Gold Weight must be greater than 0 " });
        }
    }
}
