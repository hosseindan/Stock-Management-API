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
        private readonly IValidator<UpdateCarRequest> _updateCarValidator;
        /// <summary>
        /// Used to manage cars
        /// </summary>
        /// <param name="carService"></param>
        /// <param name="createCarValidator"></param>
        /// <param name="updateCarValidator"></param>
        public CarsController(ICarService carService,
            IValidator<CreateCarRequest> createCarValidator,
            IValidator<UpdateCarRequest> updateCarValidator)
        {
            _carService = carService;
            _createCarValidator = createCarValidator;
            _updateCarValidator = updateCarValidator;
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
        [Description("Used to create a new car")]
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
        /// <param name="id"></param>
        /// <returns></returns>
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
                return BadRequest("there is a transaction that prevents this car from being deleted.");
            }
        }

        /// <summary>
        /// Used to get cars and their stock levels for the given dealer id
        /// </summary>
        /// <param name="dealerId"> Id of dealer</param>
        /// <returns>
        /// If stock levels can be found which are assigned to the dealer then list of <see cref="GetCarStockResponse"/>
        /// with AvailableStock more than zero are returned
        /// If no stock level can be found, then list of cars are returned with AvailableStock of zero
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
        public async Task<ActionResult<List<GetCarResponse>>> Search(CarSearchCriteria carSearchCriteria)
        {
            return Ok(await _carService.GetAsync(carSearchCriteria));
        }

        /// <summary>
        /// Used to get a car that matches the specified id
        /// </summary>
        /// <param name="id"></param>
        /// <remarks>
        /// Sample response:
        ///
        ///     {
        ///       "Id": "a6d2b4b1-42c4-4da9-9514-1e2e56f38da1",
        ///       "Make": "Toyota",
        ///       "Model":"Rav4"
        ///       "Year": "200"
        ///     }
        /// </remarks>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<GetCarResponse>> Get(Guid id)
        {
            var car = await _carService.GetAsync(id);
            if (car == null)
                return NotFound();
            return Ok();
        }
    }
}
