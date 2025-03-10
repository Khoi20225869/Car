using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    public Transform target; // Vật thể mà camera sẽ xoay quanh. Kéo vật thể "TargetObject" vào đây trong Inspector.
    public float rotationSpeed = 200.0f; // Tốc độ xoay camera (độ/giây)
    public float distance = 5.0f; // Bán kính xoay của camera (khoảng cách từ camera đến vật thể trung tâm)
    public float minYAngle = -80.0f; // Góc xoay dọc tối thiểu (độ)
    public float maxYAngle = 80.0f; // Góc xoay dọc tối đa (độ)

    private float currentXAngle = 0.0f; // Góc xoay ngang hiện tại
    private float currentYAngle = 0.0f; // Góc xoay dọc hiện tại

    void Start()
    {
        // Khởi tạo góc xoay ban đầu dựa trên hướng hiện tại của camera
        Vector3 angles = transform.eulerAngles;
        currentXAngle = angles.y;
        currentYAngle = angles.x;

        // Gọi hàm để thiết lập vị trí camera ban đầu
        /*UpdateRotationAndPosition();*/
    }

    void Update()
    {
        HandleMouseInput(); // Sử dụng chuột để test trên máy tính (có thể bỏ qua nếu chỉ dùng trên thiết bị cảm ứng)
        HandleTouchInput(); // Xử lý vuốt màn hình cảm ứng

        // Debug.Log("Current Y Angle: " + currentYAngle); // (Dòng debug - có thể bỏ comment để kiểm tra góc xoay dọc)
    }

    // Hàm xử lý input chuột (để test trên máy tính)
    void HandleMouseInput()
    {
        if (Input.GetMouseButton(0)) // Nếu giữ chuột trái
        {
            float mouseX = Input.GetAxis("Mouse X"); // Độ dịch chuyển chuột theo trục X
            float mouseY = Input.GetAxis("Mouse Y"); // Độ dịch chuyển chuột theo trục Y

            currentXAngle += mouseX * rotationSpeed * Time.deltaTime; // Cập nhật góc xoay ngang
            currentYAngle -= mouseY * rotationSpeed * Time.deltaTime; // Cập nhật góc xoay dọc
            currentYAngle = Mathf.Clamp(currentYAngle, minYAngle, maxYAngle); // Giới hạn góc xoay dọc

            UpdateRotationAndPosition(); // Cập nhật vị trí và hướng của camera
        }
    }

    // Hàm xử lý input cảm ứng (vuốt màn hình)
    void HandleTouchInput()
    {
        if (Input.touchCount == 1) // Nếu có một ngón tay chạm vào màn hình
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved) // Nếu ngón tay di chuyển trên màn hình
            {
                Vector2 deltaPos = touch.deltaPosition; // Độ dịch chuyển của ngón tay từ frame trước

                currentXAngle += deltaPos.x * rotationSpeed * Time.deltaTime; // Cập nhật góc xoay ngang
                currentYAngle -= deltaPos.y * rotationSpeed * Time.deltaTime; // Cập nhật góc xoay dọc
                currentYAngle = Mathf.Clamp(currentYAngle, minYAngle, maxYAngle); // Giới hạn góc xoay dọc

                UpdateRotationAndPosition(); // Cập nhật vị trí và hướng của camera
            }
        }
    }

    // Hàm cập nhật vị trí và hướng của camera
    void UpdateRotationAndPosition()
    {
        Quaternion rotation = Quaternion.Euler(currentYAngle, currentXAngle, 0); // Tạo Quaternion từ góc xoay - **Đảm bảo thứ tự đúng: dọc (YAngle), ngang (XAngle)**
        Vector3 desiredPosition = target.position - rotation * Vector3.forward * distance; // Tính toán vị trí mong muốn dựa trên góc xoay và khoảng cách

        transform.rotation = rotation; // Áp dụng rotation cho camera
        transform.position = desiredPosition; // Áp dụng vị trí cho camera

        transform.LookAt(target); // Đảm bảo camera luôn nhìn vào vật thể trung tâm
    }
}