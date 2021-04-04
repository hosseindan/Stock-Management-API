using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Carsales.StockManagement.Models;
using Carsales.StockManagement.Models.VeiwModels;
using Carsales.StockManagement.Services;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Carsales.StockManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _carService;
        private readonly IValidator<CreateCarRequest> _createCarValidator;
        /// <summary>
        /// Used to manage cars
        /// </summary>
        /// <param name="carService"></param>
        /// <param name="createCarValidator"> Used to validate incoming requests to create cars</param>
        public CarsController(ICarService carService,
            IValidator<CreateCarRequest> createCarValidator)
        {
            _carService = carService;
            _createCarValidator = createCarValidator;
        }

        /// <summary>
        ///  Used to create a new car
        /// </summary>
        /// <param name="car"> car details to be created</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]

        public async Task<IActionResult> Post([FromBody] CreateCarRequest car)
        {
            var request = _createCarValidator.Validate(car);
            if (!request.IsValid)
                return BadRequest(request.Errors.Select(e => new { Field = e.PropertyName, Error = e.ErrorMessage }));
            await _carService.InsertAsync(car);
            return Ok();
        }

        /// <summary>
        /// Used to delete the specified car
        /// </summary>
        /// <param name="id"> id of car </param>
        /// <returns>
        /// If the specified car could be found then 200 is returned
        /// If the specified car could be found but there is some stock transactions for it  then 400 is retuened
        /// If the specified car could not be found then 404 is retuened
        /// </returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _carService.DeleteAsync(id);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (InvalidOperationException)
            {
                return BadRequest("there is a stock transaction that prevents this car from being deleted.");
            }
        }

        /// <summary>
        /// Used to get cars and their stock levels for the given dealer id
        /// </summary>
        /// <param name="dealerId"> Id of dealer</param>
        /// <returns>
        /// list of <see cref="GetCarStockResponse"/>
        /// are returned with stock levels of the given dealer
        /// </returns>
        [HttpGet("{dealerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<GetCarStockResponse>>> GetCarsAndStockLevels(Guid dealerId)
        {
            return Ok(await _carService.GetCarsAndStockLevelsAsync(dealerId));
        }

        /// <summary>
        ///  Used to find and get list of cars which are matched with given search criteria
        /// </summary>
        /// <param name="carSearchCriteria"> </param>
        /// <remarks>
        /// Sample response:
        ///     [
        ///         {
        ///           "Id": "a6d2b4b1-42c4-4da9-9514-1e2e56f38da1",
        ///           "Make": "Toyota",
        ///           "Model":"Rav4"
        ///           "Year": "200"
        ///         },
        ///     ]
        /// </remarks>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<GetCarResponse>>> Search([FromQuery]CarSearchCriteria carSearchCriteria)
        {
            return Ok(await _carService.GetAsync(carSearchCriteria));
        }

    }
}
