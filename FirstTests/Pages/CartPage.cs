using OpenQA.Selenium;

namespace FirstTests.Pages;

/// <summary>Trang gio hang.</summary>
public class CartPage : BasePage
{
    private readonly By tenSanPhamTrongGio = By.CssSelector("[data-test='inventory-item-name']");

    public CartPage(IWebDriver driver) : base(driver) { }

    public IReadOnlyList<string> LayTenSanPhamTrongGio()
    {
        ChoUrlChua("cart.html");
        TimTatCa(tenSanPhamTrongGio);
        return driver.FindElements(tenSanPhamTrongGio).Select(e => e.Text).ToList();
    }
}
