using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTriggers : MonoBehaviour
{
    public AudioSource PinkAudio;
    public GameObject PinkText;
    public GameObject GiantsT1,Spawn1, Spawn2, Spawn3, Spawn4, Spawn5, Spawn6;
    public GameObject LabGiants,LabGiant1, LabGiant2, LabGiant3;
    public GameObject AllyGiants, AllyGiant1, AllyGiant2, AllyGiant3;
    public GameObject Arrow1;
    public string Phase = "1";
    void Start()
    {
        PinkText = GameObject.Find("Pink Text");
        Arrow1 = GameObject.Find("Arrow 1");
        Arrow1.SetActive(false);
        PinkAudio = gameObject.AddComponent<UnityEngine.AudioSource>();
        PinkAudio.playOnAwake = false;
        PinkAudio.volume = 1;

        GiantsT1 = GameObject.Find("Giants T1");
        Spawn1 = GiantsT1.transform.GetChild(0).gameObject;
        Spawn2 = GiantsT1.transform.GetChild(1).gameObject;
        Spawn3 = GiantsT1.transform.GetChild(2).gameObject;
        Spawn4 = GiantsT1.transform.GetChild(3).gameObject;
        Spawn5 = GiantsT1.transform.GetChild(4).gameObject;
        Spawn6 = GiantsT1.transform.GetChild(5).gameObject;

        Spawn1.SetActive(false);
        Spawn2.SetActive(false);
        Spawn3.SetActive(false);
        Spawn4.SetActive(false);
        Spawn5.SetActive(false);
        Spawn6.SetActive(false);

        LabGiants = GameObject.Find("Lab Giants");
        LabGiant1 = LabGiants.transform.GetChild(0).gameObject;
        LabGiant2 = LabGiants.transform.GetChild(1).gameObject;
        LabGiant3 = LabGiants.transform.GetChild(2).gameObject;

        AllyGiants = GameObject.Find("Ally Giants");
        AllyGiant1 = AllyGiants.transform.GetChild(0).gameObject;
        AllyGiant2 = AllyGiants.transform.GetChild(1).gameObject;
        AllyGiant3 = AllyGiants.transform.GetChild(2).gameObject;

        if (Phase == "1")
        {
            PinkAudio.clip = Resources.Load<AudioClip>("Audio/Pink Robot Audio/pink robot -01");
            PinkText.GetComponent<Text>().text = "Press the A, S ,W ,D Keys on your keyboard to move";
            PinkAudio.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown("a") || Input.GetKeyDown("s") || Input.GetKeyDown("w") || Input.GetKeyDown("d")) && Phase == "1" && !PinkAudio.isPlaying)
        {
                PinkText.GetComponent<Text>().text = "Nice!";
                PinkAudio.clip = Resources.Load<AudioClip>("Audio/Pink Robot Audio/pink robot -02");
                PinkAudio.Play();
                Phase = "2";
                StartCoroutine("Phase3");
        }

        if (Phase == "4")
        {
            Phase = "5";
            Spawn1 = Instantiate(Resources.Load<GameObject>("Prefabs/Giant Green"), new Vector3 (0,4,0), Quaternion.identity);
            Spawn1.transform.position = new Vector3(5, 4, 25);
            PinkText.GetComponent<Text>().text = "Watch out! Shoot down the giant!";
            PinkAudio.clip = Resources.Load<AudioClip>("Audio/Pink Robot Audio/pink robot -03");
            PinkAudio.Play();
            StartCoroutine("Phase6");
        }

        if (Phase == "7")
        {
            Phase = "8";
            PinkText.GetComponent<Text>().text = "Click your left mouse button to shoot and move your mouse to aim";
            PinkAudio.clip = Resources.Load<AudioClip>("Audio/Pink Robot Audio/pink robot -04");
            PinkAudio.Play();
            StartCoroutine("Phase9");
        }

        if (Input.GetMouseButtonDown(0) && Phase == "9" && !PinkAudio.isPlaying)
        {
            Phase = "10";
            PinkText.GetComponent<Text>().text = "Keep Shooting!";
            PinkAudio.clip = Resources.Load<AudioClip>("Audio/Pink Robot Audio/pink robot -05");
            PinkAudio.Play();
        }

        if (Spawn1.GetComponent<Giant>().KnockedOut && Phase == "10" && !PinkAudio.isPlaying)
        {
            Phase = "11";
            PinkText.GetComponent<Text>().text = "You knocked one out!";
            PinkAudio.clip = Resources.Load<AudioClip>("Audio/Pink Robot Audio/pink robot -06");
            PinkAudio.Play();
            StartCoroutine("Phase12");
        }


        if (Spawn1.GetComponent<Giant>().KnockedOut && Phase == "12" && !PinkAudio.isPlaying)
        {
            Phase = "13";
            PinkText.GetComponent<Text>().text = "That Giant took 100 hits to get knocked out!";
            PinkAudio.clip = Resources.Load<AudioClip>("Audio/Pink Robot Audio/pink robot -07");
            PinkAudio.Play();
            StartCoroutine("Phase14");
        }

        if (Phase == "14" && !PinkAudio.isPlaying)
        {
            Phase = "15";
            PinkText.GetComponent<Text>().text = "Press the U Key on your key board to summon a turret";
            PinkAudio.clip = Resources.Load<AudioClip>("Audio/Pink Robot Audio/pink robot -08");
            PinkAudio.Play();
            Spawn2 = Instantiate(Resources.Load<GameObject>("Prefabs/Giant Yellow"), new Vector3(0, 4, 0), Quaternion.identity);
            GetComponent<BodyGlow>().Color = "Yellow";
            GetComponent<SummonMech>().ColorType = "Yellow";
            transform.GetChild(0).transform.GetChild(3).GetComponent<Renderer>().material = GetComponent<BodyGlow>().YellowMat; // EYE
            transform.GetChild(0).transform.GetChild(8).GetComponent<Renderer>().material = GetComponent<BodyGlow>().YellowMat; // SHIRT
            GetComponent<LinePlots>().LinePlotShrink = true;
        }

        if (Input.GetKeyDown("u") && Phase == "15")
        {
            Phase = "16";
            StartCoroutine("TurretAudio");
        }


        if (Spawn2.GetComponent<Giant>().KnockedOut && Phase == "16" && !PinkAudio.isPlaying)
        {
            Phase = "17";
            PinkText.GetComponent<Text>().text = "That Giant took 125 hits to get knocked out!";
            PinkAudio.clip = Resources.Load<AudioClip>("Audio/Pink Robot Audio/pink robot -10");
            PinkAudio.Play();
            StartCoroutine("Phase18");
        }

        if (Phase == "18" && !PinkAudio.isPlaying)
        {
            Phase = "19";
            PinkText.GetComponent<Text>().text = "You can upgrade your turrets by collecting data and building an AI chip \n press U at any time to summon a turret";
            PinkAudio.clip = Resources.Load<AudioClip>("Audio/Pink Robot Audio/pink robot -11");
            PinkAudio.Play();
            Spawn3 = Instantiate(Resources.Load<GameObject>("Prefabs/Giant Red"), new Vector3(0, 4, 0), Quaternion.identity);
            GetComponent<BodyGlow>().Color = "Red";
            GetComponent<SummonMech>().ColorType = "Red";
            transform.GetChild(0).transform.GetChild(3).GetComponent<Renderer>().material = GetComponent<BodyGlow>().RedMat; // EYE
            transform.GetChild(0).transform.GetChild(8).GetComponent<Renderer>().material = GetComponent<BodyGlow>().RedMat; // SHIRT
            GetComponent<LinePlots>().LinePlotShrink = true;
            StartCoroutine("Phase20");
        }

        if (Phase == "20" && !PinkAudio.isPlaying)
        {
            Phase = "21";
            PinkText.GetComponent<Text>().text = "You can upgrade your turrets by collecting data and building an AI chip \n press U at any time to summon a turret";
            PinkAudio.clip = Resources.Load<AudioClip>("Audio/Pink Robot Audio/pink robot -12");
            PinkAudio.Play();
            StartCoroutine("Phase22");
        }

        if (Phase == "22" && !PinkAudio.isPlaying)
        {
            Phase = "23";
            PinkText.GetComponent<Text>().text = "You can upgrade your turrets by collecting data and building an AI chip \n press U at any time to summon a turret";
            PinkAudio.clip = Resources.Load<AudioClip>("Audio/Pink Robot Audio/pink robot -13");
            PinkAudio.Play();
        }

        if (Spawn3.GetComponent<Giant>().KnockedOut && Phase == "23" && !PinkAudio.isPlaying)
        {
            Phase = "24";
            PinkText.GetComponent<Text>().text = "Yes you got another one! that one took 150 hits ";
            PinkAudio.clip = Resources.Load<AudioClip>("Audio/Pink Robot Audio/pink robot -14");
            PinkAudio.Play();
            StartCoroutine("Phase25");
            Spawn4 = Instantiate(Resources.Load<GameObject>("Prefabs/Giant Green"), new Vector3(0, 4, 0), Quaternion.identity);
        }

        if (Phase == "25" && !PinkAudio.isPlaying)
        {
            Phase = "26";
            PinkText.GetComponent<Text>().text = "Your Body Color changes depending on which type of giant you shoot! \n press U at any time to summon a turret";
            PinkAudio.clip = Resources.Load<AudioClip>("Audio/Pink Robot Audio/pink robot -15");
            PinkAudio.Play();
        }

        if (Input.GetMouseButton(0) && Phase == "26" && !PinkAudio.isPlaying)
        {
            Phase = "27";
            PinkText.GetComponent<Text>().text = "Keep Shooting! \n press U at any time to summon a turret";
            PinkAudio.clip = Resources.Load<AudioClip>("Audio/Pink Robot Audio/pink robot -16");
        }

        if (Spawn4.GetComponent<Giant>().KnockedOut && Phase == "27" && !PinkAudio.isPlaying)
        {
            Phase = "28";
            PinkText.GetComponent<Text>().text = "Keep Up the good work, you got another one!";
            PinkAudio.clip = Resources.Load<AudioClip>("Audio/Pink Robot Audio/pink robot -17");
            PinkAudio.Play();
            Spawn5 = Instantiate(Resources.Load<GameObject>("Prefabs/Giant Red"), new Vector3(0, 4, 0), Quaternion.identity);
        }

        if (Spawn5.GetComponent<Giant>().KnockedOut && Phase == "28" && !PinkAudio.isPlaying)
        {
            Phase = "29";
            PinkText.GetComponent<Text>().text = "You took another one down there is just one left!";
            PinkAudio.clip = Resources.Load<AudioClip>("Audio/Pink Robot Audio/pink robot -18");
            PinkAudio.Play();
            Spawn6 = Instantiate(Resources.Load<GameObject>("Prefabs/Giant Green"), new Vector3(0, 4, 0), Quaternion.identity);
        }

        if (Spawn6.GetComponent<Giant>().KnockedOut && Phase == "29" && !PinkAudio.isPlaying)
        {
            Phase = "30";
            PinkText.GetComponent<Text>().text = "Now we have all the data and it's time to average it";
            PinkAudio.clip = Resources.Load<AudioClip>("Audio/Pink Robot Audio/pink robot -19");
            PinkAudio.Play();
            StartCoroutine("Phase31");
        }

        
        if (Phase == "31" && !PinkAudio.isPlaying)
        {
            Phase = "32";
            PinkText.GetComponent<Text>().text = "Hold Down your left mouse buttont to charge up your ablity, then release to shoot a powerfull blast";
            PinkAudio.clip = Resources.Load<AudioClip>("Audio/Pink Robot Audio/pink robot -20");
            PinkAudio.Play();
            //StartCoroutine("Phase33");
        }

       if (GetComponent<Shooting>().ChargeEffect.transform.GetChild(0).transform.localScale.x > .5f && Input.GetMouseButton(0) && Phase == "32" && !PinkAudio.isPlaying)
        {
            Phase = "34";
            PinkText.GetComponent<Text>().text = "shoot a charged blast at all the x's to mark them. keep going untill every x is marked";
            PinkAudio.clip = Resources.Load<AudioClip>("Audio/Pink Robot Audio/pink robot -21");
            PinkAudio.Play();
        }

        if (GetComponent<Shooting>().CurrentMarkedObject >= 1 && Phase == "34" && !PinkAudio.isPlaying)
        {
            Phase = "35";
            PinkText.GetComponent<Text>().text = "At the top left hand corner of your screen, you can see how many items you currently have marked. Keep shooting charged blast untill every X is Marked";
            PinkAudio.clip = Resources.Load<AudioClip>("Audio/Pink Robot Audio/pink robot -22");
            PinkAudio.Play();
        }

        if (GetComponent<Shooting>().CurrentMarkedObject == 6 && Phase == "35" && !PinkAudio.isPlaying)
        {
            Phase = "36";
            PinkText.GetComponent<Text>().text = "Now that all the items are Makred, press the T key on your keyboard to merge them together";
            PinkAudio.clip = Resources.Load<AudioClip>("Audio/Pink Robot Audio/pink robot -23");
            PinkAudio.Play();
        }

        if (Input.GetKeyDown("t") && GetComponent<Shooting>().CurrentMarkedObject == 6 && Phase == "36" )
        {
            Phase = "37";
            StartCoroutine("DivideSwordInstructions");
        }

        if (GetComponent<Shooting>().CurrentMarkedObject == 0 && Phase == "37" && !PinkAudio.isPlaying)
        {
            Phase = "38";
            StartCoroutine("DivideSwordSuccess");
        }

        if (AllyGiant1.GetComponent<Giant>().KnockedOut && AllyGiant2.GetComponent<Giant>().KnockedOut && AllyGiant3.GetComponent<Giant>().KnockedOut)
        {
            Phase = "39";
            PinkText.GetComponent<Text>().text = "You got Another 1!";
            PinkAudio.clip = Resources.Load<AudioClip>("Audio/Pink Robot Audio/pink robot -26");
            PinkAudio.Play();

            GetComponent<LinePlots>().LinePlot = GetComponent<LinePlots>().LinePlot3;
            GetComponent<LinePlots>().SetUpLinePlot(GetComponent<LinePlots>().LinePlot3);
        }




    }

        IEnumerator Phase3()
    {
        yield return new WaitForSeconds(9);
        Phase = "4";
    }

    IEnumerator Phase6()
    {
        yield return new WaitForSeconds(12);
        Phase = "7";
    }

    IEnumerator Phase9()
    {
        yield return new WaitForSeconds(5);
        Phase = "9";
    }

    IEnumerator Phase12()
    {
        yield return new WaitForSeconds(5);
        Phase = "12";
    }

    IEnumerator Phase14()
    {
        yield return new WaitForSeconds(3);
        Phase = "14";
    }

    IEnumerator TurretAudio()
    {
        yield return new WaitForSeconds(3);
        PinkText.GetComponent<Text>().text = "Turrets Deactivate once they run out of Ammo! Shoot down the Giant";
        PinkAudio.clip = Resources.Load<AudioClip>("Audio/Pink Robot Audio/pink robot -09");
        PinkAudio.Play();
    }


    IEnumerator Phase18()
    {
        yield return new WaitForSeconds(6);
        Phase = "18";
    }

    IEnumerator Phase20()
    {
        yield return new WaitForSeconds(5);
        Phase = "20";
    }

    IEnumerator Phase22()
    {
        yield return new WaitForSeconds(13);
        Phase = "22";
    }

    IEnumerator Phase25()
    {
        yield return new WaitForSeconds(4);
        Phase = "25";
    }

    IEnumerator Phase31()
    {
        yield return new WaitForSeconds(4);
        Phase = "31";
    }

    IEnumerator Phase33()
    {
        yield return new WaitForSeconds(7);
        Phase = "33";
    }

    IEnumerator DivideSwordInstructions()
    {
        yield return new WaitForSeconds(2.5f);
        PinkText.GetComponent<Text>().text = "Walk up close to the Ai chip and Press the Y key on your keyboard to divide it by the total number of objects Marked";
        PinkAudio.clip = Resources.Load<AudioClip>("Audio/Pink Robot Audio/pink robot -24");
        PinkAudio.Play();
    }

    IEnumerator DivideSwordSuccess()
    {
        yield return new WaitForSeconds(2.5f);
        PinkText.GetComponent<Text>().text = "Your turret's Ammo has been upgraded. you can summon a turret at any time by pressing the U key on you key board";
        PinkAudio.clip = Resources.Load<AudioClip>("Audio/Pink Robot Audio/pink robot -25");
        PinkAudio.Play();
        Arrow1.SetActive(true);

        GetComponent<LinePlots>().LinePlot = GetComponent<LinePlots>().LinePlot2;
        GetComponent<LinePlots>().SetUpLinePlot(GetComponent<LinePlots>().LinePlot2);
    }

}
