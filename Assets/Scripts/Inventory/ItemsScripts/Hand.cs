using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Create new Hand Item")]
public class Hand : ItemBase
{
    override public void Use(PlayerController player)
    {
        Debug.Log("using hands");
    }
}
