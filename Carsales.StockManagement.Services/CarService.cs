using AutoMapper;
using Carsales.StockManagement.Models;
using Carsales.StockManagement.Models.Entities;
using Carsales.StockManagement.Models.VeiwModels;
using Carsales.StockManagement.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Carsales.StockManagement.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;
        public CarService(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }
        public async Task DeleteAsync(Guid id)
        {
            var car = await _carRepository.GetAsync(id);
            if (car == null)
                throw new KeyNotFoundException();
            if(!await _carRepository.IsDeletable(id))
                throw new InvalidOperationException("");
            _carRepository.Delete(car);
            await _carRepository.SaveAsync();
        }
        public async Task<GetCarResponse> GetAsync(Guid id)
        {
            var car = await _carRepository.GetAsync(id);
            var carDto = _mapper.Map<GetCarResponse>(car);
            return carDto;
        }
        public async Task InsertAsync(CreateCarRequest model)
        {
            var car = _mapper.Map<Car>(model);
            await _carRepository.InsertAsync(car);
            await _carRepository.SaveAsync();
        }
        public async Task UpdateAsync(Guid id, UpdateCarRequest model)
        {
            var car = await _carRepository.GetAsync(id);
            if (car == null)
                throw new KeyNotFoundException();
            car = _mapper.Map<UpdateCarRequest, Car>(model, car);
            _carRepository.Update(car);
            await _carRepository.SaveAsync();
        }
        public async Task<List<GetCarStockResponse>> GetAsync(Guid dealerId, CarSearchCriteria searchCriteria)
        {
            var cars = await _carRepository.GetAsync(dealerId ,p => (string.IsNullOrEmpty(searchCriteria.Make) || p.Make.Contains(searchCriteria.Make))
            && (string.IsNullOrEmpty(searchCriteria.Model) || p.Make.Contains(searchCriteria.Model)));
            var carsDto = _mapper.Map<List<GetCarStockResponse>>(cars);
            return carsDto;
        }
        public async Task<List<GetCarStockResponse>> GetCarsAndStockLevelsAsync(Guid dealerId)
        {
            var cars = await _carRepository.GetCarsAndStockLevelsAsync(dealerId);
            var carsDto = _mapper.Map<List<GetCarStockResponse>>(cars);
            return carsDto;
        }
    }
}
