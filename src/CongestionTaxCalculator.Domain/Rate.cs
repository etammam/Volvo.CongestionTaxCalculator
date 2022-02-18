using CongestionTaxCalculator.Domain.Exceptions;
using System;

namespace CongestionTaxCalculator.Domain
{
    public class Rate
    {
        public Rate()
        {
            Id = Guid.NewGuid();
        }
        public Rate(TimeOnly start, TimeOnly end, decimal rateValue)
            : this(Guid.NewGuid(), start, end, rateValue)
        {
        }
        public Rate(TimeOnly start, TimeOnly end, decimal rateValue, Guid cityId)
        : this(Guid.NewGuid(), start, end, rateValue)
        {
            SetCityId(cityId);
        }

        public Rate(Guid id, TimeOnly start, TimeOnly end, decimal rateValue, Guid cityId)
        : this(id, start, end, rateValue)
        {
            SetCityId(cityId);
        }

        public Rate(Guid id, TimeOnly start, TimeOnly end, decimal rateValue)
        {
            Id = id;
            SetStartTime(start);
            SetEndTime(end);
            SetRateValue(rateValue);
        }

        public Guid Id { get; set; }
        public TimeOnly Start { get; private set; }
        public TimeOnly End { get; private set; }
        public decimal RateValue { get; private set; }
        public Guid CityId { get; private set; }
        public City City { get; set; }

        public Rate SetStartTime(TimeOnly start)
        {
            Start = start;
            return this;
        }

        public Rate SetEndTime(TimeOnly end)
        {
            End = end;
            return this;
        }

        public Rate SetRateValue(decimal rateValue)
        {
            if (rateValue > 60)
                throw new TaxRateCanNotBeGreaterThan60Exception();
            RateValue = rateValue;
            return this;

        }

        public Rate SetCityId(Guid cityId)
        {
            CityId = cityId;
            return this;
        }
    }
}
