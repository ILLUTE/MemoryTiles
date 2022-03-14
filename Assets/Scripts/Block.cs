using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using System;

namespace MemoryGame.Anirudh.Bhandari
{
    public class Block : MonoBehaviour, IPointerEnterHandler
    {
        public Color highlightedColor;

        public Color inCorrectColor;

        public Color correctColor;

        private Image background_img;

        public bool isAllowed = false;

        public int index;

        private void Awake()
        {
            background_img = this.GetComponent<Image>();

            ResetColor(false);
        }
        public void ResetColor(bool tween = false)
        {
            if (!tween)
            {
                background_img.color = Color.white;
            }
            else
            {
                background_img.DOColor(Color.white, 0.2f);
            }

            isAllowed = true;
        }

        public void ShowColor()
        {
            isAllowed = false;

            background_img.DOColor(highlightedColor, 0.2f);
        }

        public void ShowIncorrectColor()
        {
            isAllowed = false;

            background_img.DOColor(inCorrectColor, 0.2f);
        }

        public void ShowCorrectAnswer()
        {
            isAllowed = false;

            background_img.DOColor(correctColor, 0.2f);
        }

        public void BlinkIncorrect(int loops = 6,Action callback = null)
        {
            background_img.color = Color.white;

            background_img.DOColor(inCorrectColor, 0.1f).SetLoops(loops, LoopType.Yoyo).OnComplete(() => { callback?.Invoke(); });
        }

        public void BlinkCorrect(int loops = 6, Action callback = null)
        {
            background_img.color = Color.white;

            background_img.DOColor(correctColor, 0.1f).SetLoops(loops, LoopType.Yoyo).OnComplete(() => { callback?.Invoke(); });
        }

        public void Open()
        {
            BoardManager.Instance.Open(index);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            //throw new System.NotImplementedException();
        }
    }
}
