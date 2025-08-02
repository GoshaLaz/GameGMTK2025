using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeScript : MonoBehaviour
{
    [SerializeField] AudioSource audioS;
    [SerializeField] bool isMusic;
    [SerializeField] bool isSFX;


    private void Awake()
    {
        if (isMusic)
        {
            audioS.volume = VolumeManager.musicVolume;
        }
        if (isSFX)
        {
            audioS.volume = VolumeManager.sfxVolume;
        }

        Debug.Log(VolumeManager.sfxVolume);
    }

    private void FixedUpdate()
    {
        if (isMusic)
        {
            audioS.volume = VolumeManager.musicVolume;
        }
        if (isSFX)
        {
            audioS.volume = VolumeManager.sfxVolume;
        }

        Debug.Log(VolumeManager.sfxVolume);
    }
}