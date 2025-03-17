using UnityEngine;
using UnityEngine.EventSystems;

public class D_Camera : MonoBehaviour
{
    [Header("Orbit Target")]
    public Transform target;                // Mục tiêu camera xoay quanh
    
    public float distance = 8f;             // Khoảng cách camera tới mục tiêu
    public Vector3 offset = Vector3.zero;   // Độ lệch so với mục tiêu

    [Header("Orbit Speeds")]
    public float xSpeed = 150f;             // Tốc độ xoay ngang
    public float ySpeed = 100f;             // Tốc độ xoay dọc
    public float autoOrbitSpeed = 10f;      // Tốc độ tự động xoay khi không thao tác

    [Header("Angle Limits")]
    public float yMinLimit = 10f;           // Giới hạn góc nhìn thấp nhất
    public float yMaxLimit = 80f;           // Giới hạn góc nhìn cao nhất

    [Header("Mobile Drag Sensitivity")]
    public float touchSensitivity = 0.1f;   // Độ nhạy khi kéo trên mobile

    [Header("Smooth Transition")]
    public Transform customizeViewPoint;    // Điểm đặt camera khi nhấn Customize
    public Transform defaultViewPoint;      // Vị trí mặc định của camera trước khi vào Customize
    public Transform wheelTransform;
    public float lerpTime = 1.5f;           // Thời gian di chuyển camera đến vị trí mới
    
    private bool isAtCustomize = false;     // Kiểm tra camera có ở vị trí Customize không
    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private float transitionTimer = 0f;
    private bool isMovingBack = false;
    private bool isMovingForward = false;

    private bool Paint_Point = false;
    private bool Wheel_Point = false;

    [SerializeField]private float x = 0f;                   
    [SerializeField]private float y = 10f;                  
    private float orbitTimer = 0f;           // Thời gian không có tương tác để kích hoạt orbit


    private Quaternion rotation;
    private Vector3 negDistance;
    private Vector3 position;

    public GameObject ButtonBack;
    private void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        if (defaultViewPoint)
        {
            transform.position = defaultViewPoint.position;
            transform.rotation = defaultViewPoint.rotation;
        }
    }

    private void LateUpdate()
    {
        if (isMovingForward)
        {
            if (Paint_Point)
            {
                transitionTimer += Time.deltaTime / lerpTime;
                transform.position = Vector3.Lerp(transform.position, customizeViewPoint.position, transitionTimer);
                transform.rotation = Quaternion.Lerp(transform.rotation, customizeViewPoint.rotation, transitionTimer);
            }
            else if (Wheel_Point)
            {
                transitionTimer += Time.deltaTime / lerpTime;
                transform.position = Vector3.Lerp(transform.position, wheelTransform.position, transitionTimer);
                transform.rotation = Quaternion.Lerp(transform.rotation, wheelTransform.rotation, transitionTimer);
            }

            if (transform.position == customizeViewPoint.position)
            {
                ButtonBack.SetActive(true);
                isMovingForward = false;
                isAtCustomize = true;
                orbitTimer = 0f;
                x = 180.847f;
                y = 10f;
            }
            else if (transform.position == wheelTransform.position)
            {
                isMovingForward = false;
                ButtonBack.SetActive(true);
            }
            else
                return;
        }
        if (isMovingBack)
        {
            Debug.Log("khoi");
            transitionTimer += Time.deltaTime / lerpTime;
            transform.position = Vector3.Lerp(transform.position, defaultViewPoint.position, transitionTimer);
            transform.rotation = Quaternion.Lerp(transform.rotation, defaultViewPoint.rotation, transitionTimer);
            isAtCustomize = false;
            if (transform.position == defaultViewPoint.position)
            {
                isMovingBack = false;
                orbitTimer = 0f;
                rotation = new Quaternion();
                negDistance = new Vector3();
                position = new Vector3();
                x = 180.847f;
                y = 10f;
            }
            else
                return;
        }
        
        if (!isAtCustomize)
        {
            return;
        }

        // Kiểm tra người dùng có đang chạm vào UI không
        bool isTouchingUI = EventSystem.current.IsPointerOverGameObject();

        // Kiểm tra chuột trên Desktop
        if (Input.GetMouseButton(0) && !isTouchingUI)
        {
            orbitTimer = 0f; // Reset timer khi có tương tác
            float inputX = Input.GetAxis("Mouse X");
            float inputY = Input.GetAxis("Mouse Y");

            x += inputX * xSpeed * Time.deltaTime;
            y -= inputY * ySpeed * Time.deltaTime;
        }

        // Kiểm tra Touch trên Mobile
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved && !EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                orbitTimer = 0f; // Reset timer khi có tương tác
                x += touch.deltaPosition.x * touchSensitivity;
                y -= touch.deltaPosition.y * touchSensitivity;
            }
        }

        // Nếu không có tương tác sau 3 giây, tự động xoay quanh xe
        orbitTimer += Time.deltaTime;
        if (orbitTimer > 3f)
        {
            x += autoOrbitSpeed * Time.deltaTime;
        }

        // Giới hạn góc xoay dọc
        y = ClampAngle(y, yMinLimit, yMaxLimit);

        // Tạo góc quay
        rotation = Quaternion.Euler(y, x, 0f);
        negDistance = new Vector3(0f, 0f, -distance);
        position = rotation * negDistance + target.position + offset;
        
        transform.rotation = rotation;
        transform.position = position;
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f) angle += 360f;
        if (angle > 360f) angle -= 360f;
        return Mathf.Clamp(angle, min, max);
    }

    /// <summary>
    /// Chuyển camera đến vị trí Customize.
    /// </summary>
    public void MoveToCustomize()
    {
        isMovingForward = true;
        transitionTimer = 0f;
        Paint_Point = true;
    }

    /// <summary>
    /// Trở về vị trí ban đầu, tắt chức năng Customize.
    /// </summary>
    public void MoveBack()
    {
        isAtCustomize = false;
        isMovingBack = true;
        transitionTimer = 0f;
        Paint_Point = false;
        Wheel_Point = false;
        ButtonBack.SetActive(false);
    }

    public void MoveToWheelPoint()
    {
        isMovingForward = true;
        Wheel_Point = true;
        transitionTimer = 0f;
    }
}