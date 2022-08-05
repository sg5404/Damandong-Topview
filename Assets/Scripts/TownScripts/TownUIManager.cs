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
    [SerializeField] GameObject pausePanel;

    [Header("Dialogue")]
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] TextMeshProUGUI npcName;
    [SerializeField] TextMeshProUGUI content;

    [Header("대장장이")]
    //[SerializeField] string smithName = "Smith";
    [SerializeField] string smithName = "대장장이";
    //[SerializeField] string smithContent = "Welcome! Wanna change your weapon?";
    [SerializeField] string smithContent = "어서오게나! 무기를 바꾸고싶나?";
    [SerializeField] GameObject changeWeaponPanel;

    [Header("판매원")]
    //[SerializeField] string salesmanName = "SalesMan";
    [SerializeField] string salesmanName = "판매원";
    //[SerializeField] string salesmanContent = "No items have been added to the store yet!";
    [SerializeField] string salesmanContent = "상점에 아이템재고가 들어오지 않았습니다!";

    // AboutInteraction
    public bool isDialogue = false;
    public bool isDialogueWithNpc = false;
    public bool isWeaponChoose = false;

    private bool isFirst = false;

    private void Start()
    {
        DisActiveAllPanel();
        if (!isFirst)
        {
            isFirst = true;
            changeWeaponPanel.SetActive(true);
            isWeaponChoose = true;
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)&&!isWeaponChoose)
        {
            TogglePausePanel(!pausePanel.activeSelf);
        }
    }
    public void DisActiveAllPanel()
    {
        goDungeonPanel.SetActive(false);
        TogglePausePanel(false);
        dialoguePanel.SetActive(false);
        changeWeaponPanel.SetActive(false);
    }

    public void ToggleGoDungeonPanel(bool isActive)
    {
        goDungeonPanel.SetActive(isActive);
    }

    public void TogglePausePanel(bool isActive)
    {
        pausePanel.SetActive(isActive);
        Move.Instance.TogglePause(isActive);
    }

    public void MoveToMainScene()
    {
        SceneManager.LoadScene(2);
    }

    public void InteractionSmith()
    {
        if (isDialogue)
        {
            isDialogueWithNpc = true;
            isDialogue = false;
            Debug.Log("ChangeWeapon");
            dialoguePanel.SetActive(false);
            changeWeaponPanel.SetActive(true);
        }
        else
        {
            Debug.Log("dialogueWithSmith");
            SetDialogueInfo(smithName, smithContent);
            DialogueSystem.Instance.Begin();
            dialoguePanel.SetActive(true);
            isWeaponChoose = true;
            isDialogue = true;
        }
    }

    public void InteractionSalesman()
    {
        if (isDialogue)
        {
            isDialogue = false;
            dialoguePanel.SetActive(false);
        }
        else
        {
            Debug.Log("dialogueWithSalesMan");
            SetDialogueInfo(salesmanName, salesmanContent);
            DialogueSystem.Instance.Begin();
            dialoguePanel.SetActive(true);
            isDialogue = true;
        }
    }

    public void SetDialogueInfo(string name, string content)
    {
        Dialogue.Instance.NpcName = name;
        Dialogue.Instance.sentences.Clear();
        Dialogue.Instance.sentences.Add(content);
    }
}
