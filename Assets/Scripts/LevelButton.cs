using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace MemoryGame.Anirudh.Bhandari
{
    public class LevelButton : MonoBehaviour
    {
        public Image background;

        public Image lockIcon;

        public TextMeshProUGUI levelText;

        private int levelIndex = -1;

        public GameObject bestTimeObject;

        public TextMeshProUGUI bestTimeText;

        public void SetLock(int level, double bestTime, bool isLocked = true)
        {
            lockIcon.gameObject.SetActive(isLocked);

            levelIndex = level + 1;

            levelText.text = isLocked ? "" : levelIndex.ToString();

            if (!isLocked && bestTime > -1)
            {
                bestTimeObject.gameObject.SetActive(true);
            }
            bestTimeText.text = string.Format("{0}<size=20>s</size>", bestTime.ToString("0.00"));
        }

        public void StartLevel()
        {
            GameManager.Instance.StartGame(levelIndex - 1);
        }
    }
}
