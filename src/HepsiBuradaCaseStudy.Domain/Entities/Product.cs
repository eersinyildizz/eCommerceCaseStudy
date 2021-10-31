
namespace HepsiBuradaCaseStudy.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Code { get; set; }

        public int Price { get; set; }

        public int Stock { get; set; }
    }
}
