using UnityEngine;
 
[RequireComponent(typeof(Camera))]
public class D_Camera : MonoBehaviour
{
    public GameObject target;
    public float distance = 8.5f;
    public float xSpeed = 150f;
    public float ySpeed = 100f;
    [Range(0f, 1f)] public float orbitSpeed = 0.307f;
    public float yMinLimit = 10f;
    public float yMaxLimit = 30f;
    public Vector3 offset = new Vector3(0f, -0.45f, 0f);
 
    private bool orbiting = true;
    private float orbitingTimer = 0f;
    private float x = 0f;
    private float y = 0f;
    private float x_Smoothed = 0f;
    private float y_Smoothed = 0f;
 
    private void OnEnable()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }
 
    private void LateUpdate()
    {
        // Xử lý đầu vào từ chuột (hoặc chạm)
        if (Input.GetMouseButton(0))
        {
            orbiting = false;
            orbitingTimer = 1.5f;
 
            x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
        }
 
        if (orbitingTimer > 0f)
        {
            orbitingTimer -= Time.deltaTime;
        }
        else
        {
            orbitingTimer = 0f;
            orbiting = true;
        }
 
        if (orbiting)
        {
            x += orbitSpeed * xSpeed * Time.deltaTime;
        }
 
        y = ClampAngle(y, yMinLimit, yMaxLimit);
 
        x_Smoothed = Mathf.Lerp(x_Smoothed, x, Time.deltaTime * 10f);
        y_Smoothed = Mathf.Lerp(y_Smoothed, y, Time.deltaTime * 10f);
 
        Quaternion rotation = Quaternion.Euler(y_Smoothed, x_Smoothed, 0);
        Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.transform.position;
        position += offset;
 
        transform.rotation = rotation;
        transform.position = position;
    }
 
    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
 
    private void Reset()
    {
        GetComponent<Camera>().fieldOfView = 30f;
    }
}