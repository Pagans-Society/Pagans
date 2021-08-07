using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBaseController : MonoBehaviour, Interactable
{
    [Header("Dialogue")]
    [SerializeField] public Dialog dialog;
    [Header("Signal")]
    [SerializeField] GameObject signal;

    public void Interact()
    {
        if(dialog.Lines.Count > 0)
        {
            StartCoroutine(DialogManager.Instance.ShowDialog(dialog));
        }

        try 
        {
            signal.SetActive(false);
        }
        catch (UnassignedReferenceException)
        {
            return;
        }
    }
}
