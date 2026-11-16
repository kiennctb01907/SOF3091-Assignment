using FirstTests.Core;

namespace FirstTests.Tests;

/// <summary>
/// Y1 - Test dau tien: kiem tra tieu de trang saucedemo.
/// Tu Y5 viec mo trinh duyet va vao BaseUrl do BaseTest lam (doc tu appsettings.json).
/// </summary>
[TestFixture]
public class FirstTest : BaseTest
{
    [Test]
    public void MoTrangSauceDemo_TieuDeLaSwagLabs()
    {
        string title = driver.Title;
        TestContext.Out.WriteLine($"Tieu de trang: {title}");

        Assert.That(title, Is.EqualTo("Swag Labs"));
    }
}
