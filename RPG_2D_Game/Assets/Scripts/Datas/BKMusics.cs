using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BKMusics : MonoBehaviour
{
    private static BKMusics instance;

    public static BKMusics Instance => instance;

    private AudioSource bkSource;

    private void Awake()
    {
        instance = this;

        bkSource = this.GetComponent<AudioSource>();

        MusicDatas data = MusicDataManager.Instance.musicData;
        SetIsOpen(data.musicOpen);
        ChangeValue(data.musicValue);
    }

    public void SetIsOpen(bool isOpen)
    {
        bkSource.mute = !isOpen;
    }

    public void ChangeValue(float v)
    {
        bkSource.volume = v;
    }
}
