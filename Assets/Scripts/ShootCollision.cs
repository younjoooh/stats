using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCollision : MonoBehaviour
{
    public GameObject Player;
    public AudioSource SplashSound;
    public GameObject SplashEffect;
    public GameObject SplashEffectClone;
    public GameObject MarkedRaysEffect;
    public GameObject MarkedRaysEffectClone;

    public AudioSource WallSound;
    public GameObject WallEffect;
    public GameObject WallEffectClone;

    public GameObject SelectEffect;

    public GameObject Poison;
    public GameObject Electric;
    void Start()
    {
        SplashSound = gameObject.AddComponent<UnityEngine.AudioSource>();
        SplashSound.clip = Resources.Load<AudioClip>("Audio/Splash Sound");
        SplashSound.spatialBlend = 1;
        SplashSound.volume = .25f;
        SplashSound.playOnAwake = false;

        SplashEffect = Instantiate(Resources.Load<GameObject>("Effects/Splash Effect"), new Vector3(0, -5000, 0), Quaternion.identity);
        SplashEffect.SetActive(false);

        MarkedRaysEffect = Instantiate(Resources.Load<GameObject>("Effects/Marked Rays Effect"), new Vector3(0, -5000, 0), Quaternion.identity);
        MarkedRaysEffect.SetActive(false);

        WallSound = gameObject.AddComponent<UnityEngine.AudioSource>();
        WallSound.clip = Resources.Load<AudioClip>("Audio/Wall Sound");
        WallSound.spatialBlend = 1;
        WallSound.volume = .25f;
        WallSound.playOnAwake = false;

        WallEffect = Instantiate(Resources.Load<GameObject>("Effects/Wall Effect"), new Vector3(0, -5000, 0), Quaternion.identity);
        WallEffect.SetActive(false);

        SelectEffect = GameObject.Instantiate(Resources.Load<GameObject>("Effects/Select Effect"), new Vector3(0, -500,0), Quaternion.identity);
        SelectEffect.SetActive(false);
    }

    void Update()
    {


    }

    private void OnCollisionEnter(Collision Coll)
    {
        // LOWER QUARTILE
        if (Coll.gameObject.transform.name == "Lower Quart1")
        {
            SelectEffect.SetActive(false);
            SelectEffect.SetActive(true);
            SelectEffect.transform.position = Coll.gameObject.transform.position;
            SplashSound.PlayOneShot(SplashSound.clip, 1f);

            if (Poison.GetComponent<BoxAndWhisker>().Started)
            {

                if (Poison.GetComponent<BoxAndWhisker>().LowerPhase == 1)
                {
                    Poison.GetComponent<BoxAndWhisker>().LowerHasBeenShot2();
                }

                if (Poison.GetComponent<BoxAndWhisker>().LowerPhase == 0)
                {
                    Poison.GetComponent<BoxAndWhisker>().LowerHasBeenShot();
                }
            }
        }

        // UPPER QUARTILE
        if (Coll.gameObject.transform.name == "Upper Quart1")
        {
            SelectEffect.SetActive(false);
            SelectEffect.SetActive(true);
            SelectEffect.transform.position = Coll.gameObject.transform.position;
            SplashSound.PlayOneShot(SplashSound.clip, 1f);

            if (Poison.GetComponent<BoxAndWhisker>().Started)
            {

                if (Poison.GetComponent<BoxAndWhisker>().UpperPhase == 1)
                {
                    Poison.GetComponent<BoxAndWhisker>().UpperHasBeenShot2();
                }

                if (Poison.GetComponent<BoxAndWhisker>().UpperPhase == 0)
                {
                    Poison.GetComponent<BoxAndWhisker>().UpperHasBeenShot();
                }

            }
        }


        // IQR
        if (Coll.gameObject.transform.name == "IQR1")
        {
            SelectEffect.SetActive(false);
            SelectEffect.SetActive(true);
            SelectEffect.transform.position = Coll.gameObject.transform.position;
            SplashSound.PlayOneShot(SplashSound.clip, 1f);

            if (Poison.GetComponent<BoxAndWhisker>().Started)
            {
                Poison.GetComponent<BoxAndWhisker>().IQRHasBeenShot();
            }
        }








        // LOWER QUARTILE
        if (Coll.gameObject.transform.name == "Lower Quart2")
        {
            SelectEffect.SetActive(false);
            SelectEffect.SetActive(true);
            SelectEffect.transform.position = Coll.gameObject.transform.position;
            SplashSound.PlayOneShot(SplashSound.clip, 1f);

            if (Electric.GetComponent<BoxAndWhisker>().Started)
            {
                if (Electric.GetComponent<BoxAndWhisker>().LowerPhase == 1)
                {
                    Electric.GetComponent<BoxAndWhisker>().LowerHasBeenShot2();
                }

                if (Electric.GetComponent<BoxAndWhisker>().LowerPhase == 0)
                {
                    Electric.GetComponent<BoxAndWhisker>().LowerHasBeenShot();
                }

            }
        }

        // UPPER QUARTILE
        if (Coll.gameObject.transform.name == "Upper Quart2")
        {
            SelectEffect.SetActive(false);
            SelectEffect.SetActive(true);
            SelectEffect.transform.position = Coll.gameObject.transform.position;
            SplashSound.PlayOneShot(SplashSound.clip, 1f);

            if (Electric.GetComponent<BoxAndWhisker>().Started)
            {
                if (Electric.GetComponent<BoxAndWhisker>().UpperPhase == 1)
                {
                    Electric.GetComponent<BoxAndWhisker>().UpperHasBeenShot2();
                }

                if (Electric.GetComponent<BoxAndWhisker>().UpperPhase == 0)
                {
                    Electric.GetComponent<BoxAndWhisker>().UpperHasBeenShot();
                }
            }
        }


        // IQR
        if (Coll.gameObject.transform.name == "IQR2")
        {
            SelectEffect.SetActive(false);
            SelectEffect.SetActive(true);
            SelectEffect.transform.position = Coll.gameObject.transform.position;
            SplashSound.PlayOneShot(SplashSound.clip, 1f);

            if (Electric.GetComponent<BoxAndWhisker>().Started)
            {
                Electric.GetComponent<BoxAndWhisker>().IQRHasBeenShot();
            }
        }


        // MARK PARASITE
        if (Coll.gameObject.transform.name == "Parasite" && !Coll.gameObject.GetComponent<Parasite>().Marked  && Coll.gameObject.transform.tag == "Red" && this.gameObject.transform.childCount != 0 && Player.GetComponent<Shooting>().MarkedObjects[0] == null)
        {
            Coll.gameObject.GetComponent<Parasite>().Marked = true;
            Player.gameObject.GetComponent<Shooting>().MarkedParasites[Player.gameObject.GetComponent<Shooting>().CurrentMarkedParasite] = Coll.gameObject;
            Player.gameObject.GetComponent<Shooting>().CurrentMarkedParasite += 1;
            this.gameObject.transform.GetChild(0).transform.parent = Coll.gameObject.transform;
            this.gameObject.GetComponent<Collider>().enabled = false;
        }

        // MARK NUMBER SOUL
        if (Coll.gameObject.transform.tag == "Number Soul" && !Coll.gameObject.GetComponent<NumberSoul>().Marked && this.gameObject.transform.childCount != 0 && Player.gameObject.GetComponent<Shooting>().MarkedObjects[Player.gameObject.GetComponent<Shooting>().MaxMarkedObjects -1 ] == null)
        {
            Player.GetComponent<CombineSouls>().MarkedSound.PlayOneShot(Player.GetComponent<CombineSouls>().MarkedSound.clip, 1f);
            SplashSound.PlayOneShot(SplashSound.clip, 1f);

            Player.GetComponent<CombineSouls>().MarkedSound.clip = Resources.Load<AudioClip>("Audio/Marked Sounds/Marked Sound " + Random.Range(1, 4).ToString());

            Coll.gameObject.GetComponent<NumberSoul>().Combined = false;
            Coll.gameObject.GetComponent<NumberSoul>().Marked = true;
            Player.gameObject.GetComponent<Shooting>().MarkedObjects[Player.gameObject.GetComponent<Shooting>().CurrentMarkedObject] = Coll.gameObject;
            Player.gameObject.GetComponent<Shooting>().CurrentMarkedObject += 1;
            //GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);

            this.gameObject.transform.GetChild(0).transform.parent = Coll.gameObject.transform;
            Coll.gameObject.transform.GetChild(2).transform.localPosition = new Vector3(0,0,0);
            Coll.gameObject.transform.GetChild(2).transform.GetChild(0).localScale = new Vector3(1.5f, 1.5f, 1.5f);
            Coll.gameObject.transform.GetChild(2).transform.GetChild(1).transform.GetChild(0).localScale = new Vector3(1.5f, 1.5f, 1.5f);
            this.gameObject.GetComponent<Collider>().enabled = false;
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }


        // SPLASH EFFECT PARASITE/ MUTANT
        if ((Coll.gameObject.transform.name == "Parasite" || Coll.gameObject.transform.name == "Merge Mutant") && (Coll.gameObject.GetComponent<Parasite>().Marked || Coll.gameObject.transform.tag != "Red" || this.gameObject.transform.childCount == 0))
        {
            SplashSound.PlayOneShot(SplashSound.clip, 1f);
            SplashEffectClone = Instantiate(SplashEffect, transform.position, Quaternion.identity);
            SplashEffectClone.transform.GetChild(0).localScale = new Vector3(.5f, .5f, .5f);
            SplashEffectClone.transform.GetChild(1).localScale = new Vector3(.5f, .5f, .5f);
            SplashEffectClone.transform.GetChild(2).localScale = new Vector3(.5f, .5f, .5f);
            SplashEffectClone.transform.GetChild(3).localScale = new Vector3(.5f, .5f, .5f);
            SplashEffectClone.transform.GetChild(4).localScale = new Vector3(.5f, .5f, .5f);

            SplashEffectClone.SetActive(true);
            GetComponent<Collider>().enabled = false;
            GetComponent<Rigidbody>().useGravity = true;

            Coll.gameObject.transform.GetComponent<Rigidbody>().AddForce(Player.transform.forward * 10, ForceMode.VelocityChange);
            Coll.gameObject.transform.GetComponent<Parasite>().KnockedBack = true;
            Coll.gameObject.transform.GetComponent<Parasite>().StopCoroutine("Recover");
            Coll.gameObject.transform.GetComponent<Parasite>().StartCoroutine("Recover");
            if (!Coll.gameObject.transform.GetComponent<Parasite>().ScreamSound.isPlaying)
            {
                Coll.gameObject.transform.GetComponent<Parasite>().ScreamSound.PlayOneShot(Coll.gameObject.transform.GetComponent<Parasite>().ScreamSound.clip, 1f);
            }
            // KNOCKED BACK TRANSITIONS
            if (Coll.gameObject.transform.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Knocked Back 1"))
            {
                Coll.gameObject.transform.GetComponent<Parasite>().KnockedBackCycle = 2;
            }

            if (Coll.gameObject.transform.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Knocked Back 2"))
            {
                Coll.gameObject.transform.GetComponent<Parasite>().KnockedBackCycle = 3;
            }

            if (Coll.gameObject.transform.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Knocked Back 3"))
            {
                Coll.gameObject.transform.GetComponent<Parasite>().KnockedBackCycle = 1;
            }
        }


        // SPLASH EFFECT LOST SPIRIT
        if (Coll.gameObject.transform.name == "Lost Spirit" )
        {
            SplashSound.PlayOneShot(SplashSound.clip, 1f);
            SplashEffectClone = Instantiate(SplashEffect, transform.position, Quaternion.identity);
            SplashEffectClone.transform.GetChild(0).localScale = new Vector3(.3f * Coll.transform.localScale.x, .3f * Coll.transform.localScale.x, .3f * Coll.transform.localScale.x);
            SplashEffectClone.transform.GetChild(1).localScale = new Vector3(.3f * Coll.transform.localScale.x, .3f * Coll.transform.localScale.x, .3f * Coll.transform.localScale.x);
            SplashEffectClone.transform.GetChild(2).localScale = new Vector3(.3f * Coll.transform.localScale.x, .3f * Coll.transform.localScale.x, .3f * Coll.transform.localScale.x);
            SplashEffectClone.transform.GetChild(3).localScale = new Vector3(.3f * Coll.transform.localScale.x, .3f * Coll.transform.localScale.x, .3f * Coll.transform.localScale.x);
            SplashEffectClone.transform.GetChild(4).localScale = new Vector3(.3f * Coll.transform.localScale.x, .3f * Coll.transform.localScale.x, .3f * Coll.transform.localScale.x);
            SplashEffectClone.SetActive(true);
            GetComponent<Collider>().enabled = false;
            GetComponent<Rigidbody>().useGravity = true;

            Coll.gameObject.transform.GetComponent<Rigidbody>().AddForce(Player.transform.forward * 2, ForceMode.VelocityChange);
            Coll.gameObject.transform.GetComponent<LostSpirit>().KnockedBack = true;
            Coll.gameObject.transform.GetComponent<LostSpirit>().StopCoroutine("Recover");
            Coll.gameObject.transform.GetComponent<LostSpirit>().StartCoroutine("Recover");
            if (!Coll.gameObject.transform.GetComponent<LostSpirit>().ScreamSound.isPlaying)
            {
                Coll.gameObject.transform.GetComponent<LostSpirit>().ScreamSound.PlayOneShot(Coll.gameObject.transform.GetComponent<LostSpirit>().ScreamSound.clip, 1f);
            }
            // KNOCKED BACK TRANSITIONS
            if (Coll.gameObject.transform.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Knocked Back 1"))
            {
                Coll.gameObject.transform.GetComponent<LostSpirit>().KnockedBackCycle = 2;
            }

            if (Coll.gameObject.transform.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Knocked Back 2"))
            {
                Coll.gameObject.transform.GetComponent<LostSpirit>().KnockedBackCycle = 3;
            }

            if (Coll.gameObject.transform.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Knocked Back 3"))
            {
                Coll.gameObject.transform.GetComponent<LostSpirit>().KnockedBackCycle = 1;
            }
        }

        if (Coll.gameObject.transform.name == "Giant")
        {
            SplashSound.PlayOneShot(SplashSound.clip, 1f);
            SplashEffectClone = Instantiate(SplashEffect, transform.position, Quaternion.identity);
            SplashEffectClone.SetActive(true);
            StartCoroutine("DestroySplashEffect", SplashEffectClone);
            GetComponent<Collider>().enabled = false;
            GetComponent<Rigidbody>().useGravity = true;

            if (Coll.gameObject.transform.GetComponent<Giant>().CurrentHits < Coll.gameObject.transform.GetComponent<Giant>().MaxHits)
            {

                if (!Coll.gameObject.transform.GetComponent<Giant>().ScreamSound.isPlaying)
                {
                    Coll.gameObject.transform.GetComponent<Giant>().ScreamSound.PlayOneShot(Coll.gameObject.transform.GetComponent<Giant>().ScreamSound.clip, 1f);
                }

                Coll.gameObject.transform.GetComponent<Giant>().CurrentHits += 1;
                Coll.gameObject.transform.GetComponent<Giant>().KnockedBack = true;
                Coll.gameObject.transform.GetComponent<Giant>().StopCoroutine("Recover");
                Coll.gameObject.transform.GetComponent<Giant>().StartCoroutine("Recover");

                if (Coll.gameObject.transform.GetComponent<Giant>().tag == "Green" && this.gameObject.name == "Blaster") 
                {
                    Player.transform.GetChild(0).transform.GetChild(3).GetComponent<Renderer>().material = Player.GetComponent<BodyGlow>().GreenMat; // EYE
                    Player.transform.GetChild(0).transform.GetChild(8).GetComponent<Renderer>().material = Player.GetComponent<BodyGlow>().GreenMat; // SHIRT
                    Player.GetComponent<BodyGlow>().Color = "Green";

                    Player.GetComponent<SummonMech>().ColorType = "Green";

                    if (Coll.gameObject.transform.GetComponent<Giant>().Tier == 1)
                    {
                        Player.gameObject.GetComponent<Canvas>().GreenPop1 = true;
                        Player.GetComponent<Canvas>().StopCoroutine("PopGreen1");
                        Player.GetComponent<Canvas>().StartCoroutine("PopGreen1");
                    }

                    if (Coll.gameObject.transform.GetComponent<Giant>().Tier == 2)
                    {
                        Player.gameObject.GetComponent<Canvas>().GreenPop2 = true;
                        Player.GetComponent<Canvas>().StopCoroutine("PopGreen2");
                        Player.GetComponent<Canvas>().StartCoroutine("PopGreen2");
                    }

                    if (Coll.gameObject.transform.GetComponent<Giant>().Tier == 3)
                    {
                        Player.gameObject.GetComponent<Canvas>().GreenPop3 = true;
                        Player.GetComponent<Canvas>().StopCoroutine("PopGreen3");
                        Player.GetComponent<Canvas>().StartCoroutine("PopGreen3");
                    }
                }




                if (Coll.gameObject.transform.GetComponent<Giant>().tag == "Yellow" && this.gameObject.name == "Blaster")
                {
                    Player.transform.GetChild(0).transform.GetChild(3).GetComponent<Renderer>().material = Player.GetComponent<BodyGlow>().YellowMat; // EYE
                    Player.transform.GetChild(0).transform.GetChild(8).GetComponent<Renderer>().material = Player.GetComponent<BodyGlow>().YellowMat; // SHIRT
                    Player.GetComponent<BodyGlow>().Color = "Yellow";

                    Player.GetComponent<SummonMech>().ColorType = "Yellow";

                    if (Coll.gameObject.transform.GetComponent<Giant>().Tier == 1)
                    {
                        Player.gameObject.GetComponent<Canvas>().YellowPop1 = true;
                        Player.GetComponent<Canvas>().StopCoroutine("PopYellow1");
                        Player.GetComponent<Canvas>().StartCoroutine("PopYellow1");
                    }

                    if (Coll.gameObject.transform.GetComponent<Giant>().Tier == 2)
                    {
                        Player.gameObject.GetComponent<Canvas>().YellowPop2 = true;
                        Player.GetComponent<Canvas>().StopCoroutine("PopYellow2");
                        Player.GetComponent<Canvas>().StartCoroutine("PopYellow2");
                    }

                    if (Coll.gameObject.transform.GetComponent<Giant>().Tier == 3)
                    {
                        Player.gameObject.GetComponent<Canvas>().YellowPop3 = true;
                        Player.GetComponent<Canvas>().StopCoroutine("PopYellow3");
                        Player.GetComponent<Canvas>().StartCoroutine("PopYellow3");
                    }
                }



                if (Coll.gameObject.transform.GetComponent<Giant>().tag == "Red" && this.gameObject.name == "Blaster")
                {
                    Player.transform.GetChild(0).transform.GetChild(3).GetComponent<Renderer>().material = Player.GetComponent<BodyGlow>().RedMat; // EYE
                    Player.transform.GetChild(0).transform.GetChild(8).GetComponent<Renderer>().material = Player.GetComponent<BodyGlow>().RedMat; // SHIRT
                    Player.GetComponent<BodyGlow>().Color = "Red";

                    Player.GetComponent<SummonMech>().ColorType = "Red";

                    if (Coll.gameObject.transform.GetComponent<Giant>().Tier == 1)
                    {
                        Player.gameObject.GetComponent<Canvas>().RedPop1 = true;
                        Player.GetComponent<Canvas>().StopCoroutine("PopRed1");
                        Player.GetComponent<Canvas>().StartCoroutine("PopRed1");
                    }

                    if (Coll.gameObject.transform.GetComponent<Giant>().Tier == 2)
                    {
                        Player.gameObject.GetComponent<Canvas>().RedPop2 = true;
                        Player.GetComponent<Canvas>().StopCoroutine("PopRed2");
                        Player.GetComponent<Canvas>().StartCoroutine("PopRed2");
                    }

                    if (Coll.gameObject.transform.GetComponent<Giant>().Tier == 3)
                    {
                        Player.gameObject.GetComponent<Canvas>().RedPop3 = true;
                        Player.GetComponent<Canvas>().StopCoroutine("PopRed3");
                        Player.GetComponent<Canvas>().StartCoroutine("PopRed3");
                    }
                }



                if (Coll.gameObject.transform.GetComponent<Giant>().tag == "Blue" && this.gameObject.name == "Blaster")
                {
                    Player.transform.GetChild(0).transform.GetChild(3).GetComponent<Renderer>().material = Player.GetComponent<BodyGlow>().BlueMat; // EYE
                    Player.transform.GetChild(0).transform.GetChild(8).GetComponent<Renderer>().material = Player.GetComponent<BodyGlow>().BlueMat; // SHIRT
                    Player.GetComponent<BodyGlow>().Color = "Blue";

                    Player.GetComponent<SummonMech>().ColorType = "Blue";

                    if (Coll.gameObject.transform.GetComponent<Giant>().Tier == 2)
                    {
                        Player.gameObject.GetComponent<Canvas>().BluePop2 = true;
                        Player.GetComponent<Canvas>().StopCoroutine("PopBlue2");
                        Player.GetComponent<Canvas>().StartCoroutine("PopBlue2");
                    }

                    if (Coll.gameObject.transform.GetComponent<Giant>().Tier == 3)
                    {
                        Player.gameObject.GetComponent<Canvas>().BluePop3 = true;
                        Player.GetComponent<Canvas>().StopCoroutine("PopBlue3");
                        Player.GetComponent<Canvas>().StartCoroutine("PopBlue3");
                    }
                }




                if (Coll.gameObject.transform.GetComponent<Giant>().tag == "Purple" && this.gameObject.name == "Blaster")
                {
                    Player.transform.GetChild(0).transform.GetChild(3).GetComponent<Renderer>().material = Player.GetComponent<BodyGlow>().PurpleMat; // EYE
                    Player.transform.GetChild(0).transform.GetChild(8).GetComponent<Renderer>().material = Player.GetComponent<BodyGlow>().PurpleMat; // SHIRT
                    Player.GetComponent<BodyGlow>().Color = "Purple";

                    Player.GetComponent<SummonMech>().ColorType = "Purple";

                    if (Coll.gameObject.transform.GetComponent<Giant>().Tier == 3)
                    {
                        Player.gameObject.GetComponent<Canvas>().PurplePop3 = true;
                        Player.GetComponent<Canvas>().StopCoroutine("PopPurple3");
                        Player.GetComponent<Canvas>().StartCoroutine("PopPurple3");
                    }
                }


            }

            // KNOCKED BACK TRANSITIONS
            if (Coll.gameObject.transform.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Knocked Back 1"))
            {
                Coll.gameObject.transform.GetComponent<Giant>().KnockedBackCycle = 2;
            }

            if (Coll.gameObject.transform.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Knocked Back 2"))
            {
                Coll.gameObject.transform.GetComponent<Giant>().KnockedBackCycle = 3;
            }

            if (Coll.gameObject.transform.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Knocked Back 3"))
            {
                Coll.gameObject.transform.GetComponent<Giant>().KnockedBackCycle = 1;
            }

            if (Coll.gameObject.GetComponent<Giant>().Tier == 1)
            {
                Player.GetComponent<BarGraphs>().EnableBarGraph1 = true;
                Player.GetComponent<BarGraphs>().EnableBarGraph2 = false;
                Player.GetComponent<BarGraphs>().EnableBarGraph3 = false;

                Player.GetComponent<Canvas>().GiantCanvas1.SetActive(true);
                if (Player.GetComponent<Canvas>().CanvasType != "1")
                {
                    Player.GetComponent<Canvas>().CanvasObject1.transform.localPosition = new Vector3(1530.3005f, -300.3519f, 0); // Right
                }
                Player.GetComponent<Canvas>().CanvasType = "1";


                if (Coll.gameObject.transform.tag == "Green")
                {
                    Player.GetComponent<Shooting>().GiantGroup1[0] = Coll.gameObject;
                }
                if (Coll.gameObject.transform.tag == "Yellow")
                {
                    Player.GetComponent<Shooting>().GiantGroup1[1] = Coll.gameObject;
                }

                if (Coll.gameObject.transform.tag == "Red")
                {
                    Player.GetComponent<Shooting>().GiantGroup1[2] = Coll.gameObject;
                }
            }

            if (Coll.gameObject.GetComponent<Giant>().Tier == 2)
            {
                Player.GetComponent<BarGraphs>().EnableBarGraph2 = true;
                Player.GetComponent<BarGraphs>().EnableBarGraph1 = false;
                Player.GetComponent<BarGraphs>().EnableBarGraph3 = false;

                Player.GetComponent<Canvas>().GiantCanvas2.SetActive(true);
                if (Player.GetComponent<Canvas>().CanvasType != "2")
                {
                    Player.GetComponent<Canvas>().CanvasObject2.transform.localPosition = new Vector3(1530.3005f, -300.3519f, 0); // Right
                }
                Player.GetComponent<Canvas>().CanvasType = "2";

                if (Coll.gameObject.transform.tag == "Green")
                {
                    Player.GetComponent<Shooting>().GiantGroup2[0] = Coll.gameObject;
                }
                if (Coll.gameObject.transform.tag == "Yellow")
                {
                    Player.GetComponent<Shooting>().GiantGroup2[1] = Coll.gameObject;
                }

                if (Coll.gameObject.transform.tag == "Red")
                {
                    Player.GetComponent<Shooting>().GiantGroup2[2] = Coll.gameObject;
                }

                if (Coll.gameObject.transform.tag == "Blue")
                {
                    Player.GetComponent<Shooting>().GiantGroup2[3] = Coll.gameObject;
                }
            }

            if (Coll.gameObject.GetComponent<Giant>().Tier == 3)
            {
                Player.GetComponent<BarGraphs>().EnableBarGraph3 = true;
                Player.GetComponent<BarGraphs>().EnableBarGraph1 = false;
                Player.GetComponent<BarGraphs>().EnableBarGraph2 = false;

                Player.GetComponent<Canvas>().GiantCanvas3.SetActive(true);
                if (Player.GetComponent<Canvas>().CanvasType != "3")
                {
                    Player.GetComponent<Canvas>().CanvasObject3.transform.localPosition = new Vector3(1530.3005f, -300.3519f, 0); // Right
                }
                Player.GetComponent<Canvas>().CanvasType = "3";

                if (Coll.gameObject.transform.tag == "Green")
                {
                    Player.GetComponent<Shooting>().GiantGroup3[0] = Coll.gameObject;
                }
                if (Coll.gameObject.transform.tag == "Yellow")
                {
                    Player.GetComponent<Shooting>().GiantGroup3[1] = Coll.gameObject;
                }

                if (Coll.gameObject.transform.tag == "Red")
                {
                    Player.GetComponent<Shooting>().GiantGroup3[2] = Coll.gameObject;
                }

                if (Coll.gameObject.transform.tag == "Blue")
                {
                    Player.GetComponent<Shooting>().GiantGroup3[3] = Coll.gameObject;
                }

                if (Coll.gameObject.transform.tag == "Purple")
                {
                    Player.GetComponent<Shooting>().GiantGroup3[4] = Coll.gameObject;
                }
            }
        }

        // MEDIAN GATE PART 1
        if (Coll.gameObject.transform.name == "Click")
        {
            GetComponent<Collider>().enabled = false;
            GetComponent<Rigidbody>().useGravity = true;
            SelectEffect.SetActive(false);
            SelectEffect.SetActive(true);
            SelectEffect.transform.position = Coll.gameObject.transform.position;
            SplashSound.PlayOneShot(SplashSound.clip, 1f);

            Coll.gameObject.GetComponent<Collider>().enabled = false;
            Coll.gameObject.transform.parent.transform.parent.gameObject.GetComponent<MedianGate>().GrowPoint = Coll.gameObject;
            Coll.gameObject.transform.parent.transform.parent.gameObject.GetComponent<MedianGate>().PointsSelected += 1;
        }

        // MEDIAN GATE PART 2
        if (Coll.gameObject.transform.name == "0")
        {
            GetComponent<Collider>().enabled = false;
            GetComponent<Rigidbody>().useGravity = true;
            SelectEffect.SetActive(false);
            SelectEffect.SetActive(true);
            SelectEffect.transform.position = Coll.gameObject.transform.position;
            SplashSound.PlayOneShot(SplashSound.clip, 1f);
            Destroy(Coll.gameObject.transform.parent.gameObject.GetComponent<MedianGate>().Numbers[0]);

            Coll.gameObject.transform.parent.gameObject.GetComponent<MedianGate>().SmallBorderLocation = "7";
            Coll.gameObject.transform.parent.gameObject.GetComponent<MedianGate>().OrderNumnders = false;
        }

        if (Coll.gameObject.transform.name == "7")
        {
            GetComponent<Collider>().enabled = false;
            GetComponent<Rigidbody>().useGravity = true;
            SelectEffect.SetActive(false);
            SelectEffect.SetActive(true);
            SelectEffect.transform.position = Coll.gameObject.transform.position;
            SplashSound.PlayOneShot(SplashSound.clip, 1f);
            Destroy(Coll.gameObject.transform.parent.gameObject.GetComponent<MedianGate>().Numbers[7]);

            Coll.gameObject.transform.parent.GetComponent<MedianGate>().SmallBorderLocation = "1";
        }

        if (Coll.gameObject.transform.name == "1")
        {
            GetComponent<Collider>().enabled = false;
            GetComponent<Rigidbody>().useGravity = true;
            SelectEffect.SetActive(false);
            SelectEffect.SetActive(true);
            SelectEffect.transform.position = Coll.gameObject.transform.position;
            SplashSound.PlayOneShot(SplashSound.clip, 1f);
            Destroy(Coll.gameObject.transform.parent.gameObject.GetComponent<MedianGate>().Numbers[1]);

            Coll.gameObject.transform.parent.GetComponent<MedianGate>().SmallBorderLocation = "6";
        }

        if (Coll.gameObject.transform.name == "6")
        {
            GetComponent<Collider>().enabled = false;
            GetComponent<Rigidbody>().useGravity = true;
            SelectEffect.SetActive(false);
            SelectEffect.SetActive(true);
            SelectEffect.transform.position = Coll.gameObject.transform.position;
            SplashSound.PlayOneShot(SplashSound.clip, 1f);
            Destroy(Coll.gameObject.transform.parent.gameObject.GetComponent<MedianGate>().Numbers[6]);

            Coll.gameObject.transform.parent.GetComponent<MedianGate>().SmallBorderLocation = "2";
        }

        if (Coll.gameObject.transform.name == "2")
        {
            GetComponent<Collider>().enabled = false;
            GetComponent<Rigidbody>().useGravity = true;
            SelectEffect.SetActive(false);
            SelectEffect.SetActive(true);
            SelectEffect.transform.position = Coll.gameObject.transform.position;
            SplashSound.PlayOneShot(SplashSound.clip, 1f);
            Destroy(Coll.gameObject.transform.parent.gameObject.GetComponent<MedianGate>().Numbers[2]);

            Coll.gameObject.transform.parent.GetComponent<MedianGate>().SmallBorderLocation = "5";
        }

        if (Coll.gameObject.transform.name == "5")
        {
            GetComponent<Collider>().enabled = false;
            GetComponent<Rigidbody>().useGravity = true;
            SelectEffect.SetActive(false);
            SelectEffect.SetActive(true);
            SelectEffect.transform.position = Coll.gameObject.transform.position;
            SplashSound.PlayOneShot(SplashSound.clip, 1f);
            Destroy(Coll.gameObject.transform.parent.gameObject.GetComponent<MedianGate>().Numbers[5]);

            Coll.gameObject.transform.parent.GetComponent<MedianGate>().SmallBorderLocation = "3";
        }

        if (Coll.gameObject.transform.name == "Big Border")
        {
            GetComponent<Collider>().enabled = false;
            GetComponent<Rigidbody>().useGravity = true;
            SelectEffect.SetActive(false);
            SelectEffect.SetActive(true);
            SelectEffect.transform.position = Coll.gameObject.transform.position;
            SplashSound.PlayOneShot(SplashSound.clip, 1f);

            Coll.gameObject.transform.parent.GetComponent<MedianGate>().GrowAddedTech = true;
        }

        /*
        // WALL EFFECT
        if (Coll.gameObject.transform.name != "Parasite" && Coll.gameObject.transform.name != "Giant")
        {
                WallSound.PlayOneShot(WallSound.clip, 1f);
                WallEffectClone = Instantiate(WallEffect, transform.position, Quaternion.identity);
                WallEffectClone.SetActive(true);
                GetComponent<Collider>().enabled = false;
                GetComponent<Rigidbody>().useGravity = true;
        }
        */

    }

    IEnumerator DestroySplashEffect(GameObject SplashEffect)
    {
        yield return new WaitForSeconds(5);
        Destroy(SplashEffect);
    }

}
