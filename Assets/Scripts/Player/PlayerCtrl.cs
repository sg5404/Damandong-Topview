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
        float h = Input.GetAxisRaw("Horizontal") * playerBase.MoveSpeed;
        float v = Input.GetAxisRaw("Vertical") * playerBase.MoveSpeed;
        rigid.velocity = new Vector2(h, v);
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
        int num = 0;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weaponSet.SubWeaponState = weaponSet.SetWeapon("1");
            Debug.Log(weaponSet.SubWeaponState);
            ActiveFalseAllWepaon();
            num = 0;
            ActiveWeapon(num);
            PlayerAttack.Instance.ChangeText(num, true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weaponSet.SubWeaponState = weaponSet.SetWeapon("2");
            Debug.Log(weaponSet.SubWeaponState);
            ActiveFalseAllWepaon();
            num = 1;
            ActiveWeapon(num);
            PlayerAttack.Instance.ChangeText(num, true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            weaponSet.SubWeaponState = weaponSet.SetWeapon("3");
            Debug.Log(weaponSet.SubWeaponState);
            ActiveFalseAllWepaon();
            num = 2;
            ActiveWeapon(num);
            PlayerAttack.Instance.ChangeText(num, true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            weaponSet.SubWeaponState = weaponSet.SetWeapon("4");
            Debug.Log(weaponSet.SubWeaponState);
            ActiveFalseAllWepaon();
            num = 3;
            ActiveWeapon(num);
            PlayerAttack.Instance.ChangeText(num, true);
        }
    }
}
