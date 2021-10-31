
namespace HepsiBuradaCaseStudy.Domain.Entities
{
    public class Campaign : BaseEntity
    {
        public string Name { get; set; }

        public string ProductCode { get; set; }

        public int Duration { get; set; }

        public int PriceManipulationLimit { get; set; }

        public int TargetSalesCount { get; set; }

        public string Status { get; set; } = Utils.ActiveRecordStatus;
    }
}
