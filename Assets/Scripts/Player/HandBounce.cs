using UnityEngine;

public class HandBounce : MonoBehaviour
{
    public Rigidbody2D playerRb; // Will be assigned to the hips
    public float bounceForce = 20f;
    public string ropeTag = "Rope"; // Tag the rope's empty box collider "Rope"

    private bool hasBounced = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasBounced) return;

        if (collision.collider.CompareTag(ropeTag))
        {
            Debug.Log("Hand touched rope!!");

            if (playerRb != null)
            {
                // Applies a soft upward bounce
                playerRb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
                hasBounced = true;
                Invoke("ResetBounce", 0.5f); // Bounce cooldown
            }
        }
    }

    private void ResertBounce()
    {
        hasBounced = false;
    }
}
