using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseGameManager : MonoBehaviour
{
    public CanvasGroup pausePanel;
    public Rigidbody2D playerRb;
    public LambBalance playerMovement; // Your movement script (whatever it's called)
    public AudioSource musicSource; // Assign your music AudioSource here

    private bool isPaused = false;
    private float originalGravity;

    void Start()
    {
        pausePanel.alpha = 0f;
        pausePanel.blocksRaycasts = false; // Not clickable when invisible
        originalGravity = playerRb.gravityScale;
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            pausePanel.alpha = 1f;
            pausePanel.blocksRaycasts = true;
            Time.timeScale = 0f; // Freeze time
            playerRb.gravityScale = 0f; // No falling
            playerRb.linearVelocity = Vector2.zero; // Stop movement immediately
            if (playerMovement != null) playerMovement.enabled = false; // Disable input
            if (musicSource != null) musicSource.Pause();
        }
        else
        {
            pausePanel.alpha = 0f;
            pausePanel.blocksRaycasts = false;
            Time.timeScale = 1f; // Resume time
            playerRb.gravityScale = originalGravity; // Restore gravity
            if (playerMovement != null) playerMovement.enabled = true; // Re-enable input
            if (musicSource != null) musicSource.UnPause();
        }
    }
}
