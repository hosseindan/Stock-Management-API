using Carsales.StockManagement.Models.Contracts;
using Carsales.StockManagement.Models.VeiwModels;
using Carsales.StockManagement.Services.Interfaces;
using Carsales.StockManagement.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Carsales.StockManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class StocksController : ControllerBase
    {
        private readonly IStockService _stockService;
        /// <summary>
        /// Used to manage stock level
        /// </summary>
        /// <param name="stockService"></param>
        public StocksController(IStockService stockService)
        {
            _stockService = stockService;
        }

        /// <summary>
        /// Used to get stock level of a car
        /// </summary>
        /// <param name="carId">Id of car</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("cars/{carId}", Name = "GetForCar")]
        public async Task<ActionResult<GetStockResponse>> Get(Guid carId)
        {
            var dealerId = this.User.GetDealerId();
            if (dealerId == Guid.Empty)
                return Unauthorized();
            var stock = await _stockService.GetStocksForDealerAndCarAsync(dealerId, carId);
            return Ok(stock);
        }

        /// <summary>
        /// Used to update car stock level
        /// </summary>
        /// <param name="request">request details</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("")]
        public async Task<IActionResult> Put([FromBody] UpdateStockRequest request)
        {
            var dealerId = this.User.GetDealerId();
            if (dealerId == Guid.Empty)
                return Unauthorized();
            try
            {
                await _stockService.UpdateAvailableStock(dealerId, request);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("There is no car with the given car Id");
            }
            return Ok();
        }

        /// <summary>
        /// Used to get cars and their stock levels
        /// </summary>
        /// <returns>
        /// list of <see cref="GetCarStockResponse"/>
        /// are returned with stock levels of the given dealer
        /// </returns>
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<GetCarStockResponse>>> GetCarsAndStockLevels()
        {
            var dealerId = this.User.GetDealerId();
            if (dealerId == Guid.Empty)
                return Unauthorized();
            return Ok(await _stockService.GetStocksForDealerAsync(dealerId));
        }
    }
}
