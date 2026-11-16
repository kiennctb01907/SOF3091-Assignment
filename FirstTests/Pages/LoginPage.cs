using OpenQA.Selenium;

namespace FirstTests.Pages;

/// <summary>Trang dang nhap saucedemo. Locator chi nam o day (private readonly By).</summary>
public class LoginPage : BasePage
{
    // ===== Locator: day la NOI DUY NHAT khai bao locator cua trang dang nhap =====
    private readonly By oTenDangNhap = By.Id("user-name");
    private readonly By oMatKhau = By.Id("password");
    private readonly By nutDangNhap = By.CssSelector("[data-test='login-button']");
    private readonly By thongBaoLoi = By.CssSelector("[data-test='error']");

    public LoginPage(IWebDriver driver) : base(driver) { }

    public LoginPage MoTrang()
    {
        driver.Navigate().GoToUrl(cfg.BaseUrl);
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

    /// <summary>Co thong bao loi dang hien tren trang khong (khong cho, khong nem loi).</summary>
    public bool CoThongBaoLoi() => driver.FindElements(thongBaoLoi).Any(e => e.Displayed);

    private void NhapThongTinVaBam(string tenDangNhap, string matKhau)
    {
        Nhap(oTenDangNhap, tenDangNhap);
        Nhap(oMatKhau, matKhau);
        Bam(nutDangNhap);
        ChoKetQuaDangNhap();
    }

    /// <summary>
    /// Sau khi bam Login thi cho mot trong hai: hien thong bao loi, hoac da roi khoi trang dang nhap
    /// (nut Login khong con). Nho vay ca dung lan ca sai deu khong phai cho het timeout.
    /// </summary>
    private void ChoKetQuaDangNhap()
    {
        try
        {
            wait.Until(d => d.FindElements(thongBaoLoi).Any(e => e.Displayed) || d.FindElements(nutDangNhap).Count == 0);
        }
        catch (WebDriverTimeoutException)
        {
            // de cho buoc kiem tra phia sau tu bao ket qua
        }
    }
}
