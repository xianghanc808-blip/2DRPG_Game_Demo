using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicPanel : MonoBehaviour
{
    public Button btnClose;
    public Toggle togMusic;
    public Toggle togSound;
    public Slider sliderMusic;
    public Slider sliderSound;

    public CanvasGroup canvas;
    private bool isOpenOrClose;

    public void Start()
    {
        MusicDatas data = MusicDataManager.Instance.musicData;
        togMusic.isOn = data.musicOpen;
        togSound.isOn = data.soundOpen;

        sliderMusic.value = data.musicValue;
        sliderSound.value = data.soundValue;


        togMusic.onValueChanged.AddListener((v) =>
        {
            BKMusics.Instance.SetIsOpen(v);
            MusicDataManager.Instance.musicData.musicOpen = v;
            SoundMusics.Instance.PlaySound("switch-a");
        });

        togSound.onValueChanged.AddListener((v) =>
        {
            SoundMusics.Instance.SetIsOpen(v);
            MusicDataManager.Instance.musicData.soundOpen = v;
            SoundMusics.Instance.PlaySound("switch-a");
        });
        sliderMusic.onValueChanged.AddListener((v) =>
        {
            BKMusics.Instance.ChangeValue(v);
            MusicDataManager.Instance.musicData.musicValue = v;
        });
        sliderSound.onValueChanged.AddListener((v) =>
        {
            SoundMusics.Instance.ChangeValue(v);
            MusicDataManager.Instance.musicData.soundValue = v;
        });
    }

    public void HideThisPanel()
    {
        if (isOpenOrClose)
        {
            isOpenOrClose = !isOpenOrClose;
            canvas.alpha = 0;
            canvas.interactable = false;
            canvas.blocksRaycasts = false;
            SoundMusics.Instance.PlaySound("switch-a");

            MusicDataManager.Instance.SaveMusicDatas();
        }
        else
        {
            isOpenOrClose = !isOpenOrClose;
            canvas.alpha = 1;
            canvas.interactable = true;
            canvas.blocksRaycasts = true;
            SoundMusics.Instance.PlaySound("switch-a");

        }
    }
}
