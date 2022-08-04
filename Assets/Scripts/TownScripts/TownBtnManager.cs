using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownBtnManager : MonoBehaviour
{

    public void GoDungeon()
    {
        TownUIManager.Instance.MoveToMainScene();
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
