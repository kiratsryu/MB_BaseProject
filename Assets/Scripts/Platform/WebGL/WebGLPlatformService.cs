
using System.Runtime.InteropServices;
using UnityEngine;

public class WebGLPlatformService : IPlatformService
{
    // .jslib 함수 선언
    [DllImport("__Internal")]
    private static extern void Browser_Alert(string text);

    [DllImport("__Internal")]
    private static extern void Browser_CopyToClipboard(string text);

    [DllImport("__Internal")]
    private static extern void Browser_Vibrate(int ms);

    public string PlatformName => "WebGL";

    // 문자열 저장
    public void SaveString(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
        PlayerPrefs.Save();
    }

    // 문자열 불러오기
    public string LoadString(string key, string defaultValue)
    {
        return PlayerPrefs.GetString(key, defaultValue);
    }

    // 진동
    public void Vibrate(int ms)
    {
        Browser_Vibrate(ms);
    }

    // 브라우저 메시지 표시
    public void showMessage(string t)
{
    Browser_Alert(t);
}

    // 클립보드 복사
    public void Share(string t)
    {
        Browser_CopyToClipboard(t);
    }

    // 결제
    public void Purchase(string id)
    {
        Debug.Log("[WebGL] 결제 미구현: " + id);
    }
}
