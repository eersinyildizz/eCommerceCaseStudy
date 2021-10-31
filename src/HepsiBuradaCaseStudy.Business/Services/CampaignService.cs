using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using HepsiBuradaCaseStudy.Business.Interfaces;
using HepsiBuradaCaseStudy.Domain;
using HepsiBuradaCaseStudy.Domain.Dtos;
using HepsiBuradaCaseStudy.Domain.Entities;
using HepsiBuradaCaseStudy.Domain.Exceptions;
using HepsiBuradaCaseStudy.Infrastructure.Repository;

namespace HepsiBuradaCaseStudy.Business.Services
{
    public class CampaignService : ICampaignService
    {
        private readonly ICampaignRepository campaignRepository;
        private readonly IProductRepository productRepository;
        private readonly IOrderRepository orderRepository;

        public CampaignService(ICampaignRepository campaignRepository, IProductRepository productRepository, IOrderRepository orderRepository)
        {
            this.campaignRepository = campaignRepository;
            this.productRepository = productRepository;
            this.orderRepository = orderRepository;
        }

        public async Task<Campaign> AddAsync(Campaign campaign)
        {
            var campaignEntity = await FindCampaignByNameAsync(campaign.Name);
            if (campaignEntity != null)
                throw new BusinessException(string.Format("Campaign already exists with same name {0}", campaign.Name));
            await campaignRepository.AddAsync(campaign);
            return await GetCampaignByIdAsync(campaign.Id);
        }

        public async Task<IEnumerable<Campaign>> GetAllAsync()
        {
            return await campaignRepository.GetAllAsync();
        }

        public async Task<Campaign> GetCampaignByIdAsync(string id)
        {
            return await campaignRepository.GetByIdAsync(id);
        }

        public async Task<Campaign> UpdateAsync(Campaign campaign)
        {
            var campaignEntity = await FindCampaignByNameAsync(campaign.Name);
            campaignEntity.Duration = campaign.Duration;
            campaignEntity.PriceManipulationLimit = campaign.PriceManipulationLimit;
            campaignEntity.ProductCode = campaign.ProductCode;
            campaignEntity.TargetSalesCount = campaign.TargetSalesCount;
            await campaignRepository.UpdateAsync(campaignEntity);
            return await GetCampaignByIdAsync(campaignEntity.Id);
        }

        public async Task<Campaign> FindCampaignByNameAsync(string name)
        {
            return await campaignRepository.FirstOrDefaultAsync(p => p.Name == name);
        }

        public async Task CheckCampaignsDuration(int systemTimeHour)
        {
            var campaigns = await campaignRepository.GetActiveCampaigns();
            foreach(var campaign in campaigns.Where(c=> c.Duration <= systemTimeHour))
            {
                await UpdateCampaignStatusAsPassiveAsync(campaign);
            }
        }

        private async Task UpdateCampaignStatusAsPassiveAsync(Campaign campaign)
        {
            campaign.Status = Utils.PassiveRecordStatus;
            await campaignRepository.UpdateAsync(campaign);
        }

        public async Task CheckCampaignTargetSalesCount(string productCode, int orderQuantity)
        {
            var campaigns = await campaignRepository.GetActiveCampaigns();
            if(campaigns != null)
            {
                foreach (var campaign in campaigns.Where(c => c.ProductCode == productCode && c.TargetSalesCount <= orderQuantity))
                {
                    await UpdateCampaignStatusAsPassiveAsync(campaign);
                }
            }
        }

        public async Task<Campaign> GetCampaignByProductCodeAsync(string productCode)
        {
            return await campaignRepository.FirstOrDefaultAsync(c=> c.ProductCode == productCode && c.Status == Utils.ActiveRecordStatus);
        }

        public async Task<CampaignDto> GetCampaignByNameAsync(string name)
        {
            var campaignEntity = await campaignRepository.FirstOrDefaultAsync(p => p.Name == name);
            Guard.Against.Null(campaignEntity, nameof(campaignEntity));
            var productEntity = await productRepository.FirstOrDefaultAsync(p => p.Code == campaignEntity.ProductCode);
            Guard.Against.Null(productEntity, nameof(productEntity));
            var orderEntity = await orderRepository.FirstOrDefaultAsync(o => o.ProductCode == campaignEntity.ProductCode);
            Guard.Against.Null(orderEntity, nameof(orderEntity));
            var campaignDto = new CampaignDto
            {
                AverageItemPrice = productEntity.Price,
                Name = campaignEntity.Name,
                ProductCode = productEntity.Code,
                Status = campaignEntity.Status,
                TargetSalesCount = campaignEntity.TargetSalesCount,
                TotalSalesCount = orderEntity.Quantity,
                Turnover = (campaignEntity.TargetSalesCount - orderEntity.Quantity) * productEntity.Price
            };
            return campaignDto;
        }
    }
}
