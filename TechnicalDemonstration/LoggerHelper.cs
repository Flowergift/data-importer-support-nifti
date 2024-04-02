using Serilog;
using System;

namespace Medica.Uk.TechnicalDemonstration.Logging
{
    public sealed class LoggerHelper
    {
        private static readonly Lazy<ILogger> _lazyLogger = new Lazy<ILogger>(() =>
        {
            return new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(ConfigurationHelper.Configuration["logger:FilePath"], rollingInterval: RollingInterval.Day)
                .CreateLogger();
        });

        private LoggerHelper() { }

        public static ILogger Logger => _lazyLogger.Value;
    }
}