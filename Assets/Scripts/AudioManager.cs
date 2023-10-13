using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public bool useSoundEffects = true;
    public bool useMusic = true;

    private Queue<AudioSource> audioSources = new Queue<AudioSource>();

    private AudioSource musicSource;

    public static AudioManager Instance;

    protected virtual void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            print("Aawake");
            Instance = GetComponent<AudioManager>();
            DontDestroyOnLoad(Instance.gameObject);
        }
    }

    private void Start()
    {
        musicSource = GetComponent<AudioSource>();

        useSoundEffects = PlayerPrefs.GetInt("SoundEffects") == 0;
        useMusic = PlayerPrefs.GetInt("Music") == 0;

        if (!useMusic)
        {
            musicSource.volume = 0;
        }
    }

    public void PlaySoundEffect(SimpleAudioEvent audio)
    {
        if (!useSoundEffects)
        {
            return;
        }

        if (audioSources.Count == 0)
        {
            audioSources.Enqueue(gameObject.AddComponent<AudioSource>());
        }
        var source = audioSources.Dequeue();
        int index = audio.Play(source);

        StartCoroutine(ReturnToQueue(source, audio.Clips[index].length));
    }

    private IEnumerator ReturnToQueue(AudioSource source, float length)
    {
        yield return new WaitForSeconds(length);

        audioSources.Enqueue(source);
    }

    public void ToggleSoundEffects(bool isOn)
    {
        useSoundEffects = isOn;
        PlayerPrefs.SetInt("SoundEffects", useSoundEffects ? 0 : -1);
    }

    public void ToggleMusic(bool isOn)
    {
        useMusic = isOn;
        PlayerPrefs.SetInt("Music", useMusic ? 0 : -1);
    }
}
