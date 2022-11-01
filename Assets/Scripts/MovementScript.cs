//using System;
using UnityEngine;
using SaveLoading;

namespace main
{
    public class MovementScript : MonoBehaviour
    {
        public float moveSpeed;
        public float jumpForce;

        public float mouseSensitivity;
        public float clampAngle;

        private float rotX = 0.0f;
        private float rotY = 0.0f;

        private bool paused = false;

        public GameObject PauseMenu;

        private static Color currentColor = new Color(1, 1, 1, 1);

        Transform m_Camera;
        GameObject m_PauseMenu;
        Rigidbody m_Rigidbody;

        // Start is called before the first frame update
        void Start()
        {

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            m_Camera = GetComponentInChildren<Transform>();
            m_Rigidbody = GetComponent<Rigidbody>();
            Vector3 rot = m_Rigidbody.transform.localRotation.eulerAngles;
            rotX = rot.x;
            rotY = rot.y;
            print("Start.cubeColor = " + currentColor);
        }

        // Update is called once per frame
        void Update()
        {
            Pausing();
            if (paused == true)
                return;
            Movement();
            Looking();
            ColorSwitching();
            Placing();
            Destroying();
        }

        public void Pausing()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (paused == false)
                {
                    m_PauseMenu = Instantiate(PauseMenu);
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    paused = true;
                    print("Pausing.paused");
                }
                else
                {
                    DestroyImmediate(m_PauseMenu, true);
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    paused = false;
                    print("Pausing.unpaused");
                }
            }
        }

        //DONE NEWER @ PlayerMovement
        public void Movement()
        {
            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //    if(m_Rigidbody.transform.position.y <= currentGroundLevel)
            //    {
            //        currentGroundLevel = m_Rigidbody.transform.position.y;
            //        m_Rigidbody.AddForce(new Vector3(0, jumpForce, 0));
            //    }
            //}
            if (Input.GetKey(KeyCode.W))
            {
                m_Rigidbody.MovePosition(m_Rigidbody.position + (transform.forward * moveSpeed));
            }
            if (Input.GetKey(KeyCode.A))
            {
                m_Rigidbody.MovePosition(m_Rigidbody.position + (-transform.right * moveSpeed));
            }
            if (Input.GetKey(KeyCode.S))
            {
                m_Rigidbody.MovePosition(m_Rigidbody.position + (-transform.forward * moveSpeed));
            }
            if (Input.GetKey(KeyCode.D))
            {
                m_Rigidbody.MovePosition(m_Rigidbody.position + (transform.right * moveSpeed));
            }
        }

        //DONE NEWER @ PlayerMovement
        public void Looking()
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            rotX += mouseY * mouseSensitivity * Time.deltaTime;
            rotY += mouseX * mouseSensitivity * Time.deltaTime;

            rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

            Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
            m_Camera.transform.rotation = localRotation;
        }

        //DONE NEWER @ CubeHandler
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

        //DONE NEWER @ CubeHandler
        public void Placing()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
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
                    Instantiate(createdCube, placeLocation, new Quaternion());
                    print("RGBA To String = " + rgbaToString(createdCube.GetComponent<Renderer>().material.GetColor("_Color")));
                }
            }
        }

        //DONE NEWER @ CubeHandler
        public void Destroying()
        {
            if (Input.GetMouseButtonDown(1))
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
        }

        public static void PlaceLoading(SaveData data)
        {
            Resetting();

            CubeLoader(data);
        }

        //DONE NEWER @ SaveHandler
        public static void Resetting()
        {
            GameObject[] currentGameObjects = GameObject.FindGameObjectsWithTag("BuildingBlock");
            int currentGameObjectsLenght = currentGameObjects.Length;
            for (int i = 0; i < currentGameObjectsLenght; i++)
            {
                Destroy(currentGameObjects[i].transform.gameObject);
            }
        }

        //DONE NEWER @ SaveHandler
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

        //DONE NEWER @ SaveHandler
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
                Instantiate(createdCube, data.cubes[i], new Quaternion());
            }
        }


        //DONE NEWER @ SaveHandler
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

        public Color stringToRGBA(string color)
        {
            switch (color)
            {
                case "White":
                    return new Color(1, 1, 1, 1);
                case "Black":
                    return new Color(0, 0, 0, 1);
                case "Red":
                    return new Color(1, 0, 0, 1);
                case "Green":
                    return new Color(0, 1, 0, 1);
                case "Blue":
                    return new Color(0, 0, 1, 1);
                case "Cyan":
                    return new Color(0, 1, 1, 1);
                case "Gray":
                    return new Color((float)0.5, (float)0.5, (float)0.5, 1);
                case "Magenta":
                    return new Color(1, 0, 1, 1);
                case "Yellow":
                    return new Color(1, (float)0.92, (float)0.16, 1);
                default:
                    return new Color(1, 1, 1, 1);
            }
        }

        public static string getCurrentColor()
        {
            string returnValue = rgbaToString(currentColor);
            return returnValue;
        }
    }
}
