using FirstTests.Core;
using FirstTests.Pages;

namespace FirstTests.Tests;

/// <summary>Cac ca tren trang san pham: gio hang, sap xep gia, thu thap du lieu san pham.</summary>
[TestFixture]
public class InventoryTests : BaseTest
{
    // BaseTest da mo san trang dang nhap, o day chi can dang nhap bang tai khoan chuan
    private InventoryPage DangNhapChuan() => new LoginPage(driver).DangNhap("standard_user", "secret_sauce");

    [Test]
    public void ThemSanPhamVaoGio_MoGioHang_ThayDungSanPham()
    {
        var inventory = DangNhapChuan();

        inventory.ThemVaoGio("sauce-labs-backpack");
        Assert.That(inventory.LaySoLuongTrenBieuTuongGio(), Is.EqualTo("1"));

        var gio = inventory.MoGioHang();

        Assert.That(gio.UrlHienTai, Does.Contain("cart.html"));
        Assert.That(gio.LayTenSanPhamTrongGio(), Is.EqualTo(new[] { "Sauce Labs Backpack" }));
    }

    [Test]
    public void SapXepGiaThapDenCao_DanhSachGiaTangDan()
    {
        // Chaining: dang nhap -> sap xep -> lay gia
        var gia = DangNhapChuan().SapXep("lohi").LayDanhSachGia();

        Assert.That(gia, Has.Count.EqualTo(6));
        Assert.That(gia, Is.Ordered.Ascending);
    }

    [Test]
    public void TrangSanPham_CoDu6SanPham_GiaBatDauBangDauDoLa()
    {
        var sanPham = DangNhapChuan().LayThongTinSanPham();
        Assert.That(sanPham, Has.Count.EqualTo(6));

        foreach (var (ten, gia) in sanPham)
        {
            TestContext.Out.WriteLine($"{ten} - {gia}");
            Assert.That(gia, Does.StartWith("$"), $"Gia cua '{ten}' phai bat dau bang $");
        }

        var danhSachTen = sanPham.Select(s => s.Ten).ToList();
        Assert.That(danhSachTen, Has.None.Empty);
        Assert.That(danhSachTen, Does.Contain("Sauce Labs Backpack"));
    }
}
