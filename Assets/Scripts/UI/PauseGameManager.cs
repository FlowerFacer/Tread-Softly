using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseGameManager : MonoBehaviour
{
    public CanvasGroup pausePanel;
    public Rigidbody2D playerRb;
    public LambBalance playerMovement; // Your movement script (whatever it's called)
    public AudioSource musicSource; // Assign your music AudioSource here
    public AudioClip ButtonSoundFX;

    public float fadeSpeed = 3f; // Speed of fade in/out

    private bool isPaused = false;
    private bool fading = false;
    private float originalGravity;

    void Start()
    {
        pausePanel.alpha = 0f;
        pausePanel.blocksRaycasts = false;
        originalGravity = playerRb.gravityScale;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            TogglePause();
        }

        if (fading)
        {
            float targetAlpha = isPaused ? 1f : 0f;
            pausePanel.alpha = Mathf.MoveTowards(pausePanel.alpha, targetAlpha, fadeSpeed * Time.unscaledDeltaTime);

            if (Mathf.Approximately(pausePanel.alpha, targetAlpha))
            {
                fading = false;

                if (!isPaused)
                    pausePanel.blocksRaycasts = false;
            }
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        fading = true;

        if (isPaused)
        {
            pausePanel.blocksRaycasts = true;
            Time.timeScale = 0f; // Freeze time
            playerRb.gravityScale = 0f; // No falling
            playerRb.linearVelocity = Vector2.zero; // Stop movement immediately
            if (playerMovement != null) playerMovement.enabled = false; // Disable input
            if (musicSource != null) musicSource.Pause();
        }
        else
        {
            Time.timeScale = 1f; // Resume time
            playerRb.gravityScale = originalGravity; // Restore gravity
            if (playerMovement != null) playerMovement.enabled = true; // Re-enable input
            if (musicSource != null) musicSource.UnPause();
        }
    }

    // Called when Play button is clicked
    public void ResumeGame()
    {
        AudioSource.PlayClipAtPoint(ButtonSoundFX, transform.position);
        if (!isPaused) return;
        TogglePause(); 
    }

    // Called when Quit button is clicked
    public void QuitToMainMenu()
    {
        AudioSource.PlayClipAtPoint(ButtonSoundFX, transform.position);
        if (StarManager.Instance != null)
            StarManager.Instance.ResetStars();
        Time.timeScale = 1f; // Always reset timescale before scene load!
        SceneManager.LoadScene("MainMenu"); 
    }
}
