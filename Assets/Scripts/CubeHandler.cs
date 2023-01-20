using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeHandler : MonoBehaviour
{
    public static int iCurrentColor = 1;
    public static Color currentColor = new Color(1, 1, 1, 1);
    public Material outline;
    GameObject outlinedObject;

    private void Update()
    {
        if (PlayerMovement.getPaused())
            return;
        MyInput();
        ColorSwitching();
        //SetOutline();
    }

    private void SetOutline()
    {
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
            if (hitObject.tag == "BuildingBlock")
            {
                Material[] cubeMats = new Material[2];
                cubeMats.SetValue(hitObject.GetComponent<Renderer>().materials[0], 0);
                cubeMats[0].SetColor("_Color", currentColor);
                //cubeMats[1] = outline;
                hitObject.GetComponent<Renderer>().materials = cubeMats;
            }
        }
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

            Material[] cubeMats = new Material[2];

            var createdCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            createdCube.hideFlags = HideFlags.HideInHierarchy;
            createdCube.name = "Cube";
            cubeMats.SetValue(createdCube.GetComponent<Renderer>().materials[0], 0);
            cubeMats[0].SetColor("_Color", currentColor);
            //cubeMats[1] = outline;
            createdCube.GetComponent<Renderer>().materials = cubeMats;
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
                Destroy(hitObject.transform.gameObject);
            }
        }
    }

    public void ColorSwitching()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            iCurrentColor++;
            if (iCurrentColor > 9)
            {
                iCurrentColor = 1;
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            iCurrentColor--;
            if (iCurrentColor < 1)
            {
                iCurrentColor = 9;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //WHITE
            iCurrentColor = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //BLACK
            iCurrentColor = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //RED
            iCurrentColor = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            //GREEN
            iCurrentColor = 4;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            //BLUE
            iCurrentColor = 5;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            //CYAN
            iCurrentColor = 6;
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            //GRAY
            iCurrentColor = 7;
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            //MAGENTA
            iCurrentColor = 8;
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            //YELLOW
            iCurrentColor = 9;
        }

        switch (iCurrentColor)
        {
            case 1:
                currentColor = new Color(1, 1, 1, 1);
                CurrentColor.GetColor();
                return;
            case 2:
                currentColor = new Color(0, 0, 0, 1);
                CurrentColor.GetColor();
                return;
            case 3:
                currentColor = new Color(1, 0, 0, 1);
                CurrentColor.GetColor();
                return;
            case 4:
                currentColor = new Color(0, 1, 0, 1);
                CurrentColor.GetColor();
                return;
            case 5:
                currentColor = new Color(0, 0, 1, 1);
                CurrentColor.GetColor();
                return;
            case 6:
                currentColor = new Color(0, 1, 1, 1);
                CurrentColor.GetColor();
                return;
            case 7:
                currentColor = new Color((float)0.5, (float)0.5, (float)0.5, 1);
                CurrentColor.GetColor();
                return;
            case 8:
                currentColor = new Color(1, 0, 1, 1);
                CurrentColor.GetColor();
                return;
            case 9:
                currentColor = new Color(1, (float)0.92, (float)0.16, 1);
                CurrentColor.GetColor();
                return;
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
