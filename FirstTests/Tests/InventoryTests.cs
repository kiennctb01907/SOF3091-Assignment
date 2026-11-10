using FirstTests.Pages;

namespace FirstTests.Tests;

/// <summary>Y2 (luong 2) + Lab 3 Phan B + Y4 Phan D - cac ca tren trang san pham, dung POM va chaining.</summary>
[TestFixture]
public class InventoryTests : BaseTest
{
    private InventoryPage inventory = null!;

    [SetUp]
    public void DangNhapTruoc()
    {
        inventory = new LoginPage(driver).MoTrang().DangNhap("standard_user", "secret_sauce");
    }

    [Test]
    public void ThemSanPhamVaoGio_MoGioHang_ThayDungSanPham()
    {
        inventory.ThemVaoGio("sauce-labs-backpack");
        Assert.That(inventory.LaySoLuongTrenBieuTuongGio(), Is.EqualTo("1"));

        var gio = inventory.MoGioHang();

        Assert.That(gio.UrlHienTai, Does.Contain("cart.html"));
        Assert.That(gio.LayTenSanPhamTrongGio(), Is.EqualTo(new[] { "Sauce Labs Backpack" }));
    }

    [Test]
    public void SapXepGiaThapDenCao_DanhSachGiaTangDan()
    {
        // Chaining: tu trang san pham -> sap xep -> lay gia
        var gia = inventory.SapXep("lohi").LayDanhSachGia();

        Assert.That(gia, Has.Count.EqualTo(6));
        Assert.That(gia, Is.Ordered.Ascending);
    }

    [Test]
    public void TrangSanPham_CoDu6SanPham_GiaBatDauBangDauDoLa()
    {
        var sanPham = inventory.LayThongTinSanPham();
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
