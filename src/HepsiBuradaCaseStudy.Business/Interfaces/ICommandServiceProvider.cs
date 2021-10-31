using HepsiBuradaCaseStudy.Business.Services.Interfaces;

namespace HepsiBuradaCaseStudy.Business.Interfaces
{
    public interface ICommandServiceProvider
    {
        IOperationCommand GetService(string commandName);
    }
}
