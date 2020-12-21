using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class RebelCanvas : MonoBehaviour
{
    public GameObject NarrationBar;
    public GameObject NarrationText;

    public GameObject RebelText;
    public GameObject RebelTextBR;
    public GameObject RebelButtons;
    public GameObject TimerText;

    public GameObject QuestionMenu;
    public GameObject QuestionText;
    public GameObject QuestionTip;
    public GameObject TimeStampText;
    public GameObject AnswerA;
    public GameObject AnswerB;
    public GameObject Player;
    public GameObject DataCanvas;


    public int[] RebelTimes = new int[10];
    public string[] RebelQuestions = new string[10];
    public string[] RebelAnswersA = new string[10];
    public string[] RebelAnswersB = new string[10];
    public int CurrentRebel;
    public float Timer;
    public bool TimeStarted;

    public GameObject[] BackIcon = new GameObject[10];
    public GameObject[] FilledIcon = new GameObject[10];
    public GameObject[] NumberIcon = new GameObject[10];

    public int Class1;
    public int Class2;
    public int Class3;
    public int Class4;
    public int Class5;

    public GameObject ClassText1;
    public GameObject ClassText2;
    public GameObject ClassText3;
    public GameObject ClassText4;
    public GameObject ClassText5;

    public GameObject Plot;
    public AudioSource RebelAudio;

    public GameObject Cume1;
    public GameObject Cume2;
    public GameObject Cume3;
    public GameObject Cume4;
    public GameObject Cume5;
    public GameObject CompleteCanvas;

    public int DataCollected;
    bool prompted = false;
    void Start()
    {
        RebelAudio = gameObject.AddComponent<UnityEngine.AudioSource>();
        RebelAudio.clip = Resources.Load<AudioClip>("Audio/Rebel Audio/1");
        RebelAudio.spatialBlend = 0;
        RebelAudio.volume = 1f;
        RebelAudio.playOnAwake = false;

        Player = GameObject.FindWithTag("Player");
        RebelText.SetActive(false);
        TimerText.SetActive(false);
        QuestionMenu.SetActive(false);
        RebelButtons.SetActive(false);

        CurrentRebel = 0;
        Timer = 0;

        for (int i= 0; i<= 9; i ++)
        {
            FilledIcon[i].transform.localScale = new Vector3(0f, 0f, 0f);
        }

        for (int i = 0; i <= 9; i++)
        {
            FilledIcon[i].SetActive(false);
        }

        ClassText1.transform.parent.transform.gameObject.SetActive(false);
        ClassText2.transform.parent.transform.gameObject.SetActive(false);
        ClassText3.transform.parent.transform.gameObject.SetActive(false);
        ClassText4.transform.parent.transform.gameObject.SetActive(false);
        ClassText5.transform.parent.transform.gameObject.SetActive(false);

        StartCoroutine(ShowStart());
       
        
    }

    void Update()
    {
        if (DataCollected == 10)
        {
            DataCollected = 11;
            PromptCumes();
        }
        if (Input.GetKeyDown("t") && prompted)
        {
            StartCumes();
            StopCoroutine(DeActivatePlot());
        }
        if (prompted)
        {
            Plot.SetActive(true);
        }

        RebelText.GetComponent<TextMeshProUGUI>().text = "Rebel - " + CurrentRebel;
        QuestionText.GetComponent<TextMeshProUGUI>().text = RebelQuestions[CurrentRebel];
        AnswerA.GetComponent<TextMeshProUGUI>().text = RebelAnswersA[CurrentRebel];
        AnswerB.GetComponent<TextMeshProUGUI>().text = RebelAnswersB[CurrentRebel];
        QuestionTip.GetComponent<TextMeshProUGUI>().text = "Press (" + CurrentRebel  +") to Re-Play";

        ClassText1.GetComponent<TextMeshProUGUI>().text = Class1.ToString();
        ClassText2.GetComponent<TextMeshProUGUI>().text = Class2.ToString();
        ClassText3.GetComponent<TextMeshProUGUI>().text = Class3.ToString();
        ClassText4.GetComponent<TextMeshProUGUI>().text = Class4.ToString();
        ClassText5.GetComponent<TextMeshProUGUI>().text = Class5.ToString();

        Cume1.GetComponent<TextMeshProUGUI>().text = ClassText1.GetComponent<TextMeshProUGUI>().text;

        Cume2.GetComponent<TextMeshProUGUI>().text = (int.Parse(ClassText1.GetComponent<TextMeshProUGUI>().text) + int.Parse(ClassText2.GetComponent<TextMeshProUGUI>().text)).ToString();

        Cume3.GetComponent<TextMeshProUGUI>().text = (int.Parse(ClassText1.GetComponent<TextMeshProUGUI>().text) + int.Parse(ClassText2.GetComponent<TextMeshProUGUI>().text) + int.Parse(ClassText3.GetComponent<TextMeshProUGUI>().text)).ToString();

        Cume4.GetComponent<TextMeshProUGUI>().text = (int.Parse(ClassText1.GetComponent<TextMeshProUGUI>().text) + int.Parse(ClassText2.GetComponent<TextMeshProUGUI>().text) + int.Parse(ClassText3.GetComponent<TextMeshProUGUI>().text) + int.Parse(ClassText4.GetComponent<TextMeshProUGUI>().text)).ToString();

        Cume5.GetComponent<TextMeshProUGUI>().text = (int.Parse(ClassText1.GetComponent<TextMeshProUGUI>().text) + int.Parse(ClassText2.GetComponent<TextMeshProUGUI>().text) + int.Parse(ClassText3.GetComponent<TextMeshProUGUI>().text) + int.Parse(ClassText4.GetComponent<TextMeshProUGUI>().text) + int.Parse(ClassText5.GetComponent<TextMeshProUGUI>().text)).ToString();


        if (Cume1.transform.parent.transform.localScale != new Vector3(.75f, .75f, .75f))
        {
            Cume1.transform.parent.transform.localScale = Vector3.MoveTowards(Cume1.transform.parent.transform.localScale, new Vector3(.75f, .75f, .75f), .45f * Time.deltaTime);
        }

        if (Cume2.transform.parent.transform.localScale != new Vector3(.75f, .75f, .75f))
        {
            Cume2.transform.parent.transform.localScale = Vector3.MoveTowards(Cume2.transform.parent.transform.localScale, new Vector3(.75f, .75f, .75f), .45f * Time.deltaTime);
        }

        if (Cume3.transform.parent.transform.localScale != new Vector3(.75f, .75f, .75f))
        {
            Cume3.transform.parent.transform.localScale = Vector3.MoveTowards(Cume3.transform.parent.transform.localScale, new Vector3(.75f, .75f, .75f), .45f * Time.deltaTime);
        }

        if (Cume4.transform.parent.transform.localScale != new Vector3(.75f, .75f, .75f))
        {
            Cume4.transform.parent.transform.localScale = Vector3.MoveTowards(Cume4.transform.parent.transform.localScale, new Vector3(.75f, .75f, .75f), .45f * Time.deltaTime);
        }

        if (Cume5.transform.parent.transform.localScale != new Vector3(.75f, .75f, .75f))
        {
            Cume5.transform.parent.transform.localScale = Vector3.MoveTowards(Cume5.transform.parent.transform.localScale, new Vector3(.75f, .75f, .75f), .45f * Time.deltaTime);
        }




        if (Plot.transform.localScale == new Vector3(0.79586f, 0.79586f, 0.79586f))
        {
            if (ClassText1.transform.parent.transform.localScale != new Vector3(.75f, .75f, .75f))
            {
                ClassText1.transform.parent.transform.localScale = Vector3.MoveTowards(ClassText1.transform.parent.transform.localScale, new Vector3(.75f, .75f, .75f), 2 * Time.deltaTime);
            }

            if (ClassText2.transform.parent.transform.localScale != new Vector3(.75f, .75f, .75f))
            {
                ClassText2.transform.parent.transform.localScale = Vector3.MoveTowards(ClassText2.transform.parent.transform.localScale, new Vector3(.75f, .75f, .75f), 2 * Time.deltaTime);
            }

            if (ClassText3.transform.parent.transform.localScale != new Vector3(.75f, .75f, .75f))
            {
                ClassText3.transform.parent.transform.localScale = Vector3.MoveTowards(ClassText3.transform.parent.transform.localScale, new Vector3(.75f, .75f, .75f), 2 * Time.deltaTime);
            }

            if (ClassText4.transform.parent.transform.localScale != new Vector3(.75f, .75f, .75f))
            {
                ClassText4.transform.parent.transform.localScale = Vector3.MoveTowards(ClassText4.transform.parent.transform.localScale, new Vector3(.75f, .75f, .75f), 2 * Time.deltaTime);
            }

            if (ClassText5.transform.parent.transform.localScale != new Vector3(.75f, .75f, .75f))
            {
                ClassText5.transform.parent.transform.localScale = Vector3.MoveTowards(ClassText5.transform.parent.transform.localScale, new Vector3(.75f, .75f, .75f), 2 * Time.deltaTime);
            }
        }

        if (Plot.transform.localScale != new Vector3 (0.79586f, 0.79586f, 0.79586f))
        {
            Plot.transform.localScale = Vector3.MoveTowards(Plot.transform.localScale, new Vector3(0.79586f, 0.79586f, 0.79586f), 2f * Time.deltaTime);
        }

        if (FilledIcon[CurrentRebel].activeSelf && FilledIcon[CurrentRebel].transform.localScale!= new Vector3(1,1,1))
        {
            FilledIcon[CurrentRebel].transform.localScale = Vector3.MoveTowards(FilledIcon[CurrentRebel].transform.localScale, new Vector3 (1,1,1), 3 * Time.deltaTime);
            BackIcon[CurrentRebel].transform.localScale = Vector3.MoveTowards(BackIcon[CurrentRebel].transform.localScale, new Vector3(0, 0, 0), 3 * Time.deltaTime);
        }

        FilledIcon[CurrentRebel].transform.GetChild(1).transform.gameObject.GetComponent<Text>().text = RebelTimes[CurrentRebel].ToString();


        if (Mathf.Round(Timer) <= 9)
        {
            TimerText.GetComponent<TextMeshProUGUI>().text = "00:0" + Mathf.Round(Timer);
        }

        if (Mathf.Round(Timer) > 9)
        {
            TimerText.GetComponent<TextMeshProUGUI>().text = "00:" + Mathf.Round(Timer);
        }


        if (RebelTimes[CurrentRebel] <= 9)
        {
            TimeStampText.GetComponent<TextMeshProUGUI>().text = "00:0" + RebelTimes[CurrentRebel];
        }

        if (RebelTimes[CurrentRebel] > 9)
        {
            TimeStampText.GetComponent<TextMeshProUGUI>().text = "00:" + RebelTimes[CurrentRebel];
        }




        if (!Player.GetComponent<PlayAudio>().Singing && (Input.GetKeyDown("0") || Input.GetKeyDown("1") || Input.GetKeyDown("2") || Input.GetKeyDown("3")|| Input.GetKeyDown("4") ||
            Input.GetKeyDown("5") || Input.GetKeyDown("6") || Input.GetKeyDown("7") || Input.GetKeyDown("8") || Input.GetKeyDown("9")))
        {
            Timer = 0;
            TimeStarted = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (Input.GetKeyDown("0"))
                CurrentRebel = 0;

            if (Input.GetKeyDown("1"))
                CurrentRebel = 1;

            if (Input.GetKeyDown("2"))
                CurrentRebel = 2;

            if (Input.GetKeyDown("3"))
                CurrentRebel = 3;

            if (Input.GetKeyDown("4"))
                CurrentRebel = 4;

            if (Input.GetKeyDown("5"))
                CurrentRebel = 5;

            if (Input.GetKeyDown("6"))
                CurrentRebel = 6;

            if (Input.GetKeyDown("7"))
                CurrentRebel = 7;

            if (Input.GetKeyDown("8"))
                CurrentRebel = 8;

            if (Input.GetKeyDown("9"))
                CurrentRebel = 9;

            StartSong();
        }

        else if (Timer < RebelTimes[CurrentRebel])
        {
            if (TimeStarted)
            {
                Timer += Time.deltaTime;
            }
        }
        else
        {
            EndSong();
        }

        if (RebelText.activeSelf)
        {
            RebelTextBR.SetActive(true);
        }
        else 
        {
            RebelTextBR.SetActive(false);
        }
    }

    public void PromptCumes()
    {
        NarrationText.GetComponent<TextMeshProUGUI>().text = "Now that you've collected 10 Continuous data points," +
                                                    "\npress the (t) key to fill out the cumulative data on the table.";

        if (RebelAudio.clip != Resources.Load<AudioClip>("Audio/Rebel Audio/6"))
        {
            RebelAudio.clip = Resources.Load<AudioClip>("Audio/Rebel Audio/6");
            RebelAudio.Stop();
            RebelAudio.PlayOneShot(RebelAudio.clip, 1f);
        }
        prompted = true;
    }



    public void EndSong()
    {
        RebelButtons.SetActive(false);
        RebelText.SetActive(false);
        TimerText.SetActive(false);
        QuestionMenu.SetActive(true);
        TimeStarted = false;
        Timer = 0;
    }

    public void StartSong()
    {
        NarrationBar.SetActive(false);
        QuestionMenu.SetActive(false);
        RebelText.SetActive(true);
        TimerText.SetActive(true);
        RebelButtons.SetActive(true);
        DataCanvas.SetActive(false);

        for (int i = 0; i <= 9; i++)
        {
            NumberIcon[1].SetActive(false);
        }
    }

    public void BackToMainScene()
    {
        QuestionMenu.SetActive(false);
        RebelText.SetActive(false);
        TimerText.SetActive(false);
        RebelButtons.SetActive(false);

        NarrationBar.SetActive(true);
        Timer = 0;
        TimeStarted = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Player.GetComponent<PlayAudio>().StartCoroutine("ReturnPlayer", 0);
        FilledIcon[CurrentRebel].SetActive(true);
        DataCanvas.SetActive(true);

        DataCollected += 1;
        for (int i = 0; i <= 9; i++)
        {
            NumberIcon[1].SetActive(true);
        }

        Plot.SetActive(true);
        Plot.transform.localScale = new Vector3(0, 0, 0);
        StartCoroutine(DeActivatePlot());

        if (RebelTimes[CurrentRebel] >= 21 && RebelTimes[CurrentRebel] <= 30)
        {
            ClassText1.transform.parent.transform.gameObject.SetActive(true);
            ClassText1.transform.parent.transform.localScale = new Vector3(0, 0, 0);

            NarrationText.GetComponent<TextMeshProUGUI>().text = "Success! the song's length was between 21 and 30 " +
                                                    "\nseconds. Therfore, it was added to class 1 on the table!";

            if (RebelAudio.clip != Resources.Load<AudioClip>("Audio/Rebel Audio/1"))
            {
                RebelAudio.clip = Resources.Load<AudioClip>("Audio/Rebel Audio/1");
                RebelAudio.Stop();
                RebelAudio.PlayOneShot(RebelAudio.clip, 1f);
            }

            StartCoroutine(AddClass1());
        }

        if (RebelTimes[CurrentRebel] >= 31 && RebelTimes[CurrentRebel] <= 40)
        {
            ClassText2.transform.parent.transform.gameObject.SetActive(true);
            ClassText2.transform.parent.transform.localScale = new Vector3(0, 0, 0);

            NarrationText.GetComponent<TextMeshProUGUI>().text = "Success! the song's length was between 31 and 40 " +
                                                    "\nseconds. Therefore, it was added to class 2 on the table!";

            if (RebelAudio.clip != Resources.Load<AudioClip>("Audio/Rebel Audio/2"))
            {
                RebelAudio.clip = Resources.Load<AudioClip>("Audio/Rebel Audio/2");
                RebelAudio.Stop();
                RebelAudio.PlayOneShot(RebelAudio.clip, 1f);
            }

            StartCoroutine(AddClass2());
        }

        if (RebelTimes[CurrentRebel] >= 41 && RebelTimes[CurrentRebel] <= 50)
        {
            ClassText3.transform.parent.transform.gameObject.SetActive(true);
            ClassText3.transform.parent.transform.localScale = new Vector3(0, 0, 0);

            NarrationText.GetComponent<TextMeshProUGUI>().text = "Success! the song's length was between 41 and 50 " +
                                        "\nseconds. Therefore, it was added to class 3 on the table!";

            if (RebelAudio.clip != Resources.Load<AudioClip>("Audio/Rebel Audio/3"))
            {
                RebelAudio.clip = Resources.Load<AudioClip>("Audio/Rebel Audio/3");
                RebelAudio.Stop();
                RebelAudio.PlayOneShot(RebelAudio.clip, 1f);
            }

            StartCoroutine(AddClass3());
        }

        if (RebelTimes[CurrentRebel] >= 51 && RebelTimes[CurrentRebel] <= 60)
        {
            ClassText4.transform.parent.transform.gameObject.SetActive(true);
            ClassText4.transform.parent.transform.localScale = new Vector3(0, 0, 0);

            NarrationText.GetComponent<TextMeshProUGUI>().text = "Success! the song's length was between 51 and 60 " +
                                        "\nseconds. Therefore, it was added to class 4 on the table!";

            if (RebelAudio.clip != Resources.Load<AudioClip>("Audio/Rebel Audio/4"))
            {
                RebelAudio.clip = Resources.Load<AudioClip>("Audio/Rebel Audio/4");
                RebelAudio.Stop();
                RebelAudio.PlayOneShot(RebelAudio.clip, 1f);
            }

            StartCoroutine(AddClass4());
        }

        if (RebelTimes[CurrentRebel] >= 61 && RebelTimes[CurrentRebel] <= 70)
        {
            ClassText5.transform.parent.transform.gameObject.SetActive(true);
            ClassText5.transform.parent.transform.localScale = new Vector3(0, 0, 0);

            NarrationText.GetComponent<TextMeshProUGUI>().text = "Success! the song's length was between 61 and 70 " +
                                        "\nseconds.Therefore it was added to class 5 on the table!";

            if (RebelAudio.clip != Resources.Load<AudioClip>("Audio/Rebel Audio/5"))
            {
                RebelAudio.clip = Resources.Load<AudioClip>("Audio/Rebel Audio/5");
                RebelAudio.Stop();
                RebelAudio.PlayOneShot(RebelAudio.clip, 1f);
            }

            StartCoroutine(AddClass5());
        }

    }

    public void StartCumes()
    {
        StopCoroutine(DeActivatePlot());
        Plot.SetActive(true);
        //Plot.transform.localScale = new Vector3(0,0,0);
        Cume1.transform.parent.gameObject.SetActive(true);
        Cume2.transform.parent.gameObject.SetActive(true);
        Cume3.transform.parent.gameObject.SetActive(true);
        Cume4.transform.parent.gameObject.SetActive(true);
        Cume5.transform.parent.gameObject.SetActive(true);

        RebelAudio.clip = Resources.Load<AudioClip>("Audio/Rebel Audio/Select");
        RebelAudio.PlayOneShot(RebelAudio.clip, 1f);

        NarrationText.GetComponent<TextMeshProUGUI>().text = "the cumulative data is the continous addition of each class stacked on top of each other," +
                                                           "\n examine the table to see how the cumulative data is added";

        if (RebelAudio.clip != Resources.Load<AudioClip>("Audio/Rebel Audio/7"))
        {
            RebelAudio.clip = Resources.Load<AudioClip>("Audio/Rebel Audio/7");
            RebelAudio.Stop();
            RebelAudio.PlayOneShot(RebelAudio.clip, 1f);
        }

        StartCoroutine(MissionComplet(8));
        /*
        StartCoroutine(ActivateCume2(1));

        StartCoroutine(ActivateCume3(2));

        StartCoroutine(ActivateCume4(3));

        StartCoroutine(ActivateCume5(4));
        */
    }

    public void OnHover(Button CurrentButton)
    {
        CurrentButton.interactable = true;
    }

    public void OnNotHover(Button CurrentButton)
    {
        CurrentButton.interactable = false;
    }

    public void OnClicked(Button CurrentButton)
    {
        CurrentButton.interactable = false;
    }

    IEnumerator AddClass1()
    {
        yield return new WaitForSeconds(1.5f);
        Class1 += 1;
        if (RebelAudio.clip != Resources.Load<AudioClip>("Audio/Rebel Audio/Select"))
        {
            RebelAudio.clip = Resources.Load<AudioClip>("Audio/Rebel Audio/Select");
            RebelAudio.PlayOneShot(RebelAudio.clip, 1f);
        }
    }

    IEnumerator AddClass2()
    {
        yield return new WaitForSeconds(1.5f);
        Class2 += 1;
        if (RebelAudio.clip != Resources.Load<AudioClip>("Audio/Rebel Audio/Select"))
        {
            RebelAudio.clip = Resources.Load<AudioClip>("Audio/Rebel Audio/Select");
            RebelAudio.PlayOneShot(RebelAudio.clip, 1f);
        }
    }

    IEnumerator AddClass3()
    {
        yield return new WaitForSeconds(1.5f);
        Class3 += 1;
        if (RebelAudio.clip != Resources.Load<AudioClip>("Audio/Rebel Audio/Select"))
        {
            RebelAudio.clip = Resources.Load<AudioClip>("Audio/Rebel Audio/Select");
            RebelAudio.PlayOneShot(RebelAudio.clip, 1f);
        }
    }

    IEnumerator AddClass4()
    {
        yield return new WaitForSeconds(1.5f);
        Class4 += 1;
        if (RebelAudio.clip != Resources.Load<AudioClip>("Audio/Rebel Audio/Select"))
        {
            RebelAudio.clip = Resources.Load<AudioClip>("Audio/Rebel Audio/Select");
            RebelAudio.PlayOneShot(RebelAudio.clip, 1f);
        }
    }

    IEnumerator AddClass5()
    {
        yield return new WaitForSeconds(1.5f);
        Class5 += 1;
        if (RebelAudio.clip != Resources.Load<AudioClip>("Audio/Rebel Audio/Select"))
        {
            RebelAudio.clip = Resources.Load<AudioClip>("Audio/Rebel Audio/Select");
            RebelAudio.PlayOneShot(RebelAudio.clip, 1f);
        }
    }


    IEnumerator ShowDefault()
    {
        yield return new WaitForSeconds(20);
        Plot.SetActive(false);
        NarrationText.GetComponent<TextMeshProUGUI>().text = "To interact with the rebles, Use your key board" +
                                                            "\nto press the numbers indicated above thier heads";

        if (RebelAudio.clip != Resources.Load<AudioClip>("Audio/Rebel Audio/Default"))
        {
            RebelAudio.clip = Resources.Load<AudioClip>("Audio/Rebel Audio/Default");
            RebelAudio.Stop();
            RebelAudio.PlayOneShot(RebelAudio.clip, 1f);
        }
    }

    IEnumerator DeActivatePlot()
    {
        yield return new WaitForSeconds(9);
        Plot.SetActive(false);
        NarrationText.GetComponent<TextMeshProUGUI>().text = "To interact with the rebles, Use your key board" +
                                                            "\nto press the numbers indicated above thier heads";

        if (RebelAudio.clip != Resources.Load<AudioClip>("Audio/Rebel Audio/Default"))
        {
            RebelAudio.clip = Resources.Load<AudioClip>("Audio/Rebel Audio/Default");
            RebelAudio.Stop();
            RebelAudio.PlayOneShot(RebelAudio.clip, 1f);
        }
    }

    IEnumerator ActivateCume1(float time)
    {
        Cume1.transform.parent.gameObject.transform.localScale = new Vector3(0,0,0);
        yield return new WaitForSeconds(time);
        Cume1.transform.parent.gameObject.SetActive(true);
    }

    IEnumerator ActivateCume2(float time)
    {
        Cume2.transform.parent.gameObject.transform.localScale = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(time);
        Cume2.transform.parent.gameObject.SetActive(true);
    }

    IEnumerator ActivateCume3(float time)
    {
        Cume3.transform.parent.gameObject.transform.localScale = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(time);
        Cume3.transform.parent.gameObject.SetActive(true);
    }

    IEnumerator ActivateCume4(float time)
    {
        Cume4.transform.parent.gameObject.transform.localScale = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(time);
        Cume4.transform.parent.gameObject.SetActive(true);
    }

    IEnumerator ActivateCume5(float time)
    {
        Cume5.transform.parent.gameObject.transform.localScale = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(time);
        Cume5.transform.parent.gameObject.SetActive(true);
    }

    IEnumerator MissionComplet(float time)
    {
        yield return new WaitForSeconds(time);
        NarrationText.GetComponent<TextMeshProUGUI>().text = "Mission Complete! You learned how to plot continuous data!" +
                                                           "\nLesson ending in 3...2...1...";
                                                            

        if (RebelAudio.clip != Resources.Load<AudioClip>("Audio/Rebel Audio/8"))
        {
            RebelAudio.clip = Resources.Load<AudioClip>("Audio/Rebel Audio/8");
            RebelAudio.Stop();
            RebelAudio.PlayOneShot(RebelAudio.clip, 1f);
        }

        CompleteCanvas.SetActive(true);
    }

    IEnumerator ShowStart()
    {
        yield return new WaitForSeconds(2);
        NarrationText.GetComponent<TextMeshProUGUI>().text = "this Lesson is all about Continuous data, you need to interact with the robot rebels" +
                                                     "\nand listen to them sing. As they sing, you will record data on how long each song lasts in seconds!" +
                                                     "\nbe sure to listen closely! you will be asked a question about the song once it completes." +
                                                     "\n once you answer the question, the data on the song's length will be added to a table." +
                                                     "\nyour goal is to complete the chart and learn how to plot continuous data!";


        if (RebelAudio.clip != Resources.Load<AudioClip>("Audio/Rebel Audio/9"))
        {
            RebelAudio.clip = Resources.Load<AudioClip>("Audio/Rebel Audio/9");
            RebelAudio.Stop();
            RebelAudio.PlayOneShot(RebelAudio.clip, 1f);
        }

        StartCoroutine(ShowDefault());
    }
}
