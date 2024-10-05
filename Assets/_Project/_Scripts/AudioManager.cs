using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public float delay;
    public AudioSource source;
    public AudioClip clip;

    public DistanceChecker distanceChecker;
    
    public float distanceToStartQuery;
    public float distanceToStop;

    public bool stopAllOtherAudio;
    
    public static event Action<AudioManager> OnAudioStartEvent;
    public static event Action StopAllAudioEvent;

    private bool _isPlaying;

    private void Awake()
    {
        source.clip = clip;
        
        if (stopAllOtherAudio)
            OnAudioStartEvent += OnAudioStart;

        StopAllAudioEvent += Stop;
    }

    private void OnDestroy()
    {
        OnAudioStartEvent -= OnAudioStart;
        StopAllAudioEvent -= Stop;
    }

    private void Update()
    {
        if (_isPlaying && distanceChecker.currentDistance >= distanceToStartQuery)
        {
            source.volume = 1 - (distanceChecker.currentDistance - distanceToStartQuery) / distanceToStop;
            
            if (source.volume == 0)
                Stop();
        }
    }

    public void Play()
    {
        if (delay <= 0) source.Play();
        else source.PlayDelayed(delay);

        _isPlaying = true;
        OnAudioStartEvent?.Invoke(this);
    }

    public void Stop()
    {
        source.Stop();
        _isPlaying = false;
    }

    public static void StopAll()
    {
        StopAllAudioEvent?.Invoke();
    }

    public void OnAudioStart(AudioManager manager)
    {
        if (manager == this) return;
        if (!_isPlaying) return;
        Stop();
    }
}
