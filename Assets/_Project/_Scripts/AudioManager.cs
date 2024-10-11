using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class AudioManager : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource source;
    public AudioClip clip;
    
    [Header("Settings")]
    public float delay;
    public bool stopAllOtherAudio;
    [Space]
    public bool doFadeInAudio = false;
    public float fadeInTime = 0.5f;
    [Space]
    public bool doFadeOutAudio = true;
    
    [Header("Distance Related")]
    public DistanceChecker distanceChecker;
    public float startFadeDistance = 1f;
    public float fadeOutRange = 1f;

    [Header("Events")] 
    public UnityEvent OnAudioFinish;
    
    public static event Action<AudioManager> OnAudioStartEvent;
    public static event Action StopAllAudioEvent;
    
    private bool _isPlaying;
    private Coroutine _cacheRoutine;

    private void Awake()
    {
        if (!source)
        {
            GameObject newAudioSource = new("Audio Source");
            source = newAudioSource.AddComponent<AudioSource>();
        }

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
        if (!doFadeOutAudio || !distanceChecker) return;
        if (_isPlaying && distanceChecker.currentDistance >= startFadeDistance)
        {
            source.volume = 1 - (distanceChecker.currentDistance - startFadeDistance) / fadeOutRange;
            
            if (source.volume == 0)
                Stop();
                
        }
    }

    public void Play()
    {
        if (delay <= 0)
        {
            if (doFadeInAudio)
            {
                source.volume = 0;
                source.DOFade(1, fadeInTime);
            }
            
            source.Play();
        }
        else
        {
            if (doFadeInAudio)
            {
                Sequence fadeSequence = DOTween.Sequence();
                
                source.volume = 0;
                fadeSequence
                    .Append(source.DOFade(1, fadeInTime))
                    .PrependInterval(delay);

                fadeSequence.Play();
            }
            
            source.PlayDelayed(delay);
        }

        _cacheRoutine = StartCoroutine(WaitUntilEndOfAudio());
        
        _isPlaying = true;
        OnAudioStartEvent?.Invoke(this);
    }

    public void Stop()
    {
        StopCoroutine(_cacheRoutine);
        source.Stop();
        _isPlaying = false;
    }

    public IEnumerator WaitUntilEndOfAudio()
    {
        yield return new WaitForSeconds(delay + clip.length);
        
        OnAudioFinish.Invoke();
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
