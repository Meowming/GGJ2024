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
        //��ʼ�������ʾ���� ���ݱ��ش洢��������������ʼ��
        MusicData data = GameDataMgr.Instance.musicData;

        ////��ʼ�����ؿؼ���״̬
        //togMusic.isOn = data.musicOpen;
        //togSound.isOn = data.soundOpen;

        //��ʼ���϶����ؼ��Ĵ�С
        sliderMusic.value = data.musicValue;
        //sliderSound.value = data.soundValue;

        btnClose.onClick.AddListener(() =>
        {
            //��������ɺ� �ر����ʱ �Ż�ȥ��¼����
            GameDataMgr.Instance.SaveMusicData();

            //�����Լ� �����������
            UIManager.Instance.HidePanel<SettingPanel>();
        });

        sliderMusic.onValueChanged.AddListener((v) =>
        {
            //�ñ������ִ�С�ı�
            BKMusic.Instacne.ChangeValue(v);
            //��¼���������ִ�С����
            GameDataMgr.Instance.musicData.musicValue = v;
        });

        //sliderSound.onValueChanged.AddListener((v) =>
        //{
        //    //��¼��Ч��С������
        //    GameDataMgr.Instance.musicData.soundValue = v;
        //});
    }
}
