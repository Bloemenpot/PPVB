using UnityEngine;
using UnityEngine.UI;
using main;

public class CurrentColor : MonoBehaviour
{
    public static RectTransform myRectTransform;


    public void Start()
    {
        myRectTransform = GetComponent<RectTransform>();
    }

    public static void GetColor()
    {
        string currentColor = CubeHandler.getCurrentColor();
        int currentIColor = CubeHandler.iCurrentColor;
        //color.text = "Current Color: " + currentColor;
        switch (currentIColor)
        {
            case 1:
                myRectTransform.localPosition = new Vector3(-220, 0, 0);
                return;
            case 2:
                myRectTransform.localPosition = new Vector3(-165, 0, 0);
                return;
            case 3:
                myRectTransform.localPosition = new Vector3(-110, 0, 0);
                return;
            case 4:
                myRectTransform.localPosition = new Vector3(-55, 0, 0);
                return;
            case 5:
                myRectTransform.localPosition = new Vector3(0, 0, 0);
                return;
            case 6:
                myRectTransform.localPosition = new Vector3(55, 0, 0);
                return;
            case 7:
                myRectTransform.localPosition = new Vector3(110, 0, 0);
                return;
            case 8:
                myRectTransform.localPosition = new Vector3(165, 0, 0);
                return;
            case 9:
                myRectTransform.localPosition = new Vector3(220, 0, 0);
                return;
            default:
                myRectTransform.localPosition = new Vector3(-220, 0, 0);
                return;
        }
    }
}
