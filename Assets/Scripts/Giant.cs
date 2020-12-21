using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giant : MonoBehaviour
{
    [HideInInspector]
    public GameObject Player;
    public int Tier = 1;
    public int CurrentHits = 0;
    public int MaxHits = 50;

    float RunSpeed = 1f;
    float TurnSpeed = 100;
    float Range = 5f;

    [HideInInspector]
    public bool Run, Attack, KnockedBack, KnockedOut, Counted;

    [HideInInspector]
    public int RunStyle = 1, KnockedOutStyle = 1, GrowlStyle = 1, ScreamStyle = 1, AttackCycle = 1, KnockedBackCycle = 1;
    
    [HideInInspector]
    public int Style = 1;

    Vector3 TargetDirection;
    Rigidbody RigBod;
    Animator Anim;

    Transform RightHand;
    Transform LeftHand;
    Transform RightFoot;
    Transform LeftFoot;
    Transform LeftLowerArm;
    Transform RightLowerArm;

    CapsuleCollider RightHandCollider;
    CapsuleCollider LeftHandCollider;
    CapsuleCollider RightFootCollider;
    CapsuleCollider LeftFootCollider;
    CapsuleCollider LeftLowerArmCollider;
    CapsuleCollider RightLowerArmCollider;

    [HideInInspector]
    public AudioSource GrowlSound, ScreamSound,SelectSound, GiantEliminatedSound;
    void Start()
    {
        // STYLE
        if (tag == "Green")
        {
            Style = 1;
        }

        if (tag == "Yellow")
        {
            Style = 2;
        }

        if (tag == "Red")
        {
            Style = 3;
        }

        if (Tier == 1) 
        {
            if (tag == "Green")
            {
                MaxHits = 100;
            }

            if (tag == "Yellow")
            {
                MaxHits = 125;
            }

            if (tag == "Red")
            {
                MaxHits = 150;
            }

            if (tag == "Blue")
            {
                MaxHits = 175;
            }

            if (tag == "Purple")
            {
                MaxHits = 200;
            }
        }

        if (Tier == 2)
        {
            if (tag == "Green")
            {
                MaxHits = 225;
            }

            if (tag == "Yellow")
            {
                MaxHits = 275;
            }

            if (tag == "Red")
            {
                MaxHits = 325;
            }

            if (tag == "Blue")
            {
                MaxHits = 375;
            }

            if (tag == "Purple")
            {
                MaxHits = 425;
            }
        }

        if (Tier == 3)
        {
            if (tag == "Green")
            {
                MaxHits = 475;
            }

            if (tag == "Yellow")
            {
                MaxHits = 550;
            }

            if (tag == "Red")
            {
                MaxHits = 625;
            }

            if (tag == "Blue")
            {
                MaxHits = 700;
            }

            if (tag == "Purple")
            {
                MaxHits = 200;
            }
        }

        RunStyle = Style;
        KnockedOutStyle = Style;
        GrowlStyle = Style;
        ScreamStyle = Style;
        KnockedBackCycle = Style;
        AttackCycle = Style;

        // AUDIO
        GrowlSound = gameObject.AddComponent<UnityEngine.AudioSource>();
        GrowlSound.clip = Resources.Load<AudioClip>("Audio/Growls/Growl " + GrowlStyle);
        GrowlSound.spatialBlend = 1;
        GrowlSound.volume = .5f;
        GrowlSound.playOnAwake = true;
        GrowlSound.loop = true;
        GrowlSound.Play();

        ScreamSound = gameObject.AddComponent<UnityEngine.AudioSource>();
        ScreamSound.clip = Resources.Load<AudioClip>("Audio/Screams/Scream " + ScreamStyle);
        ScreamSound.spatialBlend = 1;
        ScreamSound.volume = 1;
        ScreamSound.playOnAwake = false;

        SelectSound = gameObject.AddComponent<UnityEngine.AudioSource>();
        SelectSound.clip = Resources.Load<AudioClip>("Audio/Giant Sounds/Select Sound X");
        SelectSound.spatialBlend = 1;
        SelectSound.volume = .5f;
        SelectSound.playOnAwake = false;

        GiantEliminatedSound = gameObject.AddComponent<UnityEngine.AudioSource>();
        GiantEliminatedSound.clip = Resources.Load<AudioClip>("Audio/Giant Sounds/Giant Eliminated Sound");
        GiantEliminatedSound.spatialBlend = 1;
        GiantEliminatedSound.volume = .1f;
        GiantEliminatedSound.playOnAwake = false;

        // ADD RIGDIBODY
        if (GetComponent<Rigidbody>() == null)
        {
            RigBod = gameObject.AddComponent<UnityEngine.Rigidbody>();
        }
        RigBod = GetComponent<Rigidbody>();
        RigBod.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        RigBod.mass = 100;

        // GET ANIMATOR
        Anim = GetComponent<Animator>();

        // FIND PLAYER
        Player = GameObject.FindWithTag("Player");




        // RIGHT HAND
        RightHand = Anim.GetBoneTransform(HumanBodyBones.RightHand);
        RightHand.gameObject.SetActive(false);


        RightHandCollider = RightHand.gameObject.AddComponent(typeof(CapsuleCollider)) as CapsuleCollider;
        RightHandCollider.center = new Vector3(.075f, -.025f, 0f);
        RightHandCollider.radius = .25f;
        RightHandCollider.height = .55f;

        RightHandCollider.direction = 0;
        RightHandCollider.isTrigger = true;


        RightHand.gameObject.SetActive(true);
        RightHand.gameObject.layer = 2;
        RightHand.gameObject.name = "Right Hand";
        RightHand.gameObject.AddComponent<GiantCollision>();
        RightHand.gameObject.GetComponent<GiantCollision>().Giant = this.gameObject;




        // RIGHTLOWER ARM
        RightLowerArm = Anim.GetBoneTransform(HumanBodyBones.RightLowerArm);
        RightLowerArm.gameObject.SetActive(false);


        RightLowerArmCollider = RightLowerArm.gameObject.AddComponent(typeof(CapsuleCollider)) as CapsuleCollider;
        RightLowerArmCollider.center = new Vector3(-.1f, 0f, 0f);
        RightLowerArmCollider.radius = .15f;
        RightLowerArmCollider.height = .55f;
        RightLowerArmCollider.direction = 0;
        RightLowerArmCollider.isTrigger = true;


        RightLowerArm.gameObject.SetActive(true);
        RightLowerArm.gameObject.layer = 2;
        RightLowerArm.gameObject.name = "Right Lower Arm";
        RightLowerArm.gameObject.AddComponent<GiantCollision>();
        RightLowerArm.gameObject.GetComponent<GiantCollision>().Giant = this.gameObject;




        // LEFT HAND
        LeftHand = Anim.GetBoneTransform(HumanBodyBones.LeftHand);
        LeftHand.gameObject.SetActive(false);


        LeftHandCollider = LeftHand.gameObject.AddComponent(typeof(CapsuleCollider)) as CapsuleCollider;
        LeftHandCollider.center = new Vector3(-.075f, -.025f, 0f);
        LeftHandCollider.radius = .25f;
        LeftHandCollider.height = .55f;
        LeftHandCollider.direction = 0;
        LeftHandCollider.isTrigger = true;


        LeftHand.gameObject.SetActive(true);
        LeftHand.gameObject.layer = 2;
        LeftHand.gameObject.name = "Left Hand";
        LeftHand.gameObject.AddComponent<GiantCollision>();
        LeftHand.gameObject.GetComponent<GiantCollision>().Giant = this.gameObject;





        // LEFT LOWER ARM
        LeftLowerArm = Anim.GetBoneTransform(HumanBodyBones.LeftLowerArm);
        LeftLowerArm.gameObject.SetActive(false);


        LeftLowerArmCollider = LeftLowerArm.gameObject.AddComponent(typeof(CapsuleCollider)) as CapsuleCollider;
        LeftLowerArmCollider.center = new Vector3(-.1f, 0f, 0f);
        LeftLowerArmCollider.radius = .15f;
        LeftLowerArmCollider.height = .55f;
        LeftLowerArmCollider.direction = 0;
        LeftLowerArmCollider.isTrigger = true;


        LeftLowerArm.gameObject.SetActive(true);
        LeftLowerArm.gameObject.layer = 2;
        LeftLowerArm.gameObject.name = "Left Lower Arm";
        LeftLowerArm.gameObject.AddComponent<GiantCollision>();
        LeftLowerArm.gameObject.GetComponent<GiantCollision>().Giant = this.gameObject;


        // RIGHT FOOT
        RightFoot = Anim.GetBoneTransform(HumanBodyBones.RightFoot);
        RightFoot.gameObject.SetActive(false);


        RightFootCollider = RightFoot.gameObject.AddComponent(typeof(CapsuleCollider)) as CapsuleCollider;
        if (tag == "Red")
        {
            RightFootCollider.center = new Vector3(0f, -.11f, .5f);
            RightFootCollider.radius = .11f;
            RightFootCollider.height = .55f;
        }

        if (tag == "Yellow")
        {
            RightFootCollider.center = new Vector3(0f, -.03f, .3f);
            RightFootCollider.radius = .1f;
            RightFootCollider.height = .5f;
        }

        if (tag == "Green")
        {
            RightFootCollider.center = new Vector3(0f, -.1f, .2f);
            RightFootCollider.radius = .1f;
            RightFootCollider.height = .5f;
        }

        if (tag == "Blue")
        {
            RightFootCollider.center = new Vector3(0f, .1f, .2f);
            RightFootCollider.radius = .1f;
            RightFootCollider.height = .5f;
        }

        if (tag == "Purple")
        {
            RightFootCollider.center = new Vector3(0f, .2f, .2f);
            RightFootCollider.radius = .1f;
            RightFootCollider.height = .5f;
        }

        RightFootCollider.direction = 2;
        RightFootCollider.isTrigger = true;


        RightFoot.gameObject.SetActive(true);
        RightFoot.gameObject.layer = 2;
        RightFoot.gameObject.name = "Right Foot";
        RightFoot.gameObject.AddComponent<GiantCollision>();
        RightFoot.gameObject.GetComponent<GiantCollision>().Giant = this.gameObject;




        // LEFT FOOT
        LeftFoot = Anim.GetBoneTransform(HumanBodyBones.LeftFoot);
        LeftFoot.gameObject.SetActive(false);


        LeftFootCollider = LeftFoot.gameObject.AddComponent(typeof(CapsuleCollider)) as CapsuleCollider;
        if (tag == "Red")
        {
            LeftFootCollider.center = new Vector3(0f, -.07f, .5f);
            LeftFootCollider.radius = .1f;
            LeftFootCollider.height = .5f;
        }

        if (tag == "Yellow")
        {
            LeftFootCollider.center = new Vector3(0f, -.1f, .3f);
            LeftFootCollider.radius = .03f;
            LeftFootCollider.height = .5f;
        }

        if (tag == "Green")
        {
            LeftFootCollider.center = new Vector3(0f, -.1f, .1f);
            LeftFootCollider.radius = .05f;
            LeftFootCollider.height = .5f;
        }


        if (tag == "Blue")
        {
            LeftFootCollider.center = new Vector3(0f, -.2f, .1f);
            LeftFootCollider.radius = .05f;
            LeftFootCollider.height = .5f;
        }

        if (tag == "Purple")
        {
            LeftFootCollider.center = new Vector3(0f, -.2f, .13f);
            LeftFootCollider.radius = .05f;
            LeftFootCollider.height = .5f;
        }

        LeftFootCollider.direction = 2;
        LeftFootCollider.isTrigger = true;


        LeftFoot.gameObject.SetActive(true);
        LeftFoot.gameObject.layer = 2;
        LeftFoot.gameObject.name = "Left Foot";
        LeftFoot.gameObject.AddComponent<GiantCollision>();
        LeftFoot.gameObject.GetComponent<GiantCollision>().Giant = this.gameObject;



        // SET RUN STYLE
        if (gameObject.tag == "Green")
        {
            RunStyle = 1;
        }
        if (gameObject.tag == "Yellow")
        {
            RunStyle = 2;
        }

        if (gameObject.tag == "Red")
        {
            RunStyle = 3;
        }


        gameObject.AddComponent<TierSouls>();
        gameObject.name = "Giant";
    }

    void Update()
    {
        // VELOCITY & ROTATION
        TargetDirection = Player.transform.position - transform.position;
        TargetDirection.y = 0;

        if (!KnockedOut && !Attack)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, TargetDirection, TurnSpeed * Time.deltaTime, 0));
        }

        if (Vector3.Distance(transform.position, Player.transform.position) > Range && !KnockedOut)
        {
            if (!KnockedBack)
            {
                RigBod.velocity = transform.forward * RunSpeed;
                Run = true;
                Attack = false;
            }
        }




        // ANIMATIONS
        if (Vector3.Distance(transform.position, Player.transform.position) <= Range && !KnockedOut && !KnockedBack)
        {
            RigBod.velocity = new Vector3(0, 0, 0);
            Run = false;
            Attack = true;
        }

        if (KnockedOut)
        {
            RigBod.velocity = new Vector3(0, 0, 0);
            RigBod.isKinematic = false;
            Attack = false;
            Run = false;
            KnockedBack = false;
        }

        if (KnockedBack)
        {
            RigBod.velocity = new Vector3(0,0,0);
            Attack = false;
            Run = false;
        }

        if (Anim.GetCurrentAnimatorStateInfo(0).IsTag("Knocked Back") && !Anim.IsInTransition(0))
        {
            RigBod.velocity = new Vector3(0, 0, 0);
        }



        // ATTACK TRANSITIONS
            if (Anim.GetCurrentAnimatorStateInfo(0).IsName("Monster Attack 1") && Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >.9f)
        {
            AttackCycle = 2;
        }


        if (Anim.GetCurrentAnimatorStateInfo(0).IsName("Monster Attack 2") && Anim.GetCurrentAnimatorStateInfo(0).normalizedTime > .9f)
        {
            AttackCycle = 3;
        }

        if (Anim.GetCurrentAnimatorStateInfo(0).IsName("Monster Attack 3") && Anim.GetCurrentAnimatorStateInfo(0).normalizedTime > .9f)
        {
            AttackCycle = 1;
        }




        // KNOCKED OUT
        if (CurrentHits >= MaxHits)
        {
            GetComponent<Collider>().enabled = false;
            if (!KnockedOut) 
            {
                ScreamSound.PlayOneShot(ScreamSound.clip, 1f);
                SelectSound.PlayOneShot(SelectSound.clip, 1f);
                GiantEliminatedSound.PlayOneShot(GiantEliminatedSound.clip, 1f);
                GrowlSound.Stop();
            }
            KnockedOut = true;
        }

        if (KnockedOut && !Counted)
        {
            Counted = true;
            /*
            if (Tier == 1 && Player.GetComponent<LinePlots>().LinePlot != Player.GetComponent<LinePlots>().LinePlot1)
            {
                Player.GetComponent<LinePlots>().LinePlot = Player.GetComponent<LinePlots>().LinePlot1;
                Player.GetComponent<LinePlots>().SetUpLinePlot(Player.GetComponent<LinePlots>().LinePlot1);
            }
            if (Tier == 2 && Player.GetComponent<LinePlots>().LinePlot != Player.GetComponent<LinePlots>().LinePlot2)
            {
                Player.GetComponent<LinePlots>().LinePlot = Player.GetComponent<LinePlots>().LinePlot2;
                Player.GetComponent<LinePlots>().SetUpLinePlot(Player.GetComponent<LinePlots>().LinePlot2);
            }
            if (Tier == 3 && Player.GetComponent<LinePlots>().LinePlot != Player.GetComponent<LinePlots>().LinePlot3)
            {
                Player.GetComponent<LinePlots>().LinePlot = Player.GetComponent<LinePlots>().LinePlot3;
                Player.GetComponent<LinePlots>().SetUpLinePlot(Player.GetComponent<LinePlots>().LinePlot3);
            }
            */
            if (tag == "Green")
            {
                Player.GetComponent<LinePlots>().GreenGiants += 1;
            }

            if (tag == "Yellow")
            {
                Player.GetComponent<LinePlots>().YellowGiants += 1;
            }

            if (tag == "Red")
            {
                Player.GetComponent<LinePlots>().RedGiants += 1;
            }

            if (tag == "Blue")
            {
                Player.GetComponent<LinePlots>().BlueGiants += 1;
            }

            if (tag == "Purple")
            {
                Player.GetComponent<LinePlots>().PurpleGiants += 1;
            }
        }




        Anim.SetBool("Run", Run);
        Anim.SetBool("Attack", Attack);
        Anim.SetBool("Knocked Back", KnockedBack);
        Anim.SetBool("Knocked Out", KnockedOut);

        Anim.SetInteger("Run Style", RunStyle);
        Anim.SetInteger("Knocked Out Style", KnockedOutStyle);
        Anim.SetInteger("Attack Cycle", AttackCycle);
        Anim.SetInteger("Knocked Back Cycle", KnockedBackCycle);


        //IGNORE OTHER GIANTS
        /*
        foreach (GameObject Giant in GameObject.FindObjectsOfType<GameObject>())
        {
            if (Giant.name == "Giant")
            {
                Physics.IgnoreCollision(Giant.GetComponent<Collider>(), GetComponent<Collider>());
            }
        }
        */
    }

    IEnumerator Recover()
    {
        yield return new WaitForSeconds(.5f);
        KnockedBack = false;
    }
}
