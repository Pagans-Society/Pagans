using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ItemFunctions/WannaEquip")]
public class WannaEquip : DialogScript
{
    public override void onNo()
    {
        return;
    }

    public override void onYes()
    {
        FindObjectOfType<PlayerController>().equipedItem = FindObjectOfType<PlayerController>().GetComponent<Inventory>().Slots[1].Item;
    }
}
