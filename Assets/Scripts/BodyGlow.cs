using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyGlow : MonoBehaviour
{
    public Material GreenMat;
    public Material YellowMat;
    public Material RedMat;
    public Material BlueMat;
    public Material PurpleMat;
    public string Color;
    void Start()
    {
        GreenMat = Resources.Load<Material>("Materials/Body Glow/Glow Green");
        YellowMat = Resources.Load<Material>("Materials/Body Glow/Glow Yellow");
        RedMat = Resources.Load<Material>("Materials/Body Glow/Glow Red");
        BlueMat = Resources.Load<Material>("Materials/Body Glow/Glow Blue");
        PurpleMat = Resources.Load<Material>("Materials/Body Glow/Glow Purple");

        transform.GetChild(0).transform.GetChild(3).GetComponent<Renderer>().material = GreenMat; // EYE
        transform.GetChild(0).transform.GetChild(8).GetComponent<Renderer>().material = GreenMat; // SHIRT
    }


    void Update()
    {
        if (Input.GetKeyDown("z"))
        {
            transform.GetChild(0).transform.GetChild(3).GetComponent<Renderer>().material = GreenMat; // EYE
            transform.GetChild(0).transform.GetChild(8).GetComponent<Renderer>().material = GreenMat; // SHIRT
            Color = "Green";
        }

        if (Input.GetKeyDown("x"))
        {
            transform.GetChild(0).transform.GetChild(3).GetComponent<Renderer>().material = YellowMat; // EYE
            transform.GetChild(0).transform.GetChild(8).GetComponent<Renderer>().material = YellowMat; // SHIRT
            Color = "Yellow";
        }

        if (Input.GetKeyDown("c"))
        {
            transform.GetChild(0).transform.GetChild(3).GetComponent<Renderer>().material = RedMat; // EYE
            transform.GetChild(0).transform.GetChild(8).GetComponent<Renderer>().material = RedMat; // SHIRT
            Color = "Red";
        }

        if (Input.GetKeyDown("v"))
        {
            transform.GetChild(0).transform.GetChild(3).GetComponent<Renderer>().material = BlueMat; // EYE
            transform.GetChild(0).transform.GetChild(8).GetComponent<Renderer>().material = BlueMat; // SHIRT
            Color = "Blue";
        }

        if (Input.GetKeyDown("b"))
        {
            transform.GetChild(0).transform.GetChild(3).GetComponent<Renderer>().material = PurpleMat; // EYE
            transform.GetChild(0).transform.GetChild(8).GetComponent<Renderer>().material = PurpleMat; // SHIRT
            Color = "Purple";
        }
    }
}
