using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinePlots : MonoBehaviour
{
    public int GreenGiants;
    public int YellowGiants;
    public int RedGiants;
    public int BlueGiants;
    public int PurpleGiants;

    public float LinePlotHeight = 0f;
    public float LinePlotDistance = 11f;
    public float LeftOffSet = -3;
    public GameObject LinePlot;
    public GameObject LinePlot1;
    public GameObject LinePlot2;
    public GameObject LinePlot3;

    public GameObject[] GreenX1 = new GameObject[5];
    public GameObject[] YellowX1 = new GameObject[5];
    public GameObject[] RedX1= new GameObject[5];
    public GameObject[] BlueX1 = new GameObject[5];
    public GameObject[] PurpleX1 = new GameObject[5];
    public bool LinePlotShrink = true;
    void Start()
    {
        LinePlot1 = GameObject.Find("Line Plot 1");
        SetUpLinePlot(LinePlot1);
        LinePlot1.transform.localScale = new Vector3(0, 0, 0);

        LinePlot2 = GameObject.Find("Line Plot 2");
        SetUpLinePlot(LinePlot2);
        LinePlot2.transform.localScale = new Vector3(0, 0, 0);

        LinePlot3 = GameObject.Find("Line Plot 3");
        SetUpLinePlot(LinePlot3);
        LinePlot3.transform.localScale = new Vector3(0, 0, 0);
    }

    public void SetUpLinePlot(GameObject LinePlotPar)
    {
        LinePlot = LinePlotPar;
        if (LinePlot != null)
        {
            for (int i = 0; i <= 4; i++)
            {
                GreenX1[i] = LinePlot.transform.GetChild(i).gameObject;
                GreenX1[i].transform.localScale = new Vector3(0, 0, 0);
                GreenX1[i].SetActive(false);
            }

            for (int i = 5; i <= 9; i++)
            {
                YellowX1[i - 5] = LinePlot.transform.GetChild(i).gameObject;
                YellowX1[i - 5].transform.localScale = new Vector3(0, 0, 0);
                YellowX1[i - 5].SetActive(false);
            }

            for (int i = 10; i <= 14; i++)
            {
                RedX1[i - 10] = LinePlot.transform.GetChild(i).gameObject;
                RedX1[i - 10].transform.localScale = new Vector3(0, 0, 0);
                RedX1[i - 10].SetActive(false);
            }

            for (int i = 15; i <= 19; i++)
            {
                BlueX1[i - 15] = LinePlot.transform.GetChild(i).gameObject;
                BlueX1[i - 15].transform.localScale = new Vector3(0, 0, 0);
                BlueX1[i - 15].SetActive(false);
            }

            for (int i = 20; i <= 24; i++)
            {
                PurpleX1[i - 20] = LinePlot.transform.GetChild(i).gameObject;
                PurpleX1[i - 20].transform.localScale = new Vector3(0, 0, 0);
                PurpleX1[i - 20].SetActive(false);
            }
        }
    }
    void Update()
    {
        if (LinePlot == null)
        {
            LinePlot = GameObject.Find("Line Plot 1");
        }

        if (GreenGiants > 0)
        {
            if (GreenX1[GreenGiants - 1] != null)
            {
                GreenX1[GreenGiants - 1].SetActive(true);
                if (GreenX1[GreenGiants - 1].transform.localScale != new Vector3(1, 1, 1))
                {
                    GreenX1[GreenGiants - 1].transform.localScale = Vector3.MoveTowards(GreenX1[GreenGiants - 1].transform.localScale, new Vector3(1, 1, 1), 3 * Time.deltaTime);

                    LinePlot.transform.position = transform.position + new Vector3(Camera.main.transform.forward.x, LinePlotHeight, Camera.main.transform.forward.z) * LinePlotDistance + Camera.main.transform.right * LeftOffSet;
                    LinePlot.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(LinePlot.transform.forward, new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z), 9999 * Time.deltaTime, 0));
                    if (LinePlotShrink)
                    {
                        StopCoroutine("ShrinkLinePlot");
                        StartCoroutine("ShrinkLinePlot");
                    }
                    LinePlotShrink = false;
                }
            }
        }




        if (YellowGiants > 0)
        {
            if (YellowX1[YellowGiants - 1] != null)
            {
                YellowX1[YellowGiants - 1].SetActive(true);
                if (YellowX1[YellowGiants - 1].transform.localScale != new Vector3(1, 1, 1))
                {
                    YellowX1[YellowGiants - 1].transform.localScale = Vector3.MoveTowards(YellowX1[YellowGiants - 1].transform.localScale, new Vector3(1, 1, 1), 3 * Time.deltaTime);

                    LinePlot.transform.position = transform.position + new Vector3(Camera.main.transform.forward.x, LinePlotHeight, Camera.main.transform.forward.z) * LinePlotDistance + Camera.main.transform.right * LeftOffSet;
                    LinePlot.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(LinePlot.transform.forward, new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z), 9999 * Time.deltaTime, 0));
                    if (LinePlotShrink)
                    {
                        StopCoroutine("ShrinkLinePlot");
                        StartCoroutine("ShrinkLinePlot");
                    }
                    LinePlotShrink = false;
                }
            }
        }




        if (RedGiants > 0)
        {
            if (RedX1[RedGiants - 1] != null)
            {
                RedX1[RedGiants - 1].SetActive(true);
                if (RedX1[RedGiants - 1].transform.localScale != new Vector3(1, 1, 1))
                {
                    RedX1[RedGiants - 1].transform.localScale = Vector3.MoveTowards(RedX1[RedGiants - 1].transform.localScale, new Vector3(1, 1, 1), 3 * Time.deltaTime);

                    LinePlot.transform.position = transform.position + new Vector3(Camera.main.transform.forward.x, LinePlotHeight, Camera.main.transform.forward.z) * LinePlotDistance + Camera.main.transform.right * LeftOffSet;
                    LinePlot.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(LinePlot.transform.forward, new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z), 9999 * Time.deltaTime, 0));
                    if (LinePlotShrink)
                    {
                        StopCoroutine("ShrinkLinePlot");
                        StartCoroutine("ShrinkLinePlot");
                    }
                    LinePlotShrink = false;
                }
            }
        }




        if (BlueGiants > 0)
        {
            if (BlueX1[BlueGiants - 1] != null)
            {
                BlueX1[BlueGiants - 1].SetActive(true);
                if (BlueX1[BlueGiants - 1].transform.localScale != new Vector3(1, 1, 1))
                {
                    BlueX1[BlueGiants - 1].transform.localScale = Vector3.MoveTowards(BlueX1[BlueGiants - 1].transform.localScale, new Vector3(1, 1, 1), 3 * Time.deltaTime);

                    LinePlot.transform.position = transform.position + new Vector3(Camera.main.transform.forward.x, LinePlotHeight, Camera.main.transform.forward.z) * LinePlotDistance + Camera.main.transform.right * LeftOffSet;
                    LinePlot.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(LinePlot.transform.forward, new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z), 9999 * Time.deltaTime, 0));
                    if (LinePlotShrink)
                    {
                        StopCoroutine("ShrinkLinePlot");
                        StartCoroutine("ShrinkLinePlot");
                    }
                    LinePlotShrink = false;
                }
            }
        }




        if (PurpleGiants > 0)
        {
            if (PurpleX1[PurpleGiants - 1] != null)
            {
                PurpleX1[PurpleGiants - 1].SetActive(true);
                if (PurpleX1[PurpleGiants - 1].transform.localScale != new Vector3(1, 1, 1))
                {
                    PurpleX1[PurpleGiants - 1].transform.localScale = Vector3.MoveTowards(PurpleX1[PurpleGiants - 1].transform.localScale, new Vector3(1, 1, 1), 3 * Time.deltaTime);

                    LinePlot.transform.position = transform.position + new Vector3(Camera.main.transform.forward.x, LinePlotHeight, Camera.main.transform.forward.z) * LinePlotDistance + Camera.main.transform.right * LeftOffSet;
                    LinePlot.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(LinePlot.transform.forward, new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z), 9999 * Time.deltaTime, 0));
                    if (LinePlotShrink)
                    {
                        StopCoroutine("ShrinkLinePlot");
                        StartCoroutine("ShrinkLinePlot");
                    }
                    LinePlotShrink = false;
                }
            }
        }


        if (!LinePlotShrink)
        {
            if (LinePlot.transform.localScale != new Vector3(1, 1, 1))
            {
                LinePlot.transform.localScale = Vector3.MoveTowards(LinePlot.transform.localScale, new Vector3(1, 1, 1), 10 * Time.deltaTime);
            }
        }


        if (LinePlotShrink)
        {
            if (LinePlot.transform.localScale != new Vector3(0, 0, 0))
            {
                LinePlot.transform.localScale = Vector3.MoveTowards(LinePlot.transform.localScale, new Vector3(0, 0, 0), 5 * Time.deltaTime);
                LinePlot.transform.position = Vector3.MoveTowards(LinePlot.transform.position, transform.position, 10 * Time.deltaTime);
            }
        }


    }


    IEnumerator ShrinkLinePlot()
    {
        yield return new WaitForSeconds(600);
        LinePlotShrink = true;
    }
}
