using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Create new key")]
public class KeyItem : ItemBase
{
    [SerializeField] public string code;

    private void Awake()
    {
        name = "key";
    }

    override public void Use(PlayerController player)
    {
        Debug.Log("key equiped");
    }
}
