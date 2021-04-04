using AutoMapper;
using Carsales.StockManagement.Models.Contracts;
using Carsales.StockManagement.Models.Entities;
using Carsales.StockManagement.Repository;
using Carsales.StockManagement.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Carsales.StockManagement.Services
{
    public class StockService : IStockService
    {
        private readonly IStockTransactionRepository _stockTransactionRepository;
        private readonly IMapper _mapper;
        private readonly IStockRepository _stockRepository;
        public StockService(IStockTransactionRepository stockTransactionRepository,
            IStockRepository stockRepository, IMapper mapper)
        {
            _stockTransactionRepository = stockTransactionRepository;
            _mapper = mapper;
            _stockRepository = stockRepository;
        }
        public async Task<GetStockResponse> GetAsync(Guid id)
        {
            var stock = await _stockRepository.GetAsync(id);
            var stockLevel = _mapper.Map<GetStockResponse>(stock);
            return stockLevel;
        }
        public async Task<List<GetStockResponse>> GetStocksForDealerAsync(Guid dealerId)
        {
            var stock = await _stockRepository.GetStocksForDealerAsync(dealerId);
            var stockLevel = _mapper.Map<List<GetStockResponse>>(stock);
            return stockLevel;
        }
        public async Task<GetStockResponse> GetStocksForDealerAndCarAsync(Guid dealerId, Guid carId)
        {
            var stock = await _stockRepository.GetStocksForDealerAndCarAsync(dealerId, carId);
            var stockLevel = _mapper.Map<GetStockResponse>(stock);
            return stockLevel;
        }
        public async Task UpdateAvailableStock(Guid dealerId, UpdateStockRequest request)
        {
            //save transaction record in DB
            var transaction = _mapper.Map<StockTransaction>(request);
            transaction.DealerId = dealerId;
            transaction.Id = Guid.NewGuid();
            await _stockTransactionRepository.InsertAsync(transaction);

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
                ProcessStockAvailability(stock, transaction);
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
