using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MemoryGame.Anirudh.Bhandari
{
    public class EndGameMenu : IScreen
    {
        public TextMeshProUGUI resultText;

        public TextMeshProUGUI gameTimeBestText;

        public GameObject nextLevelButton;

        public Button nextButton;

        public Button retryButton;

        public Button menuButton;

        public Image background;

        public Color incorrrectColor;

        public Color successColor;

        private void Awake()
        {
            OnUpScreenCompleted += Reset;

            OnDownScreenCompleted += DownScreenCompleted;
        }

        private void DownScreenCompleted()
        {
            this.myTransform.anchoredPosition = new Vector2(0, 1920);
        }

        private void Reset()
        {
            resultText.text = string.Empty;
        }

        public override void OnInteractScreen()
        {
            base.OnInteractScreen();

            Setup();
        }

        private void Setup()
        {
            retryButton.gameObject.SetActive(true);

            menuButton.gameObject.SetActive(true);

            if (BoardManager.Instance.IsSuccess)
            {
                if (BoardManager.Instance.level < GameManager.Instance.maxAllowedLevel - 1)
                {
                    nextButton.gameObject.SetActive(true);
                }
                else
                {
                    nextButton.gameObject.SetActive(false);
                }

                if (BoardManager.Instance.bestScore)
                {
                    gameTimeBestText.text = string.Format("<size=75><color=#FFFA00>{1}</color></size>Cleared \n{0}<size=50>s</size>", BoardManager.Instance.gameTime.ToString("0.00"), "Best Score \n");
                }
                else
                {
                    SoundManager.Instance.PlaySound(Sounds.Win);

                    gameTimeBestText.text = string.Format("Cleared \n{0}<size=50>s</size>", BoardManager.Instance.gameTime.ToString("0.00"));
                }

                background.color = successColor;
            }
            else
            {
                nextButton.gameObject.SetActive(false);

                gameTimeBestText.text = string.Format("Failed");

                SoundManager.Instance.PlaySound(Sounds.Lose);

                background.color = incorrrectColor;
            }
        }

        public void Retry()
        {
            GameManager.Instance.LoadLevel(BoardManager.Instance.level);
        }

        public void MainMenu()
        {
            GameManager.Instance.BackToLevelMenu();
        }

        public void Next()
        {
            GameManager.Instance.LoadLevel(BoardManager.Instance.level + 1);
        }

        public void LevelScreen()
        {
            GameManager.Instance.BackToLevelMenu();
        }
    }
}