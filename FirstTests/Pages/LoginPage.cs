using OpenQA.Selenium;

namespace FirstTests.Pages;

/// <summary>Trang dang nhap saucedemo. Locator chi nam o day (private readonly By).</summary>
public class LoginPage : BasePage
{
    public const string Url = "https://www.saucedemo.com/";

    // ===== Locator: day la NOI DUY NHAT khai bao locator cua trang dang nhap =====
    private readonly By oTenDangNhap = By.Id("user-name");
    private readonly By oMatKhau = By.Id("password");
    private readonly By nutDangNhap = By.CssSelector("[data-test='login-button']");
    private readonly By thongBaoLoi = By.CssSelector("[data-test='error']");

    public LoginPage(IWebDriver driver) : base(driver) { }

    public LoginPage MoTrang()
    {
        driver.Navigate().GoToUrl(Url);
        return this;
    }

    /// <summary>Dang nhap va chuyen sang trang san pham.</summary>
    public InventoryPage DangNhap(string tenDangNhap, string matKhau)
    {
        NhapThongTinVaBam(tenDangNhap, matKhau);
        return new InventoryPage(driver);
    }

    /// <summary>Dang nhap that bai: o lai trang dang nhap.</summary>
    public LoginPage DangNhapKhongThanhCong(string tenDangNhap, string matKhau)
    {
        NhapThongTinVaBam(tenDangNhap, matKhau);
        return this;
    }

    public string LayThongBaoLoi() => LayChu(thongBaoLoi);

    private void NhapThongTinVaBam(string tenDangNhap, string matKhau)
    {
        Nhap(oTenDangNhap, tenDangNhap);
        Nhap(oMatKhau, matKhau);
        Bam(nutDangNhap);
    }
}
