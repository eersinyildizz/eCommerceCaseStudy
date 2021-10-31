using HepsiBuradaCaseStudy.Domain;
using HepsiBuradaCaseStudy.Domain.Entities;
using HepsiBuradaCaseStudy.Infrastructure.Data.Context;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HepsiBuradaCaseStudy.Infrastructure.Repository
{
    public class CampaignRepository : MongoRepository<Campaign>, ICampaignRepository
    {
        public CampaignRepository(ICoreContext context) : base(context)
        {
        }

        public async Task<List<Campaign>> GetActiveCampaigns()
        {
            return await FetchAsync(c => c.Status == Utils.ActiveRecordStatus);
        }
    }
}
