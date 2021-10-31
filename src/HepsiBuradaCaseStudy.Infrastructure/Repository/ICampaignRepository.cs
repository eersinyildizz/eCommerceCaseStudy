using HepsiBuradaCaseStudy.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HepsiBuradaCaseStudy.Infrastructure.Repository
{
    public interface ICampaignRepository : IRepository<Campaign>
    {
        Task<List<Campaign>> GetActiveCampaigns();
    }
}
