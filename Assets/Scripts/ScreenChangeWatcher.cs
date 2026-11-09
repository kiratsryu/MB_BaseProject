using UnityEngine;

public class ScreenChangeWatcher : MonoBehaviour
{
   int lastW, lastH;

    void Start()
    {
        lastW = Screen.width;
        lastH = Screen.height;
    }

    
    void Update()
    {
        if (Screen.width != lastW || Screen.height != lastH)
        {
            lastW = Screen.width;
            lastW = Screen.height;
            Debug.Log($"화면 크기 변경: {lastW} x {lastH} (비율 {(float)lastW / lastH:F2})");
        }
    }
}
