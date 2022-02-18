using System.Net;
using CongestionTaxCalculator.Application.Common.Wrappers.Models;

namespace CongestionTaxCalculator.Application.Common.Wrappers
{
    public class ValidationFilterOutputResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Model { get; set; }
        public List<Validations> Errors { get; set; }
    }
}
