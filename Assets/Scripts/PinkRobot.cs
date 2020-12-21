using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkRobot : MonoBehaviour
{
    GameObject Player;
    Rigidbody RigBod;
    Vector3 TargetDirection;
    Animator Anim;
    public float TurnSpeed = 5;
    public float RunSpeed = 2;
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        Anim = GetComponent<Animator>();
        RigBod = GetComponent <Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        TargetDirection = Player.transform.position  - transform.position;
        TargetDirection.y = 0;

        if (Anim.GetCurrentAnimatorStateInfo(0).IsTag("Run") && Vector3.Distance(transform.position, Player.transform.position) > 5)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, TargetDirection, TurnSpeed * Time.deltaTime, 0));
            RigBod.velocity = transform.forward * RunSpeed;
        }
        else
        {
            RigBod.velocity = Vector3.zero;
        }


    }
}
