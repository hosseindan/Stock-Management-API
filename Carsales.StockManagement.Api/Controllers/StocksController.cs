using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Carsales.StockManagement.Models.Contracts;
using Carsales.StockManagement.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Carsales.StockManagement.Api.Controllers
{
    [Route("api/[controller]/{dealerId}")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly IStockService _stockService;
        public StocksController(IStockService stockService)
        {
            _stockService = stockService;
        }

        /// <summary>
        /// Used to get stock level of a car
        /// </summary>
        /// <param name="dealerId"> Id of dealer</param>
        /// <param name="carId">Id of car</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("cars/{carId}/stocks", Name = "GetForCar")]
        public async Task<IActionResult> Get(Guid dealerId, Guid carId)
        {
            var stock = await _stockService.GetStocksForDealerAndCarAsync(dealerId, carId);
            return Ok(stock);
        }
        /// <summary>
        /// Used to get list of cars and stock levels for the specified dealer
        /// </summary>
        /// <param name="dealerId"> Id of dealer </param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("Stocks", Name = "GetForDealer")]
        public async Task<IActionResult> GetForDealer(Guid dealerId)
        {
            var stocks = await _stockService.GetStocksForDealerAsync(dealerId);
            return Ok(stocks);
        }

        /// <summary>
        /// Used to update car stock level
        /// </summary>
        /// <param name="dealerId">Id of dealer</param>
        /// <param name="request">request details</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("")]
        public async Task<IActionResult> Put(Guid dealerId, [FromBody] UpdateStockRequest request)
        {
            await _stockService.UpdateAvailableStock(dealerId, request);
            return Ok();
        }
    }
}
