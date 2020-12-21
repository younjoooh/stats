using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostSpirit : MonoBehaviour
{
    [HideInInspector]
    public GameObject Player;
    GameObject Blaster;
    GameObject BlasterClone;

    float TurnSpeed;
    public bool Captured;
    public float RunSpeed = 1f;
    float Range = 10f;

    [HideInInspector]
    public float FireRate = 1f;

    [HideInInspector]
    public bool Reload = true;

    public int Style = 3;
    public int Tier = 2;
    public int CurrentLocation = 0;


    [HideInInspector]
    public bool Run, Attack, KnockedBack, KnockedOut;
    bool PlayerFound = true;
    bool ReachedTop, ReachedBot;
    public bool Ally = false;
    public GameObject[] Locations = new GameObject[8];



    Vector3 TargetDirection;
    Vector3 TargetDirectionP;
    Rigidbody RigBod;
    Animator Anim;

    Transform RightHand;
    Transform LeftHand;
    Transform Head;
    Transform HeadEnd;
    CapsuleCollider RightHandCollider;
    CapsuleCollider LeftHandCollider;

    [HideInInspector]
    public AudioSource LostSpiritShootSound, GrowlSound, ScreamSound;

    GameObject FireEffect;
    GameObject ElectricBlast;
    GameObject ElectricBlastClone;
    GameObject ElectricCharge;

    GameObject IceExplosion;
    GameObject IceExplosionLow;
    GameObject IceExplosionLowClone;
    GameObject IceExplosionClone;

    [HideInInspector]
    public int KnockedBackCycle = 1;

    Material[] Mats;

    void Start()
    {

        //NAME
        this.gameObject.name = "Lost Spirit";
        // TURN SPEED
        TurnSpeed = RunSpeed * 2;



        // CREATE BLASTER
        Blaster = Instantiate(Resources.Load<GameObject>("Prefabs/Blaster"), new Vector3(0, -5000, 0), Quaternion.identity);
        if (Blaster.GetComponent<Rigidbody>() == null)
        {
            Rigidbody RigBod = Blaster.AddComponent(typeof(Rigidbody)) as Rigidbody;
        }
        Blaster.SetActive(false);



        // AUDIO

        LostSpiritShootSound = gameObject.AddComponent<UnityEngine.AudioSource>();
        LostSpiritShootSound.clip = Resources.Load<AudioClip>("Audio/LostSpirit/LostSpirit Shoot Sound");
        LostSpiritShootSound.spatialBlend = 1;
        LostSpiritShootSound.volume = .75f;
        LostSpiritShootSound.playOnAwake = false;


        GrowlSound = gameObject.AddComponent<UnityEngine.AudioSource>();
        GrowlSound.clip = Resources.Load<AudioClip>("Audio/Growls/Growl " + Style);
        GrowlSound.spatialBlend = 1;
        GrowlSound.volume = .25f;
        GrowlSound.playOnAwake = true;
        GrowlSound.loop = true;
        GrowlSound.Play();

        ScreamSound = gameObject.AddComponent<UnityEngine.AudioSource>();
        ScreamSound.clip = Resources.Load<AudioClip>("Audio/Screams/Scream " + Style);
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



        // HEAD
        Head = Anim.GetBoneTransform(HumanBodyBones.Head);
        Head.gameObject.SetActive(false);
        Head.gameObject.SetActive(true);
        Head.gameObject.layer = 2;
        Head.gameObject.name = "Head";

        HeadEnd = Head.GetChild(0).transform;





        // GET FIRE EFFECT     
        FireEffect = HeadEnd.GetChild(0).transform.gameObject;
        FireEffect.transform.GetChild(0).transform.localScale = new Vector3(0, 0, 0);
        FireEffect.SetActive(false);


        // GET ICE EXPLOSION
        IceExplosion = Instantiate(Resources.Load<GameObject>("Effects/Ice Explosion"), new Vector3(0, -500, 0), Quaternion.identity);
        IceExplosion.SetActive(false);

        IceExplosionLow = Instantiate(Resources.Load<GameObject>("Effects/Ice Explosion Low"), new Vector3(0, -500, 0), Quaternion.identity);
        IceExplosionLow.SetActive(false);

        // GET ELECTRIC BLAST
        ElectricBlast = Instantiate(Resources.Load<GameObject>("Effects/Electric Blast Effect"), new Vector3(0, -500, 0), Quaternion.identity);
        ElectricBlast.SetActive(false);


        ElectricCharge = HeadEnd.GetChild(1).transform.gameObject;
        ElectricCharge.SetActive(false);



        // PLAYER
        Player = GameObject.FindWithTag("Player");

        // ADD TIER SOULS
        gameObject.AddComponent<TierSouls>();


        transform.position = new Vector3(transform.position.x, (.5f * transform.localScale.x + .5f) + .25f * transform.localScale.x, transform.position.z);
        ReachedTop = true;

        Range = 1.5f * transform.localScale.x + 1.5f;

        // SET MATERIAL
        /*
        Mats = transform.GetChild(0).transform.GetChild(0).GetComponent<Renderer>().materials;
        Mats[0] = Resources.Load<Material>(tag + " Lost Spirit/" + tag + " Lost Spirit 3M");
        Mats[1] = Resources.Load<Material>(tag + " Lost Spirit/" + tag + " Lost Spirit 1M");
        Mats[2] = Resources.Load<Material>(tag + " Lost Spirit/" + tag + " Lost Spirit 2M");
        Mats[3] = Resources.Load<Material>(tag + " Lost Spirit/" + tag + " Lost Spirit 3M");
        transform.GetChild(0).transform.GetChild(0).GetComponent<Renderer>().materials = Mats;
        */

        if (tag == "Green")
        {
            Style = 4;
        }


        if (tag == "Blue")
        {
            Style = 3;
        }

        if (tag == "Yellow")
        {
            Style = 2;
        }

        if (tag == "Red")
        {
            Style = 1;
        }



        if (Ally)
        {
            RunSpeed = 4;
        }
        else
        {
            //RunSpeed = Random.Range(1, 4);
            RunSpeed = 1;
        }

        //gameObject.AddComponent<FindClosestParasite>();
    }

    void Update()
    {
        if (Ally)
        {
            Range = 1.5f * transform.localScale.x + 1.5f;
            GetComponent<CapsuleCollider>().enabled = false;
            if (CurrentLocation <= 7) {
                Player = Locations[CurrentLocation];
            }
        }

        if (!Ally)
        {
            GetComponent<CapsuleCollider>().enabled = true;
            Range = 12.5f;

            if (Player.GetComponent<Collider>() != null)
            {
                if (GetComponent<Collider>() != null)
                {
                    Physics.IgnoreCollision(Player.GetComponent<Collider>(), GetComponent<Collider>());
                }
            }
        }



        // FLY UP & DOWN
        if (ReachedTop && !KnockedOut)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, (.5f * transform.localScale.x + .5f) - .25f * transform.localScale.x, transform.position.z), 2.5f * Time.deltaTime);
        }

        if (ReachedBot && !KnockedOut)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, (.5f * transform.localScale.x + .5f) + .25f * transform.localScale.x, transform.position.z), 2.5f * Time.deltaTime);
        }

        if (Mathf.Round(transform.position.y * 1) / 1 == (.5f * transform.localScale.x + .5f) + .25f * transform.localScale.x)
        {
            ReachedTop = true;
            ReachedBot = false;
        }

        if (Mathf.Round(transform.position.y * 10) /10 == (.5f * transform.localScale.x + .5f) - .25f * transform.localScale.x )
        {
            ReachedBot = true;
            ReachedTop = false;
        }




        // VELOCITY & ROTATION
        if (Player != null)
        {
            TargetDirection = Player.transform.position - transform.position;
            TargetDirection.y = .5f * transform.localScale.x + .5f;

            if (!KnockedOut)
            {
                transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, TargetDirection, TurnSpeed * Time.deltaTime, 0));
            }

            if (Vector3.Distance(transform.position, Player.transform.position) > Range && !KnockedOut && FireEffect.transform.GetChild(0).transform.localScale.x  == 0f)
            {
                RigBod.velocity = transform.forward * RunSpeed;

                if (!KnockedBack && PlayerFound)
                {
                    Run = true;
                    Attack = false;
                }
            }

            if (Anim.GetCurrentAnimatorStateInfo(0).IsTag("Knocked Out") && Anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            {
                transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, TargetDirection, TurnSpeed * 10 * Time.deltaTime, 0));
            }
        }
        if (Player == null)
        {
            RigBod.velocity = Vector3.zero;
            RigBod.angularVelocity = Vector3.zero;
            Attack = false;
        }



        if (Style == 1) // FIRE RED
        {
            // SHOOT FLAMES
            if (Reload && Anim.GetCurrentAnimatorStateInfo(0).IsTag("Lost Spirit Attack") && Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= .215f && Anim.GetCurrentAnimatorStateInfo(0).normalizedTime < .8f)
            {
                FireEffect.SetActive(false);
                FireEffect.SetActive(true);
                //FireEffect.name = "Fire Effect";
                FireEffect.transform.GetChild(0).transform.localScale = new Vector3(1, 1, 1);
                CurrentLocation += 1;
                Reload = false;
            }

            if (!Captured && Anim.GetCurrentAnimatorStateInfo(0).IsTag("Lost Spirit Attack") && (Anim.GetCurrentAnimatorStateInfo(0).normalizedTime < .215f || Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= .8f))
            {
                Reload = true;
                FireEffect.transform.GetChild(0).transform.localScale = Vector3.MoveTowards(FireEffect.transform.GetChild(0).transform.localScale, new Vector3(0, 0, 0), 5 * Time.deltaTime);
            }
        }


        if (Style == 3 && CurrentLocation <= 7) // ICE BLUE
        {
            // ICE EXPLOSION
            if (Reload && Anim.GetCurrentAnimatorStateInfo(0).IsTag("Lost Spirit Attack") && Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= .525f && Anim.GetCurrentAnimatorStateInfo(0).normalizedTime < .9f)
            {
                if (Ally) 
                {
                    IceExplosionClone = Instantiate(IceExplosionLow, new Vector3(transform.position.x, -1f, transform.position.z), Quaternion.identity);
                    StartCoroutine(DestroyEffect(IceExplosionClone));
                }

                else
                {
                    IceExplosionClone = Instantiate(IceExplosionLow, new Vector3(transform.position.x, -1f, transform.position.z), Quaternion.identity);
                    StartCoroutine(DestroyEffect(IceExplosionClone));
                }
                CurrentLocation += 1;
                IceExplosionClone.name = "Ice Explosion";
                IceExplosionClone.transform.localScale = new Vector3(transform.localScale.x * 0.5f, transform.localScale.y * 0.5f, transform.localScale.z * 0.5f);
                IceExplosionClone.SetActive(false);
                IceExplosionClone.SetActive(true);
                StartCoroutine("DestroyBlaster", IceExplosionClone);
                Reload = false;
            }

            if (Anim.GetCurrentAnimatorStateInfo(0).IsTag("Lost Spirit Attack") && (Anim.GetCurrentAnimatorStateInfo(0).normalizedTime < .525f || Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= .9f))
            {
                Reload = true;
            }
        }
        

        if (Style == 2) //ELECTRO YELLOW
        {
            // SHOOT ELECTRICITY
            if (Reload && Anim.GetCurrentAnimatorStateInfo(0).IsTag("Lost Spirit Attack") && Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= .4f && Anim.GetCurrentAnimatorStateInfo(0).normalizedTime < .9f)
            {
                ElectricBlastClone = Instantiate(ElectricBlast, new Vector3(Head.transform.GetChild(0).position.x, Head.transform.GetChild(0).position.y, Head.transform.GetChild(0).position.z) + Head.transform.GetChild(0).transform.forward* .5f, Quaternion.identity);
                ElectricBlastClone.SetActive(false);
                ElectricBlastClone.SetActive(true);
                ElectricBlastClone.name = "Electric Blast Effect";

                StartCoroutine(DestroyElectric(ElectricBlastClone));
                Reload = false;
            }

            if (Anim.GetCurrentAnimatorStateInfo(0).IsTag("Lost Spirit Attack") && Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= .75f )
            {
                ElectricCharge.SetActive(false);
            }

            if (!Reload && Anim.GetCurrentAnimatorStateInfo(0).IsTag("Lost Spirit Attack") && (Anim.GetCurrentAnimatorStateInfo(0).normalizedTime < .4f ))
            {
                Reload = true;
                if (ElectricBlastClone != null)
                {
                    ElectricBlastClone.SetActive(false);
                }
                ElectricCharge.SetActive(true);
            }

            if (Reload && Anim.GetCurrentAnimatorStateInfo(0).IsTag("Lost Spirit Attack") && (Anim.GetCurrentAnimatorStateInfo(0).normalizedTime < .4f))
            {
                ElectricCharge.SetActive(true);
            }
        }


        if (Style == 4) //POSION GREEN
        {
            // SHOOT POISON
            if (Reload && Anim.GetCurrentAnimatorStateInfo(0).IsTag("Lost Spirit Attack") && Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= .215f && Anim.GetCurrentAnimatorStateInfo(0).normalizedTime < .8f)
            {
                FireEffect.SetActive(false);
                FireEffect.SetActive(true);
                //FireEffect.name = "Fire Effect";
                FireEffect.transform.GetChild(0).transform.localScale = new Vector3(1, 1, 1);
                CurrentLocation += 1;
                Reload = false;
            }

            if (!Captured && Anim.GetCurrentAnimatorStateInfo(0).IsTag("Lost Spirit Attack") && (Anim.GetCurrentAnimatorStateInfo(0).normalizedTime < .215f || Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= .8f))
            {
                Reload = true;
                FireEffect.transform.GetChild(0).transform.localScale = Vector3.MoveTowards(FireEffect.transform.GetChild(0).transform.localScale, new Vector3(0, 0, 0), 5 * Time.deltaTime);
            }
        }





        // ANIMATIONS
        if (Player != null)
        {
            if ((Vector3.Distance(transform.position, Player.transform.position) <= Range || FireEffect.transform.GetChild(0).transform.localScale.x != 0f ) && !KnockedOut && !KnockedBack)
            {
                RigBod.velocity = new Vector3(0, 0, 0);
                Run = false;
                Attack = true;
            }
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
            Attack = false;
            Run = false;
        }


        Anim.SetBool("Run", Run);
        Anim.SetBool("Attack", Attack);
        Anim.SetBool("Reload", Reload);
        Anim.SetBool("Knocked Back", KnockedBack);
        Anim.SetInteger("Knocked Back Cycle", KnockedBackCycle);
        Anim.SetBool("Knocked Out", KnockedOut);
        Anim.SetInteger("Style", Style);


        if (Captured)
        {
            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            if (FireEffect!= null)
            {
                FireEffect.transform.GetChild(0).transform.localScale = Vector3.MoveTowards(FireEffect.transform.GetChild(0).transform.localScale, new Vector3(0, 0, 0), 5 * Time.deltaTime);
            }
        }
    }


    IEnumerator Recover()
    {
        yield return new WaitForSeconds(.5f);
        KnockedBack = false;
    }

    IEnumerator DestroyBlaster(GameObject Blaster)
    {
        yield return new WaitForSeconds(10);
        Destroy(Blaster);
    }

    IEnumerator DestroyElectric(GameObject Blaster)
    {
        yield return new WaitForSeconds(2);
        Destroy(Blaster);
    }

    IEnumerator DestroyEffect(GameObject Effect)
    {
        yield return new WaitForSeconds(5);
        Destroy(Effect);
    }
}
