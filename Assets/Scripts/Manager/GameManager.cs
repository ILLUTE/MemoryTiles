using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

namespace MemoryGame.Anirudh.Bhandari
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;

        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<GameManager>();
                }

                return instance;
            }
        }

        public MainMenuHandler mainMenu;

        public LevelHandler levelHandler;

        public BoardManager boardManager;

        public EndGameMenu endGameMenu;

        public int maxUnlockLevel = 0;

        public readonly int maxAllowedLevel = 29;

        private List<PlayerLevelData> levelData = new List<PlayerLevelData>();

        private void Awake()
        {
            if (PlayerPrefs.HasKey("MaxLevel"))
            {
                maxUnlockLevel = PlayerPrefs.GetInt("MaxLevel");
            }
            else
            {
                maxUnlockLevel = 0;
            }

            DOTween.SetTweensCapacity(1000, 50);

            LoadSavedData();
        }
        public void MoveScreen(int screenIndex, int screenPos)
        {
            IScreen screen = null;


            switch (screenIndex)
            {
                case 0:
                    screen = mainMenu;
                    SoundManager.Instance.PlaySound(Sounds.Start);
                    break;
                case 1:
                    screen = levelHandler;
                    SoundManager.Instance.PlaySound(Sounds.Start);
                    break;
                case 2:
                    screen = boardManager;
                    SoundManager.Instance.PlaySound(Sounds.Start);
                    break;
                case 3:
                    screen = endGameMenu;
                    break;
            }

            if (screen != null)
            {
                screen.SetScreen(screenPos);
            }
        }

        public void StartGame(int level)
        {
            if (level <= maxUnlockLevel)
            {
                boardManager.level = level;

                MoveScreen(1, -1);

                MoveScreen(2, 0);
            }
        }

        private void LoadSavedData()
        {
            levelData = SaveManager.GetPlayerLevelData();

            if (levelData != null)
            {
                Debug.Log("Old");
            }
            else
            {
                Debug.Log("new");

                levelData = new List<PlayerLevelData>();

                PlayerLevelData data;

                for (int i = 0; i < maxAllowedLevel; i++)
                {
                    data = new PlayerLevelData();

                    data.levelClearTime = -1.0f;

                    data.levelData = i;

                    levelData.Add(data);
                }
            }
        }

        public void SetBestTimeForLevel(int level, double bestTime)
        {
            bool flag = false;

            if (level >= 0 && level <= maxAllowedLevel)
            {
                for (int i = 0; i < levelData.Count; i++)
                {
                    if (levelData[i].levelData == level)
                    {
                        levelData[i].levelClearTime = bestTime;

                        flag = true;

                        break;
                    }
                }

                if (!flag)
                {
                    PlayerLevelData pd = new PlayerLevelData();

                    pd.levelData = level;

                    pd.levelClearTime = bestTime;

                    levelData.Add(pd);
                }
            }

            SaveManager.SetPlayerLevelData(levelData);
        }

        public double GetBestTimeData(int level)
        {
            foreach (PlayerLevelData pd in levelData)
            {
                if (pd.levelData == level)
                {
                    return pd.levelClearTime;
                }
            }

            return -1.0f;
        }

        public void LoadLevel(int level)
        {
            boardManager.level = level;

            MoveScreen(3, 1);

            MoveScreen(2, 0);
        }

        public void BackToLevelMenu()
        {
            levelHandler.SetScreenBottom();

            MoveScreen(2, 1);

            MoveScreen(3, 1);

            MoveScreen(1, 0);
        }

        public void BackToMainMenu()
        {
            MoveScreen(2, 1);

            MoveScreen(3, 1);

            MoveScreen(0, 0);

            levelHandler.SetScreenTop();

            boardManager.SetScreenTop();
        }

        public void ShowEndGame()
        {
            MoveScreen(3, 0);
        }

        private void OnApplicationPause()
        {
            SaveManager.SetPlayerLevelData(levelData);
        }
    }
}