using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog
{
    [SerializeField] List<string> lines;
    [SerializeField] public bool isQuestion = false;
    [SerializeField] public DialogScript itemFunc;

    public List<string> Lines
    {
        get { return lines; }
    }
}
