using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float RunSpeed = 5;
    public float JumpHeight = 5;
    public float TurnSpeed = 5;

    float TurnAngle;

    Vector3 ObjectSize;
    Animator Anim;
    Rigidbody RigBod;
    Vector3 TargetDirection;
    Vector3 ShootTargetDirection;

    [HideInInspector]
    public bool Grounded;

    [HideInInspector]
    public GameObject FacingObject;

    [HideInInspector]
    public bool Idle, Jump, Land, Run, Stop, TurnLeft, TurnRight, Drop, RunLeft, RunRight, RunForward, RunBack, RunBackLeft, RunBackRight, RunForwardLeft, RunForwardRight, DashForward, DashBack, DashLeft, DashRight, DashForwardPrep, DashBackPrep, DashLeftPrep, DashRightPrep;

    [HideInInspector]
    public GameObject TriggerHit, ColliderHit;

    void Start()
    {
        // ADD RIGIDBODY
        if (GetComponent<Rigidbody>() == null)
        {
            RigBod = gameObject.AddComponent<UnityEngine.Rigidbody>();
        }
        RigBod = GetComponent<Rigidbody>();
        RigBod.constraints = RigidbodyConstraints.FreezeRotation;

        // SET TAG
        gameObject.tag = "Player";
        Transform[] AllChildren = gameObject.GetComponentsInChildren<Transform>();
        foreach (Transform Child in AllChildren)
        {
            Child.gameObject.tag = "Player";
        }

        // GET ANIMATOR
        Anim = GetComponent<Animator>();

        // GET OBJECT SIZE
        ObjectSize = GetComponent<Renderer>().bounds.size;

        // ADD CAMERA SCRIPT
        if (GetComponent<Camera3D>() == null)
        {
            gameObject.AddComponent<Camera3D>();
        }
    }

    void Update()
    {
        // TARGET DIRECTION
        if (Input.GetKey("d") && !Input.GetKey("a") && !Input.GetKey("w") && !Input.GetKey("s"))
        {
            TargetDirection = Camera.main.transform.right;
        }

        if (Input.GetKey("a") && !Input.GetKey("d") && !Input.GetKey("w") && !Input.GetKey("s"))
        {
            TargetDirection = -Camera.main.transform.right;
        }

        if (Input.GetKey("w") && !Input.GetKey("s") && !Input.GetKey("d") && !Input.GetKey("a"))
        {
            TargetDirection = Camera.main.transform.forward;
        }

        if (Input.GetKey("s") && !Input.GetKey("w") && !Input.GetKey("d") && !Input.GetKey("a"))
        {
            TargetDirection = -Camera.main.transform.forward;
        }




        if (Input.GetKey("w") && Input.GetKey("d") && !Input.GetKey("a") && !Input.GetKey("s"))
        {
            TargetDirection = Camera.main.transform.forward + Camera.main.transform.right;
        }

        if (Input.GetKey("w") && Input.GetKey("a") && !Input.GetKey("d") && !Input.GetKey("s"))
        {
            TargetDirection = Camera.main.transform.forward - Camera.main.transform.right;
        }

        if (Input.GetKey("s") && Input.GetKey("d") && !Input.GetKey("a") && !Input.GetKey("w"))
        {
            TargetDirection = -Camera.main.transform.forward + Camera.main.transform.right;
        }

        if (Input.GetKey("s") && Input.GetKey("a") && !Input.GetKey("d") && !Input.GetKey("w"))
        {
            TargetDirection = -Camera.main.transform.forward - Camera.main.transform.right;
        }



        /*
        // DASH FORWARD
        if (Input.GetKeyUp("w") && Grounded && GetComponent<Health>().Recovered)
        {
            StopCoroutine("PrepDashForward");
            StartCoroutine("PrepDashForward");
        }

        if (Input.GetKeyDown("w") && DashForwardPrep && Grounded && GetComponent<Health>().Recovered)
        {
            GetComponent<Shooting>().CombatMode = true;
            DashForward = true;
            Run = false;
        }

        if (DashForward)
        {
            if (GetComponent<Health>().Enemy != null)
            {
                TargetDirection = GetComponent<Health>().Enemy.transform.position - transform.position;
                TargetDirection.y = 0;
                transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, TargetDirection, TurnSpeed * 5 * Time.deltaTime, 0));
                RigBod.AddForce(transform.forward * 30 * Time.deltaTime, ForceMode.VelocityChange);
            }
            else
            {
                RigBod.AddForce(transform.forward * 30 * Time.deltaTime, ForceMode.VelocityChange);
            }
        }

        if (Anim.GetCurrentAnimatorStateInfo(0).IsName("Dash Forward") && !Anim.IsInTransition(0))
        {
            if (GetComponent<Health>().Enemy != null)
            {
                TargetDirection = GetComponent<Health>().Enemy.transform.position - transform.position;
                TargetDirection.y = 0;
                transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, TargetDirection, TurnSpeed * 5 * Time.deltaTime, 0));
                RigBod.AddForce(transform.forward * 10 * Time.deltaTime, ForceMode.VelocityChange);
            }
            else
            {
                RigBod.AddForce(transform.forward * 10 * Time.deltaTime, ForceMode.VelocityChange);
            }
            DashForward = false;
        }

        if (Anim.GetCurrentAnimatorStateInfo(0).IsName("Dash Forward") && Anim.IsInTransition(0))
        {
            RigBod.velocity = new Vector3(0, RigBod.velocity.y, 0);
        }




        // DASH BACK
        if (Input.GetKeyUp("s") && Grounded && GetComponent<Health>().Recovered)
        {
            StopCoroutine("PrepDashBack");
            StartCoroutine("PrepDashBack");
        }

        if (Input.GetKeyDown("s") && DashBackPrep && Grounded && GetComponent<Health>().Recovered)
        {
            GetComponent<Shooting>().CombatMode = true;
            DashBack = true;
            Run = false;
        }

        if (DashBack)
        {
            RigBod.AddForce(-transform.forward * 30 * Time.deltaTime, ForceMode.VelocityChange);
        }

        if (Anim.GetCurrentAnimatorStateInfo(0).IsName("Dash Back") && !Anim.IsInTransition(0))
        {
            RigBod.AddForce(-transform.forward * 10 * Time.deltaTime, ForceMode.VelocityChange);
            DashBack = false;
        }

        if (Anim.GetCurrentAnimatorStateInfo(0).IsName("Dash Back") && Anim.IsInTransition(0))
        {
            RigBod.velocity = new Vector3(0, RigBod.velocity.y, 0);
        }



        

        // DASH LEFT
        if (Input.GetKeyUp("a") && Grounded && GetComponent<Health>().Recovered)
        {
            StopCoroutine("PrepDashLeft");
            StartCoroutine("PrepDashLeft");
        }

        if (Input.GetKeyDown("a") && DashLeftPrep && Grounded && GetComponent<Health>().Recovered)
        {
            GetComponent<Shooting>().CombatMode = true;
            DashLeft = true;
            Run = false;
            TurnLeft = false;
            TurnRight = false;
        }

        if (DashLeft)
        {
            if (GetComponent<Health>().Enemy != null)
            {
                TargetDirection = GetComponent<Health>().Enemy.transform.position - transform.position;
                TargetDirection.y = 0;
                transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, TargetDirection, TurnSpeed * 5 * Time.deltaTime, 0));
                RigBod.AddForce(-transform.right * 30 * Time.deltaTime, ForceMode.VelocityChange);
            }
            else
            {
                RigBod.AddForce(-transform.right * 30 * Time.deltaTime, ForceMode.VelocityChange);
            }
        }

        if (Anim.GetCurrentAnimatorStateInfo(0).IsName("Dash Left") && !Anim.IsInTransition(0))
        {
            if (GetComponent<Health>().Enemy != null)
            {
                TargetDirection = GetComponent<Health>().Enemy.transform.position - transform.position;
                TargetDirection.y = 0;
                transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, TargetDirection, TurnSpeed * 5 * Time.deltaTime, 0));
                RigBod.AddForce(transform.forward * 0 * Time.deltaTime, ForceMode.VelocityChange);
            }
            else
            {
                RigBod.AddForce(-transform.right * 0 * Time.deltaTime, ForceMode.VelocityChange);
            }
            DashLeft = false;
        }

        if (Anim.GetCurrentAnimatorStateInfo(0).IsName("Dash Left") && Anim.IsInTransition(0))
        {
            RigBod.velocity = new Vector3(0, RigBod.velocity.y, 0);
        }





        // DASH RIGHT
        if (Input.GetKeyUp("d") && Grounded && GetComponent<Health>().Recovered)
        {
            StopCoroutine("PrepDashRight");
            StartCoroutine("PrepDashRight");
        }

        if (Input.GetKeyDown("d") && DashRightPrep && Grounded && GetComponent<Health>().Recovered)
        {
            GetComponent<Shooting>().CombatMode = true;
            DashRight = true;
            Run = false;
            TurnLeft = false;
            TurnRight = false;
        }

        if (DashRight)
        {
            if (GetComponent<Health>().Enemy != null)
            {
                TargetDirection = GetComponent<Health>().Enemy.transform.position - transform.position;
                TargetDirection.y = 0;
                transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, TargetDirection, TurnSpeed * 5 * Time.deltaTime, 0));
                RigBod.AddForce(transform.right * 30 * Time.deltaTime, ForceMode.VelocityChange);
            }
            else
            {
                RigBod.AddForce(transform.right * 30 * Time.deltaTime, ForceMode.VelocityChange);
            }
            
        }

        if (Anim.GetCurrentAnimatorStateInfo(0).IsName("Dash Right") && !Anim.IsInTransition(0))
        {
            if (GetComponent<Health>().Enemy != null)
            {
                TargetDirection = GetComponent<Health>().Enemy.transform.position - transform.position;
                TargetDirection.y = 0;
                transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, TargetDirection, TurnSpeed * 5 * Time.deltaTime, 0));
                RigBod.AddForce(transform.forward * 0 * Time.deltaTime, ForceMode.VelocityChange);
            }
            else
            {
                RigBod.AddForce(transform.right * 0 * Time.deltaTime, ForceMode.VelocityChange);
            }
            DashRight = false;
        }

        if (Anim.GetCurrentAnimatorStateInfo(0).IsName("Dash Right") && Anim.IsInTransition(0))
        {
            RigBod.velocity = new Vector3(0, RigBod.velocity.y, 0);
        }


    */

        // VELOCITY & ROTATION
        if (Input.GetKey("d") || Input.GetKey("a") || Input.GetKey("w") || Input.GetKey("s"))
        {

            TargetDirection.y = 0;
            if (FacingObject == null && GetComponent<Health>().Recovered && !Anim.GetCurrentAnimatorStateInfo(0).IsName("Divide Attack 1") && !Anim.GetCurrentAnimatorStateInfo(0).IsName("Divide Attack 2"))
            {
                RigBod.velocity = TargetDirection * RunSpeed * 50 * Time.deltaTime + new Vector3(0, RigBod.velocity.y, 0);
            }
            if (!GetComponent<Shooting>().CombatMode && GetComponent<Health>().Recovered)
            {
                transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, TargetDirection, TurnSpeed * Time.deltaTime, 0));
            }
            else
            {

                ShootTargetDirection = Camera.main.transform.forward;
                ShootTargetDirection.y = 0;
                transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, ShootTargetDirection, TurnSpeed * Time.deltaTime, 0));
            }
        }




        // GROUNDED
        if (Physics.SphereCast(transform.position + new Vector3(0, -ObjectSize.y / 4 + ObjectSize.y / 100, 0), ObjectSize.z / 2, -transform.up, out RaycastHit GroundHit, ObjectSize.y / 5)
            && GroundHit.transform.gameObject.tag != "Player")
        {
            Grounded = true;
        }
        else
        {
            Grounded = false;
        }




        // JUMP
        if (Grounded && Input.GetKeyDown("space"))
        {
            RigBod.velocity = new Vector3(RigBod.velocity.x, transform.up.y * JumpHeight, RigBod.velocity.z);
        }




        // STOP VELOCITY
        if ((RigBod.velocity.x != 0 || RigBod.velocity.z != 0) && (Input.GetKeyUp("w") || Input.GetKeyUp("s") || Input.GetKeyUp("d") || Input.GetKeyUp("a")))
        {
            RigBod.velocity = new Vector3(0, RigBod.velocity.y, 0);
        }




        // FACING OBJECT
        if (Physics.SphereCast(transform.position + new Vector3(0, -ObjectSize.y / 4) - transform.forward / (100 / ObjectSize.z), ObjectSize.z / 2, TargetDirection, out RaycastHit FacingHit, .1f)
            && FacingHit.transform.gameObject.tag != "Player" && !FacingHit.transform.gameObject.GetComponent<Collider>().isTrigger)
        {
            FacingObject = FacingHit.transform.gameObject;
            if (!Grounded)
            {
                RigBod.velocity = new Vector3(0, RigBod.velocity.y, 0);
            }
        }
        else
        {
            FacingObject = null;
        }



        // TURN ANGLE
        if (!GetComponent<Shooting>().CombatMode)
        {
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            TurnAngle = Mathf.Round(Vector3.SignedAngle(TargetDirection, transform.forward, Vector3.up));
        }
        else
        {
            TurnAngle = 0;
        }




        // ANIMATIONS
        Anim.SetBool("Idle", Idle);
        Anim.SetBool("Jump", Jump);
        Anim.SetBool("Land", Land);
        Anim.SetBool("Run", Run);
        Anim.SetBool("Stop", Stop);
        Anim.SetBool("Turn Left", TurnLeft);
        Anim.SetBool("Turn Right", TurnRight);
        Anim.SetBool("Drop", Drop);
        Anim.SetFloat("Animation Speed", (.02f) * RunSpeed + .8f);

        Anim.SetBool("Run Left", RunLeft);
        Anim.SetBool("Run Right", RunRight);
        Anim.SetBool("Run Forward", RunForward);
        Anim.SetBool("Run Back", RunBack);
        Anim.SetBool("Run Back Left", RunBackLeft);
        Anim.SetBool("Run Back Right", RunBackRight);
        Anim.SetBool("Run Forward Left", RunForwardLeft);
        Anim.SetBool("Run Forward Right", RunForwardRight);
        Anim.SetBool("Dash Forward", DashForward);
        Anim.SetBool("Dash Back", DashBack);
        Anim.SetBool("Dash Left", DashLeft);
        Anim.SetBool("Dash Right", DashRight);



        // IDLE ANIMATION
        if (((!Input.GetKey("w") && !Input.GetKey("s") && !Input.GetKey("a") && !Input.GetKey("d")) || FacingObject != null && RigBod.velocity == Vector3.zero) && Grounded && !Anim.IsInTransition(0) &&
             (Anim.GetCurrentAnimatorStateInfo(0).IsTag("Stop") && Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= .75f
             || Anim.GetCurrentAnimatorStateInfo(0).IsTag("Melee Attack") && Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= .9f && !Anim.GetCurrentAnimatorStateInfo(0).IsName("Weapon Attack") && !Anim.GetCurrentAnimatorStateInfo(0).IsName("Weapon Attack 2") && !Anim.GetCurrentAnimatorStateInfo(0).IsName("Weapon Attack 3")
             || Anim.GetCurrentAnimatorStateInfo(0).IsTag("Melee Attack End") && Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= .9f
             || Anim.GetCurrentAnimatorStateInfo(0).IsTag("Get Up") && Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= .9f
             || Anim.GetCurrentAnimatorStateInfo(0).IsTag("Knocked Back") && Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= .9f
             || Anim.GetCurrentAnimatorStateInfo(0).IsTag("Dash") && Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= .9f
             || Anim.GetCurrentAnimatorStateInfo(0).IsName("Summon Graph Part 2") && Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= .9f
        ))
        {
            Idle = true;
        }
        else
        {
            Idle = false;
        }




        // JUMP ANIMATION
        if (Grounded && Input.GetKeyDown("space"))
        {
            Jump = true;
        }
        else
        {
            Jump = false;
        }




        // DROP ANIMATION
        if (!Grounded && Mathf.Round(RigBod.velocity.y) < 0)
        {
            Drop = true;
        }
        else
        {
            Drop = false;
        }




        // LAND ANIMATION
        if (Grounded && Mathf.Round(RigBod.velocity.y) == 0 && Anim.GetCurrentAnimatorStateInfo(0).IsTag("Drop") && !GetComponent<WallMachanics>().Climbing)
        {
            Land = true;
            RigBod.velocity = new Vector3(0, RigBod.velocity.y, 0);
        }
        else
        {
            Land = false;
        }




        // RUN ANIMATION
        if ((Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("d") || Input.GetKey("a")) && Grounded && TurnAngle == 0 && FacingObject == null && !DashForward && !DashBack
            && !GetComponent<MeleeAttack>().Attack && GetComponent<Health>().Recovered && !GetComponent<BarGraphs>().SummonBarGraph && !GetComponent<CombineSouls>().CombineAnimation && !GetComponent<CombineSouls>().DivideAttack && !GetComponent<SummonMech>().SummonMechAnimation
            && !Anim.GetCurrentAnimatorStateInfo(0).IsTag("Jump") && !Anim.GetCurrentAnimatorStateInfo(0).IsTag("Drop") && !Anim.GetCurrentAnimatorStateInfo(0).IsTag("Climb")
            && (!Anim.GetCurrentAnimatorStateInfo(0).IsTag("Turn") || Anim.GetCurrentAnimatorStateInfo(0).IsTag("Turn") && Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= .5f)
            && (!Anim.GetCurrentAnimatorStateInfo(0).IsTag("Land") || Anim.GetCurrentAnimatorStateInfo(0).IsTag("Land") && Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= .3f)
            && (!Anim.GetCurrentAnimatorStateInfo(0).IsTag("Melee Attack") || Anim.GetCurrentAnimatorStateInfo(0).IsTag("Melee Attack") && Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= .9f)
            && (!Anim.GetCurrentAnimatorStateInfo(0).IsTag("Get Up") || Anim.GetCurrentAnimatorStateInfo(0).IsTag("Get Up") && Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= .35f)
            && (!Anim.GetCurrentAnimatorStateInfo(0).IsTag("Dash") || Anim.GetCurrentAnimatorStateInfo(0).IsTag("Dash") && Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= .9f)
        )
        {
            Run = true;
        }
        else
        {
            Run = false;
        }




        // TURN LEFT ANIMATION
        if ((Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("d") || Input.GetKey("a")) && Grounded && TurnAngle > 0 && FacingObject == null && !DashForward && !DashBack
            && !GetComponent<MeleeAttack>().Attack && GetComponent<Health>().Recovered && !GetComponent<BarGraphs>().SummonBarGraph && !GetComponent<CombineSouls>().CombineAnimation && !GetComponent<CombineSouls>().DivideAttack && !GetComponent<SummonMech>().SummonMechAnimation
            && !Anim.GetCurrentAnimatorStateInfo(0).IsTag("Jump") && !Anim.GetCurrentAnimatorStateInfo(0).IsTag("Drop") && !Anim.GetCurrentAnimatorStateInfo(0).IsTag("Climb")
            && (!Anim.GetCurrentAnimatorStateInfo(0).IsTag("Turn") || Anim.GetCurrentAnimatorStateInfo(0).IsTag("Turn") && Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= .5f)
            && (!Anim.GetCurrentAnimatorStateInfo(0).IsTag("Land") || Anim.GetCurrentAnimatorStateInfo(0).IsTag("Land") && Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= .3f)
            && (!Anim.GetCurrentAnimatorStateInfo(0).IsTag("Melee Attack") || Anim.GetCurrentAnimatorStateInfo(0).IsTag("Melee Attack") && Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= .9f)
            && (!Anim.GetCurrentAnimatorStateInfo(0).IsTag("Get Up") || Anim.GetCurrentAnimatorStateInfo(0).IsTag("Get Up") && Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= .35f)
        )
        {
            if (!GetComponent<Shooting>().CombatMode)
            {
                TurnLeft = true;
            }
        }
        else
        {
            TurnLeft = false;
        }




        // TURN RIGHT ANIMATION
        if ((Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("d") || Input.GetKey("a")) && Grounded && TurnAngle < 0 && FacingObject == null && !DashForward && !DashBack
            && !GetComponent<MeleeAttack>().Attack && GetComponent<Health>().Recovered && !GetComponent<BarGraphs>().SummonBarGraph && !GetComponent<CombineSouls>().CombineAnimation && !GetComponent<CombineSouls>().DivideAttack && !GetComponent<SummonMech>().SummonMechAnimation
            && !Anim.GetCurrentAnimatorStateInfo(0).IsTag("Jump") && !Anim.GetCurrentAnimatorStateInfo(0).IsTag("Drop") && !Anim.GetCurrentAnimatorStateInfo(0).IsTag("Climb")
            && (!Anim.GetCurrentAnimatorStateInfo(0).IsTag("Turn") || Anim.GetCurrentAnimatorStateInfo(0).IsTag("Turn") && Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= .5f)
            && (!Anim.GetCurrentAnimatorStateInfo(0).IsTag("Land") || Anim.GetCurrentAnimatorStateInfo(0).IsTag("Land") && Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= .3f)
            && (!Anim.GetCurrentAnimatorStateInfo(0).IsTag("Melee Attack") || Anim.GetCurrentAnimatorStateInfo(0).IsTag("Melee Attack") && Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= .9f)
            && (!Anim.GetCurrentAnimatorStateInfo(0).IsTag("Get Up") || Anim.GetCurrentAnimatorStateInfo(0).IsTag("Get Up") && Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= .35f)
        )
        {
            if (!GetComponent<Shooting>().CombatMode)
            {
                TurnRight = true;
            }
        }
        else
        {
            TurnRight = false;
        }




        // STOP ANIMATION
        if (!Input.GetKey("w") && !Input.GetKey("s") && !Input.GetKey("d") && !Input.GetKey("a") && RigBod.velocity.y <= 0 && Grounded && FacingObject == null
           && !GetComponent<MeleeAttack>().Attack && GetComponent<Health>().Recovered && !GetComponent<BarGraphs>().SummonBarGraph && !GetComponent<CombineSouls>().CombineAnimation && !GetComponent<CombineSouls>().DivideAttack && !GetComponent<SummonMech>().SummonMechAnimation
           && (Anim.GetCurrentAnimatorStateInfo(0).IsTag("Run") || Anim.GetCurrentAnimatorStateInfo(0).IsTag("Turn")))
        {
            Stop = true;
        }
        else
        {
            Stop = false;
        }




        // RUN LEFT ANIMATION
        if (GetComponent<Shooting>().CombatMode && Input.GetKey("a") && !Input.GetKey("d") && !Input.GetKey("w") && !Input.GetKey("s") && GetComponent<Health>().Recovered)
        {
            RunLeft = true;
        }
        else
        {
            RunLeft = false;
        }




        // RUN RIGHT ANIMATION
        if (GetComponent<Shooting>().CombatMode && Input.GetKey("d") && !Input.GetKey("a") && !Input.GetKey("w") && !Input.GetKey("s") && GetComponent<Health>().Recovered)
        {
            RunRight = true;
        }
        else
        {
            RunRight = false;
        }




        // RUN FORWARD ANIMATION
        if (GetComponent<Shooting>().CombatMode && Input.GetKey("w") && !Input.GetKey("s") && !Input.GetKey("a") && !Input.GetKey("d") && GetComponent<Health>().Recovered)
        {
            RunForward = true;
        }
        else
        {
            RunForward = false;
        }




        // RUN BACK ANIMATION
        if (GetComponent<Shooting>().CombatMode && Input.GetKey("s") && !Input.GetKey("w") && !Input.GetKey("a") && !Input.GetKey("d") && GetComponent<Health>().Recovered)
        {
            RunBack = true;
        }
        else
        {
            RunBack = false;
        }





        // RUN BACK - LEFT
        if (GetComponent<Shooting>().CombatMode && Input.GetKey("s") && !Input.GetKey("w") && Input.GetKey("a") && !Input.GetKey("d") && GetComponent<Health>().Recovered)
        {
            RunBackLeft = true;
        }
        else
        {
            RunBackLeft = false;
        }





        // RUN BACK - RIGHT
        if (GetComponent<Shooting>().CombatMode && Input.GetKey("s") && !Input.GetKey("w") && !Input.GetKey("a") && Input.GetKey("d") && GetComponent<Health>().Recovered)
        {
            RunBackRight = true;
        }
        else
        {
            RunBackRight = false;
        }




        // RUN FORWARD - LEFT
        if (GetComponent<Shooting>().CombatMode && !Input.GetKey("s") && Input.GetKey("w") && Input.GetKey("a") && !Input.GetKey("d") && GetComponent<Health>().Recovered)
        {
            RunForwardLeft = true;
        }
        else
        {
            RunForwardLeft = false;
        }





        // RUN FORWARD - RIGHT
        if (GetComponent<Shooting>().CombatMode && !Input.GetKey("s") && Input.GetKey("w") && !Input.GetKey("a") && Input.GetKey("d") && GetComponent<Health>().Recovered)
        {
            RunForwardRight = true;
        }
        else
        {
            RunForwardRight = false;
        }


    }

    private void OnCollisionEnter(Collision Coll)
    {
        ColliderHit = Coll.transform.gameObject;
    }

    private void OnCollisionExit(Collision Coll)
    {
        ColliderHit = null;
    }

    private void OnTriggerEnter(Collider Coll)
    {
        TriggerHit = Coll.transform.gameObject;
    }

    private void OnTriggerExit(Collider Coll)
    {
        TriggerHit = null;
    }

    IEnumerator PrepDashForward()
    {
        DashForwardPrep = true;
        yield return new WaitForSeconds(.5f);
        DashForwardPrep = false;
    }

    IEnumerator PrepDashBack()
    {
        DashBackPrep = true;
        yield return new WaitForSeconds(.5f);
        DashBackPrep = false;
    }
    IEnumerator PrepDashLeft()
    {
        DashLeftPrep = true;
        yield return new WaitForSeconds(.5f);
        DashLeftPrep = false;
    }

    IEnumerator PrepDashRight()
    {
        DashRightPrep = true;
        yield return new WaitForSeconds(.5f);
        DashRightPrep = false;
    }
}
