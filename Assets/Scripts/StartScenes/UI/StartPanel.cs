using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : BasePanel
{
    public Button StartButton;

    public Button QuitButton;
    public override void Init()
    {
        StartButton.onClick.AddListener(() =>
        {

        });

        QuitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}
