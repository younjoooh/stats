using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PiChartRender : MonoBehaviour
{
    public LineRenderer LineRender;
    public Vector2 StartingMousePosition;
    public Vector2 CurrentMousePosition;

    //public GameObject AngleText;
    public float Angle;
    public float AngleConverted;

    //public GameObject PercentText;
    public float Percent;


    public GameObject PercentText3D;
    public GameObject DegreeText3D;

    public GameObject DegreeSymbol1;
    public GameObject DegreeSymbol2;
    public GameObject DegreeSymbol3;
    public GameObject DegreeSymbol4;

    public float DropValue;

    void Start()
    {

        PercentText3D = GameObject.Find("Percent Text 3D Text");
        DegreeText3D = GameObject.Find("Degree Text 3D Text");

        //AngleText = GameObject.Find("Angle Text");
        //PercentText = GameObject.Find("Percent Text");
        LineRender = GetComponent<LineRenderer>();
        LineRender.positionCount = 2;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        DegreeSymbol1 = GameObject.Find("Degree Symbol 1");
        DegreeSymbol2 = GameObject.Find("Degree Symbol 2");
        DegreeSymbol3 = GameObject.Find("Degree Symbol 3");
        DegreeSymbol4 = GameObject.Find("Degree Symbol 4");

        DegreeSymbol1.SetActive(true);
        DegreeSymbol2.SetActive(false);
        DegreeSymbol3.SetActive(false);
        DegreeSymbol4.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            CurrentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            LineRender.SetPosition(0, new Vector3(StartingMousePosition.x, StartingMousePosition.y, 0f));
            LineRender.SetPosition(1, new Vector3(CurrentMousePosition.x, CurrentMousePosition.y, 0f));

            DegreeText3D.GetComponent<TextMeshProUGUI>().text = (AngleConverted - DropValue).ToString("F0") + "";
            PercentText3D.GetComponent<TextMeshProUGUI>().text = Mathf.Round((((AngleConverted - DropValue) / 360) * 100)).ToString("F0") + " %";
        }


        Angle = Vector3.SignedAngle(CurrentMousePosition - StartingMousePosition, transform.up, Vector3.forward);

        if (Angle < 0)
        {
            AngleConverted = Angle + 360;
        }
        else 
        {
            AngleConverted = Angle;
        }



        if (AngleConverted - DropValue <= 19)
        {
            DegreeSymbol1.SetActive(true);
            DegreeSymbol2.SetActive(false);
            DegreeSymbol3.SetActive(false);
            DegreeSymbol4.SetActive(false);
        }

        if (AngleConverted - DropValue >= 20 && AngleConverted - DropValue <= 99)
        {
            DegreeSymbol1.SetActive(false);
            DegreeSymbol2.SetActive(true);
            DegreeSymbol3.SetActive(false);
            DegreeSymbol4.SetActive(false);
        }
        if (AngleConverted - DropValue >= 100 && AngleConverted - DropValue <= 199)
        {
            DegreeSymbol1.SetActive(false);
            DegreeSymbol2.SetActive(false);
            DegreeSymbol3.SetActive(true);
            DegreeSymbol4.SetActive(false);
        }

        if (AngleConverted - DropValue >= 200)
        {
            DegreeSymbol1.SetActive(false);
            DegreeSymbol2.SetActive(false);
            DegreeSymbol3.SetActive(false);
            DegreeSymbol4.SetActive(true);
        }
    }
}
