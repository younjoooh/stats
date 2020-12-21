using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [HideInInspector]
    public bool Attack;
    [HideInInspector]
    public bool WeaponEquipped = true;

    Animator Anim;
    public float AttackDamage = 5;
    public float WeaponDamage = 15;

    Transform LeftFoot;
    CapsuleCollider LeftFootCollider;

    Transform RightHand;
    CapsuleCollider RightHandCollider;

    [HideInInspector]
    public GameObject Sword;

    public int ComboCount = 1;
    public bool CanComboCount = false;
    void Start()
    {
        Anim = GetComponent<Animator>();

        //LEFT FOOT
        LeftFoot = Anim.GetBoneTransform(HumanBodyBones.LeftFoot);
        LeftFoot.gameObject.SetActive(false);
        LeftFootCollider = LeftFoot.gameObject.AddComponent(typeof(CapsuleCollider)) as CapsuleCollider;
        LeftFootCollider.center = new Vector3 (0,-.05f,.075f) * GetComponent<Health>().ModelMultiplier;
        LeftFootCollider.radius = .075f * GetComponent<Health>().ModelMultiplier;
        LeftFootCollider.height = .35f * GetComponent<Health>().ModelMultiplier;
        LeftFootCollider.direction = 2;
        LeftFootCollider.isTrigger = true;
        LeftFoot.gameObject.SetActive(true);
        LeftFoot.gameObject.layer = 2;
        LeftFoot.gameObject.name = "Left Foot";
        LeftFoot.gameObject.AddComponent<MeleeCollision>();
        LeftFoot.gameObject.GetComponent<MeleeCollision>().Player = this.gameObject;




        // RIGHT HAND
        RightHand = Anim.GetBoneTransform(HumanBodyBones.RightHand);
        RightHand.gameObject.SetActive(false);

        if (WeaponEquipped)
        {
            RightHandCollider = RightHand.GetChild(1).gameObject.AddComponent(typeof(CapsuleCollider)) as CapsuleCollider;
            RightHand.GetChild(1).gameObject.name = "Right Hand";
            RightHandCollider.center = new Vector3(.007f, 0f, -.0003f); // *GetComponent<Health>().ModelMultiplier;
            //RightHandCollider.radius = .0005f; // * GetComponent<Health>().ModelMultiplier;
            RightHandCollider.radius = .001f; // * GetComponent<Health>().ModelMultiplier;
            RightHandCollider.height = .015f; // * GetComponent<Health>().ModelMultiplier;
            RightHandCollider.direction = 0;
            RightHandCollider.isTrigger = true;
        }
        /*
        else
        {
            RightHandCollider = RightHand.gameObject.AddComponent(typeof(CapsuleCollider)) as CapsuleCollider;
            RightHandCollider.center = new Vector3(.075f, -.025f, 0f) * GetComponent<Health>().ModelMultiplier;
            RightHandCollider.radius = .07f * GetComponent<Health>().ModelMultiplier;
            RightHandCollider.height = .17f * GetComponent<Health>().ModelMultiplier;
            RightHandCollider.direction = 0;
            RightHandCollider.isTrigger = true;
        }
        */

        RightHand.GetChild(1).gameObject.SetActive(true);
        RightHand.GetChild(1).gameObject.layer = 2;
        RightHand.GetChild(1).gameObject.name = "Right Hand";
        RightHand.GetChild(1).gameObject.AddComponent<MeleeCollision>();
        RightHand.GetChild(1).gameObject.GetComponent<MeleeCollision>().Player = this.gameObject;




        // CREATE SWORD
        Sword = Instantiate(Resources.Load<GameObject>("Prefabs/Sword"), RightHand.GetChild(1).position, Quaternion.identity);
        Sword.transform.parent = RightHand.GetChild(1).transform;
        Sword.transform.localPosition = new Vector3(0.00587f, -0.00015f, -0.00029f); //  * GetComponent<Health>().ModelMultiplier;
        Sword.transform.localEulerAngles = new Vector3(179.996f, -0.003997803f, -89.16101f);
        Sword.transform.localScale = new Vector3(0.0003086175f, 0.007009106f, 0.0003086182f);
        Sword.SetActive(false);
    }

    void Update()
    {

        Anim.SetInteger("Combo Count", ComboCount);

        Anim.SetBool("Weapon Equipped", WeaponEquipped);
        if (Input.GetKeyDown("q") && !Anim.GetCurrentAnimatorStateInfo(0).IsTag("Melee Attack") && !(Anim.GetCurrentAnimatorStateInfo(0).IsTag("Melee Attack End") && Anim.IsInTransition(0)) && GetComponent<Health>().Recovered)
        {
            Attack = true;
            Sword.SetActive(true);
            CanComboCount = true;
        }

        if (Input.GetKeyDown("q") && Anim.GetCurrentAnimatorStateInfo(0).IsTag("Melee Attack End") && !Anim.GetCurrentAnimatorStateInfo(0).IsName("Melee Attack End 3") && !Anim.IsInTransition(0) && CanComboCount)
        {
            ComboCount += 1;
            CanComboCount = false;
        }

        /*
        if (ComboCount ==4)
        {
            ComboCount = 1;
        }
        */

        if (Anim.GetCurrentAnimatorStateInfo(0).IsTag("Melee Attack End") && Anim.GetCurrentAnimatorStateInfo(0).IsName("Melee Attack End 3") )
        {
            ComboCount =1;
            CanComboCount = false;
        }



        if (Sword.activeSelf)
        {
            if (GetComponent<BodyGlow>().Color == "Green")
            {
                Sword.transform.GetChild(0).GetComponent<Renderer>().material = GetComponent<Shooting>().GreenMat;
            }
            if (GetComponent<BodyGlow>().Color == "Yellow")
            {
                Sword.transform.GetChild(0).GetComponent<Renderer>().material = GetComponent<Shooting>().YellowMat;
            }
            if (GetComponent<BodyGlow>().Color == "Red")
            {
                Sword.transform.GetChild(0).GetComponent<Renderer>().material = GetComponent<Shooting>().RedMat;
            }
            if (GetComponent<BodyGlow>().Color == "Blue")
            {
                Sword.transform.GetChild(0).GetComponent<Renderer>().material = GetComponent<Shooting>().BlueMat;
            }
            if (GetComponent<BodyGlow>().Color == "Purple")
            {
                Sword.transform.GetChild(0).GetComponent<Renderer>().material = GetComponent<Shooting>().PurpleMat;
            }
        }


        if (Attack && GetComponent<FindClosestParasite>().Parasite == null)
        {
            Vector3 TargetDirection = transform.position - Camera.main.transform.position;
            TargetDirection.y = 0;
            transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, TargetDirection, 10 * Time.deltaTime, 0));
        }

        if (Anim.GetCurrentAnimatorStateInfo(0).IsName ("Divide Attack 1") || Anim.GetCurrentAnimatorStateInfo(0).IsName("Divide Attack 2"))
        {
            GetComponent<Rigidbody>().velocity = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z) * 7.5f;
        }

        if (GetComponent<FindClosestParasite>().Parasite != null)
        {
            if ((Attack || Anim.GetCurrentAnimatorStateInfo(0).IsTag("Melee Attack") || Anim.GetCurrentAnimatorStateInfo(0).IsName("Melee Attack End 3") )
                && Vector3.Distance(transform.position, GetComponent<FindClosestParasite>().Parasite.transform.position) > .75f && GetComponent<FindClosestParasite>().Parasite != null)
            {
                transform.LookAt(GetComponent<FindClosestParasite>().Parasite.transform.position);
                transform.position = Vector3.MoveTowards(transform.position, GetComponent<FindClosestParasite>().Parasite.transform.position, 3.75f * Time.deltaTime);
            }
        }

        Anim.SetBool("Attack", Attack);

        if (Anim.GetCurrentAnimatorStateInfo(0).IsName("Weapon Attack") || Anim.GetCurrentAnimatorStateInfo(0).IsName("Weapon Attack 2") || Anim.GetCurrentAnimatorStateInfo(0).IsName("Weapon Attack 3") || Anim.GetCurrentAnimatorStateInfo(0).IsName("Weapon Attack 4") 
         || Anim.GetCurrentAnimatorStateInfo(0).IsName("Weapon Attack 5") || Anim.GetCurrentAnimatorStateInfo(0).IsName("Weapon Attack 6") || Anim.GetCurrentAnimatorStateInfo(0).IsName("Weapon Attack 7") || Anim.GetCurrentAnimatorStateInfo(0).IsName("Weapon Attack 8"))
        {
            Attack = false;
            if (Anim.GetCurrentAnimatorStateInfo(0).normalizedTime <= .5f)
            {
                Vector3 TargetDirection = transform.position - Camera.main.transform.position;
                TargetDirection.y = 0;
                transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, TargetDirection, 10 * Time.deltaTime, 0));
            }
        }


        if (Anim.GetCurrentAnimatorStateInfo(0).IsTag("Melee Attack") && Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >.99f) 
        {
            //StopCoroutine("RemoveSword");
            //StartCoroutine("RemoveSword");
        }

        /*
        if (WeaponEquipped)
        {
            RightHandCollider.center = new Vector3(.09f, -.02f, .85f) * GetComponent<Health>().ModelMultiplier;
            RightHandCollider.radius = .065f * GetComponent<Health>().ModelMultiplier;
            RightHandCollider.height = 2f * GetComponent<Health>().ModelMultiplier;
            RightHandCollider.direction = 2;
            RightHandCollider.isTrigger = true;
        }
        else
        {
            RightHandCollider.center = new Vector3(.075f, -.025f, 0f) * GetComponent<Health>().ModelMultiplier;
            RightHandCollider.radius = .07f * GetComponent<Health>().ModelMultiplier;
            RightHandCollider.height = .17f * GetComponent<Health>().ModelMultiplier;
            RightHandCollider.direction = 0;
            RightHandCollider.isTrigger = true;
        }
        */

    }

    IEnumerator RemoveSword()
    {
        yield return new WaitForSeconds(5);
        Sword.SetActive(false);
    }

}
