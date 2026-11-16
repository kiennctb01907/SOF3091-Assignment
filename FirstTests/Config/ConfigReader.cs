using Microsoft.Extensions.Configuration;

namespace FirstTests.Config;

/// <summary>
/// Doc appsettings.json mot lan duy nhat cho ca phien chay.
/// Dung Lazy de an toan khi nhieu luong cung goi Load() (chay song song o Y6).
/// </summary>
public static class ConfigReader
{
    private static readonly Lazy<TestConfig> cache = new(DocFile);

    public static TestConfig Load() => cache.Value;

    private static TestConfig DocFile()
    {
        var config = new ConfigurationBuilder()
            // AppContext.BaseDirectory luon tro vao bin/Debug/net8.0 du chay bang terminal hay Test Explorer
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("Config/appsettings.json", optional: false)
            // Cho phep ghi de khi chay, vi du: $env:TEST_Browser="edge"; dotnet test
            .AddEnvironmentVariables("TEST_")
            .Build();

        var cfg = config.Get<TestConfig>()
                  ?? throw new InvalidOperationException("Khong doc duoc Config/appsettings.json (kiem tra CopyToOutputDirectory).");

        if (string.IsNullOrWhiteSpace(cfg.BaseUrl))
            throw new InvalidOperationException("BaseUrl trong appsettings.json dang de trong.");

        return cfg;
    }
}
