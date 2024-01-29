using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BKMusic : MonoBehaviour
{
    private static BKMusic instance;
    public static BKMusic Instacne => instance;

    private AudioSource bkSource;

    void Awake()
    {
        instance = this;

        bkSource = GetComponent<AudioSource>();

        MusicData data = GameDataMgr.Instance.musicData;

        ChangeValue(data.musicValue);
    }
    public void ChangeValue(float v)
    {
        bkSource.volume = v;
    }
}
