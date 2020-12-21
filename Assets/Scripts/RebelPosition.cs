using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RebelPosition : MonoBehaviour
{
    Animator Anim;
    float HandLerpRight = 1, HandLerpLeft = 1;
    Transform LeftHand, RightHand;
    void Start()
    {
        Anim = GetComponent<Animator>();
        LeftHand = Anim.GetBoneTransform(HumanBodyBones.LeftHand);
        RightHand = Anim.GetBoneTransform(HumanBodyBones.RightHand);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.name == ("Male Red"))
        {
            transform.position = new Vector3(transform.position.x, .075f, 28.62f);
        }

        if (gameObject.name == ("Female Red"))
        {
            transform.position = new Vector3(-0.04f, transform.position.y, transform.position.z);
        }

        if (gameObject.name == ("Female Purple"))
        {
            if (Anim.GetCurrentAnimatorStateInfo(0).IsName("Wall Flip") || Anim.GetCurrentAnimatorStateInfo(0).IsName("Front Flip") || Anim.GetCurrentAnimatorStateInfo(0).IsName("Northern Split") && !Anim.IsInTransition(0)) 
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(-2.56f, transform.position.y, 34.45f), 1 * Time.deltaTime);
            }

            if (Anim.GetCurrentAnimatorStateInfo(0).IsName("Wall Flip 180") || Anim.GetCurrentAnimatorStateInfo(0).IsName("Front Flip 180") || Anim.GetCurrentAnimatorStateInfo(0).IsName("Northern Split 180") && !Anim.IsInTransition(0))
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(4.421f, transform.position.y, 34.45f), 1 * Time.deltaTime);
            }
        }
    }

    void OnAnimatorIK(int layerIndex)
    {
        if (gameObject.name == ("Male Red"))
        {
            Anim.SetIKPositionWeight(AvatarIKGoal.RightHand, HandLerpRight);
            Anim.SetIKRotationWeight(AvatarIKGoal.RightHand, HandLerpRight);
            Anim.SetIKPosition(AvatarIKGoal.RightHand, new Vector3(RightHand.position.x, RightHand.position.y, 28.725f));
            //Anim.SetIKRotation(AvatarIKGoal.RightHand, Camera.main.transform.rotation * new Quaternion(-1, 0, 0, 1));


            Anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, HandLerpLeft);
            Anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, HandLerpLeft);
            Anim.SetIKPosition(AvatarIKGoal.LeftHand, new Vector3(LeftHand.position.x, LeftHand.position.y, 28.725f));
            //Anim.SetIKRotation(AvatarIKGoal.LeftHand, Camera.main.transform.rotation * new Quaternion(-1, 0, 0, 1))
        }
    }
}
