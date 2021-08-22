using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DialogManager : MonoBehaviour
{
    [SerializeField] GameObject dialogBox;
    [SerializeField] GameObject QuestBox;

    [SerializeField] Text YesText;
    [SerializeField] Text NoText;
    [SerializeField] Text dialogText;

    [SerializeField] int LettersPerSecond;

    public event Action OnShowDialog;
    public event Action OnCloseDialog;

    public static DialogManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    Dialog dialog;
    int currentLine = 0;
    int selectedResp = 0;
    bool isTyping;
    bool isQuestion;

    public IEnumerator ShowDialog(Dialog dialog)
    {

        yield return new WaitForEndOfFrame();

        OnShowDialog?.Invoke();

        this.dialog = dialog;
        dialogBox.SetActive(true);
        StartCoroutine(TypeDialog(dialog.Lines[0]));

        if (dialog.isQuestion)
            QuestBox.SetActive(true);
        isQuestion = dialog.isQuestion;

    }

    public void HandleUpdate()
    {
        if(!isQuestion || currentLine < dialog.Lines.Count-1)
        {
            if (Input.GetKeyDown(KeyCode.Z) && !isTyping)
            {
                ++currentLine;
                if (currentLine < dialog.Lines.Count)
                {
                    StartCoroutine(TypeDialog(dialog.Lines[currentLine]));
                }
                else
                {
                    currentLine = 0;
                    dialogBox.SetActive(false);
                    OnCloseDialog?.Invoke();
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
                --selectedResp;
            else if (Input.GetKeyDown(KeyCode.DownArrow))
                ++selectedResp;

            selectedResp = Mathf.Clamp(selectedResp, 0, 1);

            UpdateSelection();

            if(Input.GetKeyDown(KeyCode.Z))
            {
                if (selectedResp == 0)
                    dialog.itemFunc.onYes();
                else
                    dialog.itemFunc.onNo();

                currentLine = 0;
                dialogBox.SetActive(false);
                QuestBox.SetActive(false);
                OnCloseDialog?.Invoke();
            }
        }

    }

    public void UpdateSelection()
    {
        if(selectedResp == 0)
        {
            YesText.color = GlobalSettings.i.HighlightedColor;
            NoText.color = Color.black;
        }
        else if(selectedResp == 1)
        {
            YesText.color = Color.black;
            NoText.color = GlobalSettings.i.HighlightedColor;
        }
    }

    public IEnumerator TypeDialog(string dialog)
    {
        isTyping = true;
        dialogText.text = "";
        foreach(var letter in dialog.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / LettersPerSecond);
        }
        isTyping = false;
        //yield return new WaitForSeconds(1f);
    }
}
