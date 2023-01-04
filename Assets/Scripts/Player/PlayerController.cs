using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoSingleton<PlayerController>
{

    [Header("�Ѿ�")]
    [SerializeField] private GameObject rifleBullet = null;
    [SerializeField] private GameObject sniperBullet = null;
    [SerializeField] private GameObject shotgunBullet = null;
    [SerializeField] private GameObject granadeBullet = null;

    [SerializeField] private Transform bulletTransform = null;
    [SerializeField] private GameObject leftGunpoint = null;
    [SerializeField] private GameObject rightGunpoint = null;
    //[SerializeField] private GameObject weaponObj = null;
    [SerializeField] private GameObject leftWeaponPos = null;
    [SerializeField] private GameObject rightWeaponPos = null;

    [SerializeField] private TextMeshProUGUI lText = null;
    [SerializeField] private TextMeshProUGUI rText = null;

    public List<GameObject> fireEff;
    public List<GameObject> fireEff2;

    public GameObject fireEffect;
    private GameObject leftEffect;
    private GameObject rightEffect;

    public GameObject[] leftWeaponList;
    public GameObject[] rightWeaponList;

    //private Vector2 weaponPos = new Vector2(0, 0);

    public CWeaponModule[] module;
    public int leftWeapon { private set; get; } = 0;
    public int rightWeapon { private set; get; } = 0;
    public bool infinityBullet { set; get; } = false;

    private float leftCurtime = 0;
    private float rightCurtime = 0;

    public float leftTimer = 0;
    public float rightTimer = 0;
    float leftRotation;

    private bool left = true;
    private bool right = false;

    GameObject bullet;

    public WeaponSet weaponSet = null;
    private PlayerSkills playerSkills = null;

    private UIManager _ui;

    public List<int> LcurrentBullet = new List<int>();
    public List<int> RcurrentBullet = new List<int>();
    public List<int> magazineAmount = new List<int>();

    //[ReadOnly] public int rifleBulletAmount = 25;
    //[ReadOnly] public int sniperBulletAmount = 5;
    //[ReadOnly] public int shotgunBulletAmount = 7;
    //[ReadOnly] public int granadeBulletAmount = 3;
    private int[] InitBulletAmounts = { 25, 5, 7, 3 };
    [ReadOnly] public int[] BulletAmounts = { 25, 5, 7, 3 };
    public float Ltimer;
    public float Rtimer;

    public Image rightWeaponFade;
    public Image leftWeaponFade;

    Vector3 leftWeaponPosTemp;
    Vector3 rightWeaponPosTemp;

    void Start()
    {
        _ui = FindObjectOfType<UIManager>();
        weaponSet = GetComponent<WeaponSet>();
        playerSkills = GetComponent<PlayerSkills>();

        //weaponPos = weaponObj.transform.localPosition;
        leftWeaponPosTemp = leftWeaponPos.transform.localPosition;
        rightWeaponPosTemp = rightWeaponPos.transform.localPosition;

        for (int i = 0; i < BulletAmounts.Length; ++i)
        {
            BulletAmounts[i] += (InitBulletAmounts[i] / 2) * PlayerStat.GetAddAmmo();
        }

        LoadBulletAmount();
        Setting();
        CurrentWeapon();
    }

    void Update()
    {
        if (PlayerCtrl.Instance.playerBase.IsDead) return;
        ReloadLeft();
        ReloadRight();
        leftCurtime += Time.deltaTime;
        rightCurtime += Time.deltaTime;
        if (!_ui.isStoped) RotateGun();
        CurrentWeapon();
        if (Input.GetKeyDown(KeyCode.E)) WeaponSkills();

        DisableEff(leftTimer, 0);
        DisableEff(rightTimer, 1);
    }

    private void CreateEffect()
    {
        leftEffect = Instantiate(fireEffect, leftGunpoint.transform);
        rightEffect = Instantiate(fireEffect, rightGunpoint.transform);

        leftEffect.SetActive(false);
        rightEffect.SetActive(false);
    }

    private void showFireEff(int pos) //0�϶� FireEff1.x 1�϶� FireEff1.y 2�϶� FireEff2.x 3�̻���� FireEff2.y 
    {
        //GameObject fireEffect = pos switch
        //{
        //    0 => fireEff[(int)weaponSet.SetWeaponNum().y - 1],
        //    _ => fireEff2[(int)weaponSet.SetWeaponNum().x - 1],
        //};

        if (pos == 0)
        {
            leftEffect.SetActive(true);
        }
        else
        {
            rightEffect.SetActive(true);
        }
    }

    private void DisableEff(float timer, int pos)
    {
        //var Eff = pos switch
        //{
        //    0 => fireEff[(int)weaponSet.SetWeaponNum().y - 1],
        //    _ => fireEff2[(int)weaponSet.SetWeaponNum().x - 1],
        //};

        var Eff = pos switch
        {
            0 => leftEffect,
            _ => rightEffect,
        };

        if (leftTimer > 0) leftTimer -= Time.deltaTime;
        if (rightTimer > 0) rightTimer -= Time.deltaTime;
        if (timer <= 0) Eff.SetActive(false);
    }

    private void Setting()
    {
        //for (int i = 0; i < 4; i++)
        //{
        //    fireEff[i].SetActive(false);
        //    fireEff2[i].SetActive(false);
        //}
        //fireEff[(int)weaponSet.SetWeaponNum().y - 1].SetActive(true);
        //fireEff2[(int)weaponSet.SetWeaponNum().x - 1].SetActive(true);

        CreateEffect();
    }

    void RotateGun()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        var leftDir = mousePosition - leftWeaponPos.transform.position;
        leftRotation = Mathf.Atan2(leftDir.y, leftDir.x) * Mathf.Rad2Deg;

        rightWeaponPos.transform.rotation = Quaternion.Euler(0, 0, leftRotation);
        leftWeaponPos.transform.rotation = Quaternion.Euler(0, 0, leftRotation);

        if (leftRotation >= -90 && leftRotation <= 90)
        {
            flipWeapons(false);
            return;
        }
        flipWeapons(true);
    }

    void flipWeapons(bool isturn)
    {
        gameObject.GetComponent<SpriteRenderer>().flipX = isturn;
        rightWeaponList[rightWeapon].GetComponent<SpriteRenderer>().flipY = isturn;
        leftWeaponList[leftWeapon].GetComponent<SpriteRenderer>().flipY = isturn;

        TurnCheck(isturn);
    }

    int TurnCheck(bool isturn)
    {
        int num = 0;
        Vector3 temp = Vector3.zero;
        leftWeaponPos.transform.localPosition = isturn switch
        {
            true => rightWeaponPosTemp,
            false => new Vector3(leftWeaponPosTemp.x, rightWeaponPosTemp.y, leftWeaponPosTemp.z), //left
        };

        rightWeaponPos.transform.localPosition = isturn switch
        {
            true => leftWeaponPosTemp, //left
            false => new Vector3(rightWeaponPosTemp.x, leftWeaponPosTemp.y, rightWeaponPosTemp.z),
        };

        return num = isturn switch
        {
            true => -1,
            false => 1,
        };
    }

    private void LoadBulletAmount()
    {
        ChangeText(weaponNum(left), left);
        ChangeText(weaponNum(right), right);
    }

    public void ChangeText(int num, bool isLeft)
    {
        if (isLeft)
        {
            lText.text = $"{LcurrentBullet[num]} / {magazineAmount[num] * BulletAmounts[num]}";
            leftWeapon = num;
            return;
        }
        rText.text = $"{RcurrentBullet[num]} / {magazineAmount[num] * BulletAmounts[num]}";
    }

    void CurrentWeapon()
    {
        LeftWeaponFire(weaponNum(left));
        RightWeaponFire(weaponNum(right));

        //switch (weaponSet.SubWeaponState)
        //{
        //    case WeaponKind.RIFLE: LeftWeaponFire(0); break;
        //    case WeaponKind.SNIPER: LeftWeaponFire(1); break;
        //    case WeaponKind.SHOTGUN: LeftWeaponFire(2); break;
        //    case WeaponKind.GRANADE: LeftWeaponFire(3); break;
        //    case WeaponKind.SWORD: LeftWeaponFire(4); break;
        //    default: break;
        //}

        //switch (weaponSet.MainWeaponState)
        //{
        //    case WeaponKind.RIFLE: RightWeaponFire(0); break;
        //    case WeaponKind.SNIPER: RightWeaponFire(1); break;
        //    case WeaponKind.SHOTGUN: RightWeaponFire(2); break;
        //    case WeaponKind.GRANADE: RightWeaponFire(3); break;
        //    case WeaponKind.SWORD: RightWeaponFire(4); break;
        //    default:
        //        break;
        //}
    }

    int weaponNum(bool a)
    {
        int num = 0;

        var weapon = a switch
        {
            true => weaponSet.SubWeaponState,
            false => weaponSet.MainWeaponState,
        };

        num = weapon switch
        {
            WeaponKind.RIFLE => 0,
            WeaponKind.SNIPER => 1,
            WeaponKind.SHOTGUN => 2,
            WeaponKind.GRANADE => 3,
            _ => 0,
        };
        return num;
    }

    void LeftWeaponFire(int num)//weaponNum�� 0�̸� ���� �ƴϸ� ������
    {
        leftWeapon = num;
        if (Input.GetMouseButton(0))
        {
            if (InventoryScript.Instance.isShop) return;
            if (leftCurtime < module[leftWeapon].atkSpeed) return;
            if (LcurrentBullet[num] < 1) return;
            else
            {
                int bulletAmount = num switch
                {
                    2 => 8,
                    _ => 1,
                };

                if (!module[3].bulletModule.isUpgrade)
                {
                    for (int i = 0; i < bulletAmount; i++)
                    {
                        GameObject bullet = Instantiate(module[leftWeapon].bullet, leftGunpoint.transform);
                        if (bulletAmount > 1) bullet.transform.Rotate(0, 0, Random.Range(-20f, 20f));
                        bullet.transform.SetParent(null);
                    }
                }
                else
                {
                    for (int i = -1; i < 2; i++)
                    {
                        GameObject bullet = Instantiate(module[leftWeapon].bullet, leftGunpoint.transform);
                        bullet.transform.Rotate(0, 0, i * 20f);
                        bullet.transform.SetParent(null);
                    }
                }
                Debug.Log((SoundManager.STATE)(weaponSet.MainWeaponState - 1));
                SoundManager.Instance.EffectSoundPlay((SoundManager.STATE)weaponSet.MainWeaponState - 1);
                showFireEff(0);
                leftCurtime = 0;
                leftTimer = 0.08f;
                if(!infinityBullet)
                    LcurrentBullet[num]--;
                lText.text = $"{LcurrentBullet[num]} / {magazineAmount[num] * BulletAmounts[num]}";
            }
        }
    }

    void RightWeaponFire(int num)
    {
        rightWeapon = num;
        if (Input.GetMouseButton(1))
        {
            if (InventoryScript.Instance.isShop) return;
            if (rightCurtime < module[rightWeapon].atkSpeed) return;
            if (RcurrentBullet[num] < 1) return;
            else
            {
                int bulletAmount = num switch
                {
                    2 => 8,
                    _ => 1,
                };

                if(!module[3].bulletModule.isUpgrade)
                {
                    for (int i = 0; i < bulletAmount; i++)
                    {
                        GameObject bullet = Instantiate(module[rightWeapon].bullet, rightGunpoint.transform);
                        if (bulletAmount > 1) bullet.transform.Rotate(0, 0, Random.Range(-20f, 20f));
                        bullet.transform.SetParent(null);
                    }
                }
                else
                {
                    for(int i = -1; i < 2; i++)
                    {
                        GameObject bullet = Instantiate(module[rightWeapon].bullet, rightGunpoint.transform);
                        bullet.transform.Rotate(0, 0, i * 20f);
                        bullet.transform.SetParent(null);
                    }
                }
                Debug.Log((SoundManager.STATE)(weaponSet.SubWeaponState - 1));
                SoundManager.Instance.EffectSoundPlay((SoundManager.STATE)(weaponSet.SubWeaponState - 1));

                rightCurtime = 0;
                rightTimer = 0.08f;
                showFireEff(1);
                RcurrentBullet[num]--;
                rText.text = $"{RcurrentBullet[num]} / {magazineAmount[num] * BulletAmounts[num]}";
            }
        }
    }
    void WeaponSkills()
    {
        switch (weaponSet.SubWeaponState)
        {
            case WeaponKind.RIFLE: StartCoroutine(PlayerSkills.Instance.Lambo()); break;
            case WeaponKind.SHOTGUN: PlayerSkills.Instance.MadangSslGi(); break;
            case WeaponKind.GRANADE: StartCoroutine(PlayerSkills.Instance.Stun()); break;
        }
    }

    void ReloadLeft()
    {
        if (LcurrentBullet[leftWeapon] > 0) return;
        if (magazineAmount[leftWeapon] < 1) return;
        leftWeaponFade.gameObject.SetActive(true);
        Ltimer += Time.deltaTime;
        leftWeaponFade.fillAmount = 1 - (Ltimer / 5f);
        if (Ltimer < 5f) return;
        leftWeaponFade.gameObject.SetActive(false);
        magazineAmount[leftWeapon]--;
        LcurrentBullet[leftWeapon] = BulletAmounts[leftWeapon];
        Ltimer = 0f;
        LoadBulletAmount();
    }

    void ReloadRight()
    {
        if (RcurrentBullet[rightWeapon] > 0) return;
        if (magazineAmount[rightWeapon] < 1) return;
        rightWeaponFade.gameObject.SetActive(true);
        rightWeaponFade.fillAmount = 1 - (Rtimer / 5f);
        Rtimer += Time.deltaTime;
        if (Rtimer < 5f) return;
        rightWeaponFade.gameObject.SetActive(false);
        magazineAmount[rightWeapon]--;
        RcurrentBullet[rightWeapon] = BulletAmounts[rightWeapon];
        Rtimer = 0f;
        LoadBulletAmount();
    }

    void GranadeUpgrade()
    {
        var b_module = module[3].bulletModule;
        if (b_module.isUpgrade) return;
        b_module.explosionRange -= 2;
        b_module.isUpgrade = false;
    }

    public void UpdateDUpgrade()
    {
        foreach(DungeonItem dungeonItem in SaveManager.Instance.CurrentUser.shopItemInDungeonOne)
        {
            if (!dungeonItem.isBuyit) return;

            var num = dungeonItem.itemNumber switch
            {
                0 => 2,
                1 => 2,
                2 => 2,
                3 => 2,
                _ => 1,
            };

            module[dungeonItem.itemNumber].bulletModule.atk *= num;


            switch (dungeonItem.itemNumber)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    module[dungeonItem.itemNumber].bulletModule.atk *= 2;
                    break;
                case 4: 
                    // 미니언
                    break;
                case 5:
                    // 장판
                    break;
                case 6:
                    // 번개
                    break;
                case 7: 
                    // 보호 장막
                    break;
                case 8: 
                case 9: 
                case 10:
                case 11:
                    module[dungeonItem.itemNumber].bulletModule.atk *= 2;
                    break;
                default: Debug.Log("응애 오류");
                    break;
            }
        }
        foreach(DungeonItem dungeonItem in SaveManager.Instance.CurrentUser.shopItemInDungeonMul)
        {
            switch(dungeonItem.itemNumber)
            {
                case 0: break;
                case 1: break;
                case 2: break;
                case 3: break;
                case 4: break;
                case 5: break;
                case 6: break;
                case 7: break;
                case 8: break;
                case 9: break;
                case 10: break;
                case 11: break;
                case 12: break;
                default: break;
            }
        }
    }
}
