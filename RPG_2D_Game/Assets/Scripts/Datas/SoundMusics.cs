using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMusics : MonoBehaviour
{
    private static SoundMusics instance;
    public static SoundMusics Instance => instance;
    private AudioSource soundMusic;
    private void Awake()
    {
        instance = this;
        soundMusic = GetComponent<AudioSource>();

        MusicDatas data = MusicDataManager.Instance.musicData;
        SetIsOpen(data.soundOpen);
        ChangeValue(data.soundValue);
    }

    public void PlaySound(string sound )
    {
            soundMusic.clip = Resources.Load<AudioClip>(sound);
            soundMusic.Play();
    }
    public void SetIsOpen(bool isOpen)
    {
        soundMusic.mute = !isOpen;
    }
    public void ChangeValue(float value)
    {
        soundMusic.volume = value;
    }
}
