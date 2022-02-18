using System;

namespace CongestionTaxCalculator.Domain.Exceptions
{
    public class VehicleIdNullOrEmptyException : Exception
    {
        public VehicleIdNullOrEmptyException()
        : base("vehicleId can't be null or empty")
        {
        }
    }
}
