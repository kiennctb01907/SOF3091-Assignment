using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace FirstTests;

/// <summary>
/// Y3 - Xu ly tinh huong thuc te &amp; cho tuong minh.
/// Trang https://the-internet.herokuapp.com/dynamic_loading/2: bam Start, phan tu #finish
/// ("Hello World!") chi duoc JavaScript TAO RA sau vai giay -> phan tu tai cham.
/// </summary>
[TestFixture]
public class WaitTests
{
    private const string DynamicLoadingUrl = "https://the-internet.herokuapp.com/dynamic_loading/2";

    private IWebDriver driver = null!;
    private WebDriverWait wait = null!;

    [SetUp]
    public void SetUp()
    {
        driver = new ChromeDriver();
        driver.Manage().Window.Maximize();
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
    }

    [TearDown]
    public void TearDown()
    {
        // Chi chup anh khi test that bai, de truy vet loi.
        if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
        {
            string dir = Path.Combine(TestContext.CurrentContext.WorkDirectory, "failed-screenshots");
            Directory.CreateDirectory(dir);
            string path = Path.Combine(dir, TestContext.CurrentContext.Test.Name + ".png");
            ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(path);
            TestContext.AddTestAttachment(path);
        }

        driver.Quit();
        driver.Dispose();
    }

    /// <summary>
    /// BUOC 1 - CHUNG MINH VAN DE: tim #finish NGAY sau khi bam Start (khong cho) -> RoT voi
    /// NoSuchElementException. Danh dau [Explicit] de khong lam do CI; chay thu cong bang:
    /// dotnet test --filter "Name=KhongCho_TimNgay_BiNoSuchElement"
    /// </summary>
    [Test, Explicit("Co y ROT de chung minh loi dong bo (Y3). Chay thu cong bang --filter.")]
    public void KhongCho_TimNgay_BiNoSuchElement()
    {
        driver.Navigate().GoToUrl(DynamicLoadingUrl);
        driver.FindElement(By.CssSelector("#start button")).Click();

        // KHONG co wait: #finish chua ton tai trong DOM -> NoSuchElementException
        string text = driver.FindElement(By.Id("finish")).Text;

        Assert.That(text, Is.EqualTo("Hello World!"));
    }

    /// <summary>BUOC 2 - SUA: cung kich ban nhung dung WebDriverWait + Until -> PASS on dinh.</summary>
    [Test]
    public void CoCho_PhanTuTaiCham_HienHelloWorld()
    {
        driver.Navigate().GoToUrl(DynamicLoadingUrl);
        driver.FindElement(By.CssSelector("#start button")).Click();

        // Cho den khi #finish ton tai VA hien thi, toi da 15 giay (khong phai ngu co dinh)
        var finish = wait.Until(d =>
        {
            var e = d.FindElement(By.Id("finish"));
            return e.Displayed ? e : null;
        })!;

        Assert.That(finish.Text, Is.EqualTo("Hello World!"));
    }

    /// <summary>
    /// Tinh huong "thanh dang tai bien mat": thay vi doi co dinh, cho den khi #loading khong con hien thi.
    /// </summary>
    [Test]
    public void CoCho_ThanhLoadingBienMat_RoiMoiDocKetQua()
    {
        driver.Navigate().GoToUrl(DynamicLoadingUrl);
        driver.FindElement(By.CssSelector("#start button")).Click();

        wait.Until(d => d.FindElements(By.Id("loading")).Count == 0
                        || !d.FindElement(By.Id("loading")).Displayed);

        Assert.That(driver.FindElement(By.Id("finish")).Text, Is.EqualTo("Hello World!"));
    }

    /// <summary>
    /// Tinh huong trang chuyen buoc + tai cham: performance_glitch_user lam dang nhap cham,
    /// ta cho URL doi sang inventory.html thay vi doi co dinh.
    /// </summary>
    [Test]
    public void CoCho_TrangChuyenBuoc_PerformanceGlitchUser_VaoDuocTrangSanPham()
    {
        driver.Navigate().GoToUrl("https://www.saucedemo.com/");
        driver.FindElement(By.Id("user-name")).SendKeys("performance_glitch_user");
        driver.FindElement(By.Id("password")).SendKeys("secret_sauce");
        driver.FindElement(By.Id("login-button")).Click();

        wait.Until(d => d.Url.Contains("inventory.html"));

        Assert.That(driver.FindElement(By.CssSelector("[data-test='title']")).Text, Is.EqualTo("Products"));
    }
}
