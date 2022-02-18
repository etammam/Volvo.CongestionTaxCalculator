using AutoMapper;
using CongestionTaxCalculator.Application.Services.TaxHistories.Commands;
using CongestionTaxCalculator.Application.Services.TaxHistories.Queries;
using CongestionTaxCalculator.Domain;

namespace CongestionTaxCalculator.Application.Common.Mappers
{
    public class TaxMap : Profile
    {
        public TaxMap()
        {
            CreateMap<TaxHistory, CreateNewTaxCommandResult>()
                .ForMember(t => t.CityId, opt => opt.MapFrom(c => c.City.Id))
                .ForMember(t => t.CityName, opt => opt.MapFrom(c => c.City.Name))
                .ForMember(t => t.CityCode, opt => opt.MapFrom(c => c.City.Code))
                .ForMember(t => t.VehicleType, opt => opt.MapFrom(t => t.VehicleType.ToString()));

            CreateMap<TaxHistory, GetTaxHistoryByVehicleQueryResult>()
                .ForMember(t => t.VehicleType, opt => opt.MapFrom(t => t.VehicleType.ToString()))
                .ForMember(t => t.CityName, opt => opt.MapFrom(t => t.City.Name));

            CreateMap<TaxHistory, GetTaxHistoryByCityQueryResult>()
                .ForMember(t => t.VehicleType, opt => opt.MapFrom(t => t.VehicleType.ToString()))
                .ForMember(t => t.CityName, opt => opt.MapFrom(t => t.City.Name));

            //CreateMap<TaxHistory, TaxHistoryList>()
            //    .ForMember(t => t.VehicleType, opt => opt.MapFrom(t => t.VehicleType.ToString()));
        }
    }
}
