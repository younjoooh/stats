using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TierSouls : MonoBehaviour
{
    float Speed = 300;
    Vector3 Angle1;
    Vector3 Angle2;
    Vector3 Angle3;
    GameObject Soul1;
    GameObject Soul2;
    GameObject Soul3;
    Transform UpperChest;
    Animator Anim;
    void Start()
    {
       
        // GET ANIMATOR
        Anim = GetComponent<Animator>();

        // UpperChest
        UpperChest = Anim.GetBoneTransform(HumanBodyBones.UpperChest);
        UpperChest.gameObject.SetActive(false);
        UpperChest.gameObject.SetActive(true);
        UpperChest.gameObject.layer = 2;
        UpperChest.gameObject.name = "UpperChest";


        if (GetComponent<Giant>()!= null)
        {

            // GET SOUL
            Soul1 = Instantiate(Resources.Load<GameObject>("Prefabs/Soul " + tag), UpperChest.transform.position + UpperChest.forward * 2.5f, Quaternion.identity);
            //SET PARENT
            Soul1.transform.parent = UpperChest;

            if (GetComponent<Giant>().Tier >= 2)
            {
                Soul2 = Instantiate(Resources.Load<GameObject>("Prefabs/Soul " + tag), UpperChest.transform.position - UpperChest.forward * 2.5f, Quaternion.identity);
                //SET PARENT
                Soul2.transform.parent = UpperChest;
            }

            if (GetComponent<Giant>().Tier == 3)
            {
                Soul3 = Instantiate(Resources.Load<GameObject>("Prefabs/Soul " + tag), UpperChest.transform.position - UpperChest.forward * 2.5f, Quaternion.identity);
                //SET PARENT
                Soul3.transform.parent = UpperChest;
            }
        }




        if (GetComponent<LostSpirit>() != null)
        {
            // GET SOUL
            Soul1 = Instantiate(Resources.Load<GameObject>("Prefabs/Soul " + tag), UpperChest.transform.position + UpperChest.forward * .5f * transform.localScale.x, Quaternion.identity);
            //SET PARENT
            Soul1.transform.parent = UpperChest;
            Soul1.transform.localScale = new Vector3(.0025f, .0025f, .0025f);
            Soul1.transform.GetChild(0).localScale = Soul1.transform.localScale * 250f; //Particle
            Soul1.transform.GetChild(0).transform.GetChild(1).localScale = Soul1.transform.localScale * 150f;

            if (GetComponent<LostSpirit>().Tier >= 2)
            {
                Soul2 = Instantiate(Resources.Load<GameObject>("Prefabs/Soul " + tag), UpperChest.transform.position - UpperChest.forward * .5f * transform.localScale.x, Quaternion.identity);
                //SET PARENT
                Soul2.transform.parent = UpperChest;
                Soul2.transform.localScale = new Vector3(.0025f, .0025f, .0025f);
                Soul2.transform.GetChild(0).localScale = Soul2.transform.localScale * 250f;
                Soul2.transform.GetChild(0).transform.GetChild(1).localScale = Soul2.transform.localScale * 150f;
            }

            if (GetComponent<LostSpirit>().Tier == 3)
            {
                Soul3 = Instantiate(Resources.Load<GameObject>("Prefabs/Soul " + tag), UpperChest.transform.position - UpperChest.forward * .5f * transform.localScale.x, Quaternion.identity);
                //SET PARENT
                Soul3.transform.parent = UpperChest;
                Soul3.transform.localScale = new Vector3(.0025f, .0025f, .0025f);
                Soul3.transform.GetChild(0).localScale = Soul3.transform.localScale * 150f;
                Soul3.transform.GetChild(0).transform.GetChild(1).localScale = Soul3.transform.localScale * 150f;
            }
        }




        if (GetComponent<Parasite>() != null)
        {
            // GET SOUL
            Soul1 = Instantiate(Resources.Load<GameObject>("Prefabs/Soul " + tag), UpperChest.transform.position + UpperChest.forward * .5f * transform.localScale.x, Quaternion.identity);
            //SET PARENT
            Soul1.transform.parent = UpperChest;
            Soul1.transform.localScale = new Vector3(.25f, .25f, .25f);
            Soul1.transform.GetChild(0).localScale = Soul1.transform.localScale * 1.6f;
            Soul1.transform.GetChild(0).transform.GetChild(1).localScale = Soul1.transform.localScale * 2f;

            if (GetComponent<Parasite>().Tier >= 2)
            {
                Soul2 = Instantiate(Resources.Load<GameObject>("Prefabs/Soul " + tag), UpperChest.transform.position - UpperChest.forward * .5f * transform.localScale.x, Quaternion.identity);
                //SET PARENT
                Soul2.transform.parent = UpperChest;
                Soul2.transform.localScale = new Vector3(.25f, .15f, .25f);
                Soul2.transform.GetChild(0).localScale = Soul2.transform.localScale * 1.6f;
                Soul2.transform.GetChild(0).transform.GetChild(1).localScale = Soul2.transform.localScale * 2f; ;
            }

            if (GetComponent<Parasite>().Tier == 3)
            {
                Soul3 = Instantiate(Resources.Load<GameObject>("Prefabs/Soul " + tag), UpperChest.transform.position - UpperChest.forward * .5f * transform.localScale.x, Quaternion.identity);
                //SET PARENT
                Soul3.transform.parent = UpperChest;
                Soul3.transform.localScale = new Vector3(.25f, .25f, .25f);
                Soul3.transform.GetChild(0).localScale = Soul3.transform.localScale * 1.6f;
                Soul3.transform.GetChild(0).transform.GetChild(1).localScale = Soul3.transform.localScale * 12f;
            }
        }

        UpperChest.GetChild(3).gameObject.tag = "Tier Soul";
    }

    void Update()
    {
        // SET ANGLE 1
        Angle1 = -UpperChest.right + UpperChest.up;
        Soul1.transform.RotateAround(UpperChest.position, Angle1, Speed * Time.deltaTime);


        if (GetComponent<Giant>() != null)
        {
            // SET ANGLE 2
            if (GetComponent<Giant>().Tier >= 2)
            {
                Angle2 = -UpperChest.right - UpperChest.up;
                Soul2.transform.RotateAround(UpperChest.position, Angle2, Speed * Time.deltaTime);
            }

            // SET ANGLE 3
            if (GetComponent<Giant>().Tier == 3)
            {
                Angle3 = UpperChest.up;
                Soul3.transform.RotateAround(UpperChest.position, Angle3, Speed * Time.deltaTime);
            }
        }



        if (GetComponent<LostSpirit>() != null)
        {
            // SET ANGLE 2
            if (GetComponent<LostSpirit>().Tier >= 2)
            {
                Angle2 = -UpperChest.right - UpperChest.up;
                Soul2.transform.RotateAround(UpperChest.position, Angle2, Speed * Time.deltaTime);
            }

            // SET ANGLE 3
            if (GetComponent<LostSpirit>().Tier == 3)
            {
                Angle3 = UpperChest.up;
                Soul3.transform.RotateAround(UpperChest.position, Angle3, Speed * Time.deltaTime);
            }
        }



        if (GetComponent<Parasite>() != null)
        {
            // SET ANGLE 2
            if (GetComponent<Parasite>().Tier >= 2)
            {
                Angle2 = -UpperChest.right - UpperChest.up;
                Soul2.transform.RotateAround(UpperChest.position, Angle2, Speed * Time.deltaTime);
            }

            // SET ANGLE 3
            if (GetComponent<Parasite>().Tier == 3)
            {
                Angle3 = UpperChest.up;
                Soul3.transform.RotateAround(UpperChest.position, Angle3, Speed * Time.deltaTime);
            }
        }
    }
}
