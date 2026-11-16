namespace FirstTests.Utilities;

/// <summary>
/// Ghi nhat ky cho tung test: moi dong co thoi gian, muc do, luong chay va ten test.
/// Vua in ra NUnit (TestContext.Progress) vua ghi vao file logs/test-run.log de xem lai sau.
/// </summary>
public static class Log
{
    private static readonly object khoa = new();   // nhieu luong cung ghi file khi chay song song

    public static string DuongDanFile { get; } =
        Path.Combine(AppContext.BaseDirectory, "logs", "test-run.log");

    public static void Info(string noiDung) => Ghi("INFO ", noiDung);
    public static void Warn(string noiDung) => Ghi("WARN ", noiDung);
    public static void Error(string noiDung) => Ghi("ERROR", noiDung);

    private static void Ghi(string mucDo, string noiDung)
    {
        string tenTest = TestContext.CurrentContext?.Test?.Name ?? "-";
        string dong = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] [{mucDo}] [T{Environment.CurrentManagedThreadId:00}] [{tenTest}] {noiDung}";

        TestContext.Progress.WriteLine(dong);
        lock (khoa)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(DuongDanFile)!);
            File.AppendAllText(DuongDanFile, dong + Environment.NewLine);
        }
    }
}
