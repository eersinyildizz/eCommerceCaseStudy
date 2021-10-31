using System;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using HepsiBuradaCaseStudy.Business.Interfaces;
using HepsiBuradaCaseStudy.Business.Services.Interfaces;
using HepsiBuradaCaseStudy.Domain.Entities;

namespace HepsiBuradaCaseStudy.Business.Services
{
    public class CreateCampaignCommand : IOperationCommand
    {
        private readonly ICampaignService campaignService;

        public CreateCampaignCommand(ICampaignService campaignService)
        {
            this.campaignService = campaignService;
        }

        public string SuccessfulResponseMessage => "Campaign created";

        public async Task<object> ExecuteAsync(string[] parameters)
        {
            Guard.Against.NullOrEmpty(parameters, nameof(parameters));
            Campaign campaignEntity = new Campaign
            {
                Name = parameters[0],
                ProductCode = parameters[1],
                Duration = Convert.ToInt32(parameters[2]),
                PriceManipulationLimit = Convert.ToInt32(parameters[3]),
                TargetSalesCount = Convert.ToInt32(parameters[4])
            };
            return await campaignService.AddAsync(campaignEntity);
        }
    }
}
