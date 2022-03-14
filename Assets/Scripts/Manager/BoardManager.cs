using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System;

namespace MemoryGame.Anirudh.Bhandari
{
    public class BoardManager : IScreen
    {
        private static BoardManager instance;

        public static BoardManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<BoardManager>();
                }

                return instance;
            }
        }

        public MemoryBlockGrid block4x4;

        public MemoryBlockGrid block8x8;

        public MemoryBlockGrid currentGrid;

        private List<int> gameMemoryIndices;

        private bool isInputEnabled;

        private bool isStarted;

        public bool IsSuccess { get; private set; } = false;

        public bool bestScore = false;

        private bool allowStart;

        public bool IsInputEnabled
        {
            get
            {
                return isInputEnabled;
            }
            set
            {
                isInputEnabled = value;
            }

        }

        private int currentIndex = 0;

        public float gameTime = 0.0f;

        public int level = -1;

        private double bestTimeData;

        public RectTransform levelTransform;

        public RectTransform endGameMenu;

        public CanvasGroup endCanvasGroup;

        public TextMeshProUGUI levelText;

        public TextMeshProUGUI gameTimeText;

        public TextMeshProUGUI gameBestTimeText;

        public GameObject showHintButton;

        public GameObject onReady;

        private void Awake()
        {
            OnInteractScreenCompleted += StartGame;

            OnDownScreenCompleted += DownScreenComplete;
        }

        public override void OnInteractScreen()
        {
            base.OnInteractScreen();

            if (currentGrid != null)
            {
                currentGrid.ResetBoard();
            }

            if (currentGrid != null)
            {
                currentGrid.gameObject.SetActive(false);

                currentGrid = null;
            }

            if (level < 24)
            {
                currentGrid = block4x4;
            }
            else if (level >= 24 && level < 29)
            {
                currentGrid = block8x8;
            }

            currentGrid.gameObject.SetActive(true);

            currentGrid.ResetBoard();

        }

        private void DownScreenComplete()
        {
            this.myTransform.anchoredPosition = new Vector2(0, 1920);
        }

        private void BoardSetup()
        {
            IsInputEnabled = false;

            bestScore = false;

            currentIndex = 0;

            gameTime = 0.0f;

            gameMemoryIndices = new List<int>();

            showHintButton.gameObject.SetActive(true);

            if (level == -1)
            {
                foreach (int x in LevelConstants.GetLevel())
                {
                    gameMemoryIndices.Add(x);
                }
            }
            else
            {
                level = Mathf.Clamp(level, 0, GameManager.Instance.maxAllowedLevel + 1);

                foreach (int x in LevelConstants.GetLevel(level))
                {
                    gameMemoryIndices.Add(x);
                }
            }

            StartCoroutine(ShowCombination());
        }

        private void UpdateText()
        {
            bestTimeData = GameManager.Instance.GetBestTimeData(level);

            if (bestTimeData > 0)
            {
                gameBestTimeText.text = string.Format("Best Time {0}", bestTimeData.ToString("0.00"));
            }
            else
            {
                gameBestTimeText.text = string.Empty;
            }

            gameTimeText.text = string.Format("<size=100>60</size>.<size=35>0</size>");

            levelText.text = string.Format("LEVEL {0}", level + 1);
        }

        public void StartGame()
        {
            onReady.SetActive(true);

            UpdateText();

            allowStart = true;
        }

        public void SetGame()
        {
            onReady.SetActive(false);

            BoardSetup();

            SeekCombination();
        }
        public void OnReady()
        {
            if (allowStart)
            {
                Invoke("SetGame", 0.5f);

                allowStart = true;

                SoundManager.Instance.PlaySound(Sounds.Start);
            }
        }

        public void SeekCombination()
        {
            if (isInputEnabled)
            {
                StartCoroutine(ShowCombination());
            }
        }

        private IEnumerator ShowCombination()
        {
            isInputEnabled = false;

            yield return new WaitForSeconds(0.25f);

            for (int i = 0; i < gameMemoryIndices.Count; i++)
            {
                yield return new WaitForSeconds(0.2f); // Per Tile wait

                currentGrid.ShowBlockColor(gameMemoryIndices[i]);
            }

            yield return new WaitForSeconds(1f); // Per wait

            currentGrid.ResetBoard();

            yield return null; // Extra

            IsInputEnabled = true;
        }

        private void Update()
        {
            if (isStarted)
            {
                gameTime += Time.deltaTime;

                gameTimeText.text = string.Format("{0}", (60 - gameTime).ToString("<size=100>00</size>.<size=35>00</size>"));

                if (gameTime > 59.9)
                {
                    isInputEnabled = false;

                    ShowFullTileColour("incorrect");

                    gameTime = 0;
                }
            }
        }
        public void Open(int index)
        {
            if (IsInputEnabled && currentGrid.IsBlockAllowed(index))
            {
                if (currentIndex == 0)
                {
                    IsStarted(true);
                }
                if (index == gameMemoryIndices[currentIndex])
                {
                    currentGrid.ShowBlockColor(index);

                    currentIndex++;

                    SoundManager.Instance.PlaySound(0);

                    CheckEndGame();
                }
                else
                {
                    isInputEnabled = false;

                    SoundManager.Instance.PlaySound(Sounds.IncorrectTile);

                    currentGrid.BlinkIncorrect(index, 6, () =>
                       {
                           ShowFullTileColour("incorrect");
                       });

                    currentGrid.BlinkCorrect(gameMemoryIndices[currentIndex], 6);
                }
            }
        }

        public void ShowFullTileColour(string color)
        {
            IsStarted(false);

            if (color.Equals("correct"))
            {
                if (bestTimeData > gameTime || bestTimeData == -1)
                {
                    bestTimeData = gameTime;

                    bestScore = true;

                    GameManager.Instance.SetBestTimeForLevel(level, gameTime);
                }
            }
            IsSuccess = color.Equals("correct") ? true : false;

            if ((level) <= GameManager.Instance.maxAllowedLevel && IsSuccess)
            {
                if (level >= GameManager.Instance.maxUnlockLevel)
                {
                    GameManager.Instance.maxUnlockLevel = level + 1;
                }

                GameManager.Instance.maxUnlockLevel = Mathf.Clamp(GameManager.Instance.maxUnlockLevel, 0, GameManager.Instance.maxAllowedLevel);

                if (PlayerPrefs.HasKey("MaxLevel"))
                {
                    if (PlayerPrefs.GetInt("MaxLevel") < GameManager.Instance.maxUnlockLevel)
                    {
                        PlayerPrefs.SetInt("MaxLevel", GameManager.Instance.maxUnlockLevel);
                    }
                }
                else
                {
                    PlayerPrefs.SetInt("MaxLevel", GameManager.Instance.maxUnlockLevel);
                }
            }
            // - Bring Best Time and check if beats it or not

            StartCoroutine(currentGrid.ShowFullTileColor(color, () =>
              {
                  GameManager.Instance.ShowEndGame();
              }));
        }

        private void CheckEndGame()
        {
            if (currentIndex >= gameMemoryIndices.Count)
            {
                IsInputEnabled = false;

                ShowFullTileColour("correct");
            }
        }

        private void IsStarted(bool _on)
        {
            isStarted = _on;

            if (isStarted)
            {
                showHintButton.gameObject.SetActive(false);
            }
        }
    }
}