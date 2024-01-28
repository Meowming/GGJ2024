using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting1Panel : BasePanel
{
    public Button btnClose;

    public Button btnQuit;

    public Slider sliderMusic;
    public override void Init()
    {
        MusicData data = GameDataMgr.Instance.musicData;

        sliderMusic.value = data.musicValue;

        btnClose.onClick.AddListener(() =>
        {
            GameDataMgr.Instance.SaveMusicData();

            UIManager.Instance.HidePanel<Setting1Panel>();
        });

        btnQuit.onClick.AddListener(() =>
        {
            Application.Quit();
        });

        sliderMusic.onValueChanged.AddListener((v) =>
        {
            BKMusic.Instacne.ChangeValue(v);

            GameDataMgr.Instance.musicData.musicValue = v;
        });
    }
}
