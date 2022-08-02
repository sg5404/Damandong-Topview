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

}
