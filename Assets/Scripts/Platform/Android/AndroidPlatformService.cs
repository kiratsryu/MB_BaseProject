
using UnityEngine;

public class AndroidPlatformService : IPlatformService
{
    public string PlatformName => "Android";

    public void SaveString(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
        PlayerPrefs.Save();
    }

    public string LoadString(string key, string d) => PlayerPrefs.GetString(key, d);

    public void Vibrate(int ms) => Handheld.Vibrate();

    public void showMessage(string text)
    {
        using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        using (var activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
        {
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                using (var toastClass = new AndroidJavaClass("android.widget.Toast"))
                using (var toast = toastClass.CallStatic<AndroidJavaObject>(
                    "makeText", activity, text, 0))
                {
                    toast.Call("show");
                }
            }));
        }
    }

    public void Share(string text)
    {
        using (var intentClass = new AndroidJavaClass("android.content.Intent"))
        using (var intent = new AndroidJavaObject("android.content.Intent"))
        using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        using (var activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
        {
            intent.Call<AndroidJavaObject>(
                "setAction",
                intentClass.GetStatic<string>("ACTION_SEND")
            );

            intent.Call<AndroidJavaObject>("setType", "text/plain");

            intent.Call<AndroidJavaObject>(
                "putExtra",
                intentClass.GetStatic<string>("EXTRA_TEXT"),
                text
            );

            activity.Call("startActivity", intent);
        }
    }

    public void Purchase(string id) => Debug.Log("(Android) 결제 미구현: " + id);
}
