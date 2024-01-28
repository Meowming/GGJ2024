using Platformer.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataMgr
{
    private static GameDataMgr instance =  new GameDataMgr();
    public static GameDataMgr Instance => instance;

    //��Ч�������
    public MusicData musicData;

    private GameDataMgr()
    {
        //��ʼ��һЩĬ������
        musicData = JsonMgr.Instance.LoadData<MusicData>("MusicData");
    }

    /// <summary>
    /// �洢��Ч����
    /// </summary>
    public void SaveMusicData()
    {
        JsonMgr.Instance.SaveData(musicData, "MusicData");
    }

    /// <summary>
    /// ������Ч����
    /// </summary>
    /// <param name="resName"></param>
    public void PlaySound(string resName)
    {
        GameObject musicObj = new GameObject();
        AudioSource a = musicObj.AddComponent<AudioSource>();
        a.clip = Resources.Load<AudioClip>(resName);
        a.volume = musicData.soundValue;
        //a.mute = !musicData.soundOpen;
        a.Play();

        GameObject.Destroy(musicObj, 1);
    }
}
