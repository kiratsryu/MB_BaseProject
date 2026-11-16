
using UnityEngine;

// 기울기로 카메라를 살짝 회전한다.
// 어떤 센서인지 알 필요 없이 인터페이스만 사용한다.
public class TiltCamera : MonoBehaviour
{
    // Fake 또는 Device 센서 중 하나를 Inspector에서 연결
    public MonoBehaviour sensorSource;

    // 최대 회전 각도
    public float maxAngle = 10f;

    // 센서 인터페이스
    private IMotionSensor sensor;

    void Awake()
    {
        // 연결된 컴포넌트를 IMotionSensor로 변환
        sensor = sensorSource as IMotionSensor;

        if (sensor == null)
        {
            Debug.LogError(
                "Sensor Source에 IMotionSensor를 구현한 컴포넌트를 연결하세요.",
                this
            );
        }
    }

    void LateUpdate()
    {
        // 센서가 없거나 사용 불가능하면 종료
        if (sensor == null || !sensor.IsAvailable)
            return;

        // 센서에서 기울기 값 가져오기
        Vector2 tilt = sensor.GetTilt();

        // 카메라 회전 적용
        transform.localRotation = Quaternion.Euler(
            -tilt.y * maxAngle,
            tilt.x * maxAngle,
            0f
        );
    }
}
