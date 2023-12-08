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
        "Mining",
        "denied",
        "Confirmation",
        "PutDown",
        "Warrior roar",
        "portal",
        "Building demolish",
        "removeActionSound",
        "Click",
    };

    private List<AudioClip> _sounds = new List<AudioClip>();


    void Awake() 
    {
        if (Instance == null) 
        {
            Instance = this; 
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Destroy(gameObject);
        }

        // Load the sounds
        LoadSoundClips();
    }

    private void LoadSoundClips()
    {
        foreach(string clipName in soundClipNames)
        {
            AudioClip clip = Resources.Load<AudioClip>(clipName);
            if(clip != null)
            {
                _sounds.Add(clip);
            }
        }
    }

    private AudioClip _GetClip(string name)
    {
        AudioClip clipToReturn = null;
        foreach(AudioClip clip in _sounds)
        {
            if(clip.name == name)
                clipToReturn = clip;
        }

        return clipToReturn;
    }

    public void PlayActionSound(string name) 
    {
        _ActionNotificationSound.PlayOneShot(_GetClip(name));
    }

    public void PlayActionSound(AudioClip clip)
    {

        _ActionNotificationSound.PlayOneShot(clip);
    }
}

