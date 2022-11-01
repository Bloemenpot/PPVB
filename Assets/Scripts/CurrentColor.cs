using UnityEngine;
using UnityEngine.UI;
using main;

public class CurrentColor : MonoBehaviour
{
    public Text color;

    public void Update()
    {
        GetColor();
    }

    public void GetColor()
    {
        string currentColor = CubeHandler.getCurrentColor();
        color.text = "Current Color: " + currentColor;
    }
}
