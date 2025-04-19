using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class RagdollFall : MonoBehaviour
{
    public float maxTiltAngle = 60f;
    private bool hasFallen = false;
    public float fallThresholdDuration = 0.75f;
    private float tiltTimer = 0f;

    public GameObject headObject; // Assign your lamb's head GameObject in the Inspector

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

        // Do NOT reload scene yet—we wait for the head to hit the fall zone
    }

    public void TriggerFallRestart()
    {
        Debug.Log("Head hit fall zone — restarting in 2 seconds!");
        StartCoroutine(ReloadWithDelay(2f)); // 2 seconds delay
    }

    private IEnumerator ReloadWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    float NormalizeAngle(float z)
    {
        if (z > 180f) z -= 360f;
        return z;
    }
}