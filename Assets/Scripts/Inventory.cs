using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public struct ItemStruct
{
    public GameObject invItem;
    public Sprite itemSprite;
    public int count;
}

public class Inventory : MonoBehaviour
{
    public Dictionary<int, ItemStruct> itemDictionary = new Dictionary<int, ItemStruct>();
    public GameObject scroll;
    [SerializeField]
    private Button[] slot;
    private int usingSlots = 0;
    private Image[] slotImage;
    private TextMeshProUGUI[] slotTMP;
    ItemStruct itemStruct;

    private void Awake()
    {
        itemStruct.count = 1;
        slot = scroll.transform.GetComponentsInChildren<Button>();
        slotImage = new Image[slot.Length];
        slotTMP = new TextMeshProUGUI[slot.Length];
        for(int i=0; i<slot.Length; i++)
        {
            slotImage[i] = slot[i].transform.GetChild(0).GetComponent<Image>();
            slotTMP[i] = slot[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        }
    }

    public void Push(int itemCode, GameObject item, Sprite itemSprite)
    {
        itemStruct.invItem = item;
        itemStruct.itemSprite = itemSprite;
        if(!itemDictionary.ContainsKey(itemCode)) itemDictionary.Add(itemCode, itemStruct);
        else if (itemDictionary[itemCode].count>=1)
        {
            itemDictionary[itemCode].count.CompareTo(itemDictionary[itemCode].count+1);
        }
        InvenChange(itemCode ,itemSprite);
    }

    private void InvenChange(int itemCode, Sprite itemSprite)
    {
        slotImage[usingSlots].gameObject.SetActive(true);
        slotImage[usingSlots].sprite = itemSprite;
        if(itemDictionary[itemCode].count == 1)
        {
            slotTMP[usingSlots].SetText("");
        }
        else
        {
            slotTMP[usingSlots].SetText(itemDictionary[itemCode].count.ToString());
        }
        usingSlots++;
    }
}
