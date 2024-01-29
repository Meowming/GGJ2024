using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level1Panel : BasePanel
{
    public Button MenuButton;
    public override void Init()
    {
        MenuButton.onClick.AddListener(() =>
        {
            UIManager.Instance.ShowPanel<Setting1Panel>();
        });
    }
}
