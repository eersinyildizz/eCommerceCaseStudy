using HepsiBuradaCaseStudy.Business.Helper;
using HepsiBuradaCaseStudy.Business.Interfaces;

namespace HepsiBuradaCaseStudy.Business.Services
{
    public class SystemService : ISystemService
    {
        private readonly SharedSystemVariables systemVariables;
        public SystemService(SharedSystemVariables systemVariables)
        {
            this.systemVariables = systemVariables;
        }
        public int GetTimeInHour()
        {
            systemVariables.SystemTime.TryGetValue("Hour",out int hour);
            return hour;
        }

        public int IncreaseTimeInHour(int hour)
        {
            systemVariables.SystemTime.AddOrUpdate("Hour", hour, (key,oldValue) => oldValue + hour);
            return GetTimeInHour();
        }
    }
}
