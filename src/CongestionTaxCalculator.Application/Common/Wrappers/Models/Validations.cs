using FluentValidation;

namespace CongestionTaxCalculator.Application.Common.Wrappers.Models
{
    public class Validations
    {
        public string Property { get; set; }
        public List<ValidationProperty> Validation { get; set; }
    }

    public class ValidationProperty
    {
        public Severity Severity { get; set; }
        public string Message { get; set; }
        public string ErrorCode { get; set; }
        public object AttemptedValue { get; set; }
    }
}
