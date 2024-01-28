using Platformer.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataMgr
{
    private static GameDataMgr instance =  new GameDataMgr();
    public static GameDataMgr Instance => instance;

    //音效相关数据
    public MusicData musicData;

    private GameDataMgr()
    {
        //初始化一些默认数据
        musicData = JsonMgr.Instance.LoadData<MusicData>("MusicData");
    }

    /// <summary>
    /// 存储音效数据
    /// </summary>
    public void SaveMusicData()
    {
        JsonMgr.Instance.SaveData(musicData, "MusicData");
    }

    /// <summary>
    /// 播放音效方法
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
