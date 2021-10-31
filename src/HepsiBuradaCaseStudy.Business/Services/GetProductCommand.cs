using System.Threading.Tasks;
using Ardalis.GuardClauses;
using HepsiBuradaCaseStudy.Business.Interfaces;
using HepsiBuradaCaseStudy.Business.Services.Interfaces;

namespace HepsiBuradaCaseStudy.Business.Services
{
    public class GetProductCommand : IOperationCommand
    {
        private readonly IProductService productService;
        private string productCode;
        public GetProductCommand(IProductService productService)
        {
            this.productService = productService;
        }

        public string SuccessfulResponseMessage => string.Format("Product {0} info", productCode);

        public async Task<object> ExecuteAsync(string[] parameters)
        {
            Guard.Against.NullOrEmpty(parameters, nameof(parameters));
            productCode = parameters[0];
            return await productService.GetProductByCodeAsync(parameters[0]);
        }
    }
}
