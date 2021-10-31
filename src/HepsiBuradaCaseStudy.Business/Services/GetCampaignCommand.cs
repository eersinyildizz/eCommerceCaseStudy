using System;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using HepsiBuradaCaseStudy.Business.Interfaces;
using HepsiBuradaCaseStudy.Business.Services.Interfaces;

namespace HepsiBuradaCaseStudy.Business.Services
{
    public class GetCampaignCommand : IOperationCommand
    {
        private readonly ICampaignService campaignService;
        private string campaignName;

        public GetCampaignCommand(ICampaignService campaignService)
        {
            this.campaignService = campaignService;
        }

        public string SuccessfulResponseMessage => string.Format("Campaign {0} info", campaignName);

        public async Task<object> ExecuteAsync(string[] parameters)
        {
            Guard.Against.NullOrEmpty(parameters, nameof(parameters));
            campaignName = parameters[0];
            return await campaignService.GetCampaignByNameAsync(parameters[0]);
        }
    }
}
