using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shooting : MonoBehaviour
{
    GameObject Blaster;
    Transform RightHand;
    GameObject BlasterClone;

    public GameObject Poison;
    public GameObject Electric;

    public float BlasterSpeed = 50;
    public float BlasterDamage = 5;
    float BodyLerp = 0;
    float BodyLerpLeft = 0;
    //[HideInInspector]
    public bool CombatMode, CombatModeMelee, Aim, Shoot, Charged, Charging;

    Ray MouseRay;
    Animator Anim;

    [HideInInspector]
    public GameObject ChargeEffect, ChargeEffectClone;
    [HideInInspector]
    public GameObject ChargeEffectPlus, ChargeEffectClonePlus;

    [HideInInspector]
    public int MaxMarkedObjects = 25;
    [HideInInspector]
    public GameObject[] MarkedObjects = new GameObject[25];
    [HideInInspector]
    public GameObject[] MarkedParasites = new GameObject[25];

    [HideInInspector]
    public int CurrentMarkedObject = 0;
    [HideInInspector]
    public int CurrentMarkedParasite = 0;

    [HideInInspector]
    public GameObject[] GiantGroup1 = new GameObject[3];
    [HideInInspector]
    public GameObject[] GiantGroup2 = new GameObject[4];
    [HideInInspector]
    public GameObject[] GiantGroup3 = new GameObject[5];

    [HideInInspector]
    public AudioSource ChargedShootSound1, ChargedShootSound2, DefaultShootSound;


    [HideInInspector]
    public Material GreenMat;
    [HideInInspector]
    public Material YellowMat;
    [HideInInspector]
    public Material RedMat;
    [HideInInspector]
    public Material BlueMat;
    [HideInInspector]
    public Material PurpleMat;
    [HideInInspector]
    public string Color;
    void Start()
    {
        GreenMat = Resources.Load<Material>("Materials/Blaster Glow/Glow Green");
        YellowMat = Resources.Load<Material>("Materials/Blaster Glow/Glow Yellow");
        RedMat = Resources.Load<Material>("Materials/Blaster Glow/Glow Red");
        BlueMat = Resources.Load<Material>("Materials/Blaster Glow/Glow Blue");
        PurpleMat = Resources.Load<Material>("Materials/Blaster Glow/Glow Purple");


        MarkedObjects = new GameObject[MaxMarkedObjects];
        Anim = GetComponent<Animator>();

        Blaster = Instantiate(Resources.Load<GameObject>("Prefabs/Blaster"), new Vector3(0, -5000, 0), Quaternion.identity);
        if (Blaster.GetComponent<Rigidbody>() == null) {
            Rigidbody RigBod = Blaster.AddComponent(typeof(Rigidbody)) as Rigidbody;
        }
        Blaster.SetActive(false);

        RightHand = Anim.GetBoneTransform(HumanBodyBones.RightHand);
        RightHand.gameObject.SetActive(false);
        RightHand.gameObject.SetActive(true);
        RightHand.gameObject.layer = 2;
        RightHand.gameObject.name = "Right Hand";

        ChargeEffect = Instantiate(Resources.Load<GameObject>("Effects/Charge Effect"), RightHand.GetChild(1).position, Quaternion.identity);
        ChargeEffect.transform.GetChild(0).transform.localScale = new Vector3(0, 0, 0);
        ChargeEffect.transform.parent = RightHand;
        ChargeEffect.SetActive(false);

        ChargeEffectPlus = Instantiate(Resources.Load<GameObject>("Effects/Charge Effect Plus"), RightHand.GetChild(1).position, Quaternion.identity);
        ChargeEffectPlus.transform.GetChild(0).transform.localScale = new Vector3(0, 0, 0);
        ChargeEffectPlus.transform.parent = RightHand;
        ChargeEffectPlus.SetActive(false);

        ChargedShootSound1 = gameObject.AddComponent<UnityEngine.AudioSource>();
        ChargedShootSound1.clip = Resources.Load<AudioClip>("Audio/Shoot Sounds/Charged Shoot Sound 1");
        ChargedShootSound1.spatialBlend = 1;
        ChargedShootSound1.volume = .25f;
        ChargedShootSound1.playOnAwake = false;

        ChargedShootSound2 = gameObject.AddComponent<UnityEngine.AudioSource>();
        ChargedShootSound2.clip = Resources.Load<AudioClip>("Audio/Shoot Sounds/Charged Shoot Sound 2");
        ChargedShootSound2.spatialBlend = 1;
        ChargedShootSound2.volume = .25f;
        ChargedShootSound2.playOnAwake = false;

        DefaultShootSound = gameObject.AddComponent<UnityEngine.AudioSource>();
        DefaultShootSound.clip = Resources.Load<AudioClip>("Audio/Shoot Sounds/Default Shoot Sound");
        DefaultShootSound.spatialBlend = 1;
        DefaultShootSound.volume = .25f;
        DefaultShootSound.playOnAwake = false;
    }
    void FixedUpdate()
    {
    }
    void Update()
    {
        // COMBAT MODE
        if (Input.GetKeyDown("1"))
        {
            CombatMode = false;
        }

        if (Input.GetKeyDown("q") || Input.GetKeyDown("r"))
        {
            CombatMode = true;
        }

        if (Anim.GetCurrentAnimatorStateInfo(0).IsTag("Melee Attack") && !Anim.IsInTransition(0) && Anim.GetCurrentAnimatorStateInfo(0).normalizedTime > .5f)
        {
            CombatMode = true;
        }

        /*
        if (!Input.GetKey(KeyCode.Mouse0) && !Input.GetKey(KeyCode.Mouse1) && !Input.GetKey("q") && !Shoot
            && !Anim.GetCurrentAnimatorStateInfo(0).IsTag("Melee Attack") && !Anim.IsInTransition(0)
            && Anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 3
            && Anim.GetCurrentAnimatorStateInfo(1).IsName("None") && !Anim.IsInTransition(1)
            && Anim.GetCurrentAnimatorStateInfo(1).normalizedTime > 3)
        {
            CombatMode = false;
        }
        */

        Anim.SetBool("Combat Mode", CombatMode);


        // COMBAT MODE MELEE
        if (Input.GetKeyDown("1") )
        {
            CombatModeMelee = false;
        }

        if (GetComponent<CombineSouls>().AIChipClone != null)
        {
            CombatModeMelee = false;
        }

        if (Input.GetKeyDown("q"))
        {
            CombatModeMelee = true;
        }

        if (Anim.GetCurrentAnimatorStateInfo(0).IsTag("Melee Attack") &&
            !Anim.GetCurrentAnimatorStateInfo(0).IsName("Summon Graph") &&
            !Anim.GetCurrentAnimatorStateInfo(0).IsName("Combine") &&
            !Anim.GetCurrentAnimatorStateInfo(0).IsName("Divide Attack 3") &&
            !Anim.IsInTransition(0) && Anim.GetCurrentAnimatorStateInfo(0).normalizedTime > .5f)
        {
            CombatModeMelee = true;
        }

        if (!Input.GetKey("q")
            && !Anim.GetCurrentAnimatorStateInfo(0).IsTag("Melee Attack") && !Anim.IsInTransition(0)
            && Anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 3)
        {
            CombatModeMelee = false;
        }





        // SHOOT
        MouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Anim.SetBool("Shoot", Shoot);
        Anim.SetBool("Charged", Charged);
        Anim.SetBool("Charging", Charging);


        if (Input.GetMouseButton(0) && GetComponent<Health>().Recovered && ChargeEffect.transform.GetChild(0).transform.localScale.x <= .5f)
        {
            Charged = false;
            Charging = true;
            CombatMode = true;
            CombatModeMelee = false;

            ChargeEffect.SetActive(true);
            ChargeEffect.transform.GetChild(0).transform.localScale += new Vector3 (.75f * Time.deltaTime, .75f * Time.deltaTime, .75f * Time.deltaTime);
        }

        if (ChargeEffect.transform.GetChild(0).transform.localScale.x > .5f)
        {
            ChargeEffectPlus.SetActive(true);
            if (ChargeEffectPlus.transform.GetChild(0).transform.localScale.x <= .5f)
            {
                ChargeEffectPlus.transform.GetChild(0).transform.localScale += new Vector3(2.5f * Time.deltaTime, 2.5f * Time.deltaTime, 2.5f * Time.deltaTime);
            }
        }

        if (!Input.GetMouseButton(0))
        {
            Charging = false;
        }

        if (Input.GetMouseButtonUp(0) && GetComponent<Health>().Recovered)
        {
            if (ChargeEffect.transform.GetChild(0).transform.localScale.x <= .5f)
            {
                //DefaultShootSound.PlayOneShot(DefaultShootSound.clip, 1f);
            }

            CombatMode = true;
            CombatModeMelee = false;
            Shoot = true;
            Blaster.SetActive(true);
            Blaster.transform.position = new Vector3(0, -5000, 0);
            //BlasterClone = Instantiate(Blaster, Camera.main.transform.position + Camera.main.transform.up * .05f + Camera.main.transform.forward * 2.25f + -Camera.main.transform.right * .435f, Quaternion.identity);
            BlasterClone = Instantiate(Blaster, RightHand.GetChild(1).position, Quaternion.identity);

            if (ChargeEffect.transform.GetChild(0).transform.localScale.x > .5f)
            {
                //ChargedShootSound1.PlayOneShot(ChargedShootSound1.clip, 1f);
                ChargedShootSound2.PlayOneShot(ChargedShootSound2.clip, 1f);
                Charged = true;
                ChargeEffectClone = Instantiate(ChargeEffect, BlasterClone.transform.position, Quaternion.identity);
                ChargeEffectClone.transform.parent = BlasterClone.transform;

                ChargeEffectClonePlus = Instantiate(ChargeEffectPlus, ChargeEffectClone.transform.position, Quaternion.identity);
                ChargeEffectClonePlus.transform.parent = ChargeEffectClone.transform;
            }

            ChargeEffect.transform.GetChild(0).transform.localScale = new Vector3 (0,0,0);
            ChargeEffect.SetActive(false);

            ChargeEffectPlus.transform.GetChild(0).transform.localScale = new Vector3(0, 0, 0);
            ChargeEffectPlus.SetActive(false);

            Physics.IgnoreCollision(BlasterClone.GetComponent<Collider>(), GetComponent<Collider>());
            BlasterClone.AddComponent<ShootCollision>();
            BlasterClone.GetComponent<ShootCollision>().Player = this.gameObject;
            BlasterClone.GetComponent<ShootCollision>().Poison = Poison;
            BlasterClone.GetComponent<ShootCollision>().Electric = Electric;
            BlasterClone.layer = LayerMask.NameToLayer("Ignore Raycast");
            BlasterClone.GetComponent<Rigidbody>().AddForce(MouseRay.direction * BlasterSpeed, ForceMode.VelocityChange);
            BlasterClone.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(-1.1f, 1f), Random.Range(-1.1f, 1f), Random.Range(-1.1f, 1f)) * 10f, ForceMode.VelocityChange);
            BlasterClone.GetComponent<Rigidbody>().useGravity = false;
            BlasterClone.tag = "GameController";
            BlasterClone.name = "Blaster";

            if (GetComponent<BodyGlow>().Color == "Green")
            {
                BlasterClone.GetComponent<Renderer>().material = GreenMat;
            }
            if (GetComponent<BodyGlow>().Color == "Yellow")
            {
                BlasterClone.GetComponent<Renderer>().material = YellowMat;
            }
            if (GetComponent<BodyGlow>().Color == "Red")
            {
                BlasterClone.GetComponent<Renderer>().material = RedMat;
            }
            if (GetComponent<BodyGlow>().Color == "Blue")
            {
                BlasterClone.GetComponent<Renderer>().material = BlueMat;
            }
            if (GetComponent<BodyGlow>().Color == "Purple")
            {
                BlasterClone.GetComponent<Renderer>().material = PurpleMat;
            }

            StartCoroutine("DestroyBlaster", BlasterClone);
        }
        else
        {
            Shoot = false;
        }




        // BODY LERP
        if (Shoot || Anim.GetCurrentAnimatorStateInfo(1).IsName("Shoot") || Anim.IsInTransition(1))
        {
            if (BodyLerp < 1)
            {
                BodyLerp += 5f * Time.deltaTime;
            }
        }

        if (Shoot && Charged|| Anim.GetCurrentAnimatorStateInfo(1).IsName("Charged Shoot") || Charged && Anim.IsInTransition(1))
        {
            if (BodyLerpLeft < 1)
            {
                BodyLerpLeft += 5f * Time.deltaTime;
            }
        }

        if (Anim.GetCurrentAnimatorStateInfo(1).IsName("None") && Anim.GetCurrentAnimatorStateInfo(1).normalizedTime > .5f && !Anim.IsInTransition(1))
        {
            if (BodyLerp > 0)
            {
                BodyLerp -= 1.5f * Time.deltaTime;
            }

            if (BodyLerpLeft > 0)
            {
                BodyLerpLeft -= 1.5f * Time.deltaTime;
            }
        }

        if (GetComponent<MeleeAttack>().Attack || GetComponent<BarGraphs>().SummonBarGraph || GetComponent<CombineSouls>().CombineAnimation || GetComponent<CombineSouls>().DivideAttack || GetComponent<SummonMech>().SummonMechAnimation)
        {
            BodyLerp = 0;
            BodyLerpLeft = 0;
            Shoot = false;
        }






        // AIM
        if (Input.GetKey(KeyCode.Mouse1))
        {
            Aim = true;
            CombatMode = true;
            CombatModeMelee = false;
        }
        else 
        {
            Aim = false;
        }
    }

    void OnAnimatorIK(int layerIndex)
    {
        if (CombatMode)
        {
            Anim.SetIKPositionWeight(AvatarIKGoal.RightHand, BodyLerp *.45f);
            Anim.SetIKRotationWeight(AvatarIKGoal.RightHand,BodyLerp);
            Anim.SetIKPosition(AvatarIKGoal.RightHand, new Vector3(transform.position.x + transform.right.x *.45f, transform.position.y + 1.5f, transform.position.z + transform.right.z * .45f) + new Vector3(MouseRay.direction.x, MouseRay.direction.y, MouseRay.direction.z));
            Anim.SetIKRotation(AvatarIKGoal.RightHand, Camera.main.transform.rotation * new Quaternion(-1, 0, 0, 1));


            Anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, BodyLerpLeft * .45f);
            Anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, BodyLerpLeft);
            Anim.SetIKPosition(AvatarIKGoal.LeftHand, new Vector3(transform.position.x + transform.right.x * .45f, transform.position.y + 1.5f, transform.position.z + transform.right.z * .45f) + new Vector3(MouseRay.direction.x, MouseRay.direction.y, MouseRay.direction.z));
            Anim.SetIKRotation(AvatarIKGoal.LeftHand, Camera.main.transform.rotation * new Quaternion(-1, 0, 0, 1));
            

            Anim.SetLookAtWeight(1, 0, BodyLerp, BodyLerp, .5f); //Global Weight, Body Weight, Head Weight, Eyes Weight, Clamp Weight
            Anim.SetLookAtPosition(new Vector3(transform.position.x, transform.position.y + 1.4f, transform.position.z) + new Vector3(MouseRay.direction.x, MouseRay.direction.y, MouseRay.direction.z));



            //Anim.SetLookAtWeight(1, BodyLerp, BodyLerp, BodyLerp, .5f);//Global Weight, Body Weight, Head Weight, Eyes Weight, Clamp Weight
            //Anim.SetLookAtPosition(new Vector3(transform.position.x, transform.position.y + 1.4f, transform.position.z) + new Vector3(MouseRay.direction.x, MouseRay.direction.y, MouseRay.direction.z));
        }
    }

    IEnumerator DestroyBlaster(GameObject Blaster)
    {
        yield return new WaitForSeconds(10);
        Destroy(Blaster);
    }

}
