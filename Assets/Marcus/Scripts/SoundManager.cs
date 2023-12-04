using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SoundType{
    ActionNotificationSound,
    EffectSound,
    BackgroundMusicSound,
    BackgroundSound,
}
public class SoundManager : MonoBehaviour
{
public static SoundManager Instance; 

[SerializeField] private AudioSource _ActionNotificationSound, _EffectSound, _BackgroundMusicSound, _BackgroundSound;


public string[] soundClipNames = {
    "Building",
    "Click Sound",
    "Coins",
    "Coins 2",
    "Level up",
    "Mining"
};



void Awake() {
if (Instance == null) {
    Instance = this; 
    DontDestroyOnLoad(gameObject);
    }
    else {
        Destroy(gameObject);
    }
}

private void LoadSoundClips(){
    foreach(string clipName in soundClipNames)
    {
        AudioClip clip = Resources.Load<AudioClip>(clipName);
        
    }
}

public void PlaySound(AudioClip clip) {
    _ActionNotificationSound.PlayOneShot(clip);
}
}

