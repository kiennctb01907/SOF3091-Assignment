namespace FirstTests.Config;

/// <summary>
/// Anh xa voi Config/appsettings.json. Ten thuoc tinh phai trung voi ten khoa trong file JSON,
/// neu khac thi gia tri se bi bo trong ma khong bao loi.
/// </summary>
public class TestConfig
{
    public string BaseUrl { get; set; } = "";
    public string DynamicLoadingUrl { get; set; } = "";
    public string Browser { get; set; } = "chrome";
    public int TimeoutSeconds { get; set; } = 10;
    public bool Headless { get; set; }
    public int WindowWidth { get; set; } = 1366;
    public int WindowHeight { get; set; } = 900;
}
