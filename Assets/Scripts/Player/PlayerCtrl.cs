using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PlayerCtrl : MonoSingleton<PlayerCtrl>
{
    Rigidbody2D rigid;

    [SerializeField]
    Image Main_Weapon;

    bool isEquip = false;
    bool isItem = false;

    GameObject nearObject;
    public WeaponSet weaponSet { private set; get; } = null;

    public GameObject[] leftWeapons;
    public GameObject[] rightWeapons;

    private Inventory inventory;

    private Animator animator;

    public int rightWeapon { private set; get; } = 0;

    public PlayerBase playerBase;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rightWeapon = (int)WeaponSet.Instance.MainWeaponState - 1;
    }

    void Start()
    {
        playerBase = GetComponent<PlayerBase>();
        inventory = FindObjectOfType<Inventory>();
        rigid = GetComponent<Rigidbody2D>();
        weaponSet = GetComponent<WeaponSet>();
        ActiveFalseAllWepaon();
        //Debug.Log(PlayerAttack.Instance.leftWeapon);
        leftWeapons[0].SetActive(true);

        rightWeapons[rightWeapon].SetActive(true);
        //leftWeapons[PlayerAttack.Instance.leftWeapon].SetActive(true);

        //PlayerManager.Instance.Stat.MainMaxMagazine = rightWeapons[(int)weaponSet.SetWeaponNum().y - 1].GetComponent<Consumable>().weaponModule.maxMagazine;
        //PlayerManager.Instance.Stat.SubMaxMagazine = leftWeapons[(int)weaponSet.SetWeaponNum().y - 1].GetComponent<Consumable>().weaponModule.maxMagazine;
    }

    void Update()
    {
        if (playerBase.IsDead)
        {
            rigid.velocity = new Vector2(0, 0);
            return;
        }
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        rigid.velocity = new Vector2(h, v) * playerBase.MoveSpeed;
        //WeaponEquip();
        WeaponChange();
        //GetItem();
        if (h != 0 || v != 0)
            animator.SetBool("isMove", true);
        else
            animator.SetBool("isMove", false);
    }

    //private void WeaponEquip()
    //{
    //    if (Input.GetKeyDown(KeyCode.F) && isEquip)
    //    {
    //        Debug.Log("Click");
    //        Main_Weapon.sprite = nearObject.GetComponent<SpriteRenderer>().sprite;
    //        nearObject.SetActive(false);
    //    }
    //}
    //private void GetItem()
    //{
    //    if (Input.GetKeyDown(KeyCode.F) && isItem)
    //    {
    //        Debug.Log("Item");
    //        nearObject.SetActive(false);
    //    }
    //}

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            isEquip = false;
        }
        if (collision.CompareTag("ITEM"))
        {
            isItem = false;
        }
    }

    void ActiveFalseAllWepaon()
    {
        foreach(var weaponItem in leftWeapons)
        {
            weaponItem.SetActive(false);
        }
    }

    void ActiveWeapon(int weaponNumber)
    {
        leftWeapons[weaponNumber].SetActive(true);
        UIManager.Instance.ChangeUIWeaponSpriteImg(weaponNumber);
    }

    private void WeaponChange()
    {
        string key = Input.inputString;

        if(key == "1" || key == "2" || key =="3" || key == "4")
        {
            var num = key switch { "1" => 0, "2" => 1, "3" => 2, "4" => 3, _ => 0, };

            weaponSet.SubWeaponState = weaponSet.SetWeapon((num + 1).ToString());
            Debug.Log(weaponSet.SubWeaponState);
            ActiveFalseAllWepaon();
            ActiveWeapon(num);
            PlayerController.Instance.ChangeText(num, true);
            PlayerController.Instance.Ltimer = 0;
            PlayerController.Instance.leftWeaponFade.gameObject.SetActive(false);
        }


        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    weaponSet.SubWeaponState = weaponSet.SetWeapon("1");
        //    Debug.Log(weaponSet.SubWeaponState);
        //    ActiveFalseAllWepaon();
        //    num = 0;
        //    ActiveWeapon(num);
        //    PlayerController.Instance.ChangeText(num, true);
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    weaponSet.SubWeaponState = weaponSet.SetWeapon("2");
        //    Debug.Log(weaponSet.SubWeaponState);
        //    ActiveFalseAllWepaon();
        //    num = 1;
        //    ActiveWeapon(num);
        //    PlayerController.Instance.ChangeText(num, true);
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    weaponSet.SubWeaponState = weaponSet.SetWeapon("3");
        //    Debug.Log(weaponSet.SubWeaponState);
        //    ActiveFalseAllWepaon();
        //    num = 2;
        //    ActiveWeapon(num);
        //    PlayerController.Instance.ChangeText(num, true);
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha4))
        //{
        //    weaponSet.SubWeaponState = weaponSet.SetWeapon("4");
        //    Debug.Log(weaponSet.SubWeaponState);
        //    ActiveFalseAllWepaon();
        //    num = 3;
        //    ActiveWeapon(num);
        //    PlayerController.Instance.ChangeText(num, true);
        //}
    }
}
