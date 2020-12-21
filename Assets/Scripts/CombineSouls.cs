using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CombineSouls : MonoBehaviour
{
    public string Lesson = "5";
    public GameObject Player;
    public bool CombineAnimation;
    public bool DivideAttack;
    public bool Travel;
    public bool Divided;
    public bool TextEnabled;
    public bool ParasiteTravel;
    float TravelSpeed;
    float ParasiteTravelSpeed;
    public GameObject[] NewMarkedObjects = new GameObject[25];
    public GameObject[] ClonedMarkedObjects = new GameObject[25];
    Animator Anim;

    public AudioSource CombineSound;
    public GameObject CombineEffect;
    public GameObject CombineEffectClone;

    public GameObject DisappearEffect;
    public GameObject DisappearEffectClone;

    public GameObject SoulChip;
    public GameObject SoulChipClone;

    public GameObject MergeMutant;
    public GameObject MergeMutantClone;

    public AudioSource SlowMoSound;
    public AudioSource DivideSound;
    public AudioSource ShortSwellSound;
    public AudioSource LongSwellSound;
    public AudioSource MarkedSound;


    public GameObject DivideEffect;
    public GameObject DivideEffectClone;

    public GameObject MarkedCanvas;
    public GameObject MechCanvas;
    public GameObject AIChipClone;
    public GameObject AIChipCase;

    float SoulChipDistance = 4;
    int MutantCount = 0;
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        Anim = GetComponent<Animator>();

        CombineSound = gameObject.AddComponent<UnityEngine.AudioSource>();
        CombineSound.clip = Resources.Load<AudioClip>("Audio/Combine Sound");
        CombineSound.spatialBlend = 1;
        CombineSound.volume = .25f;
        CombineSound.playOnAwake = false;

        CombineEffect = Instantiate(Resources.Load<GameObject>("Effects/Combine Effect"), new Vector3(0, -5000, 0), Quaternion.identity);
        CombineEffect.SetActive(false);

        DisappearEffect = Instantiate(Resources.Load<GameObject>("Effects/Disappear Effect"), new Vector3(0, -5000, 0), Quaternion.identity);
        DisappearEffect.SetActive(false);

        SoulChip = Instantiate(Resources.Load<GameObject>("Prefabs/Soul Chip"), new Vector3(0, -5000, 0), Quaternion.identity);
        SoulChip.SetActive(false);

        MergeMutant = Instantiate(Resources.Load<GameObject>("Prefabs/Merge Mutant"), new Vector3(0, -5000, 0), Quaternion.identity);
        MergeMutant.SetActive(false);


        SlowMoSound = gameObject.AddComponent<UnityEngine.AudioSource>();
        SlowMoSound.clip = Resources.Load<AudioClip>("Audio/SlowMo Sound");
        SlowMoSound.spatialBlend = 1;
        SlowMoSound.volume = .25f;
        SlowMoSound.playOnAwake = false;

        DivideSound = gameObject.AddComponent<UnityEngine.AudioSource>();
        DivideSound.clip = Resources.Load<AudioClip>("Audio/Divide Sound");
        DivideSound.spatialBlend = 1;
        DivideSound.volume = .25f;
        DivideSound.playOnAwake = false;

        DivideEffect = Instantiate(Resources.Load<GameObject>("Effects/Divide Effect"), new Vector3(0, -5000, 0), Quaternion.identity);
        DivideEffect.SetActive(false);


        ShortSwellSound = gameObject.AddComponent<UnityEngine.AudioSource>();
        ShortSwellSound.clip = Resources.Load<AudioClip>("Audio/Short Swell Sound");
        ShortSwellSound.spatialBlend = 1;
        ShortSwellSound.volume = .25f;
        ShortSwellSound.playOnAwake = false;


        LongSwellSound = gameObject.AddComponent<UnityEngine.AudioSource>();
        LongSwellSound.clip = Resources.Load<AudioClip>("Audio/Long Swell Sound");
        LongSwellSound.spatialBlend = 1;
        LongSwellSound.volume = .25f;
        LongSwellSound.playOnAwake = false;


        MarkedSound = gameObject.AddComponent<UnityEngine.AudioSource>();
        MarkedSound.clip = Resources.Load<AudioClip>("Audio/Marked Sounds/Marked Sound " + Random.Range(1,4).ToString());
        MarkedSound.spatialBlend = 1;
        MarkedSound.volume = .25f;
        MarkedSound.playOnAwake = false;


        MarkedCanvas = Instantiate(Resources.Load<GameObject>("Prefabs/Marked Canvas"), new Vector3(0, 0, 0), Quaternion.identity);
        MarkedCanvas.name = "Marked Canvas";

        MechCanvas = Instantiate(Resources.Load<GameObject>("Prefabs/Mech Canvas"), new Vector3(0, 0, 0), Quaternion.identity);
        MechCanvas.name = "Mech Canvas";

        AIChipCase = Instantiate(Resources.Load<GameObject>("Prefabs/AI Chip Case"), new Vector3(0, 0, 0), Quaternion.identity);
        AIChipCase.name = "AI Chip Case";
        AIChipCase.SetActive(false);
    }

    void Update()
    {
        // REMOVE EXTRA CANVAS
        if (Lesson == "5")
        {
            if (MechCanvas.activeSelf)
            {
                MechCanvas.SetActive(false);
            }

            if (MarkedCanvas.activeSelf)
            {
                MarkedCanvas.SetActive(false);
            }

        }

        // MARKED CANVAS
        MarkedCanvas.transform.GetChild(2).GetComponent<Text>().text = GetComponent<Shooting>().CurrentMarkedObject.ToString();

        // CLEAR MARKED OBJECTS
        if (Input.GetKeyDown("r") )
        {

            foreach (GameObject SoulChip in GameObject.FindObjectsOfType<GameObject>())
            {
                if (SoulChip.name == "Soul Chip")
                {
                    Destroy(SoulChip);
                    DisappearEffectClone = Instantiate(DisappearEffect, SoulChip.transform.position, Quaternion.identity);
                    DisappearEffectClone.SetActive(true);
                }
            }

            foreach (GameObject AIChips in GameObject.FindObjectsOfType<GameObject>())
            {
                if (AIChips.name == "AI Chip")
                {
                    Destroy(AIChips);
                    DisappearEffectClone = Instantiate(DisappearEffect, AIChips.transform.position, Quaternion.identity);
                    DisappearEffectClone.SetActive(true);
                }
            }


            if (Player.GetComponent<Shooting>().MarkedObjects[0] != null)
            {
                for (int i = 0; i < Player.GetComponent<Shooting>().MarkedObjects.Length; i++)
                {
                    if (Player.GetComponent<Shooting>().MarkedObjects[i] != null)
                    {
                        if (Player.GetComponent<Shooting>().MarkedObjects[i].GetComponent<NumberSoul>() != null)
                        {
                            Player.GetComponent<Shooting>().MarkedObjects[i].transform.parent.gameObject.SetActive(true);
                            Player.GetComponent<Shooting>().MarkedObjects[i].GetComponent<NumberSoul>().Marked = false;
                            Player.GetComponent<Shooting>().MarkedObjects[i].GetComponent<NumberSoul>().Centered = false;
                            Destroy(Player.GetComponent<Shooting>().MarkedObjects[i].transform.GetChild(2).gameObject);
                            Player.GetComponent<Shooting>().MarkedObjects[i].GetComponent<NumberSoul>().Combined = true;
                            Player.GetComponent<Shooting>().MarkedObjects[i] = null;
                            Player.GetComponent<Shooting>().CurrentMarkedObject = 0;
                        }
                    }
                }
            }
        }

        // COMBINE ANIMATION
        if (Input.GetKeyDown("t") && !Anim.GetCurrentAnimatorStateInfo(0).IsTag("Melee Attack") && GetComponent<Health>().Recovered &&
            Player.GetComponent<Shooting>().MarkedObjects[0] != null && Player.GetComponent<Shooting>().MarkedObjects[0].GetComponent<NumberSoul>() != null 
        )
        {
            if (!Player.GetComponent<Shooting>().MarkedObjects[Player.GetComponent<Shooting>().CurrentMarkedObject -1].GetComponent<NumberSoul>().Combined)
            {

                foreach (GameObject DuplicateSouls in GameObject.FindObjectsOfType<GameObject>())
                {
                    if (DuplicateSouls.tag == "Duplicate Souls")
                    {
                        Destroy(DuplicateSouls);
                    }
                }

                foreach (GameObject AIChips in GameObject.FindObjectsOfType<GameObject>())
                {
                    if (AIChips.name == "AI Chip")
                    {
                        Destroy(AIChips);
                        DisappearEffectClone = Instantiate(DisappearEffect, AIChips.transform.position, Quaternion.identity);
                        DisappearEffectClone.SetActive(true);
                    }
                }

                if (!CombineAnimation) 
                {
                   LongSwellSound.PlayOneShot(LongSwellSound.clip, 1f);
                }

                if (!DivideAttack && !Anim.GetCurrentAnimatorStateInfo(0).IsTag("Melee Attack"))
                {
                    CombineAnimation = true;
                }

                Travel = true;
                TravelSpeed = Vector3.Distance(Player.GetComponent<Shooting>().MarkedObjects[0].transform.position, transform.position) / 1.35f;

                for (int i = 0; i < Player.GetComponent<Shooting>().MarkedObjects.Length; i++)
                {
                    if (Player.GetComponent<Shooting>().MarkedObjects[i] != null)
                    {
                        Player.GetComponent<Shooting>().MarkedObjects[i].GetComponent<NumberSoul>().Combined = true;
                        NewMarkedObjects[i] = Instantiate(Player.GetComponent<Shooting>().MarkedObjects[i].transform.parent.gameObject, Player.GetComponent<Shooting>().MarkedObjects[i].transform.parent.gameObject.transform.position, Quaternion.identity);
                        Player.GetComponent<Shooting>().MarkedObjects[i].transform.parent.gameObject.SetActive(false);
                        NewMarkedObjects[i].gameObject.name = "Duplicate Souls";
                        NewMarkedObjects[i].gameObject.tag = "Duplicate Souls";
                        NewMarkedObjects[i].gameObject.transform.GetChild(0).GetComponent<Collider>().enabled = false;
                        Destroy(NewMarkedObjects[i].gameObject.transform.GetChild(0).transform.GetChild(1).gameObject);
                    }
                }
            
            
            }
        }




        if (CombineAnimation)
        {
            Vector3 TargetDirection = transform.position - Camera.main.transform.position;
            TargetDirection.y = 0;
            transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, TargetDirection, 10 * Time.deltaTime, 0));
        }

        Anim.SetBool("Combine", CombineAnimation);

        if (Anim.GetCurrentAnimatorStateInfo(0).IsName("Combine"))
        {
            CombineAnimation = false;
            if (Anim.GetCurrentAnimatorStateInfo(0).normalizedTime <= .5f)
            {
                Vector3 TargetDirection = transform.position - Camera.main.transform.position;
                TargetDirection.y = 0;
                transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, TargetDirection, 10 * Time.deltaTime, 0));
            }
        }


        //COMBINE NUMBERS
        for (int i = 0; i < Player.GetComponent<Shooting>().MarkedObjects.Length; i++)
        {
            if (Player.GetComponent<Shooting>().MarkedObjects[i] != null)
            {
                if (Travel && Player.GetComponent<Shooting>().MarkedObjects[i].transform.position != transform.position + new Vector3(Camera.main.transform.forward.x, .1f, Camera.main.transform.forward.z) * SoulChipDistance)
                {
                    NewMarkedObjects[i].transform.position = Vector3.MoveTowards(NewMarkedObjects[i].transform.position, transform.position + new Vector3(Camera.main.transform.forward.x, .1f, Camera.main.transform.forward.z) * SoulChipDistance, TravelSpeed * Time.deltaTime);
                    NewMarkedObjects[i].transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(NewMarkedObjects[i].transform.forward, new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z), 100 * Time.deltaTime, 0));
                }
            }
        }

        if (Player.GetComponent<Shooting>().MarkedObjects[0] != null)
        {
            if (Travel && NewMarkedObjects[0].transform.position == transform.position + new Vector3(Camera.main.transform.forward.x, .1f, Camera.main.transform.forward.z) * SoulChipDistance)
            {
                Travel = false;
                CombineSound.PlayOneShot(CombineSound.clip, 1f);
                CombineEffectClone = Instantiate(CombineEffect, transform.position, Quaternion.identity);
                CombineEffectClone.SetActive(true);


                foreach (GameObject CombindedSouls in GameObject.FindObjectsOfType<GameObject>())
                {
                    if (CombindedSouls.name == "Duplicate Souls")
                    //if (CombindedSouls.name == "Soul Chip")
                    {
                        Destroy(CombindedSouls);
                        DisappearEffectClone = Instantiate(DisappearEffect, CombindedSouls.transform.position, Quaternion.identity);
                        DisappearEffectClone.SetActive(true);
                    }
                }


                // CREATE SOUL CHIP
                Player.GetComponent<CombineSouls>().Divided = false;
                SoulChipClone = Instantiate(SoulChip, NewMarkedObjects[0].transform.position, Quaternion.identity);
                SoulChipClone.name = "Soul Chip";
                SoulChipClone.SetActive(true);
                SoulChipClone.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(SoulChipClone.transform.forward, new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z), 9999 * Time.deltaTime, 0));

                int CombinedValue = 0;
                for (int i = 0; i < Player.GetComponent<Shooting>().MarkedObjects.Length; i++)
                {
                    if (Player.GetComponent<Shooting>().MarkedObjects[i] != null)
                    {
                        CombinedValue += int.Parse(Player.GetComponent<Shooting>().MarkedObjects[i].transform.GetComponent<TextMeshProUGUI>().text);
                    }
                }

                SoulChipClone.transform.GetChild(0).GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = CombinedValue.ToString();

                foreach (GameObject DuplicateSouls in GameObject.FindObjectsOfType<GameObject>())
                {
                    if (DuplicateSouls.tag == "Duplicate Souls")
                    {
                        Destroy(DuplicateSouls);
                    }
                }

            }
        }


        // GROW SOUL CHIP
        if (SoulChipClone!= null)
        {
            if (SoulChipClone.activeSelf)
            {
                SoulChipClone.transform.localScale = Vector3.MoveTowards(SoulChipClone.transform.localScale, new Vector3(1,1,1), 10 * Time.deltaTime);
                GetComponent<Shooting>().CombatMode = true;
            }
        }

        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Divide Attack 3") && (Mathf.Round(GetComponent<BarGraphs>().BarGraph1.transform.position.y) == 0 || Mathf.Round(GetComponent<BarGraphs>().BarGraph2.transform.position.y) == 0 || Mathf.Round(GetComponent<BarGraphs>().BarGraph3.transform.position.y) == 0))
        {
            AIChipCase.transform.position = transform.position + new Vector3(Camera.main.transform.forward.x * 4f, .6f, Camera.main.transform.forward.z * 4f) + new Vector3(Camera.main.transform.right.x * .5f, 0, Camera.main.transform.right.z * .5f);
            AIChipCase.transform.eulerAngles = Camera.main.transform.eulerAngles;
        }
        // DIVIDE ATTACK ANIMATION

        if (Input.GetKeyDown("y") && GetComponent<Health>().Recovered)
        {
            DivideAttack = true;
            CombineAnimation = false;
            GetComponent<LinePlots>().GreenGiants = 0;
            GetComponent<LinePlots>().YellowGiants = 0;
            GetComponent<LinePlots>().RedGiants = 0;
            GetComponent<LinePlots>().BlueGiants = 0;
            GetComponent<LinePlots>().PurpleGiants = 0;
            GetComponent<LinePlots>().LinePlotShrink = true;
            GetComponent<Movement>().DashBack = false;
            GetComponent<Movement>().Idle = false;
            GetComponent<Movement>().Jump = false;
            GetComponent<Movement>().Land = false;
            GetComponent<Movement>().Run = false;
            GetComponent<Movement>().Stop = false;
            GetComponent<Movement>().TurnLeft = false;
            GetComponent<Movement>().TurnRight = false;
            GetComponent<Movement>().Drop = false;
            GetComponent<Movement>().RunLeft = false;
            GetComponent<Movement>().RunRight = false;
            GetComponent<Movement>().RunForward = false;
            GetComponent<Movement>().RunBack = false;
            GetComponent<Movement>().RunBackLeft = false;
            GetComponent<Movement>().RunBackRight = false;
            GetComponent<Movement>().RunForwardLeft = false;
            GetComponent<Movement>().RunForwardRight = false;
            GetComponent<Movement>().DashForward = false;
            GetComponent<Movement>().DashBack = false;
            GetComponent<Movement>().DashLeft = false;
            GetComponent<Movement>().DashRight = false;
            GetComponent<Movement>().DashForwardPrep = false;
            GetComponent<Movement>().DashBackPrep = false;
            GetComponent<Movement>().DashLeftPrep = false;
            GetComponent<Movement>().DashRightPrep = false;
            DivideEffectClone = Instantiate(DivideEffect, transform.position, Quaternion.identity);
            DivideEffectClone.SetActive(true);
            DivideEffectClone.transform.position = transform.position;
            DivideEffectClone.transform.eulerAngles = transform.eulerAngles;
            DivideEffectClone.transform.parent = this.gameObject.transform;

            GetComponent<MeleeAttack>().Sword.SetActive(false);
            GetComponent<MeleeAttack>().Sword.SetActive(true);
            StartCoroutine("DestroyDivideEffect", DivideEffectClone);
            StartCoroutine("PlaySlowMoSound");
            StartCoroutine("PlayDivideSound");
            StartCoroutine("PlayShortSwellSound");
        }

        if (DivideAttack)
        {
            Vector3 TargetDirection = transform.position - Camera.main.transform.position;
            TargetDirection.y = 0;
            transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, TargetDirection, 10 * Time.deltaTime, 0));
        }

        Anim.SetBool("Divide Attack", DivideAttack);

        if (Anim.GetCurrentAnimatorStateInfo(0).IsName("Divide Attack 1"))
        {
            DivideAttack = false;
            Vector3 TargetDirection = transform.position - Camera.main.transform.position;
            TargetDirection.y = 0;
            transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, TargetDirection, 10 * Time.deltaTime, 0));
        }




        // GROW AI CHIP TEXT
        if (AIChipClone != null)
        {
            if (AIChipClone.transform.GetChild(0).GetChild(0).transform.GetComponent<TextMeshProUGUI>().transform.localScale == new Vector3(2, 2, 2) )
            {
                TextEnabled = false;
                AIChipClone = null;
            }

            if (TextEnabled)
            {
                MechCanvas.transform.GetChild(0).GetComponent<Text>().text = AIChipClone.transform.GetChild(0).GetChild(0).transform.GetComponent<TextMeshProUGUI>().text;
                AIChipClone.transform.GetChild(0).GetChild(0).transform.GetComponent<TextMeshProUGUI>().transform.localScale = Vector3.MoveTowards(AIChipClone.transform.GetChild(0).GetChild(0).transform.GetComponent<TextMeshProUGUI>().transform.localScale, new Vector3(2, 2, 2), 10 * Time.deltaTime);
                GetComponent<BarGraphs>().MoveBarGraphsDown = true;
                GetComponent<SummonMech>().Ammo = int.Parse(AIChipClone.transform.GetChild(0).GetChild(0).transform.GetComponent<TextMeshProUGUI>().text);

                //DisappearEffectClone = Instantiate(DisappearEffect, AIChips.transform.position, Quaternion.identity);
                //DisappearEffectClone.SetActive(true);


                for (int i = 0; i < Player.GetComponent<Shooting>().MarkedObjects.Length; i++)
                {
                    if (Player.GetComponent<Shooting>().MarkedObjects[i] != null)
                    {
                        if (Player.GetComponent<Shooting>().MarkedObjects[i].GetComponent<NumberSoul>() != null)
                        {
                            Player.GetComponent<Shooting>().MarkedObjects[i].transform.parent.gameObject.SetActive(true);
                            Player.GetComponent<Shooting>().MarkedObjects[i].GetComponent<NumberSoul>().Marked = false;
                            Player.GetComponent<Shooting>().MarkedObjects[i].GetComponent<NumberSoul>().Centered = false;
                            Destroy(Player.GetComponent<Shooting>().MarkedObjects[i].transform.GetChild(2).gameObject);
                            Player.GetComponent<Shooting>().MarkedObjects[i].GetComponent<NumberSoul>().Combined = true;
                            Player.GetComponent<Shooting>().MarkedObjects[i] = null;
                            Player.GetComponent<Shooting>().CurrentMarkedObject = 0;
                        }
                    }
                }

            }
        }



        //COMBINE PARASITES
        if (Input.GetKeyDown("t") && !Anim.GetCurrentAnimatorStateInfo(0).IsTag("Melee Attack") && GetComponent<Health>().Recovered && Player.GetComponent<Shooting>().MarkedParasites[0] != null)
        {
            if (!CombineAnimation)
            {
                LongSwellSound.PlayOneShot(LongSwellSound.clip, 1f);
            }

            CombineAnimation = true;
            ParasiteTravel = true;
            ParasiteTravelSpeed = Vector3.Distance(Player.GetComponent<Shooting>().MarkedParasites[0].transform.position, transform.position) / 1.4f;
        }

        for (int i = 0; i < Player.GetComponent<Shooting>().MarkedParasites.Length; i++)
        {
            if (Player.GetComponent<Shooting>().MarkedParasites[i] != null)
            {
                if (ParasiteTravel && Player.GetComponent<Shooting>().MarkedParasites[i].transform.position != transform.position + new Vector3(Camera.main.transform.forward.x, 1, Camera.main.transform.forward.z) * 1.5f + Camera.main.transform.right * .5f)
                {
                    Player.GetComponent<Shooting>().MarkedParasites[i].transform.position = Vector3.MoveTowards(Player.GetComponent<Shooting>().MarkedParasites[i].transform.position, transform.position + new Vector3(Camera.main.transform.forward.x, 1, Camera.main.transform.forward.z) * 1.5f + Camera.main.transform.right *.5f, ParasiteTravelSpeed * Time.deltaTime);
                    //Player.GetComponent<Shooting>().MarkedParasites[i].transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(Player.GetComponent<Shooting>().MarkedParasites[i].transform.forward, new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z), 100 * Time.deltaTime, 0));
                    if (!Player.GetComponent<Shooting>().MarkedParasites[i].GetComponent<Parasite>().KnockedOut)
                    {
                        Player.GetComponent<Shooting>().MarkedParasites[i].GetComponent<Parasite>().Merge = true;
                        Player.GetComponent<Shooting>().MarkedParasites[i].GetComponent<Parasite>().Merge = true;
                        Player.GetComponent<Shooting>().MarkedParasites[i].GetComponent<Parasite>().Run = false;
                        Player.GetComponent<Shooting>().MarkedParasites[i].GetComponent<Parasite>().Attack = false;
                        Player.GetComponent<Shooting>().MarkedParasites[i].GetComponent<Parasite>().KnockedBack = false;
                    }
                }
            }
        }

        if (Player.GetComponent<Shooting>().MarkedParasites[0] != null)
        {
            if (ParasiteTravel && Player.GetComponent<Shooting>().MarkedParasites[0].transform.position == transform.position + new Vector3(Camera.main.transform.forward.x, 1, Camera.main.transform.forward.z) * 1.5f + Camera.main.transform.right * .5f)
            {
                ParasiteTravel = false;
                CombineSound.PlayOneShot(CombineSound.clip, 1f);
                CombineEffectClone = Instantiate(CombineEffect, transform.position, Quaternion.identity);
                CombineEffectClone.SetActive(true);

                // CREATE MERGE PARASITE
                if (Player.GetComponent<Shooting>().MarkedParasites[0].transform.position == transform.position + new Vector3(Camera.main.transform.forward.x, 1, Camera.main.transform.forward.z) * 1.5f + Camera.main.transform.right * .5f)
                {

                    MergeMutantClone = Instantiate(MergeMutant, Player.GetComponent<Shooting>().MarkedParasites[0].transform.position, Quaternion.identity);
                    MergeMutantClone.name = "Merge Mutant";
                    GetComponent<SpawnMonsters>().MergeMutant[MutantCount] = MergeMutantClone;
                    MutantCount += 1;
                    MergeMutantClone.SetActive(true);
                    MergeMutantClone.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(MergeMutantClone.transform.forward, new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z), 9999 * Time.deltaTime, 0));

                    if (!MergeMutantClone.GetComponent<Parasite>().KnockedOut)
                    {
                        MergeMutantClone.GetComponent<Parasite>().MergeDrop = true;
                        MergeMutantClone.GetComponent<Parasite>().Merge = false;
                        MergeMutantClone.GetComponent<Parasite>().Run = false;
                        MergeMutantClone.GetComponent<Parasite>().Attack = false;
                        MergeMutantClone.GetComponent<Parasite>().KnockedBack = false;
                    }

                    for (int i = 0; i < Player.GetComponent<Shooting>().MarkedParasites.Length; i++)
                    {
                        if (Player.GetComponent<Shooting>().MarkedParasites[i] != null)
                        {
                            Destroy(Player.GetComponent<Shooting>().MarkedParasites[i].gameObject);
                            Player.GetComponent<Shooting>().CurrentMarkedParasite = 0;                           
                        }

                    }
                }

            }
        }


    }


    IEnumerator DestroyDivideEffect(GameObject DivideEffect)
    {
        yield return new WaitForSeconds(5);
        Destroy(DivideEffect);
    }

    IEnumerator PlaySlowMoSound()
    {
        yield return new WaitForSeconds(.09f);
        SlowMoSound.PlayOneShot(SlowMoSound.clip, 1f);
    }

    IEnumerator PlayDivideSound()
    {
        yield return new WaitForSeconds(.7f);
        DivideSound.PlayOneShot(DivideSound.clip, 1f);
    }

    IEnumerator PlayShortSwellSound()
    {
        yield return new WaitForSeconds(2.75f);
        ShortSwellSound.PlayOneShot(ShortSwellSound.clip, 1f);
    }
}



