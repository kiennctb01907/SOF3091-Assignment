using FirstTests.Config;
using FirstTests.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace FirstTests.Pages;

/// <summary>
/// Lop co so cho moi Page: giu driver + WebDriverWait va cung cap ham Tim(By) co CHO TUONG MINH.
/// Cac Page con KHONG tu khai bao lai driver / wait. Thoi gian cho lay tu appsettings.json.
/// </summary>
public abstract class BasePage
{
    protected readonly IWebDriver driver;
    protected readonly WebDriverWait wait;
    protected readonly TestConfig cfg = ConfigReader.Load();

    protected BasePage(IWebDriver driver, int? timeoutGiay = null)
    {
        this.driver = driver;
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutGiay ?? cfg.TimeoutSeconds));
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
        Log.Info($"Nhap vao {locator}");
        var e = Tim(locator);
        e.Clear();
        e.SendKeys(text);
    }

    /// <summary>
    /// Bam vao phan tu: cuon phan tu ra giua man hinh roi click; neu bi che (quang cao, banner...)
    /// thi cho va thu lai den khi het timeout.
    /// </summary>
    protected void Bam(By locator)
    {
        Log.Info($"Bam {locator}");
        wait.Until(d =>
        {
            try
            {
                var e = Tim(locator);
                ((IJavaScriptExecutor)d).ExecuteScript("arguments[0].scrollIntoView({block: 'center'});", e);
                e.Click();
                return true;
            }
            catch (ElementClickInterceptedException)
            {
                return false;
            }
        });
    }

    protected string LayChu(By locator) => Tim(locator).Text;

    /// <summary>Cho URL chua mot doan chuoi (trang chuyen buoc).</summary>
    protected void ChoUrlChua(string phanUrl) => wait.Until(d => d.Url.Contains(phanUrl));

    public string UrlHienTai => driver.Url;
}
