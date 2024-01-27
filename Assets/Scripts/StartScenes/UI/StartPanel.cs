using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartPanel : BasePanel
{
    public Button StartButton;

    public Button QuitButton;
    public override void Init()
    {
        StartButton.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<StartPanel>();

            SceneManager.LoadScene("BeginScenes");
        });

        QuitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}
