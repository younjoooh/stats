using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class PlaceTraps : MonoBehaviour
{
    GameObject Trap;
    RaycastHit hit;
    Ray ray;
    public int CaturedFireSpirits;
    public int CaturedIceSpirits;
    public int ParasitesKnockedOut = 0;

    public GameObject TrapText;

    public GameObject IceLostSpiritsMid;
    public GameObject IceLostSpiritsSmall;
    public GameObject IceLostSpiritsBig;
    public GameObject IceLostSpiritsMixed;

    public GameObject FireLostSpiritsMid;
    public GameObject FireLostSpiritsSmall;
    public GameObject FireLostSpiritsBig;
    public GameObject FireLostSpiritsMixed;

    public GameObject IceBox;
    public GameObject FireBox;

    public AudioSource TrapAudio;
    public string Phase = "0";

    public GameObject IceParasiteGroup;
    public GameObject FireParasiteGroup;

    public GameObject IceLostSpirit;
    public GameObject FireLostSpirit;

    public GameObject EndGameCanvas;
    public GameObject EndGameCanvasTitle;

    public GameObject[] IceBackIcon = new GameObject[16];
    public GameObject[] IceFilledIcon = new GameObject[16];

    public GameObject[] FireBackIcon = new GameObject[16];
    public GameObject[] FireFilledIcon = new GameObject[16];

    public GameObject IceCanvas;
    public GameObject FireCanvas;

    public GameObject IceUpper;
    public GameObject IceLower;

    public GameObject FireUpper;
    public GameObject FireLower;

    public bool ShowStats;
    public bool LessonCompleted;
    public string Set = "1";

    public string ElementType1 =  "Ice";
    public string ElementType2 = "Fire";

    public string ElementVerb1 = "Freeze";
    public string ElementVerb2 = "Burn";

    public string WaveType = "Ice";

    void Start()
    {
        WaveType = ElementType1;
        IceCanvas.SetActive(true);
        FireCanvas.SetActive(false);
        WaveType = ElementType1;

        for (int i = 0; i <= 15; i++)
        {
            IceFilledIcon[i].transform.localScale = new Vector3(0,0,0);
            FireFilledIcon[i].transform.localScale = new Vector3(0, 0, 0);
        }

        CaturedFireSpirits = 0;
        CaturedIceSpirits = 0;

        TrapAudio = gameObject.AddComponent<UnityEngine.AudioSource>();
        TrapAudio.clip = Resources.Load<AudioClip>("Audio/Trap Audio" + Set + "/1");
        TrapAudio.spatialBlend = 0;
        TrapAudio.volume = 1f;
        TrapAudio.playOnAwake = false;

        StartCoroutine(StartIceMid());
    }



    void Update()
    {

        if (WaveType == ElementType1 && CaturedIceSpirits > 0)
        {
            IceFilledIcon[CaturedIceSpirits - 1].transform.localScale = Vector3.MoveTowards(IceFilledIcon[CaturedIceSpirits - 1].transform.localScale, new Vector3(1, 1, 1), 4 * Time.deltaTime);
            IceBackIcon[CaturedIceSpirits - 1].transform.localScale = Vector3.MoveTowards(IceBackIcon[CaturedIceSpirits - 1].transform.localScale, new Vector3(0, 0, 0), 4 * Time.deltaTime);
        }

        if (WaveType == ElementType2 && CaturedFireSpirits > 0)
        {
            FireFilledIcon[CaturedFireSpirits - 1].transform.localScale = Vector3.MoveTowards(FireFilledIcon[CaturedFireSpirits - 1].transform.localScale, new Vector3(1, 1, 1), 4 * Time.deltaTime);
            FireBackIcon[CaturedFireSpirits - 1].transform.localScale = Vector3.MoveTowards(FireBackIcon[CaturedFireSpirits - 1].transform.localScale, new Vector3(0, 0, 0), 4 * Time.deltaTime);
        }

        Debug.DrawRay(transform.position, Camera.main.transform.forward * 10000, Color.green);
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.name == "Lost Spirit" && Input.GetMouseButtonDown(0))
            {
                if (!hit.transform.gameObject.GetComponent<LostSpirit>().Captured)
                {
                    if (hit.transform.gameObject.transform.localScale == new Vector3(2, 2, 2))
                    {
                        Trap = Instantiate(Resources.Load<GameObject>("Prefabs/Trap Big"), hit.transform.gameObject.transform.position - new Vector3(0, 1, 0), Quaternion.identity);
                    }

                    if (hit.transform.gameObject.transform.localScale == new Vector3(1, 1, 1))
                    {
                        Trap = Instantiate(Resources.Load<GameObject>("Prefabs/Trap Mid"), hit.transform.gameObject.transform.position * -2 - new Vector3(0, 1, 0), Quaternion.identity);
                    }

                    if (hit.transform.gameObject.transform.localScale == new Vector3(.5f, .5f, .5f))
                    {
                        Trap = Instantiate(Resources.Load<GameObject>("Prefabs/Trap Small"), hit.transform.gameObject.transform.position * -2 - new Vector3(0, 1, 0), Quaternion.identity);
                    }
                    Trap.transform.position = new Vector3(hit.transform.gameObject.transform.position.x, -0.999f, hit.transform.gameObject.transform.position.z);

                    StartCoroutine("DestroyTraps", Trap);
                }
            }
            if(hit.transform.gameObject.name != "Lost Spirit")
            {
                Debug.Log(hit.transform.gameObject.name);
            }
        }

        if (CaturedIceSpirits == 1 && Phase == "0")
            IceSmall();

        if (CaturedIceSpirits == 6 && Phase == "2")
            IceBig();

        if (CaturedIceSpirits == 9 && Phase == "3")
            IceMixed();

        if (CaturedIceSpirits == 16 && Phase == "4")
            IceCompleted();

        if (CaturedIceSpirits == 16 && Phase == "5")
            SummonIceBox();
        /*
        if (Phase == "5" && IceBox.transform.position == new Vector3(.5f, 1, -1))
        {
            MoveToIceBox();
            Phase = "6";
            TrapText.GetComponent<Text>().text = " ";
        }
        */

        if (Set == "1")
        {
            if (Phase == "6" && ParasitesKnockedOut == 22)
            {
                StartCoroutine(StartFireMid());
                Phase = "7";
                //FireMid();
            }
        }
        if (Set == "2")
        {
            if (Phase == "6" && ParasitesKnockedOut == 22)
            {
                StartCoroutine(StartFireMid());
                Phase = "7";
                //FireMid();
            }
        }




        if (CaturedFireSpirits == 1 && Phase == "7")
            FireSmall();

        if (CaturedFireSpirits == 6 && Phase == "9")
            FireBig();

        if (CaturedFireSpirits == 9 && Phase == "10")
            FireMixed();

        if (CaturedFireSpirits == 16 && Phase == "11")
            FireCompleted();

        if (CaturedFireSpirits == 16 && Phase == "12")
            SummonFireBox();

        /*
        if (Phase == "12" && FireBox.transform.position == new Vector3(.5f, 1, -1) )
        {
            MoveToFireBox();
            Phase = "13";
            TrapText.GetComponent<Text>().text = " ";
        }*/

        if (Set == "1")
        {
            if (Phase == "13" && ParasitesKnockedOut == 44)
            {
                Phase = "14";
                GetComponent<CombineSouls>().CombineAnimation = true;
                StartCoroutine(StartLessonComplete());
                //LessonComplete();
            }
        }

        if (Set == "2")
        {
            if (Phase == "13" && ParasitesKnockedOut == 44)
            {
                Phase = "14";
                GetComponent<CombineSouls>().CombineAnimation = true;
                StartCoroutine(StartLessonComplete());
                //LessonComplete();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !EndGameCanvas.activeSelf)
        {
            EndGameCanvas.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            EndGameCanvasTitle.GetComponent<TextMeshProUGUI>().text = "Pause Menu";
            ShowStats = true;
        }


        if (!ShowStats && Input.GetKeyDown(KeyCode.Escape) && EndGameCanvas.activeSelf)
        {
            ShowStats = false;
            EndGameCanvas.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void LateUpdate()
    {
        
    }

    void IceMid()
    {
        TrapText.GetComponent<Text>().text = "A Lost Spirit is a special type of monster that can be captured and later become your ally." +
                                           "\nBut first you have to Collected data on the Lost Spirit's weight then add it to a box and whisker plot." +
                                         "\n\nTo capture a lost spirt, aim at the Lost spirit then click the left mouse button." +
                                           "\nThis will place a trap bellow the lost spirt and collect data on it's weight.";

            TrapAudio.clip = Resources.Load<AudioClip>("Audio/Trap Audio" + Set + "/1");
            TrapAudio.Stop();
            TrapAudio.PlayOneShot(TrapAudio.clip, 1f);

    }

    void IceSmall()
    {
        Phase = "1";
        TrapText.GetComponent<Text>().text = "You Captured your first Lost Spirit! Later on, You'll be able to us it's"+ " " + ElementType1 +" "+ "abilities to " + ElementVerb1 + " enemies in combat!" +
                                           "\nThat lost spirit's Weighst was 32 pounds and that data has been saved on the top of your screen." +
                                            "\nonce you've capture a total of 16 Lost Spirits you'll be able to use the data to create an allied lost spirit.";

        if (TrapAudio.clip != Resources.Load<AudioClip>("Audio/Trap Audio" + Set + "/2"))
        {
            TrapAudio.clip = Resources.Load<AudioClip>("Audio/Trap Audio" + Set + "/2");
            TrapAudio.Stop();
            TrapAudio.PlayOneShot(TrapAudio.clip, 1f);
        }
        StartCoroutine(SummonIceSmall());
    }

    void IceBig()
    {
        Phase = "3";
        StartCoroutine(SummonIceBig());
    }

    void IceMixed()
    {
        Phase = "4";
        StartCoroutine(SummonIceMixed());
    }

    void IceCompleted()
    {
        Phase = "5";
        TrapText.GetComponent<Text>().text = "You've Captured all 16"+ " " + ElementType1 +" "+ "Lost Spirits, Now its time to use the data on their weights to create a Box and whisker Plot!" +
                                           "\n Move towards the box and whisker Plot to interact with it, when its completed you'll be able to summon an"+ " " + ElementType1 +" "+ "Lost Spirit as your ally!";

        if (TrapAudio.clip != Resources.Load<AudioClip>("Audio/Trap Audio" + Set + "/6"))
        {
            TrapAudio.clip = Resources.Load<AudioClip>("Audio/Trap Audio" + Set + "/6");
            TrapAudio.Stop();
            TrapAudio.PlayOneShot(TrapAudio.clip, 1f);
        }

        StartCoroutine(StartMoveToIceBox());
    }

    void SummonIceBox()
    {
    }

    void MoveToIceBox()
    {
        Phase = "6";
        TrapText.GetComponent<Text>().text = " ";

        IceBox.SetActive(true);
        IceBox.transform.position = new Vector3(.5f, 1, -1);

        transform.position = new Vector3(-3f, 0, -9);
        GetComponent<CombineSouls>().CombineAnimation = true;
        GameObject Blast = Instantiate(Resources.Load<GameObject>("Effects/Bar Graph Effect"), transform.position, Quaternion.identity);
        Blast.transform.localScale = new Vector3(1, 1, 1);

        IceUpper.GetComponent<MedianGate>().WaveType = ElementType1;
        IceLower.GetComponent<MedianGate>().WaveType = ElementType1;
    }



    void FireMid()
    {
        IceParasiteGroup.SetActive(false);
        FireParasiteGroup.SetActive(false);

        IceLostSpirit.SetActive(false);
        FireLostSpiritsMid.SetActive(true);
        IceBox.SetActive(false);
        Phase = "7";

        transform.position = new Vector3(-3, 0, -9);
        GetComponent<CombineSouls>().CombineAnimation = true;
        GameObject Blast = Instantiate(Resources.Load<GameObject>("Effects/Bar Graph Effect"), transform.position, Quaternion.identity);
        Blast.transform.localScale = new Vector3(1, 1, 1);

        TrapText.GetComponent<Text>().text = "You Defeated all the parasites With the help of your"+ " " + ElementType1 +" "+ "Type lost Spirit! Now let's" +
                                           "\nCollect some Data from" + " " + ElementType2 + " " + "type Lost Spirits. We can now captue" + " " + ElementType2 + " " + "types and collect data" +
                                           "\npoints to put on a box and whisker plot just like before!";

        if (TrapAudio.clip != Resources.Load<AudioClip>("Audio/Trap Audio" + Set + "/7"))
        {
            TrapAudio.clip = Resources.Load<AudioClip>("Audio/Trap Audio" + Set + "/7");
            TrapAudio.Stop();
            TrapAudio.PlayOneShot(TrapAudio.clip, 1f);
        }
        IceCanvas.SetActive(false);
        FireCanvas.SetActive(true);

        if (Set == "1")
        {
            StartCoroutine(ContinueFireMid(13));
        }

        if (Set == "2")
        {
            StartCoroutine(ContinueFireMid(12));
        }
    }

    void FireSmall()
    {
        Phase = "8";
        TrapText.GetComponent<Text>().text = "You Captured your first" + " " + ElementType2 + " " + "Type Lost Spirit! Later on, You'll be able to us it's" + " " + ElementType2 + " " + "abilities to " + ElementVerb2+
                                           "\nenemies in combat!That lost spirit's Weight was 36 pounds and that data has been saved on the top of your screen." +
                                            "\nonce you've capture a total of 16 Lost Spirits you'll be able to use the data to create an allied lost spirit.";

        if (TrapAudio.clip != Resources.Load<AudioClip>("Audio/Trap Audio" + Set + "/9"))
        {
            TrapAudio.clip = Resources.Load<AudioClip>("Audio/Trap Audio" + Set + "/9");
            TrapAudio.Stop();
            TrapAudio.PlayOneShot(TrapAudio.clip, 1f);
        }
        StartCoroutine(SummonFireSmall());
    }

    void FireBig()
    {
        Phase = "10";
        StartCoroutine(SummonFireBig());
    }

    void FireMixed()
    {
        Phase = "11";
        StartCoroutine(SummonFireMixed());
    }

    void FireCompleted()
    {
        Phase = "12";
        TrapText.GetComponent<Text>().text = "You've Captured all 16" + " " + ElementType2 + " " + "type Lost Spirits, Now its time to use the data on their weights to create a Box and whisker Plot!" +
                                           "\n Move towards the box and whisker Plot to interact with it. when its completed, you'll be able to summon a" + " " + ElementType2 + " " + "type Lost Spirit as your ally!";

        if (TrapAudio.clip != Resources.Load<AudioClip>("Audio/Trap Audio" + Set + "/11"))
        {
            TrapAudio.clip = Resources.Load<AudioClip>("Audio/Trap Audio" + Set + "/11");
            TrapAudio.Stop();
            TrapAudio.PlayOneShot(TrapAudio.clip, 1f);
        }

        StartCoroutine(StartMoveToFireBox());
    }

    void SummonFireBox()
    {

    }

    void MoveToFireBox()
    {
        Phase = "13";
        TrapText.GetComponent<Text>().text = " ";

        FireBox.SetActive(true);
        FireBox.transform.position = new Vector3(.5f, 1, -1);

        transform.position = new Vector3(-3f, 0, -9);
        GetComponent<CombineSouls>().CombineAnimation = true;
        GameObject Blast = Instantiate(Resources.Load<GameObject>("Effects/Bar Graph Effect"), transform.position, Quaternion.identity);
        Blast.transform.localScale = new Vector3(1, 1, 1);

        FireUpper.GetComponent<MedianGate>().WaveType = ElementType2;
        FireLower.GetComponent<MedianGate>().WaveType = ElementType2;
    }

    void LessonComplete()
    {
        Phase = "14";
        FireParasiteGroup.SetActive(false);
        //transform.position = new Vector3(-3, 0, -9);

        GameObject Blast = Instantiate(Resources.Load<GameObject>("Effects/Bar Graph Effect"), transform.position, Quaternion.identity);
        Blast.transform.localScale = new Vector3(1, 1, 1);

        TrapText.GetComponent<Text>().text = "You Did it, you learned How to Build a Box and whisker plot using the data you colected from the lost spirits weights!" +
                                           "\nMission Complete!";

        if (TrapAudio.clip != Resources.Load<AudioClip>("Audio/Trap Audio" + Set + "/12"))
        {
            TrapAudio.clip = Resources.Load<AudioClip>("Audio/Trap Audio" + Set + "/12");
            TrapAudio.Stop();
            TrapAudio.PlayOneShot(TrapAudio.clip, 1f);
        }

        StartCoroutine(EndGame1());

    }

    IEnumerator DestroyTraps(GameObject CurrentTrap)
    { 
        yield return new WaitForSeconds(7);
        if (CurrentTrap != null)
        {
            CurrentTrap.GetComponent<TrapCollision>().Shrink = true;
            CurrentTrap.GetComponent<TrapCollision>().Grow = false;
        }
    }

    IEnumerator StartIceMid()
    {
        yield return new WaitForSeconds(1f);
        IceMid();
    }

    IEnumerator SummonIceSmall()
    {
        yield return new WaitForSeconds(16f);
        Phase = "2";
        IceLostSpiritsSmall.SetActive(true);

        TrapText.GetComponent<Text>().text = "A group of Lost Spirist has spawned! as you may have noticed, lost spirits come in many different sizes." +
                                          "\nIn order to collect Data on their weights, Capture all the Monsters in this group of small Lost Spirits." +
                                         "\n\nTo capture a lost spirit aim at the Lost spirit then click the left your mouse button.";

        if (TrapAudio.clip != Resources.Load<AudioClip>("Audio/Trap Audio" + Set + "/3"))
        {
            TrapAudio.clip = Resources.Load<AudioClip>("Audio/Trap Audio" + Set + "/3");
            TrapAudio.Stop();
            TrapAudio.PlayOneShot(TrapAudio.clip, 1f);
        }
    }

    IEnumerator SummonIceBig()
    {
        yield return new WaitForSeconds(6.5f);
        IceLostSpiritsBig.SetActive(true);
        TrapText.GetComponent<Text>().text = "Good Job! now take on this group of Large, Lost Spirits!";

        if (TrapAudio.clip != Resources.Load<AudioClip>("Audio/Trap Audio" + Set + "/4"))
        {
            TrapAudio.clip = Resources.Load<AudioClip>("Audio/Trap Audio" + Set + "/4");
            TrapAudio.Stop();
            TrapAudio.PlayOneShot(TrapAudio.clip, 1f);
        }
    }

    IEnumerator SummonIceMixed()
    {
        yield return new WaitForSeconds(6.5f);
        IceLostSpiritsMixed.SetActive(true);
        TrapText.GetComponent<Text>().text = "You've Captured Larged Lost Spirts, Small ones, and every thing in between!" +
                                           "\nNow Take on this group with Mixed sizes of small, medium and large!";

        if (TrapAudio.clip != Resources.Load<AudioClip>("Audio/Trap Audio" + Set + "/5"))
        {
            TrapAudio.clip = Resources.Load<AudioClip>("Audio/Trap Audio" + Set + "/5");
            TrapAudio.Stop();
            TrapAudio.PlayOneShot(TrapAudio.clip, 1f);
        }
    }

    IEnumerator StartMoveToIceBox()
    {
        yield return new WaitForSeconds(11.5f);
        MoveToIceBox();
    }






    IEnumerator StartFireMid()
    {
        Phase = "7";
        IceParasiteGroup.SetActive(false);
        FireParasiteGroup.SetActive(false);
        yield return new WaitForSeconds(2f);
        FireMid();
    }

    IEnumerator ContinueFireMid(float Time)
    {
        yield return new WaitForSeconds(Time);
        FireCanvas.SetActive(true);

        WaveType = ElementType2;

        TrapText.GetComponent<Text>().text = "In front of you, you should see A" + " " + ElementType2 + " " + "type lost spirit," +
            "\n to captured it, aim your mouse at the lost spirt, then left click";

        if (TrapAudio.clip != Resources.Load<AudioClip>("Audio/Trap Audio" + Set + "/8"))
        {
            TrapAudio.clip = Resources.Load<AudioClip>("Audio/Trap Audio" + Set + "/8");
            TrapAudio.PlayOneShot(TrapAudio.clip, 1f);
        }
    }

    IEnumerator SummonFireSmall()
    {
        yield return new WaitForSeconds(16f);
        Phase = "9";
        FireLostSpiritsSmall.SetActive(true);

        TrapText.GetComponent<Text>().text = "A group of Lost Spirist has spawned! this is a small" + " " + ElementType2 + " " + "type group.  Capture " +
                                          "\nall the Monsters in this group of small Lost Spirits to collect Data on their weights." +
                                         "\n\nTo capture a lost spirit aim at the Lost spirit then click the left mouse button.";

        if (TrapAudio.clip != Resources.Load<AudioClip>("Audio/Trap Audio" + Set + "/10"))
        {
            TrapAudio.clip = Resources.Load<AudioClip>("Audio/Trap Audio" + Set + "/10");
            TrapAudio.PlayOneShot(TrapAudio.clip, 1f);
        }
    }

    IEnumerator SummonFireBig()
    {
        yield return new WaitForSeconds(6.5f);
        FireLostSpiritsBig.SetActive(true);
        TrapText.GetComponent<Text>().text = "Good Job! now take on this group of Large, Lost Spirits!";

        if (TrapAudio.clip != Resources.Load<AudioClip>("Audio/Trap Audio" + Set + "/4"))
        {
            TrapAudio.clip = Resources.Load<AudioClip>("Audio/Trap Audio" + Set + "/4");
            TrapAudio.PlayOneShot(TrapAudio.clip, 1f);
        }
    }

    IEnumerator SummonFireMixed()
    {
        yield return new WaitForSeconds(6.5f);
        FireLostSpiritsMixed.SetActive(true);
        TrapText.GetComponent<Text>().text = "You've Captured Larged Lost Spirts, Small ones, and every thing in between!" +
                                           "\nNow Take on this group with Mixed sizes of small, medium and large!";

        if (TrapAudio.clip != Resources.Load<AudioClip>("Audio/Trap Audio" + Set + "/5"))
        {
            TrapAudio.clip = Resources.Load<AudioClip>("Audio/Trap Audio" + Set + "/5");
            TrapAudio.PlayOneShot(TrapAudio.clip, 1f);
        }
    }

    IEnumerator StartMoveToFireBox()
    {
        yield return new WaitForSeconds(13f);
        MoveToFireBox();
    }

    IEnumerator StartLessonComplete()
    {
        yield return new WaitForSeconds(5f);
        LessonComplete();
    }

   public  IEnumerator EndGame1()
    {
        yield return new WaitForSeconds(6f);
        EndGameCanvas.SetActive(true);
        ShowStats = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        EndGameCanvasTitle.GetComponent<TextMeshProUGUI>().text = "Mission Complete";
        LessonCompleted = true;
    }

    public IEnumerator EndGameRebel()
    {
        yield return new WaitForSeconds(0);
        EndGameCanvas.SetActive(true);
        ShowStats = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        EndGameCanvasTitle.GetComponent<TextMeshProUGUI>().text = "Mission Complete";
        LessonCompleted = true;
    }


}

// 1,3,4,5,12 are generic audios