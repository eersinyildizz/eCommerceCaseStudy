using HepsiBuradaCaseStudy.Business.Interfaces;
using HepsiBuradaCaseStudy.Business.Services;
using HepsiBuradaCaseStudy.Infrastructure.Data.Context;
using HepsiBuradaCaseStudy.Infrastructure.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Autofac;
using Ardalis.GuardClauses;
using HepsiBuradaCaseStudy.Business.Services.Interfaces;
using HepsiBuradaCaseStudy.Business.Helper;

namespace HepsiBuradaCaseStudy.Api.Modules
{
    public class HepsiBuradaCaseStudyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            Guard.Against.Null(builder, nameof(builder));
            builder.RegisterType<ProductService>().As<IProductService>();
            builder.RegisterType<CampaignService>().As<ICampaignService>();
            builder.RegisterType<OrderService>().As<IOrderService>();
            builder.RegisterType<ScenarioFileService>().As<IScenarioFileService>();
            builder.RegisterType<SystemService>().As<ISystemService>();
            builder.RegisterType<CoreContext>().As<ICoreContext>();
            builder.RegisterType<ProductRepository>().As<IProductRepository>();
            builder.RegisterType<OrderRepository>().As<IOrderRepository>();
            builder.RegisterType<CampaignRepository>().As<ICampaignRepository>();
            builder.RegisterType<AutofacCommandServiceProvider>().As<ICommandServiceProvider>();
            builder.RegisterType<SharedSystemVariables>().SingleInstance();
            RegisterCommandServices(builder);
        }

        private static void RegisterCommandServices(ContainerBuilder builder)
        {
            builder.RegisterType<CreateProductCommand>().AsImplementedInterfaces().Keyed<IOperationCommand>("create_product");
            builder.RegisterType<GetProductCommand>().AsImplementedInterfaces().Keyed<IOperationCommand>("get_product_info");
            builder.RegisterType<CreateOrderCommand>().AsImplementedInterfaces().Keyed<IOperationCommand>("create_order");
            builder.RegisterType<CreateCampaignCommand>().AsImplementedInterfaces().Keyed<IOperationCommand>("create_campaign");
            builder.RegisterType<GetCampaignCommand>().AsImplementedInterfaces().Keyed<IOperationCommand>("get_campaign_info");
            builder.RegisterType<IncreaseTimeCommand>().AsImplementedInterfaces().Keyed<IOperationCommand>("increase_time");
        }
    }
}