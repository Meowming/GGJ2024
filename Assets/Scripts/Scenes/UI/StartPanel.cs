using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartPanel : BasePanel
{
    public Button StartButton;

    public Button SettingButton;

    public Button QuitButton;

    public Animation animation;
    public override void Init()
    {
        StartButton.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<StartPanel>();

            UIManager.Instance.ShowPanel<TransitionPanel>();
        });

        SettingButton.onClick.AddListener(() =>
        {
            UIManager.Instance.ShowPanel<SettingPanel>();
        });

        QuitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}
