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

    [SerializeField] private float speed;

    bool isEquip = false;
    bool isItem = false;

    GameObject nearObject;
    public WeaponSet weaponSet { private set; get; } = null;

    public GameObject[] leftWeapons;
    public GameObject[] rightWeapons;

    private Inventory inventory;

    private Animator animator;

    public int rightWeapon { private set; get; } = 0;

    public bool isDead { set; get; }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rightWeapon = (int)WeaponSet.Instance.MainWeaponState - 1;
    }

    void Start()
    {
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
        if (isDead)
        {
            rigid.velocity = new Vector2(0, 0);
            return;
        }
        float h = Input.GetAxisRaw("Horizontal") * speed;
        float v = Input.GetAxisRaw("Vertical") * speed;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            var Objects = GameObject.FindGameObjectsWithTag("Weapon").ToList();
            nearObject = Objects.OrderBy(obj =>
            {
                return Vector3.Distance(transform.position, obj.transform.position);
            }).FirstOrDefault();
            isEquip = true;
        }
        if(collision.CompareTag("ITEM"))
        {
            var item = collision.gameObject.GetComponent<Item>();
            var iSprite = item.GetComponent<SpriteRenderer>().sprite;
            nearObject = item.gameObject;
            inventory.Push(item.itemCode, item.gameObject, iSprite);
            isItem = true;
        }    
    }

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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weaponSet.SubWeaponState = weaponSet.SetWeapon("1");
            Debug.Log(weaponSet.SubWeaponState);
            ActiveFalseAllWepaon();
            ActiveWeapon(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weaponSet.SubWeaponState = weaponSet.SetWeapon("2");
            Debug.Log(weaponSet.SubWeaponState);
            ActiveFalseAllWepaon();
            ActiveWeapon(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            weaponSet.SubWeaponState = weaponSet.SetWeapon("3");
            Debug.Log(weaponSet.SubWeaponState);
            ActiveFalseAllWepaon();
            ActiveWeapon(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            weaponSet.SubWeaponState = weaponSet.SetWeapon("4");
            Debug.Log(weaponSet.SubWeaponState);
            ActiveFalseAllWepaon();
            ActiveWeapon(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            weaponSet.SubWeaponState = weaponSet.SetWeapon("5");
            Debug.Log(weaponSet.SubWeaponState);
            ActiveFalseAllWepaon();
            ActiveWeapon(4);
        }
    }
}
