
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class GestureDetector : MonoBehaviour
{
    public float swipeMinDistance = 100f; // 픽셀
    public float swipeMaxTime = 0.4f;     // 초
    public Camera targetCamera;

    Vector2 startPos;
    float startTime;
    float lastPinchDistance;

    void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    void Update()
    {
        var touches = Touch.activeTouches;

        // 손가락 하나 : 스와이프
        if (touches.Count == 1)
        {
            var t = touches[0];

            if (t.phase == TouchPhase.Began)
            {
                startPos = t.screenPosition;
                startTime = Time.time;
            }
            else if (t.phase == TouchPhase.Ended)
            {
                Vector2 delta = t.screenPosition - startPos;
                float elapsed = Time.time - startTime;

                if (delta.magnitude > swipeMinDistance &&
                    elapsed < swipeMaxTime)
                {
                    string dir =
                        Mathf.Abs(delta.x) > Mathf.Abs(delta.y)
                        ? (delta.x > 0 ? "오른쪽" : "왼쪽")
                        : (delta.y > 0 ? "위" : "아래");

                    Debug.Log("스와이프: " + dir);
                }
            }

            lastPinchDistance = 0f;
        }

        // 손가락 둘 : 핀치
        else if (touches.Count == 2)
        {
            float dist = Vector2.Distance(
                touches[0].screenPosition,
                touches[1].screenPosition
            );

            if (lastPinchDistance > 0f && targetCamera != null)
            {
                float scale = dist / lastPinchDistance;

                // 벌리면 확대, 오므리면 축소
                targetCamera.fieldOfView = Mathf.Clamp(
                    targetCamera.fieldOfView / scale,
                    30f,
                    90f
                );
            }

            lastPinchDistance = dist;
        }
        else
        {
            lastPinchDistance = 0f;
        }
    }
}
