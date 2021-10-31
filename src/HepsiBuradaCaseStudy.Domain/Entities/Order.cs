
namespace HepsiBuradaCaseStudy.Domain.Entities
{
    public class Order : BaseEntity
    {
        public string ProductCode { get; set; }

        public int Quantity { get; set; }
    }
}
