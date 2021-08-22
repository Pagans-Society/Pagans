using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : InteractableBaseController
{
    public new void Interact()
    {
        StartCoroutine(DialogManager.Instance.ShowDialog(dialog));
        Debug.Log("used the new interact");
    }

}

