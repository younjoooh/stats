using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class GraphicSettings : MonoBehaviour
{
    public bool StompEffects = true;
    public bool TurretSummonEffects = true;
    public bool TierSouls = true;
    public bool PostProcessing = true;
    public bool Lighting = true;
    public bool Buildings;
    public bool ShowSettings = false;

    public bool SetMovementSpeed;
    public bool SetCameraRenderDistance;

    public GameObject GraphicsCanvas;


    void Start()
    {
        GraphicsCanvas = GameObject.Find("Graphics Canvas");
        GraphicsCanvas.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            GraphicsCanvas.SetActive(true);
            ShowSettings = true;
        }

        if (Input.GetKeyDown("2"))
        {
            GraphicsCanvas.SetActive(false);
            ShowSettings = false;
        }




        if (SetMovementSpeed)
        {
            if (Input.GetKeyDown("1") ||Input.GetKeyDown("2") || Input.GetKeyDown("3") || Input.GetKeyDown("4") || Input.GetKeyDown("5")
                || Input.GetKeyDown("6") || Input.GetKeyDown("7") || Input.GetKeyDown("8") || Input.GetKeyDown("9") )
            {
                GetComponent<Movement>().RunSpeed = int.Parse(Input.inputString);
                GetComponent<Movement>().TurnSpeed = int.Parse(Input.inputString);
                SetMovementSpeed = false;
            }

        }



        if (SetCameraRenderDistance)
        {
            if (Input.GetKeyDown("1") || Input.GetKeyDown("2") || Input.GetKeyDown("3") || Input.GetKeyDown("4") || Input.GetKeyDown("5")
                || Input.GetKeyDown("6") || Input.GetKeyDown("7") || Input.GetKeyDown("8") || Input.GetKeyDown("9"))
            {
                Camera.main.farClipPlane  = int.Parse(Input.inputString) * 100;
                SetCameraRenderDistance = false;
            }
            if (Input.GetKeyDown("0")){
                Camera.main.farClipPlane = 10;
                SetCameraRenderDistance = false;
            }

        }
    }





    public void TurnOnStompEffect()
    {
        StompEffects = true;
    }

    public void TurnOffStompEffect()
    {
        StompEffects = false;
    }





    public void TurnOnTurretSummonEffect()
    {
        TurretSummonEffects = true;
    }

    public void TurnOffTurretSummonEffect()
    {
        TurretSummonEffects = false;
    }





    public void TurnOnTierSouls()
    {
        foreach (GameObject TierSoul in GameObject.FindObjectsOfType<GameObject>())
        {
            if (TierSoul.tag == "Tier Soul")
            {
                TierSoul.SetActive(true);
                TierSouls = true;
            }
        }
    }

    public void TurnOffTierSouls()
    {
        foreach (GameObject TierSoul in GameObject.FindObjectsOfType<GameObject>())
        {
            if (TierSoul.tag == "Tier Soul")
            {
                TierSoul.SetActive(false);
                TierSouls = false;
            }
        }
    }






    public void TurnOnPostProcessingVolume()
    {
        PostProcessVolume PostProcessingVolume = Camera.main.GetComponent<PostProcessVolume>();
        PostProcessingVolume.enabled = true;
        PostProcessing = true;
    }

    public void TurnOffPostProcessingVolume()
    {
        PostProcessVolume PostProcessingVolume = Camera.main.GetComponent<PostProcessVolume>();
        PostProcessingVolume.enabled = false;
        PostProcessing = false;
    }





    public void TurnOnLights()
    {
        foreach (GameObject Light in GameObject.FindObjectsOfType<GameObject>())
        {
            if (Light.tag == "Light")
            {
                Light.SetActive(true);
            }
        }
        Lighting = true;
    }
    public void TurnOffLights()
    {
        foreach (GameObject Light in GameObject.FindObjectsOfType<GameObject>())
        {
            if (Light.tag == "Light")
            {
                Light.SetActive(false);
            }
        }

        Lighting = false;
    }




    public void TurnOnSetMovementSpeed()
    {
        SetMovementSpeed = true;
    }

    public void TurnOffSetMovementSpeed()
    {
        SetMovementSpeed = false;
    }





    public void TurnOnSetDistance()
    {
        SetCameraRenderDistance = true;
    }

    public void TurnOffSetDistance()
    {
        SetCameraRenderDistance = false;
    }





    public void TurnOnBuildings()
    {
        foreach (GameObject Building in GameObject.FindObjectsOfType<GameObject>())
        {
            if (Building.tag == "Buildings")
            {
                Building.SetActive(true);
                Buildings = true;
            }
        }
    }

    public void TurnOffBuildings()
    {
        foreach (GameObject Building in GameObject.FindObjectsOfType<GameObject>())
        {
            if (Building.tag == "Buildings")
            {
                Building.SetActive(false);
                Buildings = false;
            }
        }
    }
}
