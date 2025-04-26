using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionTrigger : MonoBehaviour
{
    public CanvasGroup fadeCanvas; // The black screen canvas group
    public string sceneToLoad = "MainMenu"; // Main menu scene name
    public float fadeSpeed = 1.5f; // Speed of fade
    public float delayBeforeLoad = 2f; // Seconds to wait before loading

    private bool triggered = false;
    private float timer = 0f;

    void Start()
    {
        if (fadeCanvas != null)
            fadeCanvas.alpha = 0f; // Start fully transparent
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;
        }
    }

    void Update()
    {
        if (triggered)
        {
            if (fadeCanvas != null)
                fadeCanvas.alpha += Time.unscaledDeltaTime * fadeSpeed; // Fade to black

            timer += Time.unscaledDeltaTime;

            if (timer > delayBeforeLoad)
            {
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }
}
