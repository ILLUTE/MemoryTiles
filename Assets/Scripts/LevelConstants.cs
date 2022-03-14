using System;
using System.Collections;
using System.Collections.Generic;

namespace MemoryGame.Anirudh.Bhandari
{
    public static class LevelConstants
    {
        private static List<List<int>> list4x4 = new List<List<int>>
    {
        new List<int>{ 0,1,2,3,7,11,15},
        new List<int>{4,5,1,2,6,10,11,15},
        new List<int>{8,9,10,11,7,6,2},
        new List<int>{12,8,9,10,6,2},
        new List<int>{14,10,9,5,4},
        new List<int>{1,5,6,7,11,15},
        new List<int>{0,1,2,3,7,6,5,9,10,11,15,14,13,12,8,4},
        new List<int>{12,13,14,15,11,7,3,2,1,0,4,5,6,10,9,8},
        new List<int>{0,4,8,12,3,7,11,15,1,2,13,14 },
        new List<int>{0,1,2,3,7,11,15,14,13,12,8,4},
        new List<int>{1,5,9,13,14,15,11,7,3},
        new List<int>{1,2,5,6,9,10,13,14,3,7,11,15,0,4,8,12},
        new List<int>{3,6,9,12,0,5,10,15},
        new List<int>{0,4,8,12,13,14,15,11,7,3,2,1,5,10},
        new List<int>{4,5,6,7,12,13,14,15,1,9,3,11},
        new List<int>{2,1,4,8,13,14,11,7},
        new List<int>{2,3,7,6,5,1,0,4,8,9,10,11,12,13,14,15 },
        new List<int>{8,12,5,9,2,6,7,11,10,14,13},
        new List<int>{4,7,9,10,12},
        new List<int>{0,3,15,12,5,6,9,10},
        new List<int>{5,6,10,9,12,3},
        new List<int>{ 1,4,3,6,9,12,11,14},
        new List<int>{ 2,7,1,6,11,4,9,14,8,13},
        new List<int>{0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15}
        };

        private static List<List<int>> list8x8 = new List<List<int>>
    {
        new List<int>{ 0,1,2,3,4,12,20,28,36,37,38,46,47,55,63},
        new List<int>{0,8,16,24,32,33,34,35,36,28,20,12,13,14,15},
        new List<int>{7,15,23,31,39,38,37,36,35,27,19,11,10,9,8},
        new List<int>{35,36,28,20,19,18,10,11,12,13,14,22,30,38,46,54,62},
        new List<int>{28,36,35,27,19,20,21,22,30,38,46,54,53,52,51,50,49,41,33,25,17,9,10,11},
        };

        public static List<int> GetLevel(int level = -1)
        {
            if (level >= 0)
            {
                if (level < list4x4.Count)
                {
                    return list4x4[level];
                }
                else
                {
                    return list8x8[level - list4x4.Count];
                }
            }
            else
            {
                Random random = new Random();

                int num = random.Next(0, list4x4.Count);

                return list4x4[num];
            }

            return null;
        }
    }
}