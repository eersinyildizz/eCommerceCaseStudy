using Ardalis.GuardClauses;
using Autofac.Features.Indexed;
using HepsiBuradaCaseStudy.Business.Interfaces;
using HepsiBuradaCaseStudy.Business.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;

namespace HepsiBuradaCaseStudy.Business.Services
{
    public class AutofacCommandServiceProvider : ICommandServiceProvider
    {
        private readonly IIndex<string, IOperationCommand> commandService;
        private readonly ILogger<AutofacCommandServiceProvider> logger;
        public AutofacCommandServiceProvider(IIndex<string, IOperationCommand> commandService, ILogger<AutofacCommandServiceProvider> logger)
        {
            this.commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IOperationCommand GetService(string commandName)
        {
            logger.LogInformation("{commandName} service is creating", commandName);
            Guard.Against.NullOrEmpty(commandName, nameof(commandName));
            if (!commandService.TryGetValue(commandName, out IOperationCommand operationCommandService))
            {
                throw new NotImplementedException($"{commandName} service not implemented!");
            }
            logger.LogInformation("{commandName} service is created", commandName);
            return operationCommandService;
        }
    }
}
