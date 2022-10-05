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
    [SerializeField] GameObject shopPanel;
    [SerializeField] TextMeshProUGUI moneyTmp;

    [Header("Dialogue")]
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] TextMeshProUGUI npcName;
    [SerializeField] TextMeshProUGUI content;

    [Header("��������")]
    //[SerializeField] string smithName = "Smith";
    [SerializeField] string smithName = "��������";
    //[SerializeField] string smithContent = "Welcome! Wanna change your weapon?";
    [SerializeField] string smithContent = "����Գ�! ���⸦ �ٲٰ�ͳ�?";
    [SerializeField] GameObject changeWeaponPanel;

    [Header("�Ǹſ�")]
    //[SerializeField] string salesmanName = "SalesMan";
    [SerializeField] string salesmanName = "�Ǹſ�";
    //[SerializeField] string salesmanContent = "No items have been added to the store yet!";
    [SerializeField] string salesmanContent = "������ �����Ϸ� ���̳���?";

    [Header("��")]
    [SerializeField] string homeName = "����";
    [SerializeField] string homeContent = "��Ŭ�� : �޼չ��� �Ѱ���, ��Ŭ�� : ������ �������, E : ���⽺ų �ߵ�";

    [Header("â��")]
    [SerializeField] string etcName = "â��";
    [SerializeField] string etcContent = "���� ���׷��̵带 �� �� �����ϴ�!";

    // AboutInteraction
    public bool isDialogue = false;
    public bool isDialogueWithNpc = false;
    public bool isWeaponChoose = false;

    private static bool isFirst = false;

    private void Start()
    {
        DisActiveAllPanel();
        Fade.Instance.FadeOut();
        if (!isFirst)
        {
            isFirst = true;
            changeWeaponPanel.SetActive(true);
            isWeaponChoose = true;
        }
        moneyTmp.text = string.Format("{0}", SaveManager.Instance.CurrentUser.money);
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
        shopPanel.SetActive(false);
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
        SaveManager.Instance.SaveToJson();
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
            isDialogueWithNpc = true;
            isDialogue = false;
            dialoguePanel.SetActive(false);
            shopPanel.SetActive(true);
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

    public void InteractionHome()
    {
        if (isDialogue)
        {
            isDialogue = false;
            dialoguePanel.SetActive(false);
        }
        else
        {
            Debug.Log("dialogueWithHome");
            SetDialogueInfo(homeName, homeContent);
            DialogueSystem.Instance.Begin();
            dialoguePanel.SetActive(true);
            isDialogue = true;
        }
    }

    public void InteractionETC()
    {
        if (isDialogue)
        {
            isDialogue = false;
            dialoguePanel.SetActive(false);
        }
        else
        {
            Debug.Log("dialogueWithETC");
            SetDialogueInfo(etcName, etcContent);
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

    public void ExitGame()
    {
        SaveManager.Instance.SaveToJson();
        Application.Quit();
    }
}
