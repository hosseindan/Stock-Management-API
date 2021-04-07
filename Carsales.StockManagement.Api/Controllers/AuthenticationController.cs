using Carsales.StockManagement.Models;
using Carsales.StockManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Carsales.StockManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;
        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }
        /// <summary>
        /// Used to get authorization token
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> Get()
        {
            var token = _userService.GetToken(new AuthenticationModel() { Username = "dealer1", Password = "123" });
            if (string.IsNullOrEmpty(token))
                return Unauthorized();

            return Ok(token);
        }
        /// <summary>
        /// Used to login and get authorization token
        /// </summary>
        /// <returns></returns>
        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> Login(AuthenticationModel authenticationModel)
        {
            if(string.IsNullOrEmpty(authenticationModel.Username) || string.IsNullOrEmpty(authenticationModel.Password))
                return BadRequest();
            var token = _userService.GetToken(authenticationModel);
            if (string.IsNullOrEmpty(token))
                return NotFound();

            return Ok(token);
        }
    }
}
