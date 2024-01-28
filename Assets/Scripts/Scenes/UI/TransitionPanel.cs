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
            StartCoroutine(Loadscence(name));
    }
    IEnumerator Loadscence(string name)
    {
        UIManager.Instance.HidePanel<TransitionPanel>();

        yield return new WaitForSeconds(3);

        SceneManager.LoadScene(name);

        yield return new WaitForSeconds(1);

        UIManager.Instance.ShowPanel<Level1Panel>();
    }
}
