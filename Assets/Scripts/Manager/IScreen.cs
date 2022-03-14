using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public abstract class IScreen : MonoBehaviour
{
    public RectTransform myTransform;

    public CanvasGroup myCanvasGroup;

    public Action OnInteractScreenCompleted;

    public Action OnDownScreenCompleted;

    public Action OnUpScreenCompleted;

    public virtual void OnInteractScreen()
    {
        Sequence mySequence = DOTween.Sequence();

        mySequence.Append(myTransform.DOAnchorPosY(0, 0.25f)).Join(myCanvasGroup.DOFade(1, 0.25f));

        mySequence.OnComplete(() =>
        {
            OnInteractScreenCompleted?.Invoke();
        });
    }
    public virtual void OnUpScreen()
    {
        Sequence mySequence = DOTween.Sequence();

        mySequence.Append(myTransform.DOAnchorPosY(2000, 0.25f)).Join(myCanvasGroup.DOFade(0, 0.25f));

        mySequence.OnComplete(() =>
        {
            OnUpScreenCompleted?.Invoke();
        });
    }

    public virtual void OnDownScreen()
    {
        Sequence mySequence = DOTween.Sequence();

        mySequence.Append(myTransform.DOAnchorPosY(-2000, 0.25f)).Join(myCanvasGroup.DOFade(0, 0.25f));

        mySequence.OnComplete(() =>
        {
            OnDownScreenCompleted?.Invoke();
        });
    }

    public virtual void SetScreenTop()
    {
        myTransform.anchoredPosition = new Vector2(0, 1920);
    }

    public virtual void SetScreenBottom()
    {
        myTransform.anchoredPosition = new Vector2(0, -1920);
    }

    public virtual void SetScreen(int i = -2)
    {
        switch (i)
        {
            case -1:
                OnDownScreen();
                break;
            case 0:
                OnInteractScreen();
                break;
            case 1:
                OnUpScreen();
                break;
        }
    }
}