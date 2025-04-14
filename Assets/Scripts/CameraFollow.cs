using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;         // Your lamb character
    public float smoothSpeed = 5f;   // How quickly the camera catches up
    public Vector3 offset;           // Optional offset from target
    public bool followX = true;
    public bool followY = true;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = transform.position;

        if (followX)
            desiredPosition.x = target.position.x + offset.x;

        if (followY)
            desiredPosition.y = target.position.y + offset.y;

        desiredPosition.z = offset.z;

        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }
}
