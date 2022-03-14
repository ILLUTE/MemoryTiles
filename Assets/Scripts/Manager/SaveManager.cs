using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace MemoryGame.Anirudh.Bhandari
{
    public static class SaveManager
    {
        private static string savePath = string.Concat(Application.persistentDataPath, "/playerLevelData.txt");

        public static List<PlayerLevelData> GetPlayerLevelData()
        {
            try
            {
                if (File.Exists(savePath))
                {
                    Debug.Log("File Found");

                    string file = File.ReadAllText(savePath).ToString();

                    Debug.Log(file); ;

                    PlayerLevelDataInfo levelDatas = JsonUtility.FromJson<PlayerLevelDataInfo>(file);

                    if (levelDatas != null)
                    {
                        return levelDatas.info;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }

            return null;
        }

        public static void SetPlayerLevelData(List<PlayerLevelData> data)
        {
            PlayerLevelDataInfo info = new PlayerLevelDataInfo();

            if(data!= null)
            {
                info.info = new List<PlayerLevelData>(data);

                string levelInfo = JsonUtility.ToJson(info);

                File.WriteAllText(savePath, levelInfo);
            }
        }
    }
}
