using HepsiBuradaCaseStudy.Domain.Entities;
using System.Threading.Tasks;

namespace HepsiBuradaCaseStudy.Business.Services.Interfaces
{
    public interface IOperationCommand
    {
        public string SuccessfulResponseMessage { get; }
        Task<object> ExecuteAsync(string[] parameters); 
    }
}
