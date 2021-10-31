using System.Threading.Tasks;

namespace HepsiBuradaCaseStudy.Business.Interfaces
{
    public interface IScenarioFileService
    {
        Task<string> ReadFileAsync();
    }
}
