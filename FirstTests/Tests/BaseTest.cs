using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework.Interfaces;

namespace FirstTests.Tests;

/// <summary>
/// Lop co so cho moi fixture test: khoi tao / dong driver mot cho duy nhat (DRY)
/// va tu chup anh man hinh khi test that bai.
/// </summary>
public abstract class BaseTest
{
    protected IWebDriver driver = null!;

    [SetUp]
    public void KhoiTaoDriver()
    {
        driver = new ChromeDriver();
        driver.Manage().Window.Maximize();
    }

    [TearDown]
    public void DongDriver()
    {
        // Chi chup anh khi test that bai, de truy vet loi.
        if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
        {
            string dir = Path.Combine(TestContext.CurrentContext.WorkDirectory, "failed-screenshots");
            Directory.CreateDirectory(dir);
            string path = Path.Combine(dir, TestContext.CurrentContext.Test.Name + ".png");
            ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(path);
            TestContext.AddTestAttachment(path);
        }

        driver.Quit();
        driver.Dispose();
    }
}
