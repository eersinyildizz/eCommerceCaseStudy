using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HepsiBuradaCaseStudy.Business.Interfaces;
using HepsiBuradaCaseStudy.Business.Services.Interfaces;
using HepsiBuradaCaseStudy.Domain;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;

namespace HepsiBuradaCaseStudy.Business.Services
{
    public class ScenarioFileService : IScenarioFileService
    {
        private readonly ICommandServiceProvider commandServiceProvider;
        private readonly IHostingEnvironment hostingEnvironment;
        public ScenarioFileService(ICommandServiceProvider commandServiceProvider, IHostingEnvironment hostingEnvironment)
        {
            this.commandServiceProvider = commandServiceProvider;
            this.hostingEnvironment = hostingEnvironment;
        }
        public async Task<string> ReadFileAsync()
        {
            string path = Path.Combine(hostingEnvironment.WebRootPath, "txt", Utils.ScenarioFileName);
            var fileStr = await File.ReadAllLinesAsync(path);
            return await ExecuteFileCommands(fileStr);
        }

        private async Task<string> ExecuteFileCommands(string[] scenarioLines)
        {
            StringBuilder sb = new StringBuilder();
            foreach(string scenarioLine in scenarioLines)
            {
                var scenarioContent = scenarioLine.Split(Utils.BlankSeperator);
                string commandStr = scenarioContent[0];
                string[] parameters = scenarioContent.Skip(1).ToArray();
                IOperationCommand operationCommandService = commandServiceProvider.GetService(commandStr);
                var response = await operationCommandService.ExecuteAsync(parameters);
                sb.Append(string.Format("Command : {0} | Output : {1}; {2}", scenarioLine, operationCommandService.SuccessfulResponseMessage,JsonConvert.SerializeObject(response)));
                sb.AppendLine();
            }
            return sb.ToString();
        }
        
    }
}
