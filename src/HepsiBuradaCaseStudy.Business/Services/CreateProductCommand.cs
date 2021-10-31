using System;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using HepsiBuradaCaseStudy.Business.Interfaces;
using HepsiBuradaCaseStudy.Business.Services.Interfaces;
using HepsiBuradaCaseStudy.Domain.Entities;

namespace HepsiBuradaCaseStudy.Business.Services
{
    public class CreateProductCommand : IOperationCommand
    {
        private readonly IProductService productService;

        public CreateProductCommand(IProductService productService)
        {
            this.productService = productService;
        }

        public string SuccessfulResponseMessage => "Product created";

        public async Task<object> ExecuteAsync(string[] parameters)
        {
            Guard.Against.NullOrEmpty(parameters, nameof(parameters));
            Product productEntity = new Product
            {
                Code = parameters[0],
                Price = Convert.ToInt32(parameters[1]),
                Stock = Convert.ToInt32(parameters[2])
            };
            return await productService.AddAsync(productEntity);
        }
    }
}
