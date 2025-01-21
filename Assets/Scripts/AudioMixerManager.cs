using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerManager : MonoBehaviour
{
    
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioMixerGroup musicGroup;
    [SerializeField] private AudioMixerGroup soundGroup;

    private bool isMusicMuted = false; 
    private bool isSFXMuted = false;
    
    public void ToggleMusicMute()
    {
        isMusicMuted = !isMusicMuted;
        audioMixer.SetFloat("MusicMixer", isMusicMuted ? -80f : 0f);
    }

    public void ToggleSFXMute()
    {
        isSFXMuted = !isSFXMuted;
        audioMixer.SetFloat("SFXMixer", isSFXMuted ? -80f : 0f);
    }

    
}
