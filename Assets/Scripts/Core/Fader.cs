using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    Image image;
    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public IEnumerable FadeIn(float time)
    {
        //Debug.Log("porco il signore eh");
        yield return image.DOFade(1f, time).WaitForCompletion();
    }

    public IEnumerable FadeOut(float time)
    {
        yield return image.DOFade(0f, time).WaitForCompletion();
    }
}
