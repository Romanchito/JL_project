using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JLFilmApi.Helpers
{
    public sealed class AppSettings
    {
        public string hostPath { get; }
        private AppSettings(string hostPath)
        {
            this.hostPath = hostPath; 
        }       

        private static AppSettings source;

        public static AppSettings Source(string hostPath)
        {
            if (source == null) source = new AppSettings(hostPath);
            return source;
        }
    }
}
