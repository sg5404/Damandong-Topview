using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Fade : MonoSingleton<Fade>
{
    [SerializeField] Image fadeImg = null;
    public float fadeTime { get; private set; } = 1f;

    public bool isFade = false;

    private void Start()
    {
        fadeImg.gameObject.SetActive(false);
    }

    public void FadeIn()
    {
        fadeImg.raycastTarget = true;
        StartCoroutine(FadeInCoroutine());
    }

    private IEnumerator FadeInCoroutine()
    {
        isFade = true;
        fadeImg.gameObject.SetActive(true);
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
        fadeImg.fillOrigin = 0;
        fadeImg.DOFade(0f, fadeTime / 2).From(1f);
        isFade = false;
        yield return new WaitForSeconds(fadeTime / 2);
        fadeImg.gameObject.SetActive(false);
        fadeImg.raycastTarget = false;

        yield break;
    }

}
