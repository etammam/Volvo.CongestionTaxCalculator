using CongestionTaxCalculator.Application.Common.Wrappers.Models;
using System.Net;

namespace CongestionTaxCalculator.Application.Common.Wrappers
{
    public class WrappedResult<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Model { get; set; }
        public List<ErrorModel> Errors { get; set; }
    }
}
