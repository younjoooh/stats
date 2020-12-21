using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parasite : MonoBehaviour
{
    public int Tier = 1;
    //[HideInInspector]
    public GameObject Player;
    float RunSpeed =  1f;
    float TurnSpeed = 15;
    public float Range = .85f;
    public float Multiplier = 1;
    [HideInInspector]
    public bool Marked;
    [HideInInspector]
    public bool Centered;

    //[HideInInspector]
    public bool Run, Attack, KnockedBack, Merge, MergeDrop, Disabled, Burned, Frozen, Poisoned, Electrocuted;
    public bool KnockedOut, PosionCounter;

    [HideInInspector]
    public int RunStyle = 1, KnockedOutStyle = 1, GrowlStyle = 1, ScreamStyle = 1, AttackCycle = 1, KnockedBackCycle = 1;
    public int Style = 1;

    Vector3 TargetDirection;
    Vector3 TargetDirectionP;
    Rigidbody RigBod;
    Animator Anim;

    Transform RightHand;
    Transform LeftHand;
    Transform Chest;
    CapsuleCollider RightHandCollider;
    CapsuleCollider LeftHandCollider;

    [HideInInspector]
    public AudioSource GrowlSound, ScreamSound;

    public GameObject MarkedRaysEffect;
    public GameObject MarkedRaysEffectClone;
    bool DeathCounted = false;
    void Start()
    {
        // STYLE
        RunStyle = Style;
        KnockedOutStyle = Style;
        GrowlStyle = Style;
        ScreamStyle = Style;
        //RunSpeed = Random.Range(.01f, 1);
        RunSpeed = 1f;
        
        if (this.gameObject.name == "Merge Mutant")
        {
            RunStyle = Random.Range(1, 12);
            ScreamStyle = Random.Range(1, 4);
            GrowlStyle = Random.Range(1, 12);
            Range = 1.2f;
        }
        

        // AUDIO
        GrowlSound = gameObject.AddComponent<UnityEngine.AudioSource>();
        GrowlSound.clip = Resources.Load<AudioClip>("Audio/Growls/Growl " + GrowlStyle);
        GrowlSound.spatialBlend = 1;
        GrowlSound.volume = .25f;
        GrowlSound.playOnAwake = true;
        GrowlSound.loop = true;
        GrowlSound.Play();

        ScreamSound = gameObject.AddComponent<UnityEngine.AudioSource>();
        ScreamSound.clip = Resources.Load<AudioClip>("Audio/Screams/Scream " + ScreamStyle);
        ScreamSound.spatialBlend = 1;
        ScreamSound.volume = .75f;
        ScreamSound.playOnAwake = false;

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
        RightHandCollider.radius = .07f;
        if (transform.GetChild(0).transform.localScale.x > 1)
        {
            RightHandCollider.radius = .2f;
        }
        RightHandCollider.height = .17f;
        RightHandCollider.direction = 0;
        RightHandCollider.isTrigger = true;
        

        RightHand.gameObject.SetActive(true);
        RightHand.gameObject.layer = 2;
        RightHand.gameObject.name = "Right Hand";
        RightHand.gameObject.AddComponent<ParasiteCollision>();
        RightHand.gameObject.GetComponent<ParasiteCollision>().Parasite = this.gameObject;



        // LEFT HAND
        LeftHand = Anim.GetBoneTransform(HumanBodyBones.LeftHand);
        LeftHand.gameObject.SetActive(false);


        LeftHandCollider = LeftHand.gameObject.AddComponent(typeof(CapsuleCollider)) as CapsuleCollider;
        LeftHandCollider.center = new Vector3(-.075f, -.025f, 0f);
        LeftHandCollider.radius = .07f ;
        if (transform.GetChild(0).transform.localScale.x > 1)
        {
            LeftHandCollider.radius = .2f;
        }
        LeftHandCollider.height = .17f;
        LeftHandCollider.direction = 0;
        LeftHandCollider.isTrigger = true;


        LeftHand.gameObject.SetActive(true);
        LeftHand.gameObject.layer = 2;
        LeftHand.gameObject.name = "Left Hand";
        LeftHand.gameObject.AddComponent<ParasiteCollision>();
        LeftHand.gameObject.GetComponent<ParasiteCollision>().Parasite = this.gameObject;



        // CHEST
        Chest = Anim.GetBoneTransform(HumanBodyBones.Chest);
        Chest.gameObject.SetActive(false);
        Chest.gameObject.SetActive(true);
        Chest.gameObject.layer = 2;
        Chest.gameObject.name = "Chest";

        if (this.gameObject.name != "Merge Mutant")
        {
            this.gameObject.name = "Parasite";
        }
        gameObject.AddComponent<TierSouls>();

        if (transform.GetChild(0).transform.localScale.x > 1)
        {
            Range = 1.2f;
        }

        MarkedRaysEffect = Instantiate(Resources.Load<GameObject>("Effects/Marked Rays Effect"), new Vector3(0, -5000, 0), Quaternion.identity);
        MarkedRaysEffect.SetActive(false);

        Multiplier = 1;
        gameObject.AddComponent<FindClosestParasite>();
    }

    void Update()
    {
        // VELOCITY & ROTATION
        TargetDirection = Player.transform.position - transform.position;
        TargetDirection.y = 0;

        if (!KnockedOut && !Attack && !MergeDrop)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, TargetDirection, TurnSpeed * Time.deltaTime, 0));
        }

        if (Vector3.Distance(transform.position, Player.transform.position) > Range && !KnockedOut && !Poisoned && !Electrocuted && !Burned && !Frozen)
        {
            RigBod.velocity = transform.forward * RunSpeed;
            if (!KnockedBack && !Merge && !MergeDrop)
            {
                Run = true;
                Attack = false;
            }
        }

        if (Anim.GetCurrentAnimatorStateInfo(0).IsTag("Knocked Out") && Anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, TargetDirection, TurnSpeed * 10 * Time.deltaTime, 0));
        }





        // ANIMATIONS
        if (Vector3.Distance(transform.position, Player.transform.position) <= Range && !KnockedOut && !KnockedBack && !Merge && !MergeDrop && !Poisoned && !Electrocuted && !Burned && !Frozen)
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
            Merge = false;
            MergeDrop = false;
            KnockedBack = false;
        }

        if (KnockedBack)
        {
            Attack = false;
            Run = false;
        }

        if (Merge)
        {
            Run = false;
            KnockedBack = false;
            Attack = false;
        }

        if (Anim.GetCurrentAnimatorStateInfo(0).IsName("Merge") )
        {
            Run = false;
            KnockedBack = false;
            Attack = false;
            Merge = false;
        }





        // MERGE
        if (this.gameObject.name == "Merge Mutant")
        {
            KnockedOutStyle = 12;
            if (Anim.GetCurrentAnimatorStateInfo(0).IsName("Knocked Out Mutant ") && Anim.GetCurrentAnimatorStateInfo(0).normalizedTime > .9f && Anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            {
                //this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
                this.gameObject.transform.GetChild(2).gameObject.SetActive(true);
                this.gameObject.transform.GetChild(3).gameObject.SetActive(true);
            }
        }

        if (MergeDrop)
        {
            if (!ScreamSound.isPlaying)
            {
                ScreamSound.PlayOneShot(ScreamSound.clip, 1f);
            }
        }

        if (Merge)
        {
            GetComponent<Collider>().enabled = false;
        }

        if (MergeDrop && Anim.GetCurrentAnimatorStateInfo(0).IsName("Merge Drop") && Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= .9f)
        {
            MergeDrop = false;
        }


        if (Anim.GetCurrentAnimatorStateInfo(0).IsName("Merge Drop"))
        {
            RigBod.velocity = transform.forward * 0f;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, .5f, transform.position.z), 5 * Time.deltaTime);
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




        // PLAYER ROTATION
        if (Vector3.Distance(transform.position, Player.transform.position) <= 3 && !Disabled && !KnockedOut && (Player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Divide Attack 2") || Player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Divide Attack 3")))
        {
            TargetDirectionP = transform.position - Player.transform.position;
            TargetDirectionP.y = 0;
            Player.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(Player.transform.forward, TargetDirectionP, TurnSpeed * Time.deltaTime, 0));
        }




        Anim.SetBool("Run", Run);
        Anim.SetBool("Attack", Attack);
        Anim.SetBool("Knocked Back", KnockedBack);
        Anim.SetBool("Knocked Out", KnockedOut);

        Anim.SetInteger("Run Style", RunStyle);
        Anim.SetInteger("Knocked Out Style", KnockedOutStyle);
        Anim.SetInteger("Attack Cycle", AttackCycle);
        Anim.SetInteger("Knocked Back Cycle", KnockedBackCycle);

        Anim.SetBool("Merge", Merge);
        Anim.SetBool("Merge Drop", MergeDrop);
        Anim.SetFloat("Multiplier", Multiplier);

        Anim.SetBool("Burned", Burned);
        Anim.SetBool("Poisoned", Poisoned);
        Anim.SetBool("Electrocuted", Electrocuted);

        //MARKED
        if (Marked && transform.childCount > 0 && !Centered)
        {
            transform.GetChild(1).gameObject.transform.position = Vector3.MoveTowards(transform.GetChild(1).gameObject.transform.position, Chest.position, 2 * Time.deltaTime);
        }

        if (!Centered) {
            if (Marked && transform.GetChild(1).gameObject.transform.position == Chest.position)
            {
                transform.GetChild(1).gameObject.transform.parent = Chest;
                Centered = true;
                MarkedRaysEffectClone = Instantiate(MarkedRaysEffect, Chest.position, Quaternion.identity);
                MarkedRaysEffectClone.SetActive(true);
                MarkedRaysEffectClone.transform.parent = Chest;
            }
        }

        if(Player.GetComponent<Shooting>().MarkedObjects[0]!= null && Marked && Centered)
        {
            Marked = false;
            Centered = false;
            Destroy(Chest.transform.GetChild(1).gameObject);

            for (int i = 0; i < Player.GetComponent<Shooting>().MarkedParasites.Length; i++)
            {
                Player.GetComponent<Shooting>().MarkedParasites[i] = null;
            }

            Player.GetComponent<Shooting>().CurrentMarkedParasite = 0;
        }

        if (KnockedOut)
        {
            if (Disabled)
            {

                if (Frozen)
                {
                    Disabled = false;
                    Multiplier = 1;
                    Transform[] allChildren = transform.GetChild(1).transform.GetChild(1).GetComponentsInChildren<Transform>();
                    foreach (Transform child in allChildren)
                    {
                        child.gameObject.AddComponent<Rigidbody>();
                        child.gameObject.AddComponent<SphereCollider>();
                        child.gameObject.GetComponent<SphereCollider>().radius = .025f;
                        StartCoroutine("DestroyPart", child.gameObject);
                    }
                }

                if (!Frozen)
                {
                    Electrocuted = false;
                    Poisoned = false;
                    //Burned = false;
                    if (transform.GetChild(1).gameObject != null)
                    {
                        Destroy(transform.GetChild(1).gameObject);
                        
                    }
                }
            }
        }

        if (transform.childCount == 3)
        {
            Destroy(transform.GetChild(2).gameObject);
        }

        if (transform.childCount == 4)
        {
            Destroy(transform.GetChild(3).gameObject);
        }


        if (Anim.GetCurrentAnimatorStateInfo(0).IsName("Burned") && Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= .85f)
        {
            Multiplier = 0;
            KnockedOut = true;
            Disabled = false;
            Burned = false;

            if (transform.childCount >= 2)
            {
                if (transform.GetChild(1).gameObject != null)
                {
                    Destroy(transform.GetChild(1).gameObject);
                }
            }
            //ScreamSound.PlayOneShot(ScreamSound.clip, 1f);
        }

        if (KnockedOut && !DeathCounted)
        {
            DeathCounted = true;
            Player.GetComponent<PlaceTraps>().ParasitesKnockedOut += 1;
        }

        if (Burned)
        {
            Run = false;
            Attack = false;
            KnockedBack = false;
            Merge = false;
            MergeDrop = false;
            /*
            if (!PosionCounter)
            {
                PosionCounter = true;
                StartCoroutine(PosionDeath(3));
            }
            */
        }

        if (Poisoned)
        {
            Run = false;
            Attack = false;
            KnockedBack = false;
            Merge = false;
            MergeDrop = false;

            if (!PosionCounter)
            {
                PosionCounter = true;                
                StartCoroutine(PosionDeath(3));
            }
        }


        if (Electrocuted)
        {
            Run = false;
            Attack = false;
            KnockedBack = false;
            Merge = false;
            MergeDrop = false;

            if (!KnockedOut)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 2f, transform.position.z), 1 * Time.deltaTime);
            }

            if (!PosionCounter)
            {
                PosionCounter = true;
                StartCoroutine(ElectricDeath(3));
            }
        }
    }

    IEnumerator Recover()
    {
        yield return new WaitForSeconds(.5f);
        KnockedBack = false;
    }

    IEnumerator DestroyPart(GameObject Part)
    {
        yield return new WaitForSeconds(2f);
        Destroy(Part);
    }

    IEnumerator PosionDeath(float time)
    {
        yield return new WaitForSeconds(time);
        KnockedOut = true;
        Poisoned = false;

        DeathCounted = true;
        Player.GetComponent<PlaceTraps>().ParasitesKnockedOut += 1;

        if (transform.GetChild(1).gameObject != null)
        {
            Destroy(transform.GetChild(1).gameObject);
        }
    }


    IEnumerator ElectricDeath(float time)
    {
        yield return new WaitForSeconds(time);
        KnockedOut = true;
        Electrocuted = false;

        DeathCounted = true;
        Player.GetComponent<PlaceTraps>().ParasitesKnockedOut += 1;

        GameObject BarGraphEffect = Instantiate(Resources.Load<GameObject>("Effects/Bar Graph Effect"), transform.position, Quaternion.identity);
        BarGraphEffect.transform.localScale = new Vector3(0, 0, 0);
        Destroy(gameObject);
        
    }

    void OnCollisionEnter(Collision coll)
    {
        if (Poisoned)
        {
            /*
            if (coll.gameObject.name =="Parasite")
            {
                if (!coll.gameObject.GetComponent<Parasite>().Disabled && !coll.gameObject.GetComponent<Parasite>().Poisoned)
                {
                    coll.gameObject.GetComponent<Parasite>().Poisoned = true;
                }
            }
            */
        }
        
    }
}
