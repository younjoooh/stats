using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParasiteCollision : MonoBehaviour
{
    public GameObject Parasite;
    public GameObject Player;
    public bool Attacking1;
    public bool Attacking2;
    public bool Attacking3;
    public bool CanAttack = true;

    public AudioSource SplashSound;
    public GameObject SplashEffect;
    public GameObject SplashEffectClone;

    public AudioSource WallSound;
    public GameObject WallEffect;
    public GameObject WallEffectClone;
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        SplashSound = gameObject.AddComponent<UnityEngine.AudioSource>();
        SplashSound.clip = Resources.Load<AudioClip>("Audio/Splash Sound");
        SplashSound.spatialBlend = 1;
        SplashSound.volume = .75f;
        SplashSound.playOnAwake = false;

        SplashEffect = Instantiate(Resources.Load<GameObject>("Effects/Splash Effect"), new Vector3(0, -5000, 0), Quaternion.identity);
        SplashEffect.SetActive(false);

        WallSound = gameObject.AddComponent<UnityEngine.AudioSource>();
        WallSound.clip = Resources.Load<AudioClip>("Audio/Wall Sound");
        WallSound.spatialBlend = 1;
        WallSound.volume = .75f;
        WallSound.playOnAwake = false;

        WallEffect = Instantiate(Resources.Load<GameObject>("Effects/Wall Effect"), new Vector3(0, -5000, 0), Quaternion.identity);
        WallEffect.SetActive(false);
    }

    void Update()
    {
        // ATTACK 1
        if (this.gameObject.name == "Left Hand" && CanAttack)
        {
            if (Parasite.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Monster Attack 1")
              && Parasite.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime <= 1f
              && Parasite.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= .75f 
            )
            {
                Attacking1 = true;
            }
            else
            {
                Attacking1 = false;
            }
        }
        if (Parasite.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Monster Attack 1") &&
           (Parasite.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1f
            || Parasite.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime < .75f)
        )
        {
            CanAttack = true;
        }

        // ATTACK 2
        if (this.gameObject.name == "Right Hand" && CanAttack)
        {
            if (Parasite.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Monster Attack 2")
              && Parasite.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime <= .5f
              && Parasite.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= .25f
            )
            {
                Attacking2 = true;
            }
            else
            {
                Attacking2 = false;
                CanAttack = true;
            }
        }

        if (Parasite.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Monster Attack 2") &&
           (Parasite.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > .5f
           || Parasite.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime < .25f)
         )
        {
            CanAttack = true;
        }

        // ATTACK 3
        if (this.gameObject.name == "Right Hand" && CanAttack)
        {
            if (Parasite.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Monster Attack 3")
              && Parasite.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime <= .5f
              && Parasite.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= .25f
            )
            {
                Attacking3 = true;
            }
            else
            {
                Attacking3 = false;
                CanAttack = true;
            }
        }

        if (Parasite.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Monster Attack 3") &&
        (Parasite.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > .5f
        || Parasite.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime < .25f)
        )
        {
            CanAttack = true;
        }

    }

    private void OnTriggerEnter(Collider Coll)
    {
        // SPLASH EFFECT WEAPON
        if ((Attacking1 || Attacking2 || Attacking3) && Coll.gameObject.transform.tag == "Player"
            && Coll.gameObject.GetComponent<Collider>().GetType() != typeof(SphereCollider) &&
            (  Coll.gameObject.transform.name == "Hips" || Coll.gameObject.transform.name == "Spine"
            || Coll.gameObject.transform.name == "Chest"|| Coll.gameObject.transform.name == "Head"
            || Coll.gameObject.transform.name == "Upper Chest")
        )
        {

            if(!Player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Divide Attack 1")
            && !Player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Divide Attack 2")
            && !Player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Divide Attack 3")
            && !Player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("Melee Attack"))
            {
                SplashSound.PlayOneShot(SplashSound.clip, 1f);
                SplashEffectClone = Instantiate(SplashEffect, transform.position, Quaternion.identity);
                SplashEffectClone.SetActive(true);
                Player.GetComponent<Health>().HealthPoints -= 1;
                Player.GetComponent<Health>().KnockedBack = true;
                Player.GetComponent<Health>().Recovered = false;
                Player.GetComponent<Health>().StopCoroutine("Recovery");
                Player.GetComponent<Health>().StartCoroutine("Recovery");
                Player.GetComponent<Health>().KnockedBackCycle = Parasite.GetComponent<Parasite>().AttackCycle;
                Player.GetComponent<Shooting>().CombatMode = true;
                Player.GetComponent<MeleeAttack>().Attack = false;
                Player.GetComponent<Movement>().DashForward = false;
                Player.GetComponent<Movement>().DashBack = false;
                Player.GetComponent<Movement>().DashLeft = false;
                Player.GetComponent<Movement>().DashRight = false;
                Attacking1 = false;
                Attacking2 = false;
                Attacking3 = false;
                CanAttack = false;
            }
        }

        /*
        // WALL EFFECT
        if ((Attacking1 || Attacking2 || Attacking3) && Coll.gameObject.transform.tag != "Player")
        {
            WallSound.PlayOneShot(WallSound.clip, 1f);
            WallEffectClone = Instantiate(WallEffect, transform.position, Quaternion.identity);
            WallEffectClone.SetActive(true);
        }
        */


    }
}
