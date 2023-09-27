using System.Collections.Generic;
using UnityEngine;

public enum AudioStates
{
    Pull,
    CarPull,
    Collect,
    Hit,
    Coin,
    Upgrade,
}

public class AudioManager : MonoBehaviour
{
    [SerializeField] private List<AudioSource> audioSources;
    [SerializeField] private List<AudioClip> sounds;
    [SerializeField] private GameObject themeTrack;

    public static AudioManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void StartThemeTrack()
    {
        themeTrack.SetActive(true);
    }
    public void StopThemeTrack()
    {
        themeTrack.SetActive(false);
    }

    public void OnPlaySound(AudioStates soundState, bool isLoop)
    {
        PlaySoundAtIndex((int)soundState, isLoop);
    }

    public void OnStopSound(AudioStates soundState)
    {
        StopSoundAtIndex((int)soundState);
    }

    private void PlaySoundAtIndex(int index, bool loop)
    {
        foreach (var audioSource in audioSources)
        {
            if (audioSource.isPlaying)
            {
                continue;
            }

            if (index < sounds.Count)
            {
                audioSource.clip = sounds[index];
                audioSource.loop = loop;
                audioSource.Play();
            }
            else
            {
                Debug.LogWarning("Sound not found at index: " + index);
            }

            break;
        }
    }

    private void StopSoundAtIndex(int index)
    {
        if (index < audioSources.Count)
        {
            audioSources[index].Stop();
        }
        else
        {
            Debug.LogWarning("AudioSource not found at index: " + index);
        }
    }
}