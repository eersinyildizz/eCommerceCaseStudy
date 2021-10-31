
using Ardalis.GuardClauses;
using HepsiBuradaCaseStudy.Domain.Entities;
using System;

namespace HepsiBuradaCaseStudy.Business.Helper
{
    public static class ProductManager
    {
        private const int Percentage = 100;
        private const decimal QuarterPercentage = 0.25m;
        public static int CalculateProductPriceByCampaign(Product product, Campaign campaign, int systemTime)
        {
            Guard.Against.Null(product, nameof(product));
            Guard.Against.Null(campaign, nameof(campaign));
            int price;
            Random random = new Random();
            decimal durationRate = Math.Round((decimal)systemTime / (decimal)campaign.Duration,2);
            int minAvailableProductPrice = Convert.ToInt32((decimal)product.Price * (decimal)(Percentage - campaign.PriceManipulationLimit) / 100);
            int maxAvailableProductPrice = Convert.ToInt32((decimal)product.Price * (decimal)(Percentage + campaign.PriceManipulationLimit) / 100);
            if (durationRate < QuarterPercentage)
            {
                int minRandomValue = maxAvailableProductPrice - Convert.ToInt32((maxAvailableProductPrice - minAvailableProductPrice) * ( durationRate));
                price = random.Next(minRandomValue, maxAvailableProductPrice);

            }
            else if(durationRate >= QuarterPercentage && durationRate < QuarterPercentage * 3)
            {
                int maxRandomValue = minAvailableProductPrice + Convert.ToInt32((product.Price - minAvailableProductPrice) * (1 - durationRate));
                price = random.Next(minAvailableProductPrice, maxRandomValue);
            }
            else
            {
                int minRandomValue = product.Price - Convert.ToInt32(((product.Price - minAvailableProductPrice) / 2) * (durationRate));
                price = random.Next(minRandomValue, product.Price);
            }
            return price;
        }
    }
}
