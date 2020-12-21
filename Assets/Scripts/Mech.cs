using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mech : MonoBehaviour
{
    [HideInInspector]
    public GameObject Giant;
    GameObject Blaster;
    GameObject BlasterClone;

    float TurnSpeed;

    public float RunSpeed = 1f;
    public float Range = 10f;
    public float FireRate = 1f;
    public bool Reload = true;
    public int Ammo = 5;
    public string ColorType;


    [HideInInspector]
    public bool Run, Attack, KnockedBack, KnockedOut;
    bool MechSummon = true, GiantFound = true;


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
    public AudioSource MechSound;
    public AudioSource ExplosionSound;

    GameObject DestroyMechEffect;
    GameObject Spinner;
    GameObject CountDownText;
    GameObject NoTargetFoundText;

    AudioSource CountDownSound;
    AudioSource NoTargetFoundSound;
    AudioSource NoAmmoSound;
    bool CountDownStarted;
    void Start()
    {
        //SET TAG
        tag = ColorType;

        // TURN SPEED
        TurnSpeed = RunSpeed * 2;



        // CREATE BLASTER
        Blaster = Instantiate(Resources.Load<GameObject>("Prefabs/Mech Blaster " + ColorType), new Vector3(0, -5000, 0), Quaternion.identity);
        Blaster.SetActive(false);



        // AUDIO
        MechSound = gameObject.AddComponent<UnityEngine.AudioSource>();
        MechSound.clip = Resources.Load<AudioClip>("Audio/Mech Sounds/Mech Sound");
        MechSound.spatialBlend = 1;
        MechSound.volume = .25f;
        MechSound.playOnAwake = true;
        MechSound.Play();


        ExplosionSound = gameObject.AddComponent<UnityEngine.AudioSource>();
        ExplosionSound.clip = Resources.Load<AudioClip>("Audio/Mech Sounds/Mech Explosion Sound");
        ExplosionSound.spatialBlend = 1;
        ExplosionSound.volume = .25f;
        ExplosionSound.playOnAwake = true;
        ExplosionSound.Play();


        CountDownSound = gameObject.AddComponent<UnityEngine.AudioSource>();
        CountDownSound.clip = Resources.Load<AudioClip>("Audio/Mech Sounds/Count Down Sound");
        CountDownSound.spatialBlend = 1;
        CountDownSound.volume = .25f;
        CountDownSound.playOnAwake = false;


        NoTargetFoundSound = gameObject.AddComponent<UnityEngine.AudioSource>();
        NoTargetFoundSound.clip = Resources.Load<AudioClip>("Audio/Mech Sounds/No Target Found Sound");
        NoTargetFoundSound.spatialBlend = 1;
        NoTargetFoundSound.volume = .25f;
        NoTargetFoundSound.playOnAwake = false;


        NoAmmoSound = gameObject.AddComponent<UnityEngine.AudioSource>();
        NoAmmoSound.clip = Resources.Load<AudioClip>("Audio/Mech Sounds/No Ammo Sound");
        NoAmmoSound.spatialBlend = 1;
        NoAmmoSound.volume = .25f;
        NoAmmoSound.playOnAwake = false;



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




        // CREATE EFFECT      
        DestroyMechEffect = Instantiate(Resources.Load<GameObject>("Effects/Explosion Effect " + ColorType), new Vector3(0, -5000, 0), Quaternion.identity);
        DestroyMechEffect.SetActive(false);


        transform.eulerAngles = Camera.main.transform.eulerAngles;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        // FIND TARGET
        Spinner = transform.GetChild(2).gameObject;
        CountDownText = transform.GetChild(3).gameObject;
        NoTargetFoundText = transform.GetChild(4).gameObject;

        Spinner.SetActive(false);
        CountDownText.SetActive(false);
        NoTargetFoundText.SetActive(false);
    }

    void Update()
    {
        // NO TARGET FOUND
        if (Giant == null) 
        {
            if (Anim.GetCurrentAnimatorStateInfo(0).IsName("Mech Summon") && Anim.GetCurrentAnimatorStateInfo(0).normalizedTime > .9f && !CountDownStarted) 
            {
                CountDownStarted = true;
                Spinner.SetActive(true);
                CountDownText.SetActive(true);
                CountDownSound.PlayOneShot(CountDownSound.clip, 1f);
                StartCoroutine("CountDown2");
            }
        }

        if (Giant != null)
        {

            if (Giant.GetComponent<Giant>().KnockedOut)
            {
                CountDownStarted = true;
                Spinner.SetActive(true);
                CountDownText.SetActive(true);
                CountDownSound.PlayOneShot(CountDownSound.clip, 1f);
                StartCoroutine("CountDown2");
            }
        }

            // MECH SUMMON
            if (Anim.GetCurrentAnimatorStateInfo(0).IsName("Mech Summon") && Anim.GetCurrentAnimatorStateInfo(0).normalizedTime > .8f)
        {
            MechSummon = false;
        }




        //FIND GIANT
        Giant = GetComponent<FindClosestGiant>().Giant;
        if (!Anim.GetCurrentAnimatorStateInfo(0).IsName("Mech Summon") && !MechSummon && Giant == null)
        {
            GiantFound = false;
            Run = false;
            Attack = false;
        }


        // VELOCITY & ROTATION
        if (Giant != null )
        {
            TargetDirection = Giant.transform.position - transform.position;
            TargetDirection.y = 0;

            if (!KnockedOut)
            {
                transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, TargetDirection, TurnSpeed * Time.deltaTime, 0));
            }

            if (Vector3.Distance(transform.position, Giant.transform.position) > Range && !KnockedOut)
            {
                if (!MechSummon && !Anim.GetCurrentAnimatorStateInfo(0).IsName("Mech Sumon") || Anim.GetCurrentAnimatorStateInfo(0).IsName("Mech Summon") && Anim.GetCurrentAnimatorStateInfo(0).normalizedTime > .9F)
                {
                    RigBod.velocity = transform.forward * RunSpeed;


                    if (!KnockedBack && GiantFound)
                    {
                        Run = true;
                        Attack = false;
                    }
                }
            }

            if (Anim.GetCurrentAnimatorStateInfo(0).IsTag("Knocked Out") && Anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            {
                transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, TargetDirection, TurnSpeed * 10 * Time.deltaTime, 0));
            }
        }
        if (Giant == null)
        {
            RigBod.velocity = Vector3.zero;
            RigBod.angularVelocity = Vector3.zero;
            Attack = false;
        }




        // SHOOT BLASTERS
        if (Attack && Reload && Ammo > 0)
        {
            Ammo -= 1;
            BlasterClone = Instantiate(Blaster, transform.position + new Vector3(0,4.75f,0) + transform.forward * 2, Quaternion.identity);
            BlasterClone.SetActive(true);
            Physics.IgnoreCollision(BlasterClone.GetComponent<Collider>(), GetComponent<Collider>());
            BlasterClone.AddComponent<ShootCollision>();
            BlasterClone.GetComponent<ShootCollision>().Player = GameObject.FindWithTag("Player");
            BlasterClone.transform.eulerAngles = new Vector3(0,90,0);
            BlasterClone.GetComponent<Rigidbody>().useGravity = false;
            BlasterClone.GetComponent<Rigidbody>().AddForce(transform.forward * 75, ForceMode.VelocityChange);
            BlasterClone.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(-1.1f, 1f), Random.Range(-1.1f, 1f), Random.Range(-1.1f, 1f)) * 10f, ForceMode.VelocityChange);
            BlasterClone.tag = "GameController";
            StartCoroutine("DestroyBlaster", BlasterClone);
            StartCoroutine("ReloadBlaster");
        }





        // DESTROY MECH
        if (Ammo <= 0 && !KnockedOut)
        {
            Ammo = 0;
            KnockedOut = true;

            Spinner.SetActive(true);
            //CountDownSound.PlayOneShot(CountDownSound.clip, 1f);
            StartCoroutine("NoTargetFoundActivate", .25f);

            /*
            GetComponent<Mech>().ExplosionSound.PlayOneShot(GetComponent<Mech>().ExplosionSound.clip, 1f);
            DestroyMechEffect.SetActive(true);
            DestroyMechEffect.transform.position = this.transform.position + new Vector3(0,4f,0);
            Destroy(this.gameObject);
            */

        }



        // ANIMATIONS
        if (Giant != null)
        {
            if (Vector3.Distance(transform.position, Giant.transform.position) <= Range && !KnockedOut && !KnockedBack)
            {
                RigBod.velocity = new Vector3(0, 0, 0);
                Run = false;
                if (!MechSummon && !Anim.GetCurrentAnimatorStateInfo(0).IsName("Mech Sumon") || Anim.GetCurrentAnimatorStateInfo(0).IsName("Mech Summon") && Anim.GetCurrentAnimatorStateInfo(0).normalizedTime > .9F)
                {
                    Attack = true;
                }
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
        Anim.SetBool("Knocked Out", KnockedOut);
        Anim.SetBool("Mech Summon", MechSummon);
        Anim.SetBool("Giant Found", GiantFound);

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
    IEnumerator ReloadBlaster()
    {
        Reload = false;
        yield return new WaitForSeconds(10/(FireRate * 10));
        Reload = true;
        Anim.SetBool("Reload", Reload);
    }

    IEnumerator CountDown2()
    {
        yield return new WaitForSeconds(1);
        //CountDownSound.PlayOneShot(CountDownSound.clip, 1f);
        CountDownText.GetComponent<TextMesh>().text = "2";
        StartCoroutine("CountDown1");
    }

    IEnumerator CountDown1()
    {
        yield return new WaitForSeconds(1);
        //CountDownSound.PlayOneShot(CountDownSound.clip, 1f);
        CountDownText.GetComponent<TextMesh>().text = "1";
        StartCoroutine("NoTargetFoundActivate", 1);
    }

    IEnumerator NoTargetFoundActivate(float WaitTime)
    {
        yield return new WaitForSeconds(WaitTime);
        NoTargetFoundText.SetActive(true);
        if (Ammo == 0)
        {
            NoTargetFoundText.GetComponent<TextMesh>().text = "No Ammo";
            NoAmmoSound.PlayOneShot(NoAmmoSound.clip, 1f);
            StartCoroutine("DestroyMech",2);
        }
        else
        {
            NoTargetFoundText.GetComponent<TextMesh>().text = "No Target Found";
            NoTargetFoundSound.PlayOneShot(NoTargetFoundSound.clip, 1f);
            StartCoroutine("DestroyMech", 2.5f);
        }
        CountDownText.SetActive(false);
    }

    IEnumerator DestroyMech(float WaitTime)
    {
        yield return new WaitForSeconds(WaitTime);
        Ammo = 0;
        KnockedOut = true;
        AudioSource.PlayClipAtPoint(ExplosionSound.clip, transform.position);
        DestroyMechEffect.SetActive(true);
        DestroyMechEffect.transform.position = this.transform.position + new Vector3(0, 4f, 0);
        Destroy(this.gameObject);
    }
}
