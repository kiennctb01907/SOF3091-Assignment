using System.Globalization;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace FirstTests.Pages;

/// <summary>Trang danh sach san pham. Khong chua lenh kiem tra ket qua, khong tra ve phan tu web.</summary>
public class InventoryPage : BasePage
{
    private readonly By tieuDeTrang = By.CssSelector("[data-test='title']");
    private readonly By moiSanPham = By.CssSelector("[data-test='inventory-item']");
    private readonly By tenSanPham = By.CssSelector("[data-test='inventory-item-name']");
    private readonly By giaSanPham = By.CssSelector("[data-test='inventory-item-price']");
    private readonly By oSapXep = By.CssSelector("[data-test='product-sort-container']");
    private readonly By bieuTuongGioHang = By.CssSelector("[data-test='shopping-cart-link']");

    public InventoryPage(IWebDriver driver) : base(driver) { }

    /// <summary>
    /// Trang san pham da hien thi chua (URL dung + co tieu de "Products").
    /// Ham tra loi CO/KHONG nen khong duoc nem loi: neu khong vao duoc trang thi tra ve false
    /// sau toi da 3 giay, de cac ca dang nhap sai van chay toi cau Assert.
    /// </summary>
    public bool DaHienThi()
    {
        try
        {
            return new WebDriverWait(driver, TimeSpan.FromSeconds(3)).Until(d =>
                d.Url.Contains("inventory.html")
                && d.FindElements(tieuDeTrang).Any(e => e.Displayed && e.Text == "Products"));
        }
        catch (WebDriverTimeoutException)
        {
            return false;
        }
    }

    public InventoryPage SapXep(string giaTri)
    {
        new SelectElement(Tim(oSapXep)).SelectByValue(giaTri);
        return this;
    }

    public IReadOnlyList<string> LayDanhSachTen()
    {
        TimTatCa(moiSanPham);
        return driver.FindElements(tenSanPham).Select(e => e.Text).ToList();
    }

    public IReadOnlyList<decimal> LayDanhSachGia()
    {
        TimTatCa(moiSanPham);
        return driver.FindElements(giaSanPham)
            .Select(e => decimal.Parse(e.Text.Replace("$", ""), CultureInfo.InvariantCulture))
            .ToList();
    }

    /// <summary>Lay (ten, gia dang chuoi) tung san pham - dung item.FindElement ben trong moi the san pham.</summary>
    public IReadOnlyList<(string Ten, string Gia)> LayThongTinSanPham()
    {
        TimTatCa(moiSanPham);
        return driver.FindElements(moiSanPham)
            .Select(item => (
                item.FindElement(tenSanPham).Text,
                item.FindElement(giaSanPham).Text))
            .ToList();
    }

    public InventoryPage ThemVaoGio(string slugSanPham)
    {
        // slug dang "sauce-labs-backpack" -> id "add-to-cart-sauce-labs-backpack"
        Bam(By.Id($"add-to-cart-{slugSanPham}"));
        return this;
    }

    public string LaySoLuongTrenBieuTuongGio() => LayChu(By.CssSelector("[data-test='shopping-cart-badge']"));

    public CartPage MoGioHang()
    {
        Bam(bieuTuongGioHang);
        return new CartPage(driver);
    }
}
