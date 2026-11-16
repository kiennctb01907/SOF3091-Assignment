using System.Text.Json;

namespace FirstTests.Data;

/// <summary>Mot bo du lieu dang nhap trong Data/users.json. Ten thuoc tinh phai trung ten khoa JSON.</summary>
public class UserData
{
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
    public bool ThanhCong { get; set; }
    public string LoaiCa { get; set; } = "";       // Thuong | Bien | Loi
    public string ThongBaoLoi { get; set; } = "";  // doan chu phai co trong thong bao loi (ca that bai)
    public string GhiChu { get; set; } = "";

    public static List<UserData> DocTuFile()
    {
        string path = Path.Combine(AppContext.BaseDirectory, "Data", "users.json");
        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<UserData>>(json)
               ?? throw new InvalidOperationException("Data/users.json rong hoac sai dinh dang.");
    }
}
