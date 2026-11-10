using OpenQA.Selenium;

namespace FirstTests.Pages;

/// <summary>Trang the-internet /dynamic_loading/2 - phan tu #finish duoc JavaScript tao ra sau vai giay.</summary>
public class DynamicLoadingPage : BasePage
{
    public const string Url = "https://the-internet.herokuapp.com/dynamic_loading/2";

    private readonly By nutStart = By.CssSelector("#start button");
    private readonly By thanhLoading = By.Id("loading");
    private readonly By ketQua = By.Id("finish");

    // Trang nay tai cham -> timeout 15 giay
    public DynamicLoadingPage(IWebDriver driver) : base(driver, timeoutGiay: 15) { }

    public DynamicLoadingPage MoTrang()
    {
        driver.Navigate().GoToUrl(Url);
        return this;
    }

    public DynamicLoadingPage BamStart()
    {
        Bam(nutStart);
        return this;
    }

    /// <summary>Cho ket qua tai xong roi tra ve noi dung chu.</summary>
    public string LayKetQuaSauKhiTai() => LayChu(ketQua);

    public DynamicLoadingPage ChoThanhLoadingBienMat()
    {
        wait.Until(d => d.FindElements(thanhLoading).Count == 0 || !d.FindElement(thanhLoading).Displayed);
        return this;
    }

    /// <summary>Doc #finish NGAY (khong cho) - chi dung de chung minh loi dong bo.</summary>
    public string LayKetQuaNgayKhongCho() => driver.FindElement(ketQua).Text;
}
