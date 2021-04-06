using AutoMapper;
using Carsales.StockManagement.Models.Contracts;
using Carsales.StockManagement.Models.Entities;
using Carsales.StockManagement.Models.VeiwModels;
using Carsales.StockManagement.Repository;
using Carsales.StockManagement.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Carsales.StockManagement.Services
{
    public class StockService : IStockService
    {
        private readonly IMapper _mapper;
        private readonly IStockRepository _stockRepository;
        private readonly ICarRepository _carRepository;
        public StockService(IStockRepository stockRepository, IMapper mapper,
            ICarRepository carRepository)
        {
            _mapper = mapper;
            _stockRepository = stockRepository;
            _carRepository = carRepository;
        }
        public async Task<GetStockResponse> GetAsync(Guid id)
        {
            var stock = await _stockRepository.GetAsync(id);
            var stockLevel = _mapper.Map<GetStockResponse>(stock);
            return stockLevel;
        }
        public async Task<List<GetCarStockResponse>> GetStocksForDealerAsync(Guid dealerId)
        {
            var stock = await _stockRepository.GetStocksForDealerAsync(dealerId);
            var stockLevel = _mapper.Map<List<GetCarStockResponse>>(stock);
            return stockLevel;
        }
        public async Task<GetCarStockResponse> GetStocksForDealerAndCarAsync(Guid dealerId, Guid carId)
        {
            var stock = await _stockRepository.GetStocksForDealerAndCarAsync(dealerId, carId);
            var stockLevel = _mapper.Map<GetCarStockResponse>(stock);
            return stockLevel;
        }
        public async Task UpdateAvailableStock(Guid dealerId, UpdateStockRequest request)
        {
            var car = await _carRepository.GetAsync(request.CarId);
            if (car == null)
                throw new KeyNotFoundException("There is no car with the given car Id");
            //update and save stock level in DB
            var stock = await _stockRepository.GetStocksForDealerAndCarAsync(dealerId, request.CarId);

            if (stock == null)
            {
                stock = new Stock()
                {
                    Id = Guid.NewGuid(),
                    CarId = request.CarId,
                    DealerId = dealerId,
                    AvailableStock = request.Quantity
                };
                await _stockRepository.InsertAsync(stock);
            }
            else
            {
                stock.AvailableStock = request.Quantity;
                _stockRepository.Update(stock);
            }
            await _stockRepository.SaveAsync();
        }
        private void ProcessStockAvailability(Stock currentStockSnapshot, StockTransaction newTransaction)
        {
            currentStockSnapshot.AvailableStock = newTransaction.TransactionType switch
            {
                TransactionType.Increase => currentStockSnapshot.AvailableStock + newTransaction.Quantity,
                TransactionType.Decrease => currentStockSnapshot.AvailableStock - newTransaction.Quantity,
                _ => throw new NotImplementedException()
            };
        }
    }
}
