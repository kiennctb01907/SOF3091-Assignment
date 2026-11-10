using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace FirstTests;

/// <summary>
/// Y2 - Tu dong hoa luong voi Selenium (phien ban "script-based", truoc khi refactor sang POM o Y4).
/// Locator uu tien id / data-test, KHONG dung XPath dai.
/// </summary>
[TestFixture]
public class LoginTests
{
    private const string BaseUrl = "https://www.saucedemo.com/";

    private IWebDriver driver = null!;

    [SetUp]
    public void SetUp()
    {
        driver = new ChromeDriver();
        driver.Manage().Window.Maximize();
        driver.Navigate().GoToUrl(BaseUrl);
    }

    [TearDown]
    public void TearDown()
    {
        driver.Quit();
        driver.Dispose();
    }

    // Ham dung chung: gom cac buoc dang nhap de cac ca test tai su dung.
    private void DangNhap(string username, string password)
    {
        driver.FindElement(By.Id("user-name")).SendKeys(username);
        driver.FindElement(By.Id("password")).SendKeys(password);
        driver.FindElement(By.Id("login-button")).Click();
    }

    // ---------- Y2 (bat buoc): luong dang nhap ----------

    [Test]
    public void DangNhapHopLe_VaoDuocTrangSanPham()
    {
        DangNhap("standard_user", "secret_sauce");

        Assert.That(driver.Url, Does.Contain("inventory.html"));
        Assert.That(driver.FindElement(By.CssSelector("[data-test='title']")).Text, Is.EqualTo("Products"));
    }

    [Test]
    public void DangNhapSaiMatKhau_HienThongBaoLoi()
    {
        DangNhap("standard_user", "sai_mat_khau");

        string loi = driver.FindElement(By.CssSelector("[data-test='error']")).Text;
        Assert.That(loi, Does.Contain("Username and password do not match any user"));
        Assert.That(driver.Url, Is.EqualTo(BaseUrl));
    }

    [Test]
    public void DangNhapTaiKhoanBiKhoa_HienThongBaoLoi()
    {
        DangNhap("locked_out_user", "secret_sauce");

        string loi = driver.FindElement(By.CssSelector("[data-test='error']")).Text;
        Assert.That(loi, Does.Contain("this user has been locked out"));
    }

    // ---------- Y2 (nang cao): luong thu hai - them san pham vao gio va mo gio hang ----------

    [Test]
    public void ThemSanPhamVaoGio_MoGioHang_ThayDungSanPham()
    {
        DangNhap("standard_user", "secret_sauce");

        driver.FindElement(By.Id("add-to-cart-sauce-labs-backpack")).Click();
        Assert.That(driver.FindElement(By.CssSelector("[data-test='shopping-cart-badge']")).Text, Is.EqualTo("1"));

        driver.FindElement(By.CssSelector("[data-test='shopping-cart-link']")).Click();

        Assert.That(driver.Url, Does.Contain("cart.html"));
        var tenTrongGio = driver
            .FindElements(By.CssSelector("[data-test='inventory-item-name']"))
            .Select(e => e.Text)
            .ToList();
        Assert.That(tenTrongGio, Is.EqualTo(new[] { "Sauce Labs Backpack" }));
    }

    // ---------- Thu thap du lieu san pham (Lab 2 - Phan C) ----------

    [Test]
    public void TrangSanPham_CoDu6SanPham_GiaBatDauBangDauDoLa()
    {
        DangNhap("standard_user", "secret_sauce");

        var items = driver.FindElements(By.CssSelector("[data-test='inventory-item']"));
        Assert.That(items, Has.Count.EqualTo(6));

        var ten = new List<string>();
        foreach (var item in items)
        {
            string name = item.FindElement(By.CssSelector("[data-test='inventory-item-name']")).Text;
            string price = item.FindElement(By.CssSelector("[data-test='inventory-item-price']")).Text;
            TestContext.Out.WriteLine($"{name} - {price}");

            Assert.That(price, Does.StartWith("$"), $"Gia cua '{name}' phai bat dau bang $");
            ten.Add(name);
        }

        Assert.That(ten, Has.None.Empty);
        Assert.That(ten, Does.Contain("Sauce Labs Backpack"));
    }
}
