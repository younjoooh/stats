using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas : MonoBehaviour
{
    // PARASITES
    public float TotalMonsters = 0;
    public float BlueMonsters = 0;
    public float GreenMonsters = 0;
    public float YellowMonsters = 0;
    public float RedMonsters = 0;

    public float BluePercentage = 0;
    public float GreenPercentage = 0;
    public float YellowPercentage = 0;
    public float RedPercentage = 0;

    public GameObject BlueText;
    public GameObject GreenText;
    public GameObject YellowText;
    public GameObject RedText;
    public GameObject TotalText;

    public GameObject BlueSubText;
    public GameObject GreenSubText;
    public GameObject YellowSubText;
    public GameObject RedSubText;

    public GameObject ParasiteCanvas;





    // GIANTS TIER 1
    public GameObject GiantCanvas1;

    public GameObject GreenGiantText1;
    public GameObject YellowGiantText1;
    public GameObject RedGiantText1;




    // GIANTS TIER 2
    public GameObject GiantCanvas2;

    public GameObject GreenGiantText2;
    public GameObject YellowGiantText2;
    public GameObject RedGiantText2;
    public GameObject BlueGiantText2;

    

    // GIANTS TIER 3
    public GameObject GiantCanvas3;

    public GameObject GreenGiantText3;
    public GameObject YellowGiantText3;
    public GameObject RedGiantText3;
    public GameObject BlueGiantText3;
    public GameObject PurpleGiantText3;


    public string CanvasType;
    public GameObject CanvasObject1;
    public GameObject CanvasObject2;
    public GameObject CanvasObject3;

    float GiantCanvasSpeed = 3000;


    float PopSpeed = 20;
    float PopScale = 1.5f;
    public bool GreenPop1;
    public bool GreenPop2;
    public bool GreenPop3;

    public bool YellowPop1;
    public bool YellowPop2;
    public bool YellowPop3;

    public bool RedPop1;
    public bool RedPop2;
    public bool RedPop3;

    public bool BluePop2;
    public bool BluePop3;

    public bool PurplePop3;

    void Start()
    {
        // PARASITES
        ParasiteCanvas = Instantiate(Resources.Load<GameObject>("Prefabs/Parasite Canvas"), new Vector3(0, 0, 0), Quaternion.identity);
        ParasiteCanvas.name = "Parasite Canvas";

        BlueText = GameObject.Find("Blue Text");
        BlueSubText = GameObject.Find("Blue Sub Text");

        GreenText = GameObject.Find("Green Text");
        GreenSubText = GameObject.Find("Green Sub Text");

        YellowText = GameObject.Find("Yellow Text");
        YellowSubText = GameObject.Find("Yellow Sub Text");

        RedText = GameObject.Find("Red Text");
        RedSubText = GameObject.Find("Red Sub Text");

        TotalText = GameObject.Find("Total Text");




        // GIANTS
        GiantCanvas1 = Instantiate(Resources.Load<GameObject>("Prefabs/Giant Canvas 1"), new Vector3(0, 0, 0), Quaternion.identity);
        GiantCanvas1.name = "Giant Canvas 1";

        GiantCanvas2 = Instantiate(Resources.Load<GameObject>("Prefabs/Giant Canvas 2"), new Vector3(0, 0, 0), Quaternion.identity);
        GiantCanvas2.name = "Giant Canvas 2";

        GiantCanvas3 = Instantiate(Resources.Load<GameObject>("Prefabs/Giant Canvas 3"), new Vector3(0, 0, 0), Quaternion.identity);
        GiantCanvas3.name = "Giant Canvas 3";

        GreenGiantText1 = GameObject.Find("Green Giant Text 1");
        YellowGiantText1 = GameObject.Find("Yellow Giant Text 1");
        RedGiantText1 = GameObject.Find("Red Giant Text 1");

        GreenGiantText2 = GameObject.Find("Green Giant Text 2");
        YellowGiantText2 = GameObject.Find("Yellow Giant Text 2");
        RedGiantText2 = GameObject.Find("Red Giant Text 2");
        BlueGiantText2 = GameObject.Find("Blue Giant Text 2");

        GreenGiantText3 = GameObject.Find("Green Giant Text 3");
        YellowGiantText3 = GameObject.Find("Yellow Giant Text 3");
        RedGiantText3 = GameObject.Find("Red Giant Text 3");
        BlueGiantText3 = GameObject.Find("Blue Giant Text 3");
        PurpleGiantText3 = GameObject.Find("Purple Giant Text 3");

        // FIND CANVAS OBJECTS
        CanvasObject1 = GameObject.Find("Canvas Object 1");
        CanvasObject2 = GameObject.Find("Canvas Object 2");
        CanvasObject3 = GameObject.Find("Canvas Object 3");

        GiantCanvas1.SetActive(false);
        GiantCanvas2.SetActive(false);
        GiantCanvas3.SetActive(false);



    }
    void Update()
    {
        if (CanvasType == "1")
        {
            if (CanvasObject1.transform.localPosition != new Vector3(530.3005f, -300.3519f, 0))
            {
                CanvasObject1.transform.localPosition = Vector3.MoveTowards(CanvasObject1.transform.localPosition, new Vector3(530.3005f, -300.3519f, 0), GiantCanvasSpeed * Time.deltaTime);// Center
            }

            if (CanvasObject2.transform.localPosition != new Vector3(530.3005f, -700.3519f, 0))
            {
                CanvasObject2.transform.localPosition = Vector3.MoveTowards(CanvasObject2.transform.localPosition, new Vector3(530.3005f, -700.3519f, 0), GiantCanvasSpeed * Time.deltaTime);// Bottom
            }

            if (CanvasObject3.transform.localPosition != new Vector3(530.3005f, -700.3519f, 0))
            {
                CanvasObject3.transform.localPosition = Vector3.MoveTowards(CanvasObject3.transform.localPosition, new Vector3(530.3005f, -700.3519f, 0), GiantCanvasSpeed * Time.deltaTime);// Bottom
            }
        }

        if (CanvasType == "2")
        {
            if (CanvasObject2.transform.localPosition != new Vector3(530.3005f, -300.3519f, 0))
            {
                CanvasObject2.transform.localPosition = Vector3.MoveTowards(CanvasObject2.transform.localPosition, new Vector3(530.3005f, -300.3519f, 0), GiantCanvasSpeed * Time.deltaTime);// Center
            }

            if (CanvasObject1.transform.localPosition != new Vector3(530.3005f, -700.3519f, 0))
            {
                CanvasObject1.transform.localPosition = Vector3.MoveTowards(CanvasObject1.transform.localPosition, new Vector3(530.3005f, -700.3519f, 0), GiantCanvasSpeed * Time.deltaTime);// Bottom
            }

            if (CanvasObject3.transform.localPosition != new Vector3(530.3005f, -700.3519f, 0))
            {
                CanvasObject3.transform.localPosition = Vector3.MoveTowards(CanvasObject3.transform.localPosition, new Vector3(530.3005f, -700.3519f, 0), GiantCanvasSpeed * Time.deltaTime);// Bottom
            }
        }

        if (CanvasType == "3")
        {
            if (CanvasObject3.transform.localPosition != new Vector3(530.3005f, -300.3519f, 0))
            {
                CanvasObject3.transform.localPosition = Vector3.MoveTowards(CanvasObject3.transform.localPosition, new Vector3(530.3005f, -300.3519f, 0), GiantCanvasSpeed * Time.deltaTime);// Center
            }

            if (CanvasObject1.transform.localPosition != new Vector3(530.3005f, -700.3519f, 0))
            {
                CanvasObject1.transform.localPosition = Vector3.MoveTowards(CanvasObject1.transform.localPosition, new Vector3(530.3005f, -700.3519f, 0), GiantCanvasSpeed * Time.deltaTime);// Bottom
            }

            if (CanvasObject2.transform.localPosition != new Vector3(530.3005f, -700.3519f, 0))
            {
                CanvasObject2.transform.localPosition = Vector3.MoveTowards(CanvasObject2.transform.localPosition, new Vector3(530.3005f, -700.3519f, 0), GiantCanvasSpeed * Time.deltaTime);// Bottom
            }
        }
        // PARASITE CANVAS
        if (TotalMonsters != 0)
        {
            BluePercentage   = Mathf.Round(((BlueMonsters / TotalMonsters)   * 100) * 1) / 1;
            GreenPercentage  = Mathf.Round(((GreenMonsters / TotalMonsters)  * 100) * 1) / 1;
            YellowPercentage = Mathf.Round(((YellowMonsters / TotalMonsters) * 100) * 1) / 1;
            RedPercentage    = Mathf.Round(((RedMonsters / TotalMonsters)    * 100) * 1) / 1;     
        }


        BlueText.GetComponent<Text>().text = BluePercentage.ToString() + "%";
        GreenText.GetComponent<Text>().text = GreenPercentage.ToString() + "%";
        YellowText.GetComponent<Text>().text = YellowPercentage.ToString() + "%";
        RedText.GetComponent<Text>().text = RedPercentage.ToString() + "%";
        TotalText.GetComponent<Text>().text = TotalMonsters.ToString();

        BlueSubText.GetComponent<Text>().text = BlueMonsters.ToString();
        GreenSubText.GetComponent<Text>().text = GreenMonsters.ToString();
        YellowSubText.GetComponent<Text>().text = YellowMonsters.ToString();
        RedSubText.GetComponent<Text>().text = RedMonsters.ToString();




        // GIANT CANVAS 1
        if (GetComponent<Shooting>().GiantGroup1[0] != null)
        {
            GreenGiantText1.GetComponent<Text>().text = GetComponent<Shooting>().GiantGroup1[0].GetComponent<Giant>().CurrentHits.ToString();
        }
        else
        {
            GreenGiantText1.GetComponent<Text>().text = "0";
        }

        if (GetComponent<Shooting>().GiantGroup1[1] != null)
        {
            YellowGiantText1.GetComponent<Text>().text = GetComponent<Shooting>().GiantGroup1[1].GetComponent<Giant>().CurrentHits.ToString();
        }
        else
        {
            YellowGiantText1.GetComponent<Text>().text = "0";
        }

        if (GetComponent<Shooting>().GiantGroup1[2] != null)
        {
            RedGiantText1.GetComponent<Text>().text = GetComponent<Shooting>().GiantGroup1[2].GetComponent<Giant>().CurrentHits.ToString();
        }
        else
        {
            RedGiantText1.GetComponent<Text>().text = "0";
        }




        // GIANT CANVAS 2
        if (GetComponent<Shooting>().GiantGroup2[0] != null)
        {
            GreenGiantText2.GetComponent<Text>().text = GetComponent<Shooting>().GiantGroup2[0].GetComponent<Giant>().CurrentHits.ToString();
        }
        else
        {
            GreenGiantText2.GetComponent<Text>().text = "0";
        }

        if (GetComponent<Shooting>().GiantGroup2[1] != null)
        {
            YellowGiantText2.GetComponent<Text>().text = GetComponent<Shooting>().GiantGroup2[1].GetComponent<Giant>().CurrentHits.ToString();
        }
        else
        {
            YellowGiantText2.GetComponent<Text>().text = "0";
        }

        if (GetComponent<Shooting>().GiantGroup2[2] != null)
        {
            RedGiantText2.GetComponent<Text>().text = GetComponent<Shooting>().GiantGroup2[2].GetComponent<Giant>().CurrentHits.ToString();
        }
        else
        {
            RedGiantText2.GetComponent<Text>().text = "0";
        }

        if (GetComponent<Shooting>().GiantGroup2[3] != null)
        {
            BlueGiantText2.GetComponent<Text>().text = GetComponent<Shooting>().GiantGroup2[3].GetComponent<Giant>().CurrentHits.ToString();
        }
        else
        {
            BlueGiantText2.GetComponent<Text>().text = "0";
        }




        // GIANT CANVAS 3
        if (GetComponent<Shooting>().GiantGroup3[0] != null)
        {
            GreenGiantText3.GetComponent<Text>().text = GetComponent<Shooting>().GiantGroup3[0].GetComponent<Giant>().CurrentHits.ToString();
        }
        else
        {
            GreenGiantText3.GetComponent<Text>().text = "0";
        }

        if (GetComponent<Shooting>().GiantGroup3[1] != null)
        {
            YellowGiantText3.GetComponent<Text>().text = GetComponent<Shooting>().GiantGroup3[1].GetComponent<Giant>().CurrentHits.ToString();
        }
        else
        {
            YellowGiantText3.GetComponent<Text>().text = "0";
        }

        if (GetComponent<Shooting>().GiantGroup3[2] != null)
        {
            RedGiantText3.GetComponent<Text>().text = GetComponent<Shooting>().GiantGroup3[2].GetComponent<Giant>().CurrentHits.ToString();
        }
        else
        {
            RedGiantText3.GetComponent<Text>().text = "0";
        }

        if (GetComponent<Shooting>().GiantGroup3[3] != null)
        {
            BlueGiantText3.GetComponent<Text>().text = GetComponent<Shooting>().GiantGroup3[3].GetComponent<Giant>().CurrentHits.ToString();
        }
        else
        {
            BlueGiantText3.GetComponent<Text>().text = "0";
        }

        if (GetComponent<Shooting>().GiantGroup3[4] != null)
        {
            PurpleGiantText3.GetComponent<Text>().text = GetComponent<Shooting>().GiantGroup3[4].GetComponent<Giant>().CurrentHits.ToString();
        }
        else
        {
            PurpleGiantText3.GetComponent<Text>().text = "0";
        }





        if (GreenPop1)
        {
            GreenGiantText1.transform.localScale = Vector3.MoveTowards(GreenGiantText1.transform.localScale, new Vector3(PopScale, PopScale, PopScale), PopSpeed * Time.deltaTime);
            GreenGiantText1.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (GreenGiantText1.transform.localScale != new Vector3(1, 1, 1))
        {
            GreenGiantText1.transform.localScale = Vector3.MoveTowards(GreenGiantText1.transform.localScale, new Vector3(1, 1, 1), PopSpeed * Time.deltaTime);
        }
        if (GreenGiantText1.transform.localScale == new Vector3(1, 1, 1))
        {
            GreenGiantText1.transform.GetChild(0).gameObject.SetActive(false);
        }




        if (YellowPop1)
        {
            YellowGiantText1.transform.localScale = Vector3.MoveTowards(YellowGiantText1.transform.localScale, new Vector3(PopScale, PopScale, PopScale), PopSpeed * Time.deltaTime);
            YellowGiantText1.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (YellowGiantText1.transform.localScale != new Vector3(1, 1, 1))
        {
            YellowGiantText1.transform.localScale = Vector3.MoveTowards(YellowGiantText1.transform.localScale, new Vector3(1, 1, 1), PopSpeed * Time.deltaTime);
        }
        if (YellowGiantText1.transform.localScale == new Vector3(1, 1, 1))
        {
            YellowGiantText1.transform.GetChild(0).gameObject.SetActive(false);
        }



        if (RedPop1)
        {
            RedGiantText1.transform.localScale = Vector3.MoveTowards(RedGiantText1.transform.localScale, new Vector3(PopScale, PopScale, PopScale), PopSpeed * Time.deltaTime);
            RedGiantText1.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (RedGiantText1.transform.localScale != new Vector3(1, 1, 1))
        {
            RedGiantText1.transform.localScale = Vector3.MoveTowards(RedGiantText1.transform.localScale, new Vector3(1, 1, 1), PopSpeed * Time.deltaTime);
        }
        if (RedGiantText1.transform.localScale == new Vector3(1, 1, 1))
        {
            RedGiantText1.transform.GetChild(0).gameObject.SetActive(false);
        }





        if (GreenPop2)
        {
            GreenGiantText2.transform.localScale = Vector3.MoveTowards(GreenGiantText2.transform.localScale, new Vector3(PopScale, PopScale, PopScale), PopSpeed * Time.deltaTime);
            GreenGiantText2.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (GreenGiantText2.transform.localScale != new Vector3(1, 1, 1))
        {
            GreenGiantText2.transform.localScale = Vector3.MoveTowards(GreenGiantText2.transform.localScale, new Vector3(1, 1, 1), PopSpeed * Time.deltaTime);
        }
        if (GreenGiantText2.transform.localScale == new Vector3(1, 1, 1))
        {
            GreenGiantText2.transform.GetChild(0).gameObject.SetActive(false);
        }




        if (YellowPop2)
        {
            YellowGiantText2.transform.localScale = Vector3.MoveTowards(YellowGiantText2.transform.localScale, new Vector3(PopScale, PopScale, PopScale), PopSpeed * Time.deltaTime);
            YellowGiantText2.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (YellowGiantText2.transform.localScale != new Vector3(1, 1, 1))
        {
            YellowGiantText2.transform.localScale = Vector3.MoveTowards(YellowGiantText2.transform.localScale, new Vector3(1, 1, 1), PopSpeed * Time.deltaTime);
        }
        if (YellowGiantText2.transform.localScale == new Vector3(1, 1, 1))
        {
            YellowGiantText2.transform.GetChild(0).gameObject.SetActive(false);
        }





        if (RedPop2)
        {
            RedGiantText2.transform.localScale = Vector3.MoveTowards(RedGiantText2.transform.localScale, new Vector3(PopScale, PopScale, PopScale), PopSpeed * Time.deltaTime);
            RedGiantText2.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (RedGiantText2.transform.localScale != new Vector3(1, 1, 1))
        {
            RedGiantText2.transform.localScale = Vector3.MoveTowards(RedGiantText2.transform.localScale, new Vector3(1, 1, 1), PopSpeed * Time.deltaTime);
        }
        if (RedGiantText2.transform.localScale == new Vector3(1, 1, 1))
        {
            RedGiantText2.transform.GetChild(0).gameObject.SetActive(false);
        }







        if (GreenPop3)
        {
            GreenGiantText3.transform.localScale = Vector3.MoveTowards(GreenGiantText3.transform.localScale, new Vector3(PopScale, PopScale, PopScale), PopSpeed * Time.deltaTime);
            GreenGiantText3.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (GreenGiantText3.transform.localScale != new Vector3(1, 1, 1))
        {
            GreenGiantText3.transform.localScale = Vector3.MoveTowards(GreenGiantText3.transform.localScale, new Vector3(1, 1, 1), PopSpeed * Time.deltaTime);
        }
        if (GreenGiantText3.transform.localScale == new Vector3(1, 1, 1))
        {
            GreenGiantText3.transform.GetChild(0).gameObject.SetActive(false);
        }



        if (YellowPop3)
        {
            YellowGiantText3.transform.localScale = Vector3.MoveTowards(YellowGiantText3.transform.localScale, new Vector3(PopScale, PopScale, PopScale), PopSpeed * Time.deltaTime);
            YellowGiantText3.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (YellowGiantText3.transform.localScale != new Vector3(1, 1, 1))
        {
            YellowGiantText3.transform.localScale = Vector3.MoveTowards(YellowGiantText3.transform.localScale, new Vector3(1, 1, 1), PopSpeed * Time.deltaTime);
        }
        if (YellowGiantText3.transform.localScale == new Vector3(1, 1, 1))
        {
            YellowGiantText3.transform.GetChild(0).gameObject.SetActive(false);
        }



        if (RedPop3)
        {
            RedGiantText3.transform.localScale = Vector3.MoveTowards(RedGiantText3.transform.localScale, new Vector3(PopScale, PopScale, PopScale), PopSpeed * Time.deltaTime);
            RedGiantText3.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (RedGiantText3.transform.localScale != new Vector3(1, 1, 1))
        {
            RedGiantText3.transform.localScale = Vector3.MoveTowards(RedGiantText3.transform.localScale, new Vector3(1, 1, 1), PopSpeed * Time.deltaTime);
        }
        if (RedGiantText3.transform.localScale == new Vector3(1, 1, 1))
        {
            RedGiantText3.transform.GetChild(0).gameObject.SetActive(false);
        }



        if (BluePop2)
        {
            BlueGiantText2.transform.localScale = Vector3.MoveTowards(BlueGiantText2.transform.localScale, new Vector3(PopScale, PopScale, PopScale), PopSpeed * Time.deltaTime);
            BlueGiantText2.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (BlueGiantText2.transform.localScale != new Vector3(1, 1, 1))
        {
            BlueGiantText2.transform.localScale = Vector3.MoveTowards(BlueGiantText2.transform.localScale, new Vector3(1, 1, 1), PopSpeed * Time.deltaTime);
        }
        if (BlueGiantText2.transform.localScale == new Vector3(1, 1, 1))
        {
            BlueGiantText2.transform.GetChild(0).gameObject.SetActive(false);
        }




        if (BluePop3)
        {
            BlueGiantText3.transform.localScale = Vector3.MoveTowards(BlueGiantText3.transform.localScale, new Vector3(PopScale, PopScale, PopScale), PopSpeed * Time.deltaTime);
            BlueGiantText3.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (BlueGiantText3.transform.localScale != new Vector3(1, 1, 1))
        {
            BlueGiantText3.transform.localScale = Vector3.MoveTowards(BlueGiantText3.transform.localScale, new Vector3(1, 1, 1), PopSpeed * Time.deltaTime);
        }
        if (BlueGiantText3.transform.localScale == new Vector3(1, 1, 1))
        {
            BlueGiantText3.transform.GetChild(0).gameObject.SetActive(false);
        }



        if (PurplePop3)
        {
            PurpleGiantText3.transform.localScale = Vector3.MoveTowards(PurpleGiantText3.transform.localScale, new Vector3(PopScale, PopScale, PopScale), PopSpeed * Time.deltaTime);
            PurpleGiantText3.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (PurpleGiantText3.transform.localScale != new Vector3(1, 1, 1))
        {
            PurpleGiantText3.transform.localScale = Vector3.MoveTowards(PurpleGiantText3.transform.localScale, new Vector3(1, 1, 1), PopSpeed * Time.deltaTime);
        }
        if (PurpleGiantText3.transform.localScale == new Vector3(1, 1, 1))
        {
            PurpleGiantText3.transform.GetChild(0).gameObject.SetActive(false);
        }


    }

    IEnumerator PopGreen1()
    {
        yield return new WaitForSeconds(.075f);
        GreenPop1 = false;
    }

    IEnumerator PopGreen2()
    {
        yield return new WaitForSeconds(.075f);
        GreenPop2 = false;
    }

    IEnumerator PopGreen3()
    {
        yield return new WaitForSeconds(.075f);
        GreenPop3 = false;
    }




    IEnumerator PopYellow1()
    {
        yield return new WaitForSeconds(.075f);
        YellowPop1 = false;
    }

    IEnumerator PopYellow2()
    {
        yield return new WaitForSeconds(.075f);
        YellowPop2 = false;
    }

    IEnumerator PopYellow3()
    {
        yield return new WaitForSeconds(.075f);
        YellowPop3 = false;
    }





    IEnumerator PopRed1()
    {
        yield return new WaitForSeconds(.075f);
        RedPop1 = false;
    }

    IEnumerator PopRed2()
    {
        yield return new WaitForSeconds(.075f);
        RedPop2 = false;
    }

    IEnumerator PopRed3()
    {
        yield return new WaitForSeconds(.075f);
        RedPop3 = false;
    }


    IEnumerator PopBlue2()
    {
        yield return new WaitForSeconds(.075f);
        BluePop2 = false;
    }


    IEnumerator PopBlue3()
    {
        yield return new WaitForSeconds(.075f);
        BluePop3 = false;
    }

    IEnumerator PopPurple3()
    {
        yield return new WaitForSeconds(.075f);
        PurplePop3 = false;
    }

}
