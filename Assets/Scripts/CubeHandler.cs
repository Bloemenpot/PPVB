using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeHandler : MonoBehaviour
{
    public static Color currentColor = new Color(1, 1, 1, 1);

    private void Update()
    {
        if (PlayerMovement.getPaused())
            return;
        MyInput();
        ColorSwitching();
    }

    private void MyInput()
    {
        if (Input.GetMouseButtonDown(0))
            Placing();
        if (Input.GetMouseButtonDown(1))
            Destroying();
    }

    private void Placing()
    {
        print("place");
        RaycastHit hit;
        if (Camera.main == null)
        {
            print("Er is geen camera");
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Vector3 hitLocation;
        GameObject hitObject;

        if (Physics.Raycast(ray, out hit))
        {
            hitLocation.x = hit.point.x; hitLocation.y = hit.point.y; hitLocation.z = hit.point.z;
            hitObject = hit.collider.gameObject;
            Vector3 placeLocation = hitObject.transform.position + hit.normal;

            var createdCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            createdCube.hideFlags = HideFlags.HideInHierarchy;
            createdCube.name = "Cube";
            createdCube.GetComponent<Renderer>().material.SetColor("_Color", currentColor);
            createdCube.tag = "BuildingBlock";
            createdCube.layer = 10;
            Instantiate(createdCube, placeLocation, new Quaternion());
            //print("RGBA To String = " + rgbaToString(createdCube.GetComponent<Renderer>().material.GetColor("_Color")));
        }
    }

    private void Destroying()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        GameObject hitObject;

        if (Physics.Raycast(ray, out hit))
        {
            hitObject = hit.collider.gameObject;
            if (hitObject.tag == "BuildingBlock")
            {
                print("Object set false");
                Destroy(hitObject.transform.gameObject);
            }
        }
    }

    public void ColorSwitching()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //WHITE
            currentColor = new Color(1, 1, 1, 1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //BLACK
            currentColor = new Color(0, 0, 0, 1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //RED
            currentColor = new Color(1, 0, 0, 1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            //GREEN
            currentColor = new Color(0, 1, 0, 1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            //BLUE
            currentColor = new Color(0, 0, 1, 1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            //CYAN
            currentColor = new Color(0, 1, 1, 1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            //GRAY
            currentColor = new Color((float)0.5, (float)0.5, (float)0.5, 1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            //MAGENTA
            currentColor = new Color(1, 0, 1, 1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            //YELLOW
            currentColor = new Color(1, (float)0.92, (float)0.16, 1);
        }
    }

    public static string rgbaToString(Color color)
    {
        switch (color.ToString())
        {
            case "RGBA(1.000, 1.000, 1.000, 1.000)":
                return "White";
            case "RGBA(0.000, 0.000, 0.000, 1.000)":
                return "Black";
            case "RGBA(1.000, 0.000, 0.000, 1.000)":
                return "Red";
            case "RGBA(0.000, 1.000, 0.000, 1.000)":
                return "Green";
            case "RGBA(0.000, 0.000, 1.000, 1.000)":
                return "Blue";
            case "RGBA(0.000, 1.000, 1.000, 1.000)":
                return "Cyan";
            case "RGBA(0.500, 0.500, 0.500, 1.000)":
                return "Gray";
            case "RGBA(1.000, 0.000, 1.000, 1.000)":
                return "Magenta";
            case "RGBA(1.000, 0.920, 0.160, 1.000)":
                return "Yellow";
            default:
                return "Unkown Color";
        }
    }

    public static string getCurrentColor()
    {
        string returnValue = rgbaToString(currentColor);
        return returnValue;
    }
}
