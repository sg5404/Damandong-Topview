using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownBtnManager : MonoBehaviour
{

    public void GoDungeon()
    {
        StartCoroutine(GoDungeonCoroutine());
    }

    public IEnumerator GoDungeonCoroutine()
    {
        Fade.Instance.FadeIn();
        yield return new WaitForSeconds(1f);
        TownUIManager.Instance.MoveToMainScene();
        yield break;
    }

    public void DisActiveGoDungeonPanel()
    {
        TownUIManager.Instance.ToggleGoDungeonPanel(false);
    }

    public void Click()
    {
        TownUIManager.Instance.DisActiveAllPanel();
        TownUIManager.Instance.isDialogueWithNpc = false;
        TownUIManager.Instance.isDialogue = false;
        TownUIManager.Instance.isWeaponChoose = false;
    }

}
