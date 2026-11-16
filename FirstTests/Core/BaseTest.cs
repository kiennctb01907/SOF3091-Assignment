using FirstTests.Config;
using FirstTests.Utilities;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;

namespace FirstTests.Core;

/// <summary>
/// Lop cha cua moi lop test: doc cau hinh, tao trinh duyet, mo trang chu va dong trinh duyet.
/// Cac file test KHONG tu viet [SetUp]/[TearDown] hay new ChromeDriver() nua.
/// </summary>
public abstract class BaseTest
{
    protected IWebDriver driver = null!;
    protected TestConfig cfg = null!;

    [SetUp]
    public void KhoiTaoDriver()
    {
        cfg = ConfigReader.Load();
        driver = DriverFactory.Create(cfg);
        driver.Navigate().GoToUrl(cfg.BaseUrl);
    }

    [TearDown]
    public void DongDriver()
    {
        try
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
                ScreenshotHelper.Chup(driver);
        }
        finally
        {
            // Chup anh loi khong duoc lam cho trinh duyet bi bo lai chay ngam
            driver?.Quit();
            driver?.Dispose();
        }
    }
}
