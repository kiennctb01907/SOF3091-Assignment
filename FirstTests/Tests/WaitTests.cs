using FirstTests.Pages;

namespace FirstTests.Tests;

/// <summary>
/// Y3 + Y4 - Xu ly tinh huong thuc te va cho tuong minh (da chuyen sang Page Object).
/// Trang /dynamic-loading/2: #finish chi duoc JavaScript TAO RA sau vai giay.
/// </summary>
[TestFixture]
public class WaitTests : BaseTest
{
    /// <summary>
    /// BUOC 1 - CHUNG MINH VAN DE: doc #finish NGAY sau khi bam Start (khong cho) -> ROT voi
    /// NoSuchElementException. Danh dau [Explicit] de khong lam do bo test mac dinh; chay bang:
    /// dotnet test --filter "Name=KhongCho_TimNgay_BiNoSuchElement"
    /// </summary>
    [Test, Explicit("Co y ROT de chung minh loi dong bo (Y3). Chay thu cong bang --filter.")]
    public void KhongCho_TimNgay_BiNoSuchElement()
    {
        var trang = new DynamicLoadingPage(driver).MoTrang().BamStart();

        string text = trang.LayKetQuaNgayKhongCho();   // khong wait -> NoSuchElementException

        Assert.That(text, Is.EqualTo("Hello World!"));
    }

    /// <summary>BUOC 2 - SUA: cung kich ban nhung doc ket qua qua WebDriverWait -> PASS on dinh.</summary>
    [Test]
    public void CoCho_PhanTuTaiCham_HienHelloWorld()
    {
        string text = new DynamicLoadingPage(driver).MoTrang().BamStart().LayKetQuaSauKhiTai();

        Assert.That(text, Is.EqualTo("Hello World!"));
    }

    /// <summary>Cho thanh "Loading" bien mat roi moi doc ket qua.</summary>
    [Test]
    public void CoCho_ThanhLoadingBienMat_RoiMoiDocKetQua()
    {
        var trang = new DynamicLoadingPage(driver).MoTrang().BamStart().ChoThanhLoadingBienMat();

        Assert.That(trang.LayKetQuaSauKhiTai(), Is.EqualTo("Hello World!"));
    }

    /// <summary>Trang chuyen buoc + tai cham: performance_glitch_user lam dang nhap cham.</summary>
    [Test]
    public void CoCho_TrangChuyenBuoc_PerformanceGlitchUser_VaoDuocTrangSanPham()
    {
        var inventory = new LoginPage(driver).MoTrang().DangNhap("performance_glitch_user", "secret_sauce");

        Assert.That(inventory.DaHienThi(), Is.True);
    }
}
