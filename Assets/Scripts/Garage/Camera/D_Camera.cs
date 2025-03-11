using UnityEngine;
using UnityEngine.EventSystems;
public class D_Camera : MonoBehaviour
{
    [Header("Orbit Target")]
    public Transform target;              // Mục tiêu camera sẽ xoay quanh
    public float distance = 8f;         // Khoảng cách camera tới mục tiêu
    public Vector3 offset = Vector3.zero; // Dịch chuyển camera so với tâm mục tiêu

    [Header("Orbit Speeds")]
    public float xSpeed = 150f;           // Tốc độ xoay ngang
    public float ySpeed = 100f;           // Tốc độ xoay dọc

    [Header("Angle Limits")]
    public float yMinLimit = 10f;         // Giới hạn góc thấp nhất
    public float yMaxLimit = 80f;         // Giới hạn góc cao nhất

    [Header("Mobile Drag Sensitivity")]
    public float touchSensitivity = 0.1f; // Độ nhạy kéo trên mobile

    // Biến lưu góc xoay
    private float x = 0f;  
    private float y = 20f;  // Góc mặc định nhìn từ trên xuống

    private void Start()
    {
        // Lấy góc quay hiện tại
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    private void LateUpdate()
    {
        if (!target) return;

        // CÁCH 1: Dùng chuột trên Desktop
        if (Input.GetMouseButton(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return; 
            // Lấy delta chuột
            float inputX = Input.GetAxis("Mouse X");
            float inputY = Input.GetAxis("Mouse Y");

            x += inputX * xSpeed * Time.deltaTime;
            y -= inputY * ySpeed * Time.deltaTime;
        }

        // CÁCH 2: Dùng Touch trên Mobile
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                    return;
                // Mỗi pixel di chuyển tương đương delta.x, delta.y
                x += touch.deltaPosition.x * touchSensitivity;
                y -= touch.deltaPosition.y * touchSensitivity;
            }
        }

        // Giới hạn góc quay dọc [yMinLimit, yMaxLimit]
        y = ClampAngle(y, yMinLimit, yMaxLimit);

        // Tạo góc quay (x ngang, y dọc)
        Quaternion rotation = Quaternion.Euler(y, x, 0f);

        // Tính vị trí camera (lùi distance so với mục tiêu, rồi cộng offset)
        Vector3 negDistance = new Vector3(0f, 0f, -distance);
        Vector3 position = rotation * negDistance + target.position + offset;

        // Gán vào camera
        transform.rotation = rotation;
        transform.position = position;
    }

    // Hàm giới hạn góc
    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f) angle += 360f;
        if (angle > 360f)  angle -= 360f;
        return Mathf.Clamp(angle, min, max);
    }
}
