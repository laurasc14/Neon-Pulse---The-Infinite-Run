using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    public AudioClip menuMusic;
    public AudioClip gameMusic;
    public AudioClip pauseMusic;
    public AudioClip settingsMusic;

    private AudioSource audioSource;

    private Coroutine fadeCoroutine;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();

        audioSource.volume = PlayerPrefs.GetFloat("MusicVolume", 1f);

    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "MainPrincipal":
                PlayMusic(menuMusic);
                break;
            case "Game":
                PlayMusic(gameMusic);
                break;
            case "Controls":
            case "Options":
                PlayMusic(settingsMusic);
                break;
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        if (audioSource.clip == clip) return;

        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadeToMusic(clip));
    }

    IEnumerator FadeToMusic(AudioClip newClip)
    {
        float duration = 1.0f;
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.unscaledDeltaTime / duration;
            yield return null;
        }

        audioSource.Stop();
        audioSource.clip = newClip;
        audioSource.Play();

        while (audioSource.volume < startVolume)
        {
            audioSource.volume += startVolume * Time.unscaledDeltaTime / duration;
            yield return null;
        }

        audioSource.volume = startVolume;
    }
}

