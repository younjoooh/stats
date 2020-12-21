using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMachanics : MonoBehaviour
{
    GameObject Target;
    GameObject HumanModel;
    Rigidbody RigBod;
    Animator Anim;
    float Speed = 5.03f;
    bool Climb;

    [HideInInspector]
    public bool Climbing;

    void Start()
    {
        Target = new GameObject("Target");
        HumanModel = transform.GetChild(0).gameObject;
        RigBod = GetComponent<Rigidbody>();
        Anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown("p") && GetComponent<Movement>().FacingObject != null) 
        {
            Target.transform.position = new Vector3(GetComponent<Movement>().FacingObject.transform.position.x, GetComponent<Movement>().FacingObject.transform.lossyScale.y, GetComponent<Movement>().FacingObject.transform.position.z);
            Climbing = true;
            Climb = true;
            Speed = 5.03f;
        }

        if (Climbing == true)
        {
           RigBod.velocity = new Vector3(0, 0, 0);

            Vector3 TargetDirection = Target.transform.position - transform.position;
            TargetDirection.y = 0;
            transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, TargetDirection, Speed * Time.deltaTime, 0.0f));

            if (GetComponent<Movement>().FacingObject != null && transform.position.y != Target.transform.position.y)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, Target.transform.position.y + .1f, transform.position.z), Speed * Time.deltaTime);
            }

            else
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(Target.transform.position.x, Target.transform.position.y, Target.transform.position.z), Speed * Time.deltaTime);
            }

            if (transform.position == Target.transform.position && Climbing)
            {
                Climbing = false;
            }

            Anim.SetBool("Climb", Climb);


            if (Anim.GetCurrentAnimatorStateInfo(0).IsName("Climb"))
            {
                Climb = false;

                if (!Anim.IsInTransition(0) && Anim.GetCurrentAnimatorStateInfo(0).normalizedTime < .2f)
                {
                    Speed = 5.03f;
                    HumanModel.transform.localPosition = Vector3.MoveTowards(HumanModel.transform.localPosition, new Vector3(0, -1, 0f), 2.5f * Time.deltaTime);
                }

                if (Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= .2f && Anim.GetCurrentAnimatorStateInfo(0).normalizedTime < .35f)
                {
                    Speed = 5.03f;
                    HumanModel.transform.localPosition = Vector3.MoveTowards(HumanModel.transform.localPosition, new Vector3(0, -1, .17f), .75f * Time.deltaTime);
                }

                if (Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= .35f && Anim.GetCurrentAnimatorStateInfo(0).normalizedTime <= .5f)
                {
                    Speed = 1.75f;
                    HumanModel.transform.localPosition = Vector3.MoveTowards(HumanModel.transform.localPosition, new Vector3(0, -1, .375f), 1 * Time.deltaTime);
                    HumanModel.transform.localEulerAngles = Vector3.MoveTowards(HumanModel.transform.localEulerAngles, new Vector3(20, 0, 0), 30f * Time.deltaTime);
                    Camera.main.transform.localPosition = Vector3.MoveTowards(Camera.main.transform.localPosition, new Vector3(0, .75f, -2f), 1f * Time.deltaTime);
                }

                if (Anim.GetCurrentAnimatorStateInfo(0).normalizedTime > .5f)
                {
                    Speed = 5.03f;
                    HumanModel.transform.localPosition = Vector3.MoveTowards(HumanModel.transform.localPosition, new Vector3(0, -1.3f, .35f), 2 * Time.deltaTime);
                    HumanModel.transform.localEulerAngles = Vector3.MoveTowards(HumanModel.transform.localEulerAngles, new Vector3(20, 0, 0), 35f * Time.deltaTime);
                    Camera.main.transform.localPosition = Vector3.MoveTowards(Camera.main.transform.localPosition, new Vector3(0, .75f, -2f), 1f * Time.deltaTime);
                }

            }
            

        }

        if (!(Anim.GetCurrentAnimatorStateInfo(0).IsName("Climb") && !Anim.IsInTransition(0)))
        {
            HumanModel.transform.localPosition = Vector3.MoveTowards(HumanModel.transform.localPosition, new Vector3(0, -1f, 0f), 1f * Time.deltaTime);
            HumanModel.transform.localEulerAngles = Vector3.MoveTowards(HumanModel.transform.localEulerAngles, new Vector3(0, 0, 0), 200f * Time.deltaTime);
            Camera.main.transform.localPosition = Vector3.MoveTowards(Camera.main.transform.localPosition, GetComponent<Camera3D>().CameraPosition, 1f * Time.deltaTime);
        }
    }

}
