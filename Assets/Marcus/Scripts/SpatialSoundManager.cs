using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]

public class SpatialSoundManager : MonoBehaviour
{
    private AudioSource _audioSource;
    private void Start() {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.spatialBlend = 1;
    }
   public void PlaySound(AudioClip clip) {
    _audioSource.PlayOneShot(clip);
}
}
