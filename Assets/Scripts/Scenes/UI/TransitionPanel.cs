using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionPanel : BasePanel
{
    public override void Init()
    {
        Load();
    }

    private void Load()
    {
            StartCoroutine(Loadscence());
    }
    IEnumerator Loadscence()
    {
        yield return new WaitForSeconds(1);

        UIManager.Instance.HidePanel<TransitionPanel>();

        SceneManager.LoadScene("Level1");

        UIManager.Instance.ShowPanel<Level1Panel>();
    }
}
