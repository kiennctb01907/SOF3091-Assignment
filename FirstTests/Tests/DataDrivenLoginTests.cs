using FirstTests.Core;
using FirstTests.Data;
using FirstTests.Pages;

namespace FirstTests.Tests;

/// <summary>
/// Y5 - Data-Driven: mot ham test chay lai cho tung bo du lieu trong Data/users.json.
/// Them hay bot bo du lieu chi can sua file JSON, khong sua file .cs nao.
/// </summary>
[TestFixture, Category("Login"), Category("DataDriven")]
public class DataDrivenLoginTests : BaseTest
{
    // Nguon du lieu phai la static vi NUnit goi no truoc khi tao doi tuong cua lop
    private static IEnumerable<TestCaseData> DuLieuDangNhap()
    {
        var danhSach = UserData.DocTuFile();
        for (int i = 0; i < danhSach.Count; i++)
        {
            var u = danhSach[i];
            yield return new TestCaseData(u)
                .SetName($"DangNhap_{i + 1:00}_{u.LoaiCa}_{TenNgan(u.Username)}")
                .SetDescription(u.GhiChu);
        }
    }

    [TestCaseSource(nameof(DuLieuDangNhap))]
    public void DangNhap_TheoBoDuLieu(UserData u)
    {
        var loginPage = new LoginPage(driver);
        var inventory = loginPage.DangNhap(u.Username, u.Password);

        Assert.That(inventory.DaHienThi(), Is.EqualTo(u.ThanhCong), u.GhiChu);

        if (!u.ThanhCong)
            Assert.That(loginPage.LayThongBaoLoi(), Does.Contain(u.ThongBaoLoi), u.GhiChu);
    }

    // Lam gon ten dang nhap de dat ten ca test de doc trong Test Explorer
    private static string TenNgan(string username)
    {
        if (string.IsNullOrWhiteSpace(username)) return username.Length == 0 ? "(trong)" : "(khoang_trang)";
        string sach = new string(username.Trim().Select(c => char.IsLetterOrDigit(c) || c == '_' ? c : '_').ToArray());
        if (username != username.Trim()) sach = "_" + sach + "_";
        return sach.Length > 20 ? sach[..20] + "..." : sach;
    }
}
