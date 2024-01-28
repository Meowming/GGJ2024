using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : BasePanel
{
    public Button btnClose;

    public Slider sliderMusic;

    //public Slider sliderSound;

    public override void Init()
    {
        //初始化面板显示内容 根据本地存储的设置数据来初始化
        MusicData data = GameDataMgr.Instance.musicData;

        ////初始化开关控件的状态
        //togMusic.isOn = data.musicOpen;
        //togSound.isOn = data.soundOpen;

        //初始化拖动条控件的大小
        sliderMusic.value = data.musicValue;
        //sliderSound.value = data.soundValue;

        btnClose.onClick.AddListener(() =>
        {
            //当设置完成后 关闭面板时 才会去记录数据
            GameDataMgr.Instance.SaveMusicData();

            //隐藏自己 隐藏设置面板
            UIManager.Instance.HidePanel<SettingPanel>();
        });

        sliderMusic.onValueChanged.AddListener((v) =>
        {
            //让背景音乐大小改变
            BKMusic.Instacne.ChangeValue(v);
            //记录被背景音乐大小数据
            GameDataMgr.Instance.musicData.musicValue = v;
        });

        //sliderSound.onValueChanged.AddListener((v) =>
        //{
        //    //记录音效大小的数据
        //    GameDataMgr.Instance.musicData.soundValue = v;
        //});
    }
}
