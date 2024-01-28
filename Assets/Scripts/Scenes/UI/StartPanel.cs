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
    public override void Init()
    {
        StartButton.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<StartPanel>();

            SceneManager.LoadScene("Level1");
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
