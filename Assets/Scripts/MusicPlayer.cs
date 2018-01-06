using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    public float lengthInSeconds;
    public AudioClip[] clips = new AudioClip[2];
    public float volume = 1.0f;
    public bool startOnInit = false;

    private float currentVolume;
    private int currentClipIndex;
    private double nextEventTime;
    private int flip = 0;
    private AudioSource[] audioSources = new AudioSource[2];
    private bool running = false;

    void Start()
    {
        currentClipIndex = 0;
        currentVolume = volume;

        int i = 0;
        while (i < 2)
        {
            var child = new GameObject("Player");
            child.transform.parent = gameObject.transform;
            audioSources[i] = child.AddComponent<AudioSource>();
            i++;
        }

        nextEventTime = AudioSettings.dspTime;
        running = false;
    }

    void Update()
    {
        if (!running)
            return;

        double time = AudioSettings.dspTime;
        if (time + 1.0F > nextEventTime)
        {
            audioSources[flip].clip = clips[currentClipIndex];
            audioSources[flip].PlayScheduled(nextEventTime);
            Debug.Log("Scheduled source " + flip + " to start at time " + nextEventTime);
            nextEventTime += lengthInSeconds;
            flip = 1 - flip;
            currentClipIndex = (currentClipIndex + 1) % clips.Length;
        }
    }

    public void StartMusic()
    {
        running = true;
        currentClipIndex = 0;
        nextEventTime = AudioSettings.dspTime;
    }

    public void Stop()
    {
        running = false;
        foreach (var audioSource in audioSources)
        {
            audioSource.Stop();
        }
    }

    public void FadeOut(float length, float interval = 0.05F)
    {
        StartCoroutine(FadeOutCoroutine(length, interval));
    }

    public void FadeIn(float length, float interval = 0.05F)
    {
        StartCoroutine(FadeInCoroutine(length, interval));
    }

    private void ResetVolume()
    {
        foreach (var audioSource in audioSources)
        {
            audioSource.volume = volume;
        }
    }

    private IEnumerator FadeOutCoroutine(float length, float interval = 0.05F)
    {
        while (currentVolume > 0.0F)
        {
            foreach (var audioSource in audioSources)
            {
                audioSource.volume -= interval;
            }

            currentVolume -= interval;
            yield return new WaitForSeconds(length * interval);
        }
    }

    private IEnumerator FadeOutAndStopCoroutine(float length, float interval = 0.05F)
    {
        yield return FadeOutCoroutine(length, interval);
        Stop();
        ResetVolume();
    }

    private IEnumerator FadeInCoroutine(float length, float interval = 0.05F)
    {
        while (currentVolume < 1.0F)
        {
            foreach (var audioSource in audioSources)
            {
                audioSource.volume += interval;
            }

            currentVolume += interval;
            yield return new WaitForSeconds(length * interval);
        }
    }
}