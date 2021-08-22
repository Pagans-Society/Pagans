using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DialogScript : ScriptableObject
{
    public abstract void onYes();

    public abstract void onNo();
}
