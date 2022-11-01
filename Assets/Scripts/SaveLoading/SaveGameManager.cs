using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using main;

namespace SaveLoading
{
    public static class SaveGameManager
    {
        public static SaveData CurrentSaveData = new SaveData();

        public const string SaveDirectory = "/SaveData/";

        //public static bool Save(string _fileName)
        public static bool SaveGame(string _fileName)
        {
            CurrentSaveData = new SaveData();
            var dir = Application.persistentDataPath + SaveDirectory;

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            string json = JsonUtility.ToJson(CurrentSaveData, true);
            File.WriteAllText(dir + _fileName, json);

            GUIUtility.systemCopyBuffer = dir;

            return true;
        }

        public static void LoadGame(string _fileName)
        {
            if (_fileName.Equals("SaveGame0.sav"))
            {
                SaveHandler.Resetting();
                return;
            }
            string fullPath = Application.persistentDataPath + SaveDirectory + _fileName;
            SaveData tempData = new SaveData();

            if (File.Exists(fullPath))
            {
                string json = File.ReadAllText(fullPath);
                tempData = JsonUtility.FromJson<SaveData>(json);
            }
            else
            {
                Debug.LogError("Save file does not exist!");
            }

            CurrentSaveData = tempData;
            SaveHandler.PlaceLoading(CurrentSaveData);
        }
    }
}