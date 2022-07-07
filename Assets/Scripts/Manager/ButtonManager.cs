using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField]
    private GameObject StartButton;
    [SerializeField]
    private GameObject SettingButton;
    [SerializeField]
    private GameObject QuitButton;

    public void GameStart()
    {
        SceneManager.LoadScene(1);
    }

    public void Setting()
    {
        Debug.Log("Setting");
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

}
