using UnityEngine;
using SaveLoading;

public class SaveHandler : MonoBehaviour
{
    public static void PlaceLoading(SaveData data)
    {
        Resetting();
        CubeLoader(data);
    }

    public static void Resetting()
    {
        GameObject[] currentGameObjects = GameObject.FindGameObjectsWithTag("BuildingBlock");
        int currentGameObjectsLenght = currentGameObjects.Length;
        for (int i = 0; i < currentGameObjectsLenght; i++)
        {
            Destroy(currentGameObjects[i].transform.gameObject);
        }
    }

    public static void CubeLoader(SaveData data)
    {
        for (int i = 0; i < data.cubes.Length; i++)
        {
            if (data.cubes[i].x == 0 && data.cubes[i].y == 0 && data.cubes[i].z == 0)
                continue;
            var createdCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            createdCube.hideFlags = HideFlags.HideInHierarchy;
            createdCube.name = "Cube";
            createdCube.GetComponent<Renderer>().material.SetColor("_Color", data.colors[i]);
            createdCube.tag = "BuildingBlock";
            createdCube.layer = 10;
            Instantiate(createdCube, data.cubes[i], new Quaternion());
        }
    }

    public static Vector3[] CubesToSave()
    {
        Vector3[] transformPoints;
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("BuildingBlock");
        transformPoints = new Vector3[gameObjects.Length];

        for (int i = 0; i < gameObjects.Length; i++)
        {
            transformPoints[i] = gameObjects[i].transform.position;
        }

        return transformPoints;
    }

    public static Color[] ColorsToSave()
    {
        Color[] blockColor;
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("BuildingBlock");
        blockColor = new Color[gameObjects.Length];

        for (int i = 0; i < gameObjects.Length; i++)
        {
            blockColor[i] = gameObjects[i].GetComponent<Renderer>().material.GetColor("_Color");
        }

        return blockColor;
    }
}
