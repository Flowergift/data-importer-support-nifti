using System;
using System.IO;
using Microsoft.Extensions.Configuration;

public sealed class ConfigurationHelper
{
    private static readonly Lazy<IConfiguration> _configuration = new Lazy<IConfiguration>(() =>
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        return builder.Build();
    });

    private ConfigurationHelper() { }

    public static IConfiguration Configuration => _configuration.Value;
}
