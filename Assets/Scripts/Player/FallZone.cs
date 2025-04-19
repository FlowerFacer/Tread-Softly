using UnityEngine;
using UnityEngine.SceneManagement;

public class FallZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Head"))
        {
            Debug.Log("Head hit the fall zone rope!");

            RagdollFall fallManager = FindFirstObjectByType<RagdollFall>();
            if (fallManager != null)
                fallManager.TriggerFallRestart();
        }
    }
}
