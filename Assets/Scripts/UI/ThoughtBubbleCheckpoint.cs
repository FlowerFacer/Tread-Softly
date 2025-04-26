using UnityEngine;
using TMPro;

public class ThoughtBubbleCheckpoint : MonoBehaviour
{
    public CanvasGroup thoughtBubble;
    public Animator lambAnimator;
    public string talkingAnimationTrigger = "Talk";
    public float displayTime = 4f;
    public float fadeSpeed = 2f;

    private bool triggered = false;
    private float timer = 0f;

    void Start()
    {
        thoughtBubble.alpha = 0f; // Always invisible at start
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;
            thoughtBubble.alpha = 1f;
            lambAnimator.SetTrigger(talkingAnimationTrigger);
        }
    }

    void Update()
    {
        if (triggered)
        {
            timer += Time.deltaTime;

            if (timer > displayTime)
            {
                thoughtBubble.alpha -= Time.deltaTime * fadeSpeed;
                if (thoughtBubble.alpha <= 0f)
                {
                    gameObject.SetActive(false); // Disable this trigger after fading
                }
            }
        }
    }
}
