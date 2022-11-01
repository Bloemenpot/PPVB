using UnityEngine;
using main;

namespace SaveLoading
{
    [System.Serializable]
    public class SaveData
    {
        public Vector3[] cubes = MovementScript.CubesToSave();
        public Color[] colors = MovementScript.ColorsToSave();
        //public Vector3[] cubes = MovementScript.NeedToSaveCubes();
    }
}

