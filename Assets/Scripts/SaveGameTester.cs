using SaveLoading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGameTester : MonoBehaviour
{
    public void SaveGame(int i)
    {
        string fileName = $"SaveGame{i}.sav";
        SaveGameManager.SaveGame(fileName);
    }

    public void LoadGame(int i)
    {
        string fileName = $"SaveGame{i}.sav";
        SaveGameManager.LoadGame(fileName);
    }
}
