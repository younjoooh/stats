using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera3D : MonoBehaviour
{
    public float CamSensitivity = 1;
    public float ScrollSensitivity = 15;
    public GameObject Player;
    public GameObject CameraPivot;
    public Vector3 CameraPosition = new Vector3(0, .75f, -3f);


    void Start()
    {
        Player = this.gameObject;

        CameraPivot = new GameObject("Camera Pivot");
        CameraPivot.transform.parent = Player.transform;
        Camera.main.transform.parent = CameraPivot.transform;
        CameraPivot.transform.parent = null;

        Camera.main.transform.localPosition = CameraPosition;
        Camera.main.transform.eulerAngles = new Vector3(10, 0, 0);

        // CAMERA FOLLOW
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }


    void Update()
    {
        CameraPivot.transform.position = Player.transform.position;

        float newRotationX = CameraPivot.transform.localEulerAngles.x - Input.GetAxis("Mouse Y") * CamSensitivity;
        float newRotationY = CameraPivot.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * CamSensitivity;

        CameraPivot.transform.transform.localEulerAngles = new Vector3(newRotationX, newRotationY, 0);

        /*
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, CameraPivot.transform.position, ScrollSensitivity * Time.deltaTime);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, CameraPivot.transform.position, -ScrollSensitivity * Time.deltaTime);
        }
        */



        // CAMERA POSITION

        if (!Player.GetComponent<PlayAudio>().Singing)
        {
            if (GetComponent<Shooting>().CombatMode && !GetComponent<Shooting>().CombatModeMelee && !GetComponent<Shooting>().Aim)
            {
                CameraPosition = new Vector3(.75f, .85f, -1.5f); // COMBAT MODE
                Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 30, 3 * Time.deltaTime);
            }

            if (GetComponent<Shooting>().CombatModeMelee && !GetComponent<Shooting>().Aim)
            {
                CameraPosition = new Vector3(.375f, .8f, -2.25f); // COMBAT MODE MELEE
                Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 30, 3 * Time.deltaTime);
            }

            if (!GetComponent<Shooting>().CombatMode && !GetComponent<Shooting>().Aim)
            {
                CameraPosition = new Vector3(0f, .75f, -3f); // RUN MODE
                Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 30, 3 * Time.deltaTime);
            }

            if (GetComponent<Shooting>().Aim)
            {
                CameraPosition = new Vector3(.75f, 1.15f, -3f); // AIM MODE
                Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 15, 3 * Time.deltaTime);
            }
        }
      

    }
}
