using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] GameObject pausePanel;

    [SerializeField] Sprite[] weaponSprites;

    [SerializeField] GameObject[] mainWeaponImageObj; // right
    [SerializeField] GameObject[] subWeaponImageObj; // left

    [SerializeField] public Slider plyerHpSlider;
    [SerializeField] public TextMeshProUGUI playerHpTmp;
    //[SerializeField] public TextMeshProUGUI main_magazineQuantity;
    //[SerializeField] public TextMeshProUGUI sub_magazineQuantity;

    public bool isStoped = false;

    private void Start()
    {
        pausePanel.SetActive(false);
        ChangeUIWeaponSpriteImg(PlayerAttack.Instance.leftWeapon);
        mainWeaponImageObj[PlayerCtrl.Instance.rightWeapon].SetActive(true);
        Fade.Instance.FadeOut();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePausePanel();
        }
    }

    public void ChangeUIWeaponSpriteImg(int leftWeaponNumber)
    {
        DisableAllWeaponSpriteImg();
        subWeaponImageObj[leftWeaponNumber].SetActive(true);

        //sub_magazineQuantity.text = $"{PlayerBase.Instance.SubMagazine}/{PlayerBase.Instance.SubMaxMagazine}";
    }

    void DisableAllWeaponSpriteImg()
    {
        foreach(var imgItem in subWeaponImageObj)
        {
            imgItem.SetActive(false);
        }
    }

    public void TogglePausePanel()
    {
        pausePanel.SetActive(!pausePanel.activeSelf);
        isStoped = !isStoped;
        if (isStoped)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void ReturnToTown()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("TownScene");
    }

    public IEnumerator ReturnToTownCoroutine()
    {
        Fade.Instance.FadeIn();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("TownScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}