using OpenQA.Selenium;

namespace FirstTests.Utilities;

/// <summary>Chup man hinh khi test rot va dinh kem vao ket qua test.</summary>
public static class ScreenshotHelper
{
    public static string? Chup(IWebDriver? driver)
    {
        if (driver is null) return null;

        string dir = Path.Combine(AppContext.BaseDirectory, "failed-screenshots");
        Directory.CreateDirectory(dir);

        // Ten test co the chua ky tu khong hop le cho ten file (vd dau ngoac kep cua TestCaseSource)
        string ten = string.Concat(TestContext.CurrentContext.Test.Name.Select(c => Path.GetInvalidFileNameChars().Contains(c) ? '_' : c));
        string path = Path.Combine(dir, $"{ten}_{DateTime.Now:HHmmss}.png");

        ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(path);
        TestContext.AddTestAttachment(path);
        return path;
    }
}
