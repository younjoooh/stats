using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BarGraphs : MonoBehaviour
{
    // TIER 1 BAR GRPAH
    public GameObject GreenCubeText1;
    public GameObject YellowCubeText1;
    public GameObject RedCubeText1;

    public GameObject GreenCubeCanvas1;
    public GameObject YellowCubeCanvas1;
    public GameObject RedCubeCanvas1;

    public GameObject GreenBarPivot1;
    public GameObject YellowBarPivot1;
    public GameObject RedBarPivot1;

    public GameObject GreenBarTop1;
    public GameObject YellowBarTop1;
    public GameObject RedBarTop1;

    public float GreenCubeValue1;
    public float YellowCubeValue1;
    public float RedCubeValue1;

    public bool TriggerGreen1;
    public bool TriggerYellow1;
    public bool TriggerRed1;




    // TIER 2 BAR GRPAH
    public GameObject GreenCubeText2;
    public GameObject YellowCubeText2;
    public GameObject RedCubeText2;
    public GameObject BlueCubeText2;

    public GameObject GreenCubeCanvas2;
    public GameObject YellowCubeCanvas2;
    public GameObject RedCubeCanvas2;
    public GameObject BlueCubeCanvas2;

    public GameObject GreenBarPivot2;
    public GameObject YellowBarPivot2;
    public GameObject RedBarPivot2;
    public GameObject BlueBarPivot2;

    public GameObject GreenBarTop2;
    public GameObject YellowBarTop2;
    public GameObject RedBarTop2;
    public GameObject BlueBarTop2;

    public float GreenCubeValue2;
    public float YellowCubeValue2;
    public float RedCubeValue2;
    public float BlueCubeValue2;

    public bool TriggerGreen2;
    public bool TriggerYellow2;
    public bool TriggerRed2;
    public bool TriggerBlue2;




    // TIER 3 BAR GRPAH
    public GameObject GreenCubeText3;
    public GameObject YellowCubeText3;
    public GameObject RedCubeText3;
    public GameObject BlueCubeText3;
    public GameObject PurpleCubeText3;

    public GameObject GreenCubeCanvas3;
    public GameObject YellowCubeCanvas3;
    public GameObject RedCubeCanvas3;
    public GameObject BlueCubeCanvas3;
    public GameObject PurpleCubeCanvas3;

    public GameObject GreenBarPivot3;
    public GameObject YellowBarPivot3;
    public GameObject RedBarPivot3;
    public GameObject BlueBarPivot3;
    public GameObject PurpleBarPivot3;

    public GameObject GreenBarTop3;
    public GameObject YellowBarTop3;
    public GameObject RedBarTop3;
    public GameObject BlueBarTop3;
    public GameObject PurpleBarTop3;

    public float GreenCubeValue3;
    public float YellowCubeValue3;
    public float RedCubeValue3;
    public float BlueCubeValue3;
    public float PurpleCubeValue3;

    public bool TriggerGreen3;
    public bool TriggerYellow3;
    public bool TriggerRed3;
    public bool TriggerBlue3;
    public bool TriggerPurple3;




    // ENABLE BAR GRAPHS
    public bool EnableBarGraph1;
    public bool EnableBarGraph2;
    public bool EnableBarGraph3;


    public bool MoveBarGraph1;
    public bool MoveBarGraph2;
    public bool MoveBarGraph3;
    public bool MoveBarGraphsDown;


    public GameObject BarGraph1;
    public GameObject BarGraph2;
    public GameObject BarGraph3;

    public bool SummonBarGraph;

    //SOUNDS/ EFFECTS
    public AudioSource BarGraphSound;
    public AudioSource BarGraphSound2;

    public GameObject BarGraphEffect;
    public GameObject BarGraphEffectClone;

    Animator Anim;

    float BarGraphHeight = 2.25f;
    float BarGraphDistance = 8f;
    float LeftOffSet = -2;
    void Start()
    {
        // ENABLE BAR GRAPHS
        BarGraph1 = Instantiate(Resources.Load<GameObject>("Prefabs/Bar Graph 1"), new Vector3(0, -16.9f, 0), Quaternion.identity);
        BarGraph2 = Instantiate(Resources.Load<GameObject>("Prefabs/Bar Graph 2"), new Vector3(10, -16.9f, 0), Quaternion.identity);
        BarGraph3 = Instantiate(Resources.Load<GameObject>("Prefabs/Bar Graph 3"), new Vector3(22, -16.9f, 0), Quaternion.identity);

        BarGraph1.name = "Bar Graph 1";
        BarGraph2.name = "Bar Graph 2";
        BarGraph3.name = "Bar Graph 3";




        // TIER 1 BAR GRPAH
        GreenCubeText1 = GameObject.Find("Green Cube Text 1");
        YellowCubeText1 = GameObject.Find("Yellow Cube Text 1");
        RedCubeText1 = GameObject.Find("Red Cube Text 1");

        GreenCubeCanvas1 = GameObject.Find("Green Cube Canvas 1");
        YellowCubeCanvas1 = GameObject.Find("Yellow Cube Canvas 1");
        RedCubeCanvas1 = GameObject.Find("Red Cube Canvas 1");

        GreenBarPivot1 = GameObject.Find("Green Bar Pivot 1");
        YellowBarPivot1 = GameObject.Find("Yellow Bar Pivot 1");
        RedBarPivot1 = GameObject.Find("Red Bar Pivot 1");

        GreenBarTop1 = GameObject.Find("Green Bar Top 1");
        YellowBarTop1 = GameObject.Find("Yellow Bar Top 1");
        RedBarTop1 = GameObject.Find("Red Bar Top 1");




        // TIER 2 BAR GRPAH
        GreenCubeText2 = GameObject.Find("Green Cube Text 2");
        YellowCubeText2 = GameObject.Find("Yellow Cube Text 2");
        RedCubeText2 = GameObject.Find("Red Cube Text 2");
        BlueCubeText2 = GameObject.Find("Blue Cube Text 2");

        GreenCubeCanvas2 = GameObject.Find("Green Cube Canvas 2");
        YellowCubeCanvas2 = GameObject.Find("Yellow Cube Canvas 2");
        RedCubeCanvas2 = GameObject.Find("Red Cube Canvas 2");
        BlueCubeCanvas2 = GameObject.Find("Blue Cube Canvas 2");

        GreenBarPivot2 = GameObject.Find("Green Bar Pivot 2");
        YellowBarPivot2 = GameObject.Find("Yellow Bar Pivot 2");
        RedBarPivot2 = GameObject.Find("Red Bar Pivot 2");
        BlueBarPivot2 = GameObject.Find("Blue Bar Pivot 2");

        GreenBarTop2 = GameObject.Find("Green Bar Top 2");
        YellowBarTop2 = GameObject.Find("Yellow Bar Top 2");
        RedBarTop2 = GameObject.Find("Red Bar Top 2");
        BlueBarTop2 = GameObject.Find("Blue Bar Top 2");




        // TIER 3 BAR GRPAH
        GreenCubeText3 = GameObject.Find("Green Cube Text 3");
        YellowCubeText3 = GameObject.Find("Yellow Cube Text 3");
        RedCubeText3 = GameObject.Find("Red Cube Text 3");
        BlueCubeText3 = GameObject.Find("Blue Cube Text 3");
        PurpleCubeText3 = GameObject.Find("Purple Cube Text 3");

        GreenCubeCanvas3 = GameObject.Find("Green Cube Canvas 3");
        YellowCubeCanvas3 = GameObject.Find("Yellow Cube Canvas 3");
        RedCubeCanvas3 = GameObject.Find("Red Cube Canvas 3");
        BlueCubeCanvas3 = GameObject.Find("Blue Cube Canvas 3");
        PurpleCubeCanvas3 = GameObject.Find("Purple Cube Canvas 3");

        GreenBarPivot3 = GameObject.Find("Green Bar Pivot 3");
        YellowBarPivot3 = GameObject.Find("Yellow Bar Pivot 3");
        RedBarPivot3 = GameObject.Find("Red Bar Pivot 3");
        BlueBarPivot3 = GameObject.Find("Blue Bar Pivot 3");
        PurpleBarPivot3 = GameObject.Find("Purple Bar Pivot 3");

        GreenBarTop3 = GameObject.Find("Green Bar Top 3");
        YellowBarTop3 = GameObject.Find("Yellow Bar Top 3");
        RedBarTop3 = GameObject.Find("Red Bar Top 3");
        BlueBarTop3 = GameObject.Find("Blue Bar Top 3");
        PurpleBarTop3 = GameObject.Find("Purple Bar Top 3");



        Anim = GetComponent<Animator>();



        // BAR GRAPH SOUND
        BarGraphSound = gameObject.AddComponent<UnityEngine.AudioSource>();
        BarGraphSound.clip = Resources.Load<AudioClip>("Audio/Bar Graph Sound 1");
        BarGraphSound.spatialBlend = 1;
        BarGraphSound.volume = .75f;
        BarGraphSound.playOnAwake = false;

        BarGraphSound2 = gameObject.AddComponent<UnityEngine.AudioSource>();
        BarGraphSound2.clip = Resources.Load<AudioClip>("Audio/Bar Graph Sound 2");
        BarGraphSound2.spatialBlend = 1;
        BarGraphSound2.volume = .75f;
        BarGraphSound2.playOnAwake = false;


        //BAR GRAPH EFFECT
        BarGraphEffect = Instantiate(Resources.Load<GameObject>("Effects/Bar Graph Effect"), new Vector3(0, -5000, 0), Quaternion.identity);
        BarGraphEffect.SetActive(false);
    }


    void Update()
    {
        // SUMMON BAR GRAPH ANIMATION
        if (Input.GetKeyDown("r") && !Anim.GetCurrentAnimatorStateInfo(0).IsTag("Melee Attack") && GetComponent<Health>().Recovered)
        {
            SummonBarGraph = true;
            BarGraphSound.PlayOneShot(BarGraphSound.clip, 1f);
            StartCoroutine("PlayBarGraphSound");
        }


        if (SummonBarGraph)
        {
            Vector3 TargetDirection = transform.position - Camera.main.transform.position;
            TargetDirection.y = 0;
            transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, TargetDirection, 10 * Time.deltaTime, 0));
        }

        Anim.SetBool("Summon Bar Graph", SummonBarGraph);

        if (Anim.GetCurrentAnimatorStateInfo(0).IsName("Summon Graph"))
        {
            SummonBarGraph = false;
            if (Anim.GetCurrentAnimatorStateInfo(0).normalizedTime <= .5f)
            {
                Vector3 TargetDirection = transform.position - Camera.main.transform.position;
                TargetDirection.y = 0;
                transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, TargetDirection, 10 * Time.deltaTime, 0));
            }
        }

        // MOVE BAR GRAPHS DOWN

        if (MoveBarGraph1)
        {
            MoveBarGraphsDown = false;
            BarGraph2.transform.position = Vector3.MoveTowards(BarGraph2.transform.position, new Vector3(BarGraph2.transform.position.x, -7, BarGraph2.transform.position.z), 20 * Time.deltaTime);
            BarGraph3.transform.position = Vector3.MoveTowards(BarGraph3.transform.position, new Vector3(BarGraph3.transform.position.x, -7, BarGraph3.transform.position.z), 20 * Time.deltaTime);
        }

        if (MoveBarGraph2)
        {
            MoveBarGraphsDown = false;
            BarGraph1.transform.position = Vector3.MoveTowards(BarGraph1.transform.position, new Vector3(BarGraph1.transform.position.x, -7, BarGraph1.transform.position.z), 20 * Time.deltaTime);
            BarGraph3.transform.position = Vector3.MoveTowards(BarGraph3.transform.position, new Vector3(BarGraph3.transform.position.x, -7, BarGraph3.transform.position.z), 20 * Time.deltaTime);
        }

        if (MoveBarGraph3)
        {
            MoveBarGraphsDown = false;
            BarGraph1.transform.position = Vector3.MoveTowards(BarGraph1.transform.position, new Vector3(BarGraph1.transform.position.x, -7, BarGraph1.transform.position.z), 20 * Time.deltaTime);
            BarGraph2.transform.position = Vector3.MoveTowards(BarGraph2.transform.position, new Vector3(BarGraph2.transform.position.x, -7, BarGraph2.transform.position.z), 20 * Time.deltaTime);
        }

        if (MoveBarGraphsDown)
        {
            BarGraph1.transform.position = Vector3.MoveTowards(BarGraph1.transform.position, new Vector3(BarGraph1.transform.position.x, -7, BarGraph1.transform.position.z), 20 * Time.deltaTime);
            BarGraph2.transform.position = Vector3.MoveTowards(BarGraph2.transform.position, new Vector3(BarGraph2.transform.position.x, -7, BarGraph2.transform.position.z), 20 * Time.deltaTime);
            BarGraph3.transform.position = Vector3.MoveTowards(BarGraph3.transform.position, new Vector3(BarGraph3.transform.position.x, -7, BarGraph3.transform.position.z), 20 * Time.deltaTime);
        }

        //TIER 1 COLOR TRIGGERS
        if (Input.GetKey("r") && GetComponent<Shooting>().GiantGroup1[0] != null && EnableBarGraph1)
        {
            StopCoroutine("WaitTriggerGreen1");
            StartCoroutine("WaitTriggerGreen1");
        }

        if (Input.GetKey("r") && GetComponent<Shooting>().GiantGroup1[1] != null && EnableBarGraph1)
        {
            StopCoroutine("WaitTriggerYellow1");
            StartCoroutine("WaitTriggerYellow1");
        }

        if (Input.GetKey("r") && GetComponent<Shooting>().GiantGroup1[2] != null && EnableBarGraph1)
        {
            StopCoroutine("WaitTriggerRed1");
            StartCoroutine("WaitTriggerRed1");
        }

        if (Input.GetKey("r") && EnableBarGraph1 &&
           (  GetComponent<Shooting>().GiantGroup1[0] != null
           || GetComponent<Shooting>().GiantGroup1[1] != null
           || GetComponent<Shooting>().GiantGroup1[2] != null))
        {
            MoveBarGraph1 = true;
            MoveBarGraph2 = false;
            MoveBarGraph3 = false;
            BarGraph1.transform.position = transform.position + new Vector3(Camera.main.transform.forward.x, BarGraphHeight, Camera.main.transform.forward.z) * BarGraphDistance;
            BarGraph1.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(BarGraph1.transform.forward, new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z), 9999 * Time.deltaTime, 0));
        }

        if (MoveBarGraph1 && BarGraph1.transform.position != transform.position + transform.forward * 5)
        {
            BarGraph1.transform.position = Vector3.MoveTowards(BarGraph1.transform.position, transform.position +  new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z) * BarGraphDistance + Camera.main.transform.right * LeftOffSet, 20 * Time.deltaTime);
            BarGraph1.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(BarGraph1.transform.forward, new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z),  9999 * Time.deltaTime, 0));
        }

        if (MoveBarGraph1 && BarGraph1.transform.position == transform.position + new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z) * BarGraphDistance + Camera.main.transform.right * LeftOffSet)
        {
            MoveBarGraph1 = false;
        }


        //TIER 2 COLOR TRIGGERS
        if (Input.GetKey("r") && GetComponent<Shooting>().GiantGroup2[0] != null)
        {
            StopCoroutine("WaitTriggerGreen2");
            StartCoroutine("WaitTriggerGreen2");
        }

        if (Input.GetKey("r") && GetComponent<Shooting>().GiantGroup2[1] != null)
        {
            StopCoroutine("WaitTriggerYellow2");
            StartCoroutine("WaitTriggerYellow2");
        }

        if (Input.GetKey("r") && GetComponent<Shooting>().GiantGroup2[2] != null)
        {
            StopCoroutine("WaitTriggerRed2");
            StartCoroutine("WaitTriggerRed2");
        }

        if (Input.GetKey("r") && GetComponent<Shooting>().GiantGroup2[3] != null)
        {
            StopCoroutine("WaitTriggerBlue2");
            StartCoroutine("WaitTriggerBlue2");
        }

        if (Input.GetKey("r") && EnableBarGraph2 &&
           (  GetComponent<Shooting>().GiantGroup2[0] != null
           || GetComponent<Shooting>().GiantGroup2[1] != null
           || GetComponent<Shooting>().GiantGroup2[2] != null
           || GetComponent<Shooting>().GiantGroup2[3] != null))
        {
            MoveBarGraph1 = false;
            MoveBarGraph2 = true;
            MoveBarGraph3 = false;
            BarGraph2.transform.position = transform.position + new Vector3(Camera.main.transform.forward.x, BarGraphHeight, Camera.main.transform.forward.z) * BarGraphDistance + Camera.main.transform.right * LeftOffSet;
            BarGraph2.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(BarGraph2.transform.forward, new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z), 9999 * Time.deltaTime, 0));
        }

        if (MoveBarGraph2 && BarGraph2.transform.position != transform.position + transform.forward * 5)
        {
            BarGraph2.transform.position = Vector3.MoveTowards(BarGraph2.transform.position, transform.position + new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z) * BarGraphDistance + Camera.main.transform.right * LeftOffSet, 20 * Time.deltaTime);
            BarGraph2.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(BarGraph2.transform.forward, new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z), 9999 * Time.deltaTime, 0));
        }

        if (MoveBarGraph2 && BarGraph2.transform.position == transform.position + new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z) * BarGraphDistance + Camera.main.transform.right * LeftOffSet)
        {
            MoveBarGraph2 = false;
        }




        //TIER 3 COLOR TRIGGERS
        if (Input.GetKey("r") && GetComponent<Shooting>().GiantGroup3[0] != null)
        {
            StopCoroutine("WaitTriggerGreen3");
            StartCoroutine("WaitTriggerGreen3");
        }

        if (Input.GetKey("r") && GetComponent<Shooting>().GiantGroup3[1] != null)
        {
            StopCoroutine("WaitTriggerYellow3");
            StartCoroutine("WaitTriggerYellow3");
        }

        if (Input.GetKey("r") && GetComponent<Shooting>().GiantGroup3[2] != null)
        {
            StopCoroutine("WaitTriggerRed3");
            StartCoroutine("WaitTriggerRed3");
        }

        if (Input.GetKey("r") && GetComponent<Shooting>().GiantGroup3[3] != null)
        {
            StopCoroutine("WaitTriggerBlue3");
            StartCoroutine("WaitTriggerBlue3");
        }

        if (Input.GetKey("r") && GetComponent<Shooting>().GiantGroup3[4] != null)
        {
            StopCoroutine("WaitTriggerPurple3");
            StartCoroutine("WaitTriggerPurple3");
        }

        if (Input.GetKey("r") && EnableBarGraph3 &&
           (  GetComponent<Shooting>().GiantGroup3[0] != null
           || GetComponent<Shooting>().GiantGroup3[1] != null
           || GetComponent<Shooting>().GiantGroup3[2] != null
           || GetComponent<Shooting>().GiantGroup3[3] != null
           || GetComponent<Shooting>().GiantGroup3[4] != null))
        {
            MoveBarGraph1 = false;
            MoveBarGraph2 = false;
            MoveBarGraph3 = true;
            BarGraph3.transform.position = transform.position + new Vector3(Camera.main.transform.forward.x, BarGraphHeight, Camera.main.transform.forward.z) * BarGraphDistance + Camera.main.transform.right * LeftOffSet;
            BarGraph3.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(BarGraph3.transform.forward, new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z), 9999 * Time.deltaTime, 0));
        }

        if (MoveBarGraph3 && BarGraph3.transform.position != transform.position + transform.forward * 5)
        {
            BarGraph3.transform.position = Vector3.MoveTowards(BarGraph3.transform.position, transform.position + new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z) * BarGraphDistance + Camera.main.transform.right * LeftOffSet, 20 * Time.deltaTime);
            BarGraph3.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(BarGraph3.transform.forward, new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z), 9999 * Time.deltaTime, 0));
        }

        if (MoveBarGraph3 && BarGraph3.transform.position == transform.position + new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z) * BarGraphDistance + Camera.main.transform.right * LeftOffSet)
        {
            MoveBarGraph3 = false;
        }




        #region TIER 1
        // GREEN BAR
        if (TriggerGreen1 && GetComponent<Shooting>().GiantGroup1[0] != null)
        {
            GreenCubeValue1 = Mathf.MoveTowards(GreenCubeValue1, GetComponent<Shooting>().GiantGroup1[0].GetComponent<Giant>().CurrentHits, 10 * Time.deltaTime);
            GreenCubeText1.GetComponent<TextMeshProUGUI>().text = Mathf.RoundToInt(GreenCubeValue1).ToString();
            GreenBarPivot1.transform.localScale = new Vector3(1, GreenCubeValue1 / 10, 1);
        }
        else if(GetComponent<Shooting>().GiantGroup1[0] == null)
        {
            GreenCubeText1.GetComponent<TextMeshProUGUI>().text = "0";
            GreenBarPivot1.transform.localScale = new Vector3(1, 0, 1);
        }

        if (GetComponent<Shooting>().GiantGroup1[0] != null)
        {
            if (GreenCubeValue1 == GetComponent<Shooting>().GiantGroup1[0].GetComponent<Giant>().CurrentHits)
            {
                TriggerGreen1 = false;
            }
        }
        GreenCubeText1.transform.position = GreenBarTop1.transform.position + new Vector3 (0,.51f,0) - GreenBarTop1.transform.forward*.51f;
        GreenCubeCanvas1.transform.position = GreenBarTop1.transform.position + new Vector3(0, .51f, 0) - GreenBarTop1.transform.forward * .51f;




        // YELLOW BAR
        if (TriggerYellow1 && GetComponent<Shooting>().GiantGroup1[1] != null)
        {
            YellowCubeValue1 = Mathf.MoveTowards(YellowCubeValue1, GetComponent<Shooting>().GiantGroup1[1].GetComponent<Giant>().CurrentHits, 10 * Time.deltaTime);
            YellowCubeText1.GetComponent<TextMeshProUGUI>().text = Mathf.RoundToInt(YellowCubeValue1).ToString();
            YellowBarPivot1.transform.localScale = new Vector3(1, YellowCubeValue1 / 10,1);
        }
        else if (GetComponent<Shooting>().GiantGroup1[1] == null)
        {
            YellowCubeText1.GetComponent<TextMeshProUGUI>().text = "0";
            YellowBarPivot1.transform.localScale = new Vector3(1, 0, 1);
        }

        if (GetComponent<Shooting>().GiantGroup1[1] != null)
        {
            if (YellowCubeValue1 == GetComponent<Shooting>().GiantGroup1[1].GetComponent<Giant>().CurrentHits)
            {
                TriggerYellow1 = false;
            }
        }
        YellowCubeText1.transform.position = YellowBarTop1.transform.position + new Vector3(0, .51f, 0) - YellowBarTop1.transform.forward * .51f;
        YellowCubeCanvas1.transform.position = YellowBarTop1.transform.position + new Vector3(0, .51f, 0) - YellowBarTop1.transform.forward * .51f;




        // RED BAR
        if (TriggerRed1 && GetComponent<Shooting>().GiantGroup1[2] != null)
        {
            RedCubeValue1 = Mathf.MoveTowards(RedCubeValue1, GetComponent<Shooting>().GiantGroup1[2].GetComponent<Giant>().CurrentHits, 10 * Time.deltaTime);
            RedCubeText1.GetComponent<TextMeshProUGUI>().text = Mathf.RoundToInt(RedCubeValue1).ToString();
            RedBarPivot1.transform.localScale = new Vector3(1, RedCubeValue1 / 10, 1);
        }
        else if (GetComponent<Shooting>().GiantGroup1[2] == null)
        {
            RedCubeText1.GetComponent<TextMeshProUGUI>().text = "0";
            RedBarPivot1.transform.localScale = new Vector3(1, 0, 1);
        }

        if (GetComponent<Shooting>().GiantGroup1[2] != null)
        {
            if (RedCubeValue1 == GetComponent<Shooting>().GiantGroup1[2].GetComponent<Giant>().CurrentHits)
            {
                TriggerRed1 = false;
            }
        }
        RedCubeText1.transform.position = RedBarTop1.transform.position + new Vector3(0, .51f, 0) - RedBarTop1.transform.forward * .51f;
        RedCubeCanvas1.transform.position = RedBarTop1.transform.position + new Vector3(0, .51f, 0) - RedBarTop1.transform.forward * .51f;
        #endregion




        #region TIER 2
        // GREEN BAR
        if (TriggerGreen2 && GetComponent<Shooting>().GiantGroup2[0] != null)
        {
            GreenCubeValue2 = Mathf.MoveTowards(GreenCubeValue2, GetComponent<Shooting>().GiantGroup2[0].GetComponent<Giant>().CurrentHits, 10 * Time.deltaTime);
            GreenCubeText2.GetComponent<TextMeshProUGUI>().text = Mathf.RoundToInt(GreenCubeValue2).ToString();
            GreenBarPivot2.transform.localScale = new Vector3(1, GreenCubeValue2 / 20, 1);
        }
        else if (GetComponent<Shooting>().GiantGroup2[0] == null)
        {
            GreenCubeText2.GetComponent<TextMeshProUGUI>().text = "0";
            GreenBarPivot2.transform.localScale = new Vector3(1, 0, 1);
        }

        if (GetComponent<Shooting>().GiantGroup2[0] != null)
        {
            if (GreenCubeValue2 == GetComponent<Shooting>().GiantGroup2[0].GetComponent<Giant>().CurrentHits)
            {
                TriggerGreen2 = false;
            }
        }
        GreenCubeText2.transform.position = GreenBarTop2.transform.position + new Vector3(0, .51f, 0) - GreenBarTop2.transform.forward * .51f;
        GreenCubeCanvas2.transform.position = GreenBarTop2.transform.position + new Vector3(0, .51f, 0) - GreenBarTop2.transform.forward * .51f;




        // YELLOW BAR
        if (TriggerYellow2 && GetComponent<Shooting>().GiantGroup2[1] != null)
        {
            YellowCubeValue2 = Mathf.MoveTowards(YellowCubeValue2, GetComponent<Shooting>().GiantGroup2[1].GetComponent<Giant>().CurrentHits, 10 * Time.deltaTime);
            YellowCubeText2.GetComponent<TextMeshProUGUI>().text = Mathf.RoundToInt(YellowCubeValue2).ToString();
            YellowBarPivot2.transform.localScale = new Vector3(1, YellowCubeValue2 / 20, 1);
        }
        else if (GetComponent<Shooting>().GiantGroup2[1] == null)
        {
            YellowCubeText2.GetComponent<TextMeshProUGUI>().text = "0";
            YellowBarPivot2.transform.localScale = new Vector3(1, 0, 1);
        }

        if (GetComponent<Shooting>().GiantGroup2[1] != null)
        {
            if (YellowCubeValue2 == GetComponent<Shooting>().GiantGroup2[1].GetComponent<Giant>().CurrentHits)
            {
                TriggerYellow2 = false;
            }
        }
        YellowCubeText2.transform.position = YellowBarTop2.transform.position + new Vector3(0, .51f, 0) - YellowBarTop2.transform.forward * .51f;
        YellowCubeCanvas2.transform.position = YellowBarTop2.transform.position + new Vector3(0, .51f, 0) - YellowBarTop2.transform.forward * .51f;




        // RED BAR
        if (TriggerRed2 && GetComponent<Shooting>().GiantGroup2[2] != null)
        {
            RedCubeValue2 = Mathf.MoveTowards(RedCubeValue2, GetComponent<Shooting>().GiantGroup2[2].GetComponent<Giant>().CurrentHits, 10 * Time.deltaTime);
            RedCubeText2.GetComponent<TextMeshProUGUI>().text = Mathf.RoundToInt(RedCubeValue2).ToString();
            RedBarPivot2.transform.localScale = new Vector3(1, RedCubeValue2 / 20, 1);
        }
        else if (GetComponent<Shooting>().GiantGroup2[2] == null)
        {
            RedCubeText2.GetComponent<TextMeshProUGUI>().text = "0";
            RedBarPivot2.transform.localScale = new Vector3(1, 0, 1);
        }

        if (GetComponent<Shooting>().GiantGroup2[2] != null)
        {
            if (RedCubeValue2 == GetComponent<Shooting>().GiantGroup2[2].GetComponent<Giant>().CurrentHits)
            {
                TriggerRed2 = false;
            }
        }
        RedCubeText2.transform.position = RedBarTop2.transform.position + new Vector3(0, .51f, 0) - RedBarTop2.transform.forward * .51f;
        RedCubeCanvas2.transform.position = RedBarTop2.transform.position + new Vector3(0, .51f, 0) - RedBarTop2.transform.forward * .51f;




        // BLUE BAR
        if (TriggerBlue2 && GetComponent<Shooting>().GiantGroup2[3] != null)
        {
            BlueCubeValue2 = Mathf.MoveTowards(BlueCubeValue2, GetComponent<Shooting>().GiantGroup2[3].GetComponent<Giant>().CurrentHits, 10 * Time.deltaTime);
            BlueCubeText2.GetComponent<TextMeshProUGUI>().text = Mathf.RoundToInt(BlueCubeValue2).ToString();
            BlueBarPivot2.transform.localScale = new Vector3(1, BlueCubeValue2 / 20, 1);
        }
        else if (GetComponent<Shooting>().GiantGroup2[3] == null)
        {
            BlueCubeText2.GetComponent<TextMeshProUGUI>().text = "0";
            BlueBarPivot2.transform.localScale = new Vector3(1, 0, 1);
        }

        if (GetComponent<Shooting>().GiantGroup2[3] != null)
        {
            if (BlueCubeValue2 == GetComponent<Shooting>().GiantGroup2[3].GetComponent<Giant>().CurrentHits)
            {
                TriggerBlue2 = false;
            }
        }
        BlueCubeText2.transform.position = BlueBarTop2.transform.position + new Vector3(0, .51f, 0) - BlueBarTop2.transform.forward * .51f;
        BlueCubeCanvas2.transform.position = BlueBarTop2.transform.position + new Vector3(0, .51f, 0) - BlueBarTop2.transform.forward * .51f;
        #endregion




        #region TIER 3
        // GREEN BAR
        if (TriggerGreen3 && GetComponent<Shooting>().GiantGroup3[0] != null)
        {
            GreenCubeValue3 = Mathf.MoveTowards(GreenCubeValue3, GetComponent<Shooting>().GiantGroup3[0].GetComponent<Giant>().CurrentHits, 10 * Time.deltaTime);
            GreenCubeText3.GetComponent<TextMeshProUGUI>().text = Mathf.RoundToInt(GreenCubeValue3).ToString();
            GreenBarPivot3.transform.localScale = new Vector3(1, GreenCubeValue3 / 40, 1);
        }
        else if (GetComponent<Shooting>().GiantGroup3[0] == null)
        {
            GreenCubeText3.GetComponent<TextMeshProUGUI>().text = "0";
            GreenBarPivot3.transform.localScale = new Vector3(1, 0, 1);
        }

        if (GetComponent<Shooting>().GiantGroup3[0] != null)
        {
            if (GreenCubeValue3 == GetComponent<Shooting>().GiantGroup3[0].GetComponent<Giant>().CurrentHits)
            {
                TriggerGreen3 = false;
            }
        }
        GreenCubeText3.transform.position = GreenBarTop3.transform.position + new Vector3(0, .51f, 0) - GreenBarTop3.transform.forward * .51f;
        GreenCubeCanvas3.transform.position = GreenBarTop3.transform.position + new Vector3(0, .51f, 0) - GreenBarTop3.transform.forward * .51f;




        // YELLOW BAR
        if (TriggerYellow3 && GetComponent<Shooting>().GiantGroup3[1] != null)
        {
            YellowCubeValue3 = Mathf.MoveTowards(YellowCubeValue3, GetComponent<Shooting>().GiantGroup3[1].GetComponent<Giant>().CurrentHits, 10 * Time.deltaTime);
            YellowCubeText3.GetComponent<TextMeshProUGUI>().text = Mathf.RoundToInt(YellowCubeValue3).ToString();
            YellowBarPivot3.transform.localScale = new Vector3(1, YellowCubeValue3 / 40, 1);
        }
        else if (GetComponent<Shooting>().GiantGroup3[1] == null)
        {
            YellowCubeText3.GetComponent<TextMeshProUGUI>().text = "0";
            YellowBarPivot3.transform.localScale = new Vector3(1, 0, 1);
        }

        if (GetComponent<Shooting>().GiantGroup3[1] != null)
        {
            if (YellowCubeValue3 == GetComponent<Shooting>().GiantGroup3[1].GetComponent<Giant>().CurrentHits)
            {
                TriggerYellow3 = false;
            }
        }
        YellowCubeText3.transform.position = YellowBarTop3.transform.position + new Vector3(0, .51f, 0) - YellowBarTop3.transform.forward * .51f;
        YellowCubeCanvas3.transform.position = YellowBarTop3.transform.position + new Vector3(0, .51f, 0) - YellowBarTop3.transform.forward * .51f;




        // RED BAR
        if (TriggerRed3 && GetComponent<Shooting>().GiantGroup3[2] != null)
        {
            RedCubeValue3 = Mathf.MoveTowards(RedCubeValue3, GetComponent<Shooting>().GiantGroup3[2].GetComponent<Giant>().CurrentHits, 10 * Time.deltaTime);
            RedCubeText3.GetComponent<TextMeshProUGUI>().text = Mathf.RoundToInt(RedCubeValue3).ToString();
            RedBarPivot3.transform.localScale = new Vector3(1, RedCubeValue3 / 40, 1);
        }
        else if (GetComponent<Shooting>().GiantGroup3[2] == null)
        {
            RedCubeText3.GetComponent<TextMeshProUGUI>().text = "0";
            RedBarPivot3.transform.localScale = new Vector3(1, 0, 1);
        }

        if (GetComponent<Shooting>().GiantGroup3[2] != null)
        {
            if (RedCubeValue3 == GetComponent<Shooting>().GiantGroup3[2].GetComponent<Giant>().CurrentHits)
            {
                TriggerRed3 = false;
            }
        }
        RedCubeText3.transform.position = RedBarTop3.transform.position + new Vector3(0, .51f, 0) - RedBarTop3.transform.forward * .51f;
        RedCubeCanvas3.transform.position = RedBarTop3.transform.position + new Vector3(0, .51f, 0) - RedBarTop3.transform.forward * .51f;




        // BLUE BAR
        if (TriggerBlue3 && GetComponent<Shooting>().GiantGroup3[3] != null)
        {
            BlueCubeValue3 = Mathf.MoveTowards(BlueCubeValue3, GetComponent<Shooting>().GiantGroup3[3].GetComponent<Giant>().CurrentHits, 10 * Time.deltaTime);
            BlueCubeText3.GetComponent<TextMeshProUGUI>().text = Mathf.RoundToInt(BlueCubeValue3).ToString();
            BlueBarPivot3.transform.localScale = new Vector3(1, BlueCubeValue3 / 40, 1);
        }
        else if (GetComponent<Shooting>().GiantGroup3[3] == null)
        {
            BlueCubeText3.GetComponent<TextMeshProUGUI>().text = "0";
            BlueBarPivot3.transform.localScale = new Vector3(1, 0, 1);
        }

        if (GetComponent<Shooting>().GiantGroup3[3] != null)
        {
            if (BlueCubeValue3 == GetComponent<Shooting>().GiantGroup3[3].GetComponent<Giant>().CurrentHits)
            {
                TriggerBlue3 = false;
            }
        }
        BlueCubeText3.transform.position = BlueBarTop3.transform.position + new Vector3(0, .51f, 0) - BlueBarTop3.transform.forward * .51f;
        BlueCubeCanvas3.transform.position = BlueBarTop3.transform.position + new Vector3(0, .51f, 0) - BlueBarTop3.transform.forward * .51f;




        // PURPLE BAR
        if (TriggerPurple3 && GetComponent<Shooting>().GiantGroup3[4] != null)
        {
            PurpleCubeValue3 = Mathf.MoveTowards(PurpleCubeValue3, GetComponent<Shooting>().GiantGroup3[4].GetComponent<Giant>().CurrentHits, 10 * Time.deltaTime);
            PurpleCubeText3.GetComponent<TextMeshProUGUI>().text = Mathf.RoundToInt(PurpleCubeValue3).ToString();
            PurpleBarPivot3.transform.localScale = new Vector3(1, PurpleCubeValue3 / 40, 1);
        }
        else if (GetComponent<Shooting>().GiantGroup3[4] == null)
        {
            PurpleCubeText3.GetComponent<TextMeshProUGUI>().text = "0";
            PurpleBarPivot3.transform.localScale = new Vector3(1, 0, 1);
        }

        if (GetComponent<Shooting>().GiantGroup3[4] != null)
        {
            if (PurpleCubeValue3 == GetComponent<Shooting>().GiantGroup3[4].GetComponent<Giant>().CurrentHits)
            {
                TriggerPurple3 = false;
            }
        }
        PurpleCubeText3.transform.position = PurpleBarTop3.transform.position + new Vector3(0, .51f, 0) - PurpleBarTop3.transform.forward * .51f;
        PurpleCubeCanvas3.transform.position = PurpleBarTop3.transform.position + new Vector3(0, .51f, 0) - PurpleBarTop3.transform.forward * .51f;
        #endregion
    }

    // TIER 1
    IEnumerator WaitTriggerGreen1()
    {
        yield return new WaitForSeconds(.35f);
        TriggerGreen1 = true;
    }

    IEnumerator WaitTriggerYellow1()
    {
        yield return new WaitForSeconds(.35f);
        TriggerYellow1 = true;
    }

    IEnumerator WaitTriggerRed1()
    {
        yield return new WaitForSeconds(.35f);
        TriggerRed1 = true;
    }




    // TIER 2
    IEnumerator WaitTriggerGreen2()
    {
        yield return new WaitForSeconds(.35f);
        TriggerGreen2 = true;
    }

    IEnumerator WaitTriggerYellow2()
    {
        yield return new WaitForSeconds(.35f);
        TriggerYellow2 = true;
    }

    IEnumerator WaitTriggerRed2()
    {
        yield return new WaitForSeconds(.35f);
        TriggerRed2 = true;
    }

    IEnumerator WaitTriggerBlue2()
    {
        yield return new WaitForSeconds(.35f);
        TriggerBlue2 = true;
    }




    // TIER 3
    IEnumerator WaitTriggerGreen3()
    {
        yield return new WaitForSeconds(.35f);
        TriggerGreen3 = true;
    }

    IEnumerator WaitTriggerYellow3()
    {
        yield return new WaitForSeconds(.35f);
        TriggerYellow3 = true;
    }

    IEnumerator WaitTriggerRed3()
    {
        yield return new WaitForSeconds(.35f);
        TriggerRed3 = true;
    }

    IEnumerator WaitTriggerBlue3()
    {
        yield return new WaitForSeconds(.35f);
        TriggerBlue3 = true;
    }

    IEnumerator WaitTriggerPurple3()
    {
        yield return new WaitForSeconds(.35f);
        TriggerPurple3 = true;
    }

    IEnumerator PlayBarGraphSound()
    {
        yield return new WaitForSeconds(.85f);
        BarGraphSound2.PlayOneShot(BarGraphSound2.clip, 1f);
        BarGraphEffectClone = Instantiate(BarGraphEffect, transform.position + new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z) * BarGraphDistance + BarGraphEffect.transform.right * 2, Quaternion.identity);
        BarGraphEffectClone.SetActive(true);
    }
}
