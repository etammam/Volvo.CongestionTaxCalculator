using System;

namespace CongestionTaxCalculator.Domain.Exceptions
{
    public class TaxRateCanNotBeGreaterThan60Exception : Exception
    {
        public TaxRateCanNotBeGreaterThan60Exception()
        : base("rate value must be less or equal than 60 SKE")
        {

        }
    }
}
