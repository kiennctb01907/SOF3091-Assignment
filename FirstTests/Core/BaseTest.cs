using System.Diagnostics;
using FirstTests.Config;
using FirstTests.Utilities;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;

namespace FirstTests.Core;

/// <summary>
/// Lop cha cua moi lop test: doc cau hinh, tao trinh duyet, mo trang chu, ghi log va dong trinh duyet.
/// Cac file test KHONG tu viet [SetUp]/[TearDown], new ChromeDriver() hay tu ghi log.
/// driver la field thuong (khong static) nen moi lop test chay song song co trinh duyet rieng.
/// </summary>
public abstract class BaseTest
{
    protected IWebDriver driver = null!;
    protected TestConfig cfg = null!;
    private Stopwatch dongHo = null!;

    [SetUp]
    public void KhoiTaoDriver()
    {
        dongHo = Stopwatch.StartNew();
        cfg = ConfigReader.Load();
        Log.Info($"BAT DAU - trinh duyet {cfg.Browser}, headless={cfg.Headless}");

        driver = DriverFactory.Create(cfg);
        driver.Navigate().GoToUrl(cfg.BaseUrl);
        Log.Info($"Da mo {cfg.BaseUrl}");
    }

    [TearDown]
    public void DongDriver()
    {
        var ketQua = TestContext.CurrentContext.Result;
        try
        {
            if (ketQua.Outcome.Status == TestStatus.Failed)
            {
                Log.Error($"ROT sau {dongHo.Elapsed.TotalSeconds:0.0}s: {ketQua.Message?.Split('\n')[0].Trim()}");
                string? anh = ScreenshotHelper.Chup(driver);
                if (anh != null) Log.Error($"Da chup man hinh: {anh}");
            }
            else
            {
                Log.Info($"OK - xong sau {dongHo.Elapsed.TotalSeconds:0.0}s");
            }
        }
        finally
        {
            // Chup anh loi khong duoc lam cho trinh duyet bi bo lai chay ngam
            driver?.Quit();
            driver?.Dispose();
        }
    }
}
