using AutoMapper;
using CongestionTaxCalculator.Application.Services.Cities.Commands;
using CongestionTaxCalculator.Application.Services.Cities.Queries;
using CongestionTaxCalculator.Domain;

namespace CongestionTaxCalculator.Application.Common.Mappers
{
    public class CityMap : Profile
    {
        public CityMap()
        {
            CreateMap<City, CreateNewCityCommandResult>()
                .ForMember(c => c.IgnoredMonths, opt => opt.MapFrom(c => c.Ignore.Months))
                .ForMember(c => c.IgnoredDaysBeforeHoliday, opt => opt.MapFrom(c => c.Ignore.DaysBeforeHoliday));
            CreateMap<City, GetCitiesQueryResult>()
                .ForMember(c => c.IgnoredMonths, opt => opt.MapFrom(c => c.Ignore.Months))
                .ForMember(c => c.IgnoredDaysBeforeHoliday, opt => opt.MapFrom(c => c.Ignore.DaysBeforeHoliday));
            CreateMap<City, UpdateCityCommandResult>()
                .ForMember(c => c.IgnoredMonths, opt => opt.MapFrom(c => c.Ignore.Months))
                .ForMember(c => c.IgnoredDaysBeforeHoliday, opt => opt.MapFrom(c => c.Ignore.DaysBeforeHoliday));
        }
    }
}
