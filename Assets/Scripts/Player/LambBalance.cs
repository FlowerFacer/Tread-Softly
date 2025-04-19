using UnityEngine;

public class LambBalance : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float balanceTorque = 20f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float input = Input.GetAxis("Horizontal");

        Vector2 targetPos = rb.position + new Vector2(input * moveSpeed * Time.fixedDeltaTime, 0);
        rb.MovePosition(targetPos);

        // Only apply upright correction if she is not too far rotated
        float angle = NormalizeAngle(rb.rotation);
        if (Mathf.Abs(angle) < 45f)
        {
            float correction = Mathf.Lerp(0, angle * balanceTorque, 0.5f); // smoother
            rb.AddTorque(-correction * Time.fixedDeltaTime);
        }

        if (Mathf.Abs(rb.linearVelocity.x) > 0.1f && Mathf.Abs(angle) < 10f)
        {
            rb.AddForce(Vector2.up * 5f); // tiny hop to resist floor stick
        }
    }

    float NormalizeAngle(float angle)
    {
        if (angle > 180f) angle -= 360f;
        return angle;
    }
}
