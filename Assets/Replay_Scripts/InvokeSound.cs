using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokeSound : MonoBehaviour
{
    AudioSource m_MyAudioSource;

    public List<AudioClip> audioClips;
    public List<AudioSource> audiosources;

    public  List<float> rememberTime;
    // Start is called before the first frame update
    void Start()
    {
        m_MyAudioSource = GetComponent<AudioSource>();
        List<AudioClip> audioClips = new List<AudioClip>();
        List<AudioSource> audiosources = new List<AudioSource>();
        List<float> rememberTime = new List<float>();
    }

    // Update is called once per frame
    
    public void RecordSound(AudioClip audioClip, AudioSource audioSource)
    {
        if (audioSource != null && audioClip == true)
        {
            audioClips.Add(audioClip);
            audiosources.Add(audioSource);
        }
        rememberTime.Add(RePlayObjectCollecter.world_time);
    }

    public void RePlay(int rem)
    {
        if (audiosources[rem] != null)
        {
            audiosources[rem].PlayOneShot(audioClips[rem]);
        }
    }

}
