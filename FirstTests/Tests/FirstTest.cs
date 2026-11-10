namespace FirstTests.Tests;

/// <summary>
/// Y1 - Test dau tien: mo saucedemo.com va kiem tra tieu de trang.
/// </summary>
[TestFixture]
public class FirstTest : BaseTest
{
    [Test]
    public void MoTrangSauceDemo_TieuDeLaSwagLabs()
    {
        driver.Navigate().GoToUrl("https://www.saucedemo.com/");

        string title = driver.Title;
        TestContext.Out.WriteLine($"Tieu de trang: {title}");

        Assert.That(title, Is.EqualTo("Swag Labs"));
    }
}
