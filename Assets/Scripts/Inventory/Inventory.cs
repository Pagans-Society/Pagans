using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] List<ItemSlot> slots;

    public List<ItemSlot> Slots => slots;
    public static Inventory GetInventory()
    {
        return FindObjectOfType<PlayerController>().GetComponent<Inventory>();
    }

    public void AddItem(ItemSlot item)
    {
        slots.Add(item);
        // TODO fix this porca madonna
    }
}

[Serializable]
public class ItemSlot
{
    public void Init(ItemBase itm, int cnt)
    {
        item = itm;
        count = cnt;
    }
    [SerializeField] ItemBase item;
    [SerializeField] int count;

    public ItemBase Item => item;
    public int Count => count;
}