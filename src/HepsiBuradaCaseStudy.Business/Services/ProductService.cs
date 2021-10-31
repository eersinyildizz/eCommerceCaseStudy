using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HepsiBuradaCaseStudy.Business.Helper;
using HepsiBuradaCaseStudy.Business.Interfaces;
using HepsiBuradaCaseStudy.Domain;
using HepsiBuradaCaseStudy.Domain.Entities;
using HepsiBuradaCaseStudy.Domain.Exceptions;
using HepsiBuradaCaseStudy.Infrastructure.Repository;

namespace HepsiBuradaCaseStudy.Business.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly ICampaignRepository campaignRepository;
        private readonly ISystemService systemService;
        public ProductService(IProductRepository productRepository, ICampaignRepository campaignRepository, ISystemService systemService)
        {
            this.productRepository = productRepository;
            this.campaignRepository = campaignRepository;
            this.systemService = systemService;
        }

        public async Task<Product> AddAsync(Product product)
        {
            var productEntity = await GetProductByCodeAsync(product.Code);
            if (productEntity != null)
                throw new BusinessException(string.Format("Product already exists with same code {0}", product.Code));
            await productRepository.AddAsync(product);
            return await GetProductByIdAsync(product.Id);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await productRepository.GetAllAsync();
        }

        public async Task<Product> GetProductByIdAsync(string id)
        {
            return await productRepository.GetByIdAsync(id);
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            var productEntity = await productRepository.FirstOrDefaultAsync(p => p.Code == product.Code);
            productEntity.Price = product.Price;
            productEntity.Stock = product.Stock;
            await productRepository.UpdateAsync(productEntity);
            return await GetProductByIdAsync(product.Id);
        }

        public async Task<Product> GetProductByCodeAsync(string code)
        {
            var product = await productRepository.FirstOrDefaultAsync(p => p.Code == code);
            var campaign = await campaignRepository.FirstOrDefaultAsync(c => c.ProductCode == code && c.Status == Utils.ActiveRecordStatus);
            if (campaign != null)
            {
                product.Price = ProductManager.CalculateProductPriceByCampaign(product, campaign, systemService.GetTimeInHour());
            }
            return product;
        }

        public async Task StockControl(string productCode, int orderQuantity)
        {
            var productEntity = await productRepository.FirstOrDefaultAsync(p => p.Code == productCode);
            if (productEntity.Stock < orderQuantity)
            {
                throw new BusinessException(string.Format("Insufficient stock for {0}", productCode));
            }
        }
    }
}
