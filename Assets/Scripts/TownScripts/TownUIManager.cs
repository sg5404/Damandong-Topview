using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TownUIManager : MonoSingleton<TownUIManager>
{
    [SerializeField] GameObject DungeonPortal;
    [SerializeField] GameObject GoDungeonPanel;

    private void Start()
    {
        DisActiveAllPanel();
    }

    void DisActiveAllPanel()
    {
        GoDungeonPanel.SetActive(false);
    }

    public void ToggleGoDungeonPanel(bool isActive)
    {
        GoDungeonPanel.SetActive(isActive);
    }

    public void MoveToMainScene()
    {
        SceneManager.LoadScene(2);
    }

}
