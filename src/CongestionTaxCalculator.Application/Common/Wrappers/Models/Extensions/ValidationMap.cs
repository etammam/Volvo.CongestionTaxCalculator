using FluentValidation;

namespace CongestionTaxCalculator.Application.Common.Wrappers.Models.Extensions
{
    public static class ValidationMap
    {
        public static List<Validations> Map(this ValidationException errors)
        {
            var result = new List<Validations>();
            var exceptions = errors.Errors;
            var errorList = exceptions.GroupBy(d => d.PropertyName, (key, g) => new
            {
                PropertyName = key,
                Errors = g.ToList()
            });
            foreach (var error in errorList)
            {
                result.Add(new Validations()
                {
                    Property = error.PropertyName,
                    Validation = error.Errors.Select(d=> new ValidationProperty()
                    {
                        ErrorCode = d.ErrorCode,
                        Message = d.ErrorMessage,
                        Severity = d.Severity,
                        AttemptedValue = d.AttemptedValue
                    }).ToList()
                });
            }
            return result;
        }
    }
}
