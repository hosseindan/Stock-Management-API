using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mime;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Carsales.StockManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using static Carsales.StockManagement.Api.Controllers.AuthenticationController;

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
        ///  Used to get authorization token
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]

        public async Task<IActionResult> Get( )
        {
            var token = _userService.GetToken(new Models.AuthenticationModel() { Username = "Hossein1", Password = "123" });
            if (string.IsNullOrEmpty(token))
                return Unauthorized();

            return Ok(token);
        }
    }
}
