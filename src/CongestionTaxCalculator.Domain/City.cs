using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;

namespace CongestionTaxCalculator.Domain
{
    public class City
    {
        public City()
        {
        }

        public City(Guid id, string name, int code)
        : this(name, code)
        {
            Id = id;
        }
        public City(Guid id, string name, int code, Ignore ignore)
                : this(name, code, ignore)
        {
            Id = id;
        }

        public City(string name, int code)
        {
            Id = Guid.NewGuid();
            SetName(name);
            SetCode(code);
        }

        public City(string name, int code, Ignore ignore)
        {
            Id = Guid.NewGuid();
            SetName(name);
            SetCode(code);
            SetIgnore(ignore);
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public int Code { get; private set; }
        public ICollection<Rate> Rates { get; set; }
        public ICollection<TaxHistory> TaxHistories { get; set; }
        public ICollection<TaxPayment> TaxPayments { get; set; }
        public Ignore Ignore { get; private set; }

        public City SetRates(List<Rate> rates)
        {
            Guard.Against.Null(rates);
            Rates = rates;
            return this;
        }

        public City SetName(string name)
        {
            Guard.Against.NullOrEmpty(name, nameof(name));
            Name = name;
            return this;
        }

        public City SetCode(int code)
        {
            Guard.Against.NegativeOrZero(code, nameof(code));
            Code = code;
            return this;
        }

        public City SetIgnore(Ignore ignore)
        {
            if (ignore is { })
            {
                Ignore = ignore;
            }

            return this;
        }
    }
}
