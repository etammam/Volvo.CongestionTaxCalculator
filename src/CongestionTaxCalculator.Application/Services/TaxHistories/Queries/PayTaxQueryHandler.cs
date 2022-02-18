using CongestionTaxCalculator.Application.Common.Wrappers;
using CongestionTaxCalculator.Application.Constants;
using CongestionTaxCalculator.Domain;
using CongestionTaxCalculator.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CongestionTaxCalculator.Application.Services.TaxHistories.Queries
{
    public class PayTaxQueryHandler : IRequestHandler<GetTaxQuery, WrappedResult<GetTaxQueryResult>>
    {
        private readonly CongestionTaxCalculatorContext _context;
        public PayTaxQueryHandler(CongestionTaxCalculatorContext context)
        {
            _context = context;
        }

        public async Task<WrappedResult<GetTaxQueryResult>> Handle(GetTaxQuery request, CancellationToken cancellationToken)
        {
            var taxHistory = await _context.TaxHistories
                .Include(t => t.City)
                .ThenInclude(t => t.Ignore)
                .Where(t => t.VehicleId == request.VehicleId)
                .ToListAsync(cancellationToken);

            var grouped = taxHistory.GroupBy(t => t.Issued.Date);
            var taxHistoryGroupedResponse = new List<TaxHistoryGroupedResponse>();
            foreach (var group in grouped)
            {
                var histories = new List<TaxHistoryResponse>();
                var historyList = new List<TaxHistory>();
                foreach (var tax in group)
                {
                    historyList.Add(tax);
                    histories.Add(new TaxHistoryResponse(tax.Id, new TimeOnly(tax.Issued.Hour, tax.Issued.Minute), tax.TaxCost, tax.VehicleType.ToString()));
                }

                taxHistoryGroupedResponse.Add(new TaxHistoryGroupedResponse(group.First().City.Id, group.First().City.Name, group.Key, CalculateTotal(historyList), histories));
            }

            return new WrappedResult<GetTaxQueryResult>()
            {
                Success = true,
                Message = ResponseMessages.OperationComplete,
                StatusCode = HttpStatusCode.OK,
                Model = new GetTaxQueryResult()
                {
                    VehicleId = request.VehicleId,
                    Histories = taxHistoryGroupedResponse
                }
            };
        }

        private static decimal CalculateTotal(IReadOnlyCollection<TaxHistory> taxHistories)
        {
            var isIn60Minutes = Math.Abs(taxHistories.Sum(d => d.Issued.Minute) - 60) > 0;
            return isIn60Minutes
                ? taxHistories.OrderByDescending(t => t.TaxCost).First().TaxCost
                : taxHistories.Sum(t => t.TaxCost);
        }
    }
}
