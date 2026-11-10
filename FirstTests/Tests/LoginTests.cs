using FirstTests.Pages;

namespace FirstTests.Tests;

/// <summary>Y2 + Y4 - Cac ca dang nhap, da refactor sang Page Object (test khong con locator).</summary>
[TestFixture]
public class LoginTests : BaseTest
{
    private LoginPage loginPage = null!;

    [SetUp]
    public void MoTrangDangNhap()
    {
        loginPage = new LoginPage(driver).MoTrang();
    }

    [Test]
    public void DangNhapHopLe_VaoDuocTrangSanPham()
    {
        var inventory = loginPage.DangNhap("standard_user", "secret_sauce");

        Assert.That(inventory.DaHienThi(), Is.True);
        Assert.That(inventory.UrlHienTai, Does.Contain("inventory.html"));
    }

    [Test]
    public void DangNhapSaiMatKhau_HienThongBaoLoi()
    {
        loginPage.DangNhapKhongThanhCong("standard_user", "sai_mat_khau");

        Assert.That(loginPage.LayThongBaoLoi(), Does.Contain("Username and password do not match any user"));
        Assert.That(loginPage.UrlHienTai, Is.EqualTo(LoginPage.Url));
    }

    [Test]
    public void DangNhapTaiKhoanBiKhoa_HienThongBaoLoi()
    {
        loginPage.DangNhapKhongThanhCong("locked_out_user", "secret_sauce");

        Assert.That(loginPage.LayThongBaoLoi(), Does.Contain("this user has been locked out"));
    }
}
