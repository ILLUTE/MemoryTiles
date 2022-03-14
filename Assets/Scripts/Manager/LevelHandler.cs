using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace MemoryGame.Anirudh.Bhandari
{
    public class LevelHandler : IScreen
    {
        private static LevelHandler instance;

        public static LevelHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<LevelHandler>();
                }

                return instance;
            }
        }

        public List<LevelButton> levelButtons = new List<LevelButton>();

        private bool canInteract = false;

        private void Awake()
        {
            OnInteractScreenCompleted += OnVision;

            OnDownScreenCompleted += DownScreen;
        }

        private void DownScreen()
        {
            this.myTransform.anchoredPosition = new Vector2(0, 1920);
        }

        public void SetupLevelBoard()
        {
            for (int i = 0; i < levelButtons.Count; i++)
            {
                levelButtons[i].SetLock(i, GameManager.Instance.GetBestTimeData(i), !(i <= GameManager.Instance.maxUnlockLevel && i < GameManager.Instance.maxAllowedLevel));
            }
        }

        public void OnVision()
        {
            canInteract = true;
        }

        public void OnBack()
        {
            if(canInteract)
            {
                GameManager.Instance.MoveScreen(0, 0);

                GameManager.Instance.MoveScreen(1, 1);
            }
        }

        public override void OnInteractScreen()
        {
            base.OnInteractScreen();

            SetupLevelBoard();
        }
    }
}
