using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _360Widgets
{
    public class Program
    {
        public static void Main(string[] args)
        {
            EvaluateLogFile(string.Join("\n", args));
            CreateHostBuilder(args).Build().Run();
        }
        public static string EvaluateLogFile(string logContentsStr)
        {
            return ProcessLogFiles.EvaluateLogFile(logContentsStr);
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
