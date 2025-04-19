using UnityEngine;

public class CheckpointMusicTrigger : MonoBehaviour
{
    public AudioClip checkpointMusic;
    [Range(0f, 1f)] public float volume = 0.5f;
    private bool hasTriggered = false; // Optional: prevent multiple triggers

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasTriggered) return;

        if (other.transform.root.CompareTag("Player"))
        {
            Debug.Log("Checkpoint reached! Changing music.");

            if (BackgroundMusicManager.instance != null)
            {
                BackgroundMusicManager.instance.PlayCheckpointMusic(checkpointMusic, volume);
            }

            hasTriggered = true;
        }
    }
}