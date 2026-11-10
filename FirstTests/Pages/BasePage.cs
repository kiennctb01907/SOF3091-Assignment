using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace FirstTests.Pages;

/// <summary>
/// Lop co so cho moi Page: giu driver + WebDriverWait va cung cap ham Tim(By) co CHO TUONG MINH.
/// Cac Page con KHONG tu khai bao lai driver / wait.
/// </summary>
public abstract class BasePage
{
    protected readonly IWebDriver driver;
    protected readonly WebDriverWait wait;

    protected BasePage(IWebDriver driver, int timeoutGiay = 10)
    {
        this.driver = driver;
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutGiay));
    }

    /// <summary>Tim phan tu KEM cho: doi den khi phan tu ton tai va hien thi.</summary>
    protected IWebElement Tim(By locator) =>
        wait.Until(d =>
        {
            var e = d.FindElement(locator);
            return e.Displayed ? e : null;
        })!;

    /// <summary>Tim nhieu phan tu: doi den khi co it nhat 1 phan tu.</summary>
    protected IReadOnlyList<IWebElement> TimTatCa(By locator) =>
        wait.Until(d =>
        {
            var list = d.FindElements(locator);
            return list.Count > 0 ? list : null;
        })!;

    protected void Nhap(By locator, string text)
    {
        var e = Tim(locator);
        e.Clear();
        e.SendKeys(text);
    }

    protected void Bam(By locator) => Tim(locator).Click();

    protected string LayChu(By locator) => Tim(locator).Text;

    /// <summary>Cho URL chua mot doan chuoi (trang chuyen buoc).</summary>
    protected void ChoUrlChua(string phanUrl) => wait.Until(d => d.Url.Contains(phanUrl));

    public string UrlHienTai => driver.Url;
}
