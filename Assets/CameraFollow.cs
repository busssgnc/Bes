using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float mouseSensitivity = 3f;

    [Header("Kamera Mesafesi ve Açıları")]
    public float distanceFromPlayer = 14f;
    public float minYAngle = -20f;
    public float maxYAngle = 60f;

    private float rotationX = 0f;
    private float rotationY = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (target == null) return;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotationX += mouseX;
        rotationY -= mouseY;

        rotationY = Mathf.Clamp(rotationY, minYAngle, maxYAngle);

        Quaternion rotation = Quaternion.Euler(rotationY, rotationX, 0);

        Vector3 targetPosition = target.position + new Vector3(0f, 2f, 0f) - (rotation * Vector3.forward * distanceFromPlayer);

        transform.position = targetPosition;
        transform.rotation = rotation;
    }
}