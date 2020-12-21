using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PiChartObject : MonoBehaviour
{
    float TurnSpeed = 5;
    GameObject Trail;
    public int CurrentTrail = 1;

    public Material GreenMat;
    public Material YellowMat;
    public Material RedMat;
    public Material BlueMat;

    public GameObject PercentText3D;
    public GameObject DegreeText3D;

    public GameObject Render;
    public Vector3 LastAngle;
    public float LastPercent;
    public float LastDegree;
    bool Phasing = false;

    int CurrentColorPercent;
    int BluePercent = 50;
    int GreenPercent =25;
    int YellowPercent = 15;
    int RedPercent = 9;

    GameObject CurrentPieTextBlue;
    GameObject CurrentPieTextGreen;
    GameObject CurrentPieTextYellow;
    GameObject CurrentPieTextRed;

    GameObject ErrorLight;
    GameObject SuccessLight;
    GameObject Money;
    AudioSource ErrorSound, SuccessSound;
    void Start()
    {
        Money = GameObject.Find("Money");
        Money.SetActive(false);
        ErrorSound = gameObject.AddComponent<UnityEngine.AudioSource>();
        ErrorSound.clip = Resources.Load<AudioClip>("Audio/Pie Chart Sounds/Error Sound");
        ErrorSound.spatialBlend = 0;
        ErrorSound.volume = .15f;
        ErrorSound.playOnAwake = false;

        SuccessSound = gameObject.AddComponent<UnityEngine.AudioSource>();
        SuccessSound.clip = Resources.Load<AudioClip>("Audio/Pie Chart Sounds/Success Sound");
        SuccessSound.spatialBlend = 0;
        SuccessSound.volume = .15f;
        SuccessSound.playOnAwake = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;

        GreenMat = Resources.Load<Material>("Materials/Pie Line/Pie Line Green");
        YellowMat = Resources.Load<Material>("Materials/Pie Line/Pie Line Yellow");
        RedMat = Resources.Load<Material>("Materials/Pie Line/Pie Line Red");
        BlueMat = Resources.Load<Material>("Materials/Pie Line/Pie Line Blue");

        PercentText3D = GameObject.Find("Percent Text 3D");
        DegreeText3D = GameObject.Find("Degree Text 3D");

        Render = GameObject.Find("Line Drawer");

        PercentText3D.transform.position = transform.GetChild(0).transform.GetChild(0).transform.position + transform.forward * .15f + -transform.up * 1.15f;

        CurrentPieTextBlue = GameObject.Find("Current Pie Blue Text");
        CurrentPieTextGreen = GameObject.Find("Current Pie Green Text");
        CurrentPieTextYellow = GameObject.Find("Current Pie Yellow Text");
        CurrentPieTextRed = GameObject.Find("Current Pie Red Text");

        ErrorLight = GameObject.Find("Error Light");
        ErrorLight.SetActive(false);

        SuccessLight = GameObject.Find("Success Light");
        SuccessLight.SetActive(false);

        CurrentPieTextBlue.SetActive(false);
        CurrentPieTextGreen.SetActive(false);
        CurrentPieTextYellow.SetActive(false);
        CurrentPieTextRed.SetActive(false);
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !Phasing)
        {
            Phasing = true;
            Trail = Instantiate(Resources.Load<GameObject>("Prefabs/Trail" + CurrentTrail.ToString()), new Vector3(0, 0, 0), Quaternion.identity);
            Trail.tag = "Trail";
            Trail.transform.position = this.gameObject.transform.GetChild(0).transform.position;
            Trail.transform.eulerAngles = this.gameObject.transform.GetChild(0).transform.eulerAngles;
            Trail.transform.parent = this.gameObject.transform.GetChild(0).transform;

            //Trail.transform.GetChild(1).parent = this.gameObject.transform.GetChild(0).transform;
            //Trail.transform.GetChild(0).parent = this.gameObject.transform.GetChild(0).transform;

            transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).transform.localPosition = new Vector3(0, 0, 1.415f);
            transform.GetChild(0).transform.GetChild(1).transform.GetChild(1).transform.localPosition = new Vector3(0, 0,-1.65f);
        }

        if (Money.activeSelf)
        {
            foreach (GameObject Trails in GameObject.FindObjectsOfType<GameObject>())
            {
                if (Trails.tag == "Trail")
                {
                    Destroy(Trails);
                }
            }

            Money.transform.localScale = Vector3.MoveTowards(Money.transform.localScale, new Vector3(1, 1, 1), 5 * Time.deltaTime);
            transform.localScale = new Vector3(0,0,0);
        }

        if (Trail != null)
        {

            if (Input.GetMouseButtonUp(0))
            {
                if ( Mathf.Abs( Mathf.Abs (((Mathf.Round((((Render.GetComponent<PiChartRender>().AngleConverted - Render.GetComponent<PiChartRender>().DropValue) / 360) * 100))))   - Mathf.Abs ( CurrentColorPercent))  ) <= 1)
                {
                    if (CurrentTrail == 4)
                    {
                        //CurrentTrail = 0;
                        Money.SetActive(true);
                    }

                    Trail.transform.parent = null;
                    CurrentTrail += 1;
                    SuccessLight.SetActive(true);
                    SuccessSound.PlayOneShot(SuccessSound.clip, 1f);
                    StartCoroutine("TurnOffSuccessLight");
                    Render.GetComponent<PiChartRender>().DropValue = Render.GetComponent<PiChartRender>().AngleConverted;
                    Phasing = false;
                }

                else 
                {
                    transform.eulerAngles = LastAngle;
                    Render.GetComponent<PiChartRender>().DegreeText3D.GetComponent<TextMeshProUGUI>().text = LastDegree.ToString();
                    Render.GetComponent<PiChartRender>().PercentText3D.GetComponent<TextMeshProUGUI>().text = LastPercent.ToString();

                    PercentText3D.transform.position = transform.GetChild(0).transform.GetChild(0).transform.position + transform.forward * .15f + -transform.up * 1.15f;
                    Phasing = false;
                    Destroy(Trail);
                    ErrorLight.SetActive(true);
                    ErrorSound.PlayOneShot(ErrorSound.clip, 1f);
                    StartCoroutine("BlinkLight");
                }
            }

        }

        if (CurrentTrail ==1 && transform.GetChild(0).GetComponent<Renderer>().material != BlueMat)
        {
            transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Renderer>().material = BlueMat;
            transform.GetChild(0).transform.GetChild(0).GetComponent<Renderer>().material = BlueMat;
            transform.GetChild(0).GetComponent<Renderer>().material = BlueMat;

            CurrentColorPercent = BluePercent;
            LastAngle = new Vector3 (-90, 90,0);
            LastDegree = 0;
            LastPercent = 0;

            CurrentPieTextBlue.SetActive(true);
            CurrentPieTextGreen.SetActive(false);
            CurrentPieTextYellow.SetActive(false);
            CurrentPieTextRed.SetActive(false);
        }

        if (CurrentTrail == 2 && transform.GetChild(0).GetComponent<Renderer>().material != GreenMat)
        {
            transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Renderer>().material = GreenMat;
            transform.GetChild(0).transform.GetChild(0).GetComponent<Renderer>().material = GreenMat;
            transform.GetChild(0).GetComponent<Renderer>().material = GreenMat;

            CurrentColorPercent = GreenPercent;
            LastAngle = new Vector3(90, 0, 90);
            LastDegree = 0;
            LastPercent = 0;

            CurrentPieTextBlue.SetActive(false);
            CurrentPieTextGreen.SetActive(true);
            CurrentPieTextYellow.SetActive(false);
            CurrentPieTextRed.SetActive(false);
        }

        if (CurrentTrail == 3 && transform.GetChild(0).GetComponent<Renderer>().material != YellowMat)
        {
            transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Renderer>().material = YellowMat;
            transform.GetChild(0).transform.GetChild(0).GetComponent<Renderer>().material = YellowMat;
            transform.GetChild(0).GetComponent<Renderer>().material = YellowMat;


            CurrentColorPercent = YellowPercent;
            LastAngle = new Vector3(-180f, 90, 180);
            LastDegree = 0;
            LastPercent = 0;

            CurrentPieTextBlue.SetActive(false);
            CurrentPieTextGreen.SetActive(false);
            CurrentPieTextYellow.SetActive(true);
            CurrentPieTextRed.SetActive(false);
        }

        if (CurrentTrail == 4 && transform.GetChild(0).GetComponent<Renderer>().material != RedMat)
        {
            transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Renderer>().material = RedMat;
            transform.GetChild(0).transform.GetChild(0).GetComponent<Renderer>().material = RedMat;
            transform.GetChild(0).GetComponent<Renderer>().material = RedMat;

            CurrentColorPercent = RedPercent;
            LastAngle = new Vector3(-128.5f, 90, 180);
            LastDegree = 0;
            LastPercent = 0;

            CurrentPieTextBlue.SetActive(false);
            CurrentPieTextGreen.SetActive(false);
            CurrentPieTextYellow.SetActive(false);
            CurrentPieTextRed.SetActive(true);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 TargetDirection = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0) - transform.position;
            Vector3 NewDirection = Vector3.RotateTowards(transform.forward, TargetDirection, TurnSpeed * Time.deltaTime, 0);
            transform.rotation = Quaternion.LookRotation(NewDirection);

            PercentText3D.transform.position = transform.GetChild(0).transform.GetChild(0).transform.position + transform.forward * .15f + -transform.up * 1.15f;
        }
    }

    IEnumerator BlinkLight()
    {
        yield return new WaitForSeconds(.1f);
        ErrorLight.SetActive(false);
        StartCoroutine("Blink2");
    }


    IEnumerator Blink2()
    {
        yield return new WaitForSeconds(.1f);
        ErrorLight.SetActive(true);
        StartCoroutine("TurnOffLight");
    }
    IEnumerator TurnOffLight()
    {
        ErrorLight.SetActive(true);
        yield return new WaitForSeconds(.1f);
        ErrorLight.SetActive(false);
    }

    IEnumerator TurnOffSuccessLight()
    {
        ErrorLight.SetActive(true);
        yield return new WaitForSeconds(2f);
        SuccessLight.SetActive(false);
    }
}
