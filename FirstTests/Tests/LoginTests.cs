using FirstTests.Core;
using FirstTests.Pages;

namespace FirstTests.Tests;

/// <summary>Cac ca dang nhap co dinh (Y2). Bo du lieu dang nhap day du nam o DataDrivenLoginTests.</summary>
[TestFixture, Category("Login")]
public class LoginTests : BaseTest
{
    [Test, Category("Smoke")]
    public void DangNhapHopLe_VaoDuocTrangSanPham()
    {
        var inventory = new LoginPage(driver).DangNhap("standard_user", "secret_sauce");

        Assert.That(inventory.DaHienThi(), Is.True);
        Assert.That(inventory.UrlHienTai, Does.Contain("inventory.html"));
    }

    [Test]
    public void DangNhapSaiMatKhau_HienThongBaoLoi()
    {
        var loginPage = new LoginPage(driver).DangNhapKhongThanhCong("standard_user", "sai_mat_khau");

        Assert.That(loginPage.LayThongBaoLoi(), Does.Contain("Username and password do not match any user"));
        Assert.That(loginPage.UrlHienTai, Is.EqualTo(cfg.BaseUrl));
    }

    [Test]
    public void DangNhapTaiKhoanBiKhoa_HienThongBaoLoi()
    {
        var loginPage = new LoginPage(driver).DangNhapKhongThanhCong("locked_out_user", "secret_sauce");

        Assert.That(loginPage.LayThongBaoLoi(), Does.Contain("this user has been locked out"));
    }
}
