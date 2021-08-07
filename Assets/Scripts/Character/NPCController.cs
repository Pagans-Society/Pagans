using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : InteractableBaseController
{
    [Header("Drops")]
    [SerializeField] List<ItemBase> items;

    Inventory inv;

    private void Awake()
    {
        inv = Inventory.GetInventory();
    }

    public void Interact()
    {
        StartCoroutine(DialogManager.Instance.ShowDialog(dialog));
        // once the dialog ends drops all items he have.
        foreach(var item in items)
        {
            var newItemSlot = new ItemSlot();
            newItemSlot.Init(item, 1);

            inv.AddItem(newItemSlot);

            //Debug.Log($"found: {inv}");
        }
    }
}
