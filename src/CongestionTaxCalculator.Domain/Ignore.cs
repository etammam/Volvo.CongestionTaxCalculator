using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json;

namespace CongestionTaxCalculator.Domain
{
    public class Ignore
    {
        public Ignore()
        {
        }
        public Ignore(List<string> months, int daysBeforeHoliday)
        {
            SetMonths(months);
            SetDaysBeforeHoliday(daysBeforeHoliday);
        }
        public Ignore(Guid id, List<string> months, int daysBeforeHoliday)
        {
            Id = id;
            SetMonths(months);
            SetDaysBeforeHoliday(daysBeforeHoliday);
        }

        public Guid Id { get; set; }

        [NotMapped]
        public List<string> Months
        {
            get => string.IsNullOrEmpty(Month) == false ? JsonSerializer.Deserialize<List<string>>(Month) : new List<string>();
            set
            {
                if (value.Any())
                {
                    Month = JsonSerializer.Serialize(value);
                }
            }
        }

        public string Month { get; set; }

        public int DaysBeforeHoliday { get; set; }

        public City City { get; set; }

        public Ignore SetMonths(List<string> months)
        {
            Months = months;
            return this;
        }

        public Ignore SetDaysBeforeHoliday(int daysBeforeHoliday)
        {
            DaysBeforeHoliday = daysBeforeHoliday;
            return this;
        }
    }
}
