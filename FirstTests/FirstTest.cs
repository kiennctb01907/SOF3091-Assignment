using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace FirstTests;

/// <summary>
/// Y1 - Test dau tien: mo saucedemo.com va kiem tra tieu de trang.
/// </summary>
[TestFixture]
public class FirstTest
{
    private IWebDriver driver = null!;

    [SetUp]
    public void SetUp()
    {
        driver = new ChromeDriver();
        driver.Manage().Window.Maximize();
    }

    [TearDown]
    public void TearDown()
    {
        driver.Quit();
        driver.Dispose();
    }

    [Test]
    public void MoTrangSauceDemo_TieuDeLaSwagLabs()
    {
        driver.Navigate().GoToUrl("https://www.saucedemo.com/");

        string title = driver.Title;
        TestContext.Out.WriteLine($"Tieu de trang: {title}");

        Assert.That(title, Is.EqualTo("Swag Labs"));
    }
}
