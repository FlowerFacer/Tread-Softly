using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusicManager : MonoBehaviour
{
    public static BackgroundMusicManager instance; // Singleton instance

    [System.Serializable]
    public class SceneMusic
    {
        public string sceneName; // Scene name
        public AudioClip musicClip; // Music to play in this scene
        [Range(0f, 1f)] public float volume = 0.5f; // Volume for this scene
    }

    public AudioClip defaultMusic; // Default music if no specific scene music is set
    [Range(0f, 1f)] public float defaultVolume = 0.5f; // Default music volume
    public SceneMusic[] sceneMusicList; // List of scene-specific music

    private AudioSource currentSource;
    private AudioSource nextSource;
    private Coroutine fadeCoroutine;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        currentSource = gameObject.AddComponent<AudioSource>();
        nextSource = gameObject.AddComponent<AudioSource>();

        foreach (var source in new[] { currentSource, nextSource })
        {
            source.loop = true;
            source.playOnAwake = false;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        PreloadClips();
    }

    private void PreloadClips()
    {
        if (defaultMusic != null && defaultMusic.loadState != AudioDataLoadState.Loaded)
            defaultMusic.LoadAudioData();

        foreach (var entry in sceneMusicList)
        {
            if (entry.musicClip != null && entry.musicClip.loadState != AudioDataLoadState.Loaded)
                entry.musicClip.LoadAudioData();
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusicForScene(scene.name);
    }

    private void PlayMusicForScene(string sceneName)
    {
        AudioClip sceneMusic = defaultMusic;
        float sceneVolume = defaultVolume;

        // Find music & volume for the current scene
        foreach (var entry in sceneMusicList)
        {
            if (entry.sceneName == sceneName)
            {
                sceneMusic = entry.musicClip;
                sceneVolume = entry.volume;
                break;
            }
        }

        // If the same music is already playing, do nothing
        if (currentSource.clip == sceneMusic) return;

        FadeToNewMusic(sceneMusic, sceneVolume);
    }

    public void PlayCheckpointMusic(AudioClip newClip, float volume = 0.5f)
    {
        if (newClip == null || currentSource.clip == newClip) return;

        FadeToNewMusic(newClip, volume);
    }

    private void FadeToNewMusic(AudioClip newClip, float targetVolume)
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(CrossfadeMusic(newClip, targetVolume));
    }

    private IEnumerator CrossfadeMusic(AudioClip newClip, float targetVolume)
    {
        float fadeDuration = 1.5f;

        // Preload the audio data to avoid stutter
        if (!newClip.loadState.Equals(AudioDataLoadState.Loaded))
            newClip.LoadAudioData();

        // Prepare next source
        nextSource.clip = newClip;
        nextSource.volume = 0f;
        nextSource.Play();

        // Crossfade
        float t = 0f;
        float initialVolume = currentSource.volume;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float lerp = t / fadeDuration;

            currentSource.volume = Mathf.Lerp(initialVolume, 0f, lerp);
            nextSource.volume = Mathf.Lerp(0f, targetVolume, lerp);

            yield return null;
        }

        currentSource.Stop();
        currentSource.clip = null;

        // Swap references
        var temp = currentSource;
        currentSource = nextSource;
        nextSource = temp;

        currentSource.volume = targetVolume;
    }
    public void SetGlobalVolume(float volume)
    {
        float clampedVolume = Mathf.Clamp01(volume);
        currentSource.volume = clampedVolume;
    }
}
