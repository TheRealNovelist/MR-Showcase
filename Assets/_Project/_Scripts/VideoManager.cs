using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    public VideoPlayer video;
    
    public void Play()
    {
        video.Play();
    }

    public void Stop()
    {
        video.Stop();
    }
}
