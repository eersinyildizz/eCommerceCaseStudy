using System.Collections.Generic;
using System.Threading.Tasks;
using HepsiBuradaCaseStudy.Domain.Dtos;
using HepsiBuradaCaseStudy.Domain.Entities;

namespace HepsiBuradaCaseStudy.Business.Interfaces
{
    public interface ICampaignService
    {
        Task<CampaignDto> GetCampaignByNameAsync(string name);

        Task<Campaign> FindCampaignByNameAsync(string name);

        Task<Campaign> GetCampaignByProductCodeAsync(string productCode);

        Task<IEnumerable<Campaign>> GetAllAsync();

        Task<Campaign> GetCampaignByIdAsync(string id);

        Task<Campaign> AddAsync(Campaign campaign);

        Task<Campaign> UpdateAsync(Campaign campaign);

        Task CheckCampaignsDuration(int systemTimeHour);

        Task CheckCampaignTargetSalesCount(string productCode, int orderQuantity);
    }
}
