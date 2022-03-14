using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using TMPro;
using UnityEngine.UI;
using Coffee.UIEffects;

namespace MemoryGame.Anirudh.Bhandari
{
    public class MainMenuHandler : IScreen
    {
        public bool canInteract = false;

        private GameManager gameManager;

        public TextMeshProUGUI highestLevel;

        public List<Image> gridImages;

        public bool playSequence = false;

        public UIShiny shinyEffect;

        Sequence mySequence;

        private void Awake()
        {
            canInteract = true;

            gameManager = GameManager.Instance;

            OnInteractScreenCompleted += Setup;
        }

        private void Start()
        {
            Setup();
        }

        private void PlayGrid()
        {
            if (playSequence)
            {
                mySequence = DOTween.Sequence();

                for (int i = 0; i < gridImages.Count; i++)
                {
                    int k = i;
                    mySequence.AppendInterval(0.1f);

                    mySequence.Join(gridImages[k].DOFade(1f, 0.1f));
                }

                for (int i = 0; i < gridImages.Count; i++)
                {
                    int k = i;

                    mySequence.Append(gridImages[k].DOFade(0f, 0.1f));
                }

                mySequence.SetLoops(-1, LoopType.Restart);
            }
        }
        private void Setup()
        {
            Color myColor = new Color(1, 0.8f, 0.475f, 0);

            foreach (Image i in gridImages)
            {
                i.color = myColor;
            }
            canInteract = true;

            //PlayGrid();

            shinyEffect.brightness = .165f;

            highestLevel.text = string.Format("Lv\n{0}", GameManager.Instance.maxUnlockLevel + 1);
        }

        public void OnPlayButtonPressed()
        {
            if (canInteract)
            {
                canInteract = false;

                mySequence.Kill();

                Color _color = new Color(0, 0, 0, 0);

                for (int i = 0; i < gridImages.Count; i++)
                {
                    gridImages[i].color = _color;
                }
                shinyEffect.brightness = 0;

                gameManager.MoveScreen(0, -1);

                gameManager.MoveScreen(1, 0);
            }
        }

        public override void OnDownScreen()
        {
            base.OnDownScreen();
        }

        public override void OnInteractScreen()
        {
            base.OnInteractScreen();
        }

        public override void OnUpScreen()
        {
            base.OnUpScreen();
        }
    }
}
