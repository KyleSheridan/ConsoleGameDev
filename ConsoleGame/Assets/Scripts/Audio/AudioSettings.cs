using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSettings : MonoBehaviour
{
    FMOD.Studio.EventInstance SFXVolumeTestEvent;

    FMOD.Studio.Bus Music;
    FMOD.Studio.Bus SFX;
    FMOD.Studio.Bus Master;
    float musicVolume = 0.5f;
    float sfxVolume = 0.5f;
    float masterVolume = 1f;

    void Awake()
    {
        Music = FMODUnity.RuntimeManager.GetBus("bus:/Music");
        SFX = FMODUnity.RuntimeManager.GetBus("bus:/SFX"); 
        Master = FMODUnity.RuntimeManager.GetBus("bus:/");
        SFXVolumeTestEvent = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/SFXVolumeTest");
    }

    // Update is called once per frame
    void Update()
    {
        Music.setVolume(musicVolume);
        SFX.setVolume(sfxVolume);
        Master.setVolume(masterVolume);
    }

    public void MasterVolumeLevel(float newMasterVolume)
    {
        masterVolume = newMasterVolume;

        FMOD.Studio.PLAYBACK_STATE PbState;
        SFXVolumeTestEvent.getPlaybackState(out PbState);
        if (PbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            SFXVolumeTestEvent.start();
        }
    }
    public void MusicVolumeLevel(float newMusicVolume)
    {
        musicVolume = newMusicVolume;
    }
    public void SFXVolumeLevel(float newSFXVolume)
    {
        sfxVolume = newSFXVolume;

        FMOD.Studio.PLAYBACK_STATE PbState;
        SFXVolumeTestEvent.getPlaybackState(out PbState);
        if(PbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            SFXVolumeTestEvent.start();
        }
    }
}
