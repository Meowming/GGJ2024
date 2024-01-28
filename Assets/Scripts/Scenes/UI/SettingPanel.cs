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
        MusicData data = GameDataMgr.Instance.musicData;

        sliderMusic.value = data.musicValue;

        btnClose.onClick.AddListener(() =>
        {
            GameDataMgr.Instance.SaveMusicData();

            UIManager.Instance.HidePanel<SettingPanel>();
        });

        sliderMusic.onValueChanged.AddListener((v) =>
        {
            BKMusic.Instacne.ChangeValue(v);

            GameDataMgr.Instance.musicData.musicValue = v;
        });
    }
}
