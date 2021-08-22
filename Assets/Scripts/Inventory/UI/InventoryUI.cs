using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] GameObject itemList;
    [SerializeField] ItemSlotUI itemSlotUI;

    [SerializeField] Image itemIcon;
    [SerializeField] Text itemDescription;

    [SerializeField] Image upArrow;
    [SerializeField] Image downArrow;

    int selectedItem = 0;

    const int itemsInViewport = 6;

    List<ItemSlotUI> slotUIList;
    Inventory inventory;
    RectTransform itemListRect;

    public ItemSlotUI ItemSlotsUI => itemSlotUI;

    private void Awake()
    {
        inventory = Inventory.GetInventory();
        itemListRect = itemList.GetComponent<RectTransform>();
        UpdateItemList();
    }

    private void Start()
    {
        UpdateItemList();
    }

    public void UpdateItemList()
    {
        // clear all the items
        foreach (Transform child in itemList.transform)
            Destroy(child.gameObject);

        // set
        slotUIList = new List<ItemSlotUI>();
        foreach (var itemSlot in inventory.Slots)
        {
            // create a child
            var slotUIObj = Instantiate(itemSlotUI, itemList.transform);

            // set data into the child
            slotUIObj.SetData(itemSlot);

            // add to the list
            slotUIList.Add(slotUIObj);
        }

        UpdateItemSelection();
    }

    public void HandleUpdate(Action onBack)
    {
        int prevSelection = selectedItem;

        if (Input.GetKeyDown(KeyCode.DownArrow))
            ++selectedItem;
        else if (Input.GetKeyDown(KeyCode.UpArrow))
            --selectedItem;

        selectedItem = Mathf.Clamp(selectedItem, 0, inventory.Slots.Count - 1);

        if (prevSelection != selectedItem)
        {
            UpdateItemSelection();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            onBack?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log("Switching");
        }

        if (Input.GetKeyDown(KeyCode.Z)) // Equip
        {
            Debug.Log($"selected item index: {selectedItem}, wich is {inventory.Slots[selectedItem].Item.Name}");
            FindObjectOfType<PlayerController>().equipedItem = Inventory.GetInventory().Slots[selectedItem].Item;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
            if(Inventory.GetInventory().Slots[selectedItem].Item == FindObjectOfType<PlayerController>().equipedItem)
                FindObjectOfType<PlayerController>().equipedItem = null;
    }

    void Add(ItemSlot itemSlot)
    {
        // create a child
        var slotUIObj = Instantiate(itemSlotUI, itemList.transform);

        // set data into the child
        slotUIObj.SetData(itemSlot);

        // add to the list
        slotUIList.Add(slotUIObj);
    }

    void UpdateItemSelection()
    {
        for (int i = 0; i < slotUIList.Count; i++)
        {
            try
            {
                if (i == selectedItem)
                    slotUIList[i].NameText.color = GlobalSettings.i.HighlightedColor;

                else
                    slotUIList[i].NameText.color = Color.black;
            }
            catch (NullReferenceException)
            {
                Debug.Log("rotto il cazzo eh");
            }

            /*Debug.Log($"selected item is {inventory.Slots[selectedItem]}");
            if (inventory.Slots[selectedItem].Item == player.equipedItem)
                slotUIList[i].NameText.color = GlobalSettings.i.EquipedColor;*/
        }

        var item = inventory.Slots[selectedItem].Item;
        itemIcon.sprite = item.Icon;
        itemDescription.text = item.Description;

        HandleScrolling();
    }

    void HandleScrolling()
    {
        float scrollPos = Mathf.Clamp(selectedItem - itemsInViewport/2, 0, selectedItem) * slotUIList[0].Height;
        itemListRect.localPosition = new Vector2(itemListRect.localPosition.x, scrollPos);

        bool showUpArrow = selectedItem > itemsInViewport / 2;
        upArrow.gameObject.SetActive(showUpArrow);

        bool showDownArrow = selectedItem + itemsInViewport / 2 < slotUIList.Count;
        downArrow.gameObject.SetActive(showDownArrow);
    }
}
