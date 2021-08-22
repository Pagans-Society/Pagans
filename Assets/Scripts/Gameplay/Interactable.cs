using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Interactable
{
    ItemSlot getDropItem();
    void Interact();
}
