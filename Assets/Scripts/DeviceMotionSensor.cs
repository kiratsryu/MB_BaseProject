
using UnityEngine;
using UnityEngine.InputSystem;

// 실제 가속도계를 읽는 구현체
// 노이즈를 줄이기 위해 이전 값과 현재 값을 섞는다.
public class DeviceMotionSensor : MonoBehaviour, IMotionSensor
{
    [Range(0f, 1f)]
    public float smoothing = 0.1f;

    private Vector2 filtered;
    private bool isInitialized = false;

    // 가속도계 사용 가능 여부
    public bool IsAvailable
    {
        get
        {
            return Accelerometer.current != null;
        }
    }

    // 컴포넌트 활성화
    private void OnEnable()
    {
        if (Accelerometer.current != null)
        {
            InputSystem.EnableDevice(Accelerometer.current);
        }

        filtered = Vector2.zero;
        isInitialized = false;
    }

    // 컴포넌트 비활성화
    private void OnDisable()
    {
        if (Accelerometer.current != null)
        {
            InputSystem.DisableDevice(Accelerometer.current);
        }

        filtered = Vector2.zero;
        isInitialized = false;
    }

    // 기울기 값 가져오기
    public Vector2 GetTilt()
    {
        // 가속도계가 없으면 0 반환
        if (!IsAvailable)
        {
            return Vector2.zero;
        }

        // 실제 가속도 값 읽기
        Vector3 acceleration =
            Accelerometer.current.acceleration.ReadValue();

        // X, Y 값 추출
        Vector2 raw = new Vector2(
            acceleration.x,
            acceleration.y
        );

        // 첫 번째 값은 필터 초기값으로 사용
        if (!isInitialized)
        {
            filtered = raw;
            isInitialized = true;

            return filtered;
        }

        // smoothing 값 제한
        float factor = Mathf.Clamp01(smoothing);

        // 저역 통과 필터
        filtered = Vector2.Lerp(
            filtered,
            raw,
            factor
        );

        return filtered;
    }
}
