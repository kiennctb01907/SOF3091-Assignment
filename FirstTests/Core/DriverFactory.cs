using FirstTests.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace FirstTests.Core;

/// <summary>
/// Tao trinh duyet theo cau hinh. Ham Create tra ve IWebDriver chu khong phai ChromeDriver,
/// nen doi trinh duyet chi can sua chu "Browser" trong appsettings.json.
/// </summary>
public static class DriverFactory
{
    public static IWebDriver Create(TestConfig cfg) => cfg.Browser.Trim().ToLowerInvariant() switch
    {
        "chrome" => new ChromeDriver(ChromeOpts(cfg)),
        "edge" => new EdgeDriver(EdgeOpts(cfg)),
        "firefox" => new FirefoxDriver(FirefoxOpts(cfg)),
        _ => throw new ArgumentException(
            $"Trinh duyet khong ho tro: '{cfg.Browser}'. Chi dung duoc: chrome | edge | firefox")
    };

    private static ChromeOptions ChromeOpts(TestConfig cfg)
    {
        var o = new ChromeOptions();
        if (cfg.Headless) o.AddArgument("--headless=new");
        o.AddArgument($"--window-size={cfg.WindowWidth},{cfg.WindowHeight}");
        o.AddArgument("--disable-notifications");
        return o;
    }

    private static EdgeOptions EdgeOpts(TestConfig cfg)
    {
        var o = new EdgeOptions();
        if (cfg.Headless) o.AddArgument("--headless=new");
        o.AddArgument($"--window-size={cfg.WindowWidth},{cfg.WindowHeight}");
        return o;
    }

    private static FirefoxOptions FirefoxOpts(TestConfig cfg)
    {
        var o = new FirefoxOptions();
        if (cfg.Headless) o.AddArgument("--headless");   // Firefox khong co "=new"
        o.AddArgument($"--width={cfg.WindowWidth}");
        o.AddArgument($"--height={cfg.WindowHeight}");
        return o;
    }
}
