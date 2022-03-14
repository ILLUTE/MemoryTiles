using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryGame.Anirudh.Bhandari
{
    public class MemoryBlockGrid : MonoBehaviour
    {
        public List<Block> memoryBlockLayouts = new List<Block>();

        public int rows;

        private void Awake()
        {
            rows = (int)Mathf.Sqrt(memoryBlockLayouts.Count);

            for (int i = 0; i < memoryBlockLayouts.Count; i++)
            {
                memoryBlockLayouts[i].index = i;
            }
        }

        public Block GetMemoryBlock(int x)
        {
            if (memoryBlockLayouts.Count > x)
            {
                return memoryBlockLayouts[x];
            }

            return null;
        }

        public void ResetBoard()
        {
            for (int i = 0; i < memoryBlockLayouts.Count; i++)
            {
                memoryBlockLayouts[i].ResetColor();
            }
        }

        public bool IsBlockAllowed(int index)
        {
            if (index < memoryBlockLayouts.Count)
            {
                return memoryBlockLayouts[index].isAllowed;
            }

            return false;
        }

        public void ShowBlockColor(int index)
        {
            if (index < memoryBlockLayouts.Count)
            {
                memoryBlockLayouts[index].ShowColor();
            }
        }

        public void BlinkIncorrect(int index, int blinkLoops, Action callback = null)
        {
            if (index < memoryBlockLayouts.Count)
            {
                memoryBlockLayouts[index].BlinkIncorrect(blinkLoops, callback);
            }
        }

        public void BlinkCorrect(int index, int blinkLoops, Action callback = null)
        {
            if (index < memoryBlockLayouts.Count)
            {
                memoryBlockLayouts[index].BlinkCorrect(blinkLoops, callback);
            }
        }

        public void ShowIncorrect(int index)
        {
            if (index < memoryBlockLayouts.Count)
            {
                memoryBlockLayouts[index].ShowIncorrectColor();
            }
        }

        public void ShowCorrect(int index)
        {
            if (index < memoryBlockLayouts.Count)
            {
                memoryBlockLayouts[index].ShowCorrectAnswer();
            }
        }

        public IEnumerator ShowFullTileColor(string color, Action callback = null)
        {
            for (int i = 0; i < rows; i++)
            {
                int k = i;

                while (k < memoryBlockLayouts.Count)
                {
                    if (color.Equals("incorrect"))
                    {
                        ShowIncorrect(k);
                    }
                    else if (color.Equals("correct"))
                    {
                        ShowCorrect(k);
                    }

                    k += rows;
                    yield return new WaitForSeconds(0.02f);
                }
            }

            callback?.Invoke();
        }
    }
}