using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeScript : MonoBehaviour
{
    [SerializeField] AudioSource audioS;
    [SerializeField] bool isMusic;
    [SerializeField] bool isSFX;


    private void FixedUpdate()
    {
        if (isMusic)
        {
            audioS.volume = VolumeManager.musicVolume * VolumeManager.masterVolume;
        }
        if (isSFX)
        {
            audioS.volume = VolumeManager.sfxVolume * VolumeManager.masterVolume;
        }
    }
}