using System;
using Microsoft.Owin;
using Owin;
using Serilog;

namespace KevinSharp.Web
{
    public partial class Startup
    {
        private const string defaultOutputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} ({RequestId}) {Message}{NewLine}{Exception}";

        public Startup()
        {
            Log.Logger = new LoggerConfiguration()
			.Enrich.FromLogContext()
#if DNXCORE50
			.WriteTo.TextWriter(Console.Out)
#else
			.WriteTo.ColoredConsole()
			.WriteTo.Trace()
			.WriteTo.Glimpse()
            .WriteTo.RollingFile(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "App_Data\\logs\\log-KevinSharp-{Date}.txt", outputTemplate: defaultOutputTemplate)
#endif
			.CreateLogger();			

            Log.Logger.Information("Logger is configured");
        }
    }
}