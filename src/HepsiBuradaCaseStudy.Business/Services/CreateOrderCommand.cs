using System;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using HepsiBuradaCaseStudy.Business.Interfaces;
using HepsiBuradaCaseStudy.Business.Services.Interfaces;
using HepsiBuradaCaseStudy.Domain.Entities;

namespace HepsiBuradaCaseStudy.Business.Services
{
    public class CreateOrderCommand : IOperationCommand
    {
        private readonly IOrderService orderService;
        private readonly IProductService productService;
        private readonly ICampaignService campaignService;

        public CreateOrderCommand(IOrderService orderService, IProductService productService, ICampaignService campaignService)
        {
            this.orderService = orderService;
            this.productService = productService;
            this.campaignService = campaignService;
        }

        public string SuccessfulResponseMessage => "Order created";

        public async Task<object> ExecuteAsync(string[] parameters)
        {
            Guard.Against.NullOrEmpty(parameters, nameof(parameters));
            Order orderEntity = new Order
            {
                ProductCode = parameters[0],
                Quantity = Convert.ToInt32(parameters[1])
            };
            var existingOrder = await orderService.GetOrderByProductCodeAsync(orderEntity.ProductCode);
            int orderQuantity = orderEntity.Quantity;
            if(existingOrder != null)
            {
                orderQuantity += existingOrder.Quantity;
            }
            await productService.StockControl(orderEntity.ProductCode, orderQuantity);
            await campaignService.CheckCampaignTargetSalesCount(orderEntity.ProductCode, orderQuantity);
            return await orderService.AddOrUpdateAsync(orderEntity);
        }
    }
}
