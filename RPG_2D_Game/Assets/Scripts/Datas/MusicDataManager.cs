using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicDataManager
{
    private static MusicDataManager instance = new MusicDataManager();

    public static MusicDataManager Instance => instance;

    public MusicDatas musicData;

    private MusicDataManager()
    {
        musicData = JsonManager.Instance.LoadData<MusicDatas>("MusicDatas");
    }

    public void SaveMusicDatas()
    {
        JsonManager.Instance.SaveData("MusicDatas", musicData);

    }
}
