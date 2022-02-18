using AutoMapper;
using CongestionTaxCalculator.Application.Services.Rates.Commands;
using CongestionTaxCalculator.Domain;

namespace CongestionTaxCalculator.Application.Common.Mappers
{
    public class RateMap : Profile
    {
        public RateMap()
        {
            CreateMap<City, CreateCityRatesCommandResult>()
                .ForMember(c => c.CityId, opt => opt.MapFrom(c => c.Id))
                .ForMember(c => c.CityName, opt => opt.MapFrom(c => c.Name))
                .ForMember(c => c.CityCode,
                    opt => opt.MapFrom(c => c.Code));
            //.ForMember(c => c.Rates, opt => opt.MapFrom(c => c.Rates));

            //CreateMap<Rate, CreatedRateCommandResult>()
            //    .ForMember(c => c.Rate, opt => opt.MapFrom(r => r.RateValue))
            //    .ForMember(c => c.Start, opt => opt.MapFrom(r => r.Start))
            //    .ForMember(c => c.End, opt => opt.MapFrom(r => r.End));
        }
    }
}
