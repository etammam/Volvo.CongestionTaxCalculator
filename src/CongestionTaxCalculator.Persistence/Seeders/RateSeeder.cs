using CongestionTaxCalculator.Domain;

namespace CongestionTaxCalculator.Persistence.Seeders
{
    public class RateSeeder
    {
        public static List<Rate> GetValues()
        {
            return new List<Rate>()
            {
                new Rate(Guid.Parse("0d9fb9c8-1f74-44b2-b8ae-287235dc5af1"),
                    new TimeOnly(6, 0),
                    new TimeOnly(6, 29), 8,
                    Guid.Parse("9d315725-c8a0-45e1-9a55-fb480a477ab9")),

                new Rate(Guid.Parse("1c9c1143-0ad8-4f8e-b9c9-bdb65df92de5"),
                    new TimeOnly(6, 30),
                    new TimeOnly(6, 59), 13,
                    Guid.Parse("9d315725-c8a0-45e1-9a55-fb480a477ab9")),

                new Rate(Guid.Parse("0842e0e6-c6d0-46a5-a3ec-294fe3a743bb"),
                    new TimeOnly(7, 0),
                    new TimeOnly(7, 59), 18,
                    Guid.Parse("9d315725-c8a0-45e1-9a55-fb480a477ab9")),

                new Rate(Guid.Parse("87b12225-9606-415e-a447-1067a6d37cac"),
                    new TimeOnly(8, 0),
                    new TimeOnly(8, 29), 13,
                    Guid.Parse("9d315725-c8a0-45e1-9a55-fb480a477ab9")),

                new Rate(Guid.Parse("12858360-07a6-4bea-9a51-0dcb42667577"),
                    new TimeOnly(8, 30),
                    new TimeOnly(14, 59), 8,
                    Guid.Parse("9d315725-c8a0-45e1-9a55-fb480a477ab9")),

                new Rate(Guid.Parse("eb43daa0-9026-4821-92fd-8ee42dfbacf3"),
                    new TimeOnly(15, 0),
                    new TimeOnly(15, 29), 13,
                    Guid.Parse("9d315725-c8a0-45e1-9a55-fb480a477ab9")),

                new Rate(Guid.Parse("40369fa8-313b-4acf-99be-d3516f36f2b1"),
                    new TimeOnly(15, 30),
                    new TimeOnly(16, 59), 18,
                    Guid.Parse("9d315725-c8a0-45e1-9a55-fb480a477ab9")),

                new Rate(Guid.Parse("02c23596-fda9-42e8-9a6e-40fe59a86a1d"),
                    new TimeOnly(17, 0),
                    new TimeOnly(17, 59), 13,
                    Guid.Parse("9d315725-c8a0-45e1-9a55-fb480a477ab9")),

                new Rate(Guid.Parse("b61927b8-cc15-4732-8d87-dda1f5d9c9cc"),
                    new TimeOnly(18, 0),
                    new TimeOnly(18, 29), 8,
                    Guid.Parse("9d315725-c8a0-45e1-9a55-fb480a477ab9")),

                new Rate(Guid.Parse("167926a7-4fd1-403a-a0c4-b053a3498dfe"),
                    new TimeOnly(18, 30),
                    new TimeOnly(5, 59), 0,
                    Guid.Parse("9d315725-c8a0-45e1-9a55-fb480a477ab9")),

            };
        }
    }
}
