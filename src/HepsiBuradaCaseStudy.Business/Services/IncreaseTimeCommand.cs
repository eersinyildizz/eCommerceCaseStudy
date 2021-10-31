using System;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using HepsiBuradaCaseStudy.Business.Interfaces;
using HepsiBuradaCaseStudy.Business.Services.Interfaces;

namespace HepsiBuradaCaseStudy.Business.Services
{
    public class IncreaseTimeCommand : IOperationCommand
    {
        private readonly ISystemService systemService;
        private readonly ICampaignService campaignService;
        private int systemTimeHour;

        public IncreaseTimeCommand(ISystemService systemService, ICampaignService campaignService)
        {
            this.systemService = systemService;
            this.campaignService = campaignService;
        }

        public string SuccessfulResponseMessage => string.Format("Time is {0}", systemTimeHour);

        public async Task<object> ExecuteAsync(string[] parameters)
        {
            Guard.Against.NullOrEmpty(parameters, nameof(parameters));
            int hour = systemService.IncreaseTimeInHour(Convert.ToInt32(parameters[0]));
            systemTimeHour = hour;
            var systemTime = new
            {
                Hour = hour
            };
            await campaignService.CheckCampaignsDuration(hour);
            return systemTime;
        }
    }
}
