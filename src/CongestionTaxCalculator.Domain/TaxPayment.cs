using CongestionTaxCalculator.Domain.Exceptions;
using System;

namespace CongestionTaxCalculator.Domain
{
    public class TaxPayment
    {
        public TaxPayment()
        {
        }

        public TaxPayment(Guid id, DateTime issued, decimal amount, string vehicleId, Guid cityId)
        {
            Id = id;
            SetIssued(issued);
            SetVehicleId(vehicleId);
            SetAmount(amount);
            SetCityId(cityId);
        }

        public TaxPayment(DateTime issued, decimal amount, string vehicleId, Guid cityId)
            : this(Guid.NewGuid(), issued, amount, vehicleId, cityId)
        {
        }

        public Guid Id { get; set; }
        public DateTime Issued { get; private set; }
        public decimal Amount { get; private set; }
        public string VehicleId { get; private set; }

        public Guid CityId { get; set; }
        public City City { get; set; }

        public TaxPayment SetIssued(DateTime issued)
        {
            Issued = issued;
            return this;
        }

        public TaxPayment SetAmount(decimal amount)
        {
            if (amount > 60)
                throw new TaxRateCanNotBeGreaterThan60Exception();
            Amount = amount;
            return this;
        }

        public TaxPayment SetVehicleId(string vehicleId)
        {
            if (!string.IsNullOrEmpty(vehicleId))
            {
                VehicleId = vehicleId;
                return this;
            }
            throw new VehicleIdNullOrEmptyException();
        }

        public TaxPayment SetCityId(Guid cityId)
        {
            CityId = cityId;
            return this;
        }
    }
}
