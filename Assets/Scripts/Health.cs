using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int HealthPoints = 100;

    [HideInInspector]
    public bool KnockedBack, Recovered = true;

    [HideInInspector]
    public int KnockedBackCycle = 1;

    [HideInInspector]
    public GameObject Enemy;

    Animator Anim;

    Transform Chest;
    CapsuleCollider ChestCollider;

    Transform Spine;
    CapsuleCollider SpineCollider;

    Transform UpperChest;
    CapsuleCollider UpperChestCollider;

    Transform Hips;
    CapsuleCollider HipsCollider;

    Transform Head;
    CapsuleCollider HeadCollider;

    public float ModelMultiplier = .01f;
    void Start()
    {
        Anim = GetComponent<Animator>();

        // HIPS
        Hips = Anim.GetBoneTransform(HumanBodyBones.Hips);
        Hips.gameObject.SetActive(false);


        HipsCollider = Hips.gameObject.AddComponent(typeof(CapsuleCollider)) as CapsuleCollider;
        HipsCollider.center = new Vector3(0,0,0) * ModelMultiplier;
        HipsCollider.radius = .175f * ModelMultiplier; ;
        HipsCollider.height = 0f * ModelMultiplier; ;
        HipsCollider.direction = 1;
        HipsCollider.isTrigger = true;


        Hips.gameObject.SetActive(true);
        Hips.gameObject.layer = 2;
        Hips.gameObject.name = "Hips";




        // SPINE
        Spine = Anim.GetBoneTransform(HumanBodyBones.Spine);
        Spine.gameObject.SetActive(false);

        SpineCollider = Spine.gameObject.AddComponent(typeof(CapsuleCollider)) as CapsuleCollider;
        SpineCollider.center = new Vector3(0, 0, 0) * ModelMultiplier;
        SpineCollider.radius = .15f * ModelMultiplier;
        SpineCollider.height = 0f * ModelMultiplier;
        SpineCollider.direction = 1;
        SpineCollider.isTrigger = true;


        Spine.gameObject.SetActive(true);
        Spine.gameObject.layer = 2;
        Spine.gameObject.name = "Spine";




        //CHEST
        Chest = Anim.GetBoneTransform(HumanBodyBones.Chest);
        Chest.gameObject.SetActive(false);

        ChestCollider = Chest.gameObject.AddComponent(typeof(CapsuleCollider)) as CapsuleCollider;
        ChestCollider.center = new Vector3(0,0,0) * ModelMultiplier;
        ChestCollider.radius = .14f * ModelMultiplier;
        ChestCollider.height = .3f * ModelMultiplier;
        ChestCollider.direction = 1;
        ChestCollider.isTrigger = true;


        Chest.gameObject.SetActive(true);
        Chest.gameObject.layer = 2;
        Chest.gameObject.name = "Chest";




        // UPPER CHEST
        UpperChest = Anim.GetBoneTransform(HumanBodyBones.UpperChest);
        UpperChest.gameObject.SetActive(false);


        UpperChestCollider = UpperChest.gameObject.AddComponent(typeof(CapsuleCollider)) as CapsuleCollider;
        UpperChestCollider.center = new Vector3(0,0,0) * ModelMultiplier;
        UpperChestCollider.radius = .175f * ModelMultiplier;
        UpperChestCollider.height = .4f * ModelMultiplier;
        UpperChestCollider.direction = 1;
        UpperChestCollider.isTrigger = true;


        UpperChest.gameObject.SetActive(true);
        UpperChest.gameObject.layer = 2;
        UpperChest.gameObject.name = "Upper Chest";



        // HEAD
        Head = Anim.GetBoneTransform(HumanBodyBones.Head);
        Head.gameObject.SetActive(false);


        HeadCollider = Head.gameObject.AddComponent(typeof(CapsuleCollider)) as CapsuleCollider;
        HeadCollider.center = new Vector3(0, .09f, 0) * ModelMultiplier;
        HeadCollider.radius = .125f * ModelMultiplier;
        HeadCollider.height = .275f * ModelMultiplier;
        HeadCollider.direction = 1;
        HeadCollider.isTrigger = true;




        Head.gameObject.SetActive(true);
        Head.gameObject.layer = 2;
        Head.gameObject.name = "Head";
    }

    void Update()
    {
        if (Anim.GetCurrentAnimatorStateInfo(0).IsTag("Knocked Back") && Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= .9f && !Anim.IsInTransition(0)
            ||Recovered)
        {
            KnockedBack = false;
        }

        Anim.SetBool("Knocked Back", KnockedBack);
        Anim.SetInteger("Knocked Back Cycle", KnockedBackCycle);


        // GROUNDED
        if (Physics.SphereCast(transform.position + new Vector3(0, 3,0), 2, -transform.up, out RaycastHit GroundHit, 3f)
            && GroundHit.transform.gameObject.tag != "Player")
        {
            if (GroundHit.transform.gameObject.name == "Parasite")
            {
                Enemy = GroundHit.transform.gameObject;
            }
            else
            {
                Enemy = null;
            }
        }
        else
        {
            Enemy = null;
        }
    }

    IEnumerator Recovery()
    {
        yield return new WaitForSeconds(.5f);
        Recovered = true;
    }

}
