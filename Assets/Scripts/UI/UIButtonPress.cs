using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
namespace MemoryGame.Anirudh.Bhandari
{ 
    public class UIButtonPress : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler,IPointerDownHandler
    {
        public RectTransform panel;

        private void OnEnable()
        {
            panel.sizeDelta = new Vector2(500, 500);
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            panel.DOKill(true);

            panel.DOSizeDelta(new Vector2(300, 300), 0.1f);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            panel.DOKill(true);

            panel.DOSizeDelta(new Vector2(500, 500), 0.1f);
        }
    }
}
