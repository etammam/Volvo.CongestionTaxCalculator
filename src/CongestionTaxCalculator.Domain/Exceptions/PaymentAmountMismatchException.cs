using System;

namespace CongestionTaxCalculator.Domain.Exceptions
{
    public class PaymentAmountMismatchException : Exception
    {
        public PaymentAmountMismatchException() : base("Payment don't match required tax value")
        {
        }
    }
}
