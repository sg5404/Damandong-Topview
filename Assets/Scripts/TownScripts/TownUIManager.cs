using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TownUIManager : MonoSingleton<TownUIManager>
{
    [SerializeField] GameObject dungeonPortal;
    [SerializeField] GameObject goDungeonPanel;

    [Header("Dialogue")]
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] TextMeshProUGUI npcName;
    [SerializeField] TextMeshProUGUI content;

    [Header("��������")]
    [SerializeField] string smithName = "Smith";
    [SerializeField] string smithContent = "Welcome! Wanna Change your Weapon?";
    [SerializeField] GameObject changeWeaponPanel;

    bool isDialogue = false;

    private void Start()
    {
        DisActiveAllPanel();
    }

    public void DisActiveAllPanel()
    {
        goDungeonPanel.SetActive(false);
        dialoguePanel.SetActive(false);
    }

    public void ToggleGoDungeonPanel(bool isActive)
    {
        goDungeonPanel.SetActive(isActive);
    }

    public void MoveToMainScene()
    {
        SceneManager.LoadScene(2);
    }

    public bool isDialogueWithSmith = false;
    public IEnumerator InteractionSmith()
    {
        if (isDialogue)
        {
            isDialogueWithSmith = true;

            Debug.Log("ChangeWeapon");
            dialoguePanel.SetActive(false);
            //changeWeaponPanel.SetActive(true);
            yield return new WaitForSeconds(3f);
            isDialogueWithSmith = false;
        }
        else
        {
            Debug.Log("dialogueWithSmith");
            npcName.text = smithName;
            content.text = smithContent;
            dialoguePanel.SetActive(true);
            isDialogue = true;
            yield return new WaitForSeconds(5f);
            dialoguePanel.SetActive(false);
            isDialogue = false;
        }


    }
}