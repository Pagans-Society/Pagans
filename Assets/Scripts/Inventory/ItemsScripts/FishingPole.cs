using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Create new fishing pole")]
public class FishingPole : ItemBase
{
    [SerializeField] int possibility = 10;

    private void Awake()
    {
        name = "fishingrod";
    }

    override public void Use(PlayerController player)
    {
        Debug.Log("fishing pole equiped");
    }
}
