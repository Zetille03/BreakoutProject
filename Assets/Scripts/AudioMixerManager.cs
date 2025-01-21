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

    private bool _isMusicMuted = false; 
    private bool _isSFXMuted = false;
    
    public void ToggleMusicMute()
    {
        _isMusicMuted = !_isMusicMuted;
        audioMixer.SetFloat("MusicMixer", _isMusicMuted ? -80f : 0f);
    }

    public void ToggleSFXMute()
    {
        _isSFXMuted = !_isSFXMuted;
        audioMixer.SetFloat("SFXMixer", _isSFXMuted ? -80f : 0f);
    }

    
}
