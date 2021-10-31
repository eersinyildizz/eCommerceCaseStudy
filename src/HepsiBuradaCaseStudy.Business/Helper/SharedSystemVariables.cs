using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HepsiBuradaCaseStudy.Business.Helper
{
    public class SharedSystemVariables
    {
        private readonly ConcurrentDictionary<string, int> systemTime = new ConcurrentDictionary<string, int>();

        public ConcurrentDictionary<string, int> SystemTime => systemTime;
    }
}
