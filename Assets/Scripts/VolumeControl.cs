using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeControl : MonoBehaviour
{
    public GameObject bgm;
    public GameObject soundEffects;

    public void setVolume(float vol){
        bgm.GetComponent<AudioSource>().volume = vol;
        soundEffects.GetComponent<AudioSource>().volume = vol;
    }

    private void Start()
    {
        setVolume(0.5f);
    }
}
