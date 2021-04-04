using AutoMapper;
using Carsales.StockManagement.Models.Contracts;
using Carsales.StockManagement.Models.Entities;
using Carsales.StockManagement.Models.VeiwModels;
using System.Linq;

namespace Carsales.StockManagement.Api.Helpers
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Car, GetCarResponse>();
            CreateMap<GetCarResponse, Car>();
            CreateMap<UpdateCarRequest, Car>();
            CreateMap<CreateCarRequest, Car>();
            CreateMap<Car, GetCarStockResponse>()
                .ForMember(c => c.AvailableStock,
                opt => opt.MapFrom(src => src.Stocks.Select(s => s.AvailableStock).DefaultIfEmpty(0).Sum()));

            CreateMap<UpdateStockRequest, Stock>();
            CreateMap<UpdateStockRequest, UpdateStockRequest>();
            CreateMap<UpdateStockRequest, StockTransaction>();
            CreateMap<Stock, GetStockResponse>();
        }
    }
}
