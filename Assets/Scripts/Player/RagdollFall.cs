using UnityEngine;
using UnityEngine.SceneManagement;

public class RagdollFall : MonoBehaviour
{
    public float maxTiltAngle = 60f;
    private bool hasFallen = false;
    public float fallThresholdDuration = 0.75f;
    private float tiltTimer = 0f;

    void Update()
    {
        if (hasFallen) return;

        float zRot = NormalizeAngle(transform.eulerAngles.z);
        if (Mathf.Abs(zRot) > maxTiltAngle)
        {
            tiltTimer += Time.deltaTime;
            if (tiltTimer >= fallThresholdDuration)
                EnableRagdoll();
        }
        else
        {
            tiltTimer = 0f;
        }
    }

    void EnableRagdoll()
    {
        hasFallen = true;

        // Disable constraints for ragdoll effect
        Rigidbody2D[] bodies = GetComponentsInChildren<Rigidbody2D>();
        foreach (Rigidbody2D rb in bodies)
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.gravityScale = 1f;
        }

        Invoke("ReloadScene", 2f);
    }

    void ReloadScene()
    {
        Debug.Log("Lamb has fallen! Scene will reload.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    float NormalizeAngle(float z)
    {
        if (z > 180f) z -= 360f;
        return z;
    }
}