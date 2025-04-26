using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public AudioClip buttonClickSound; // ?? Assign sound in the Inspector
    [Range(0f, 1f)] public float volume = 0.5f; // ?? Adjustable volume
    public AudioClip ButtonSoundFX;

    public string sceneName; // Scene name
    public CanvasGroup pausePanel;

    private AudioSource audioSource;
    private bool isPaused = false;
    private bool fading = false;

    void Start()
    {
        pausePanel.alpha = 0f;
        pausePanel.blocksRaycasts = false;

        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); 
        }
    }

    public void PlayGame()
    {
        PlayButtonClickSound();
        SceneManager.LoadScene(sceneName);
    }

    public void ClosePanel()
    {
        PlayButtonClickSound();
        if (!isPaused) return;
        TogglePause();
    }

    public void TogglePause()
    {
        PlayButtonClickSound();
        isPaused = !isPaused;
        fading = true;

        if (isPaused)
        {
            pausePanel.alpha = 1f;  // Make the panel visible immediately
            pausePanel.blocksRaycasts = true;
            Time.timeScale = 0f;  // Freeze game
        }
        else
        {
            pausePanel.alpha = 0f; //  Hide the panel immediately
            pausePanel.blocksRaycasts = false;
            Time.timeScale = 1f;    //  Unfreeze game
        }
    }

    public void QuitGame()
    {
        PlayButtonClickSound();
        Debug.Log("Game is exiting...");
        Application.Quit(); // Closes the game
    }

    private void PlayButtonClickSound()
    {
        if (buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound, volume); // ?? Play click sound
        }
    }
}
