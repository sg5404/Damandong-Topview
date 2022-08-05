using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Fade : MonoSingleton<Fade>
{
    [SerializeField] Image fadeImg = null;
    [SerializeField] public float fadeTime { get; private set; } = 1f;

    public bool isFade = false;

    private void Awake()
    {
        fadeImg.gameObject.SetActive(true);
    }

    public void FadeIn()
    {
        fadeImg.raycastTarget = true;
        StartCoroutine(FadeInCoroutine());
    }

    private IEnumerator FadeInCoroutine()
    {
        isFade = true;
        fadeImg.gameObject.SetActive(isFade);
        fadeImg.fillOrigin = 0;
        fadeImg.DOFade(1f, fadeTime).From(0f);
        yield return new WaitForSeconds(fadeTime);
        fadeImg.raycastTarget = false;
        isFade = false;

        yield break;
    }

    public void FadeOut()
    {
        fadeImg.raycastTarget = true;
        StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeOutCoroutine()
    {
        isFade = true;
        fadeImg.gameObject.SetActive(isFade);
        fadeImg.fillOrigin = 1;
        fadeImg.DOFade(0f, fadeTime).From(1f);
        yield return new WaitForSeconds(fadeTime);
        isFade = false;
        fadeImg.gameObject.SetActive(isFade);
        fadeImg.raycastTarget = false;

        yield break;
    }

}
