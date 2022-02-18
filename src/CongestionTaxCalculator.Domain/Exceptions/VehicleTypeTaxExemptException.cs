using System;

namespace CongestionTaxCalculator.Domain.Exceptions
{
    public class VehicleTypeTaxExemptException : Exception
    {
        public VehicleTypeTaxExemptException(VehicleTypes vehicleTypes)
        : base($"this {vehicleTypes.ToString()} vehicle, is type tax exempt vehicles")
        {
        }
    }
}
