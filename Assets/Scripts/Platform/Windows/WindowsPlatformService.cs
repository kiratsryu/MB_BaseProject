
using System.IO;
using UnityEngine;

public class WindowsPlatformService : IPlatformService
{
    public string PlatformName => "Windows";

    private string PathFor(string key)
    {
        return Path.Combine(
            Application.persistentDataPath,
            key + ".txt"
        );
    }

    public void SaveString(string key, string value)
    {
        File.WriteAllText(PathFor(key), value);
    }

    public string LoadString(string key, string defaultValue)
    {
        return File.Exists(PathFor(key))
            ? File.ReadAllText(PathFor(key))
            : defaultValue;
    }

    public void Vibrate(int ms)
    {
        // 데스크톱은 진동 기능 없음
    }

    public void showMessage(string t)
    {
        Debug.Log("[Windows] " + t);
    }

    public void Share(string t)
    {
        GUIUtility.systemCopyBuffer = t;
    }

    public void Purchase(string id)
    {
        Debug.Log("[Windows] 결제 미구현: " + id);
    }
}
