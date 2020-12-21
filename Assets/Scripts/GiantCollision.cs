using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantCollision : MonoBehaviour
{
    public GameObject Giant;
    public GameObject Player;
    public bool Attacking1;
    public bool Attacking2;
    public bool Attacking3;
    public bool Stomping;
    public bool CanAttack = true;
    public bool CanStomp = true;

    public AudioSource SplashSound;
    public GameObject SplashEffect;
    public GameObject SplashEffectClone;

    public AudioSource GiantWallSound;
    public GameObject GiantWallEffect;
    public GameObject GiantWallEffectClone;

    public AudioSource GiantStompSound;
    public GameObject GiantStompEffect;
    public GameObject GiantStompEffectClone;
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        SplashSound = gameObject.AddComponent<UnityEngine.AudioSource>();
        SplashSound.clip = Resources.Load<AudioClip>("Audio/Splash Sound");
        SplashSound.spatialBlend = 1;
        SplashSound.volume = 1;
        SplashSound.playOnAwake = false;

        SplashEffect = Instantiate(Resources.Load<GameObject>("Effects/Splash Effect"), new Vector3(0, -5000, 0), Quaternion.identity);
        SplashEffect.SetActive(false);

        GiantWallSound = gameObject.AddComponent<UnityEngine.AudioSource>();
        GiantWallSound.clip = Resources.Load<AudioClip>("Audio/Giant Sounds/Giant Wall Sound");
        GiantWallSound.spatialBlend = 1;
        GiantWallSound.volume = .5f;
        GiantWallSound.playOnAwake = false;

        GiantWallEffect = Instantiate(Resources.Load<GameObject>("Effects/Stomp Effect " + Giant.tag), new Vector3(0, -5000, 0), Quaternion.identity);
        GiantWallEffect.SetActive(false);

        GiantStompSound = gameObject.AddComponent<UnityEngine.AudioSource>();
        GiantStompSound.clip = Resources.Load<AudioClip>("Audio/Giant Sounds/Giant Stomp Sound " + Giant.GetComponent<Giant>().Style);
        GiantStompSound.spatialBlend = 1;
        GiantStompSound.volume = 1f;
        GiantStompSound.playOnAwake = false;

        GiantStompEffect = Instantiate(Resources.Load<GameObject>("Effects/Stomp Effect " + Giant.tag), new Vector3(0, .49f, 0), Quaternion.identity);
        GiantStompEffect.SetActive(false);
    }

    void Update()
    {
        // ATTACK 1
        if ((this.gameObject.name == "Left Lower Arm")  && CanAttack)
        {
            if (Giant.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Monster Attack 1")
              && Giant.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime <= 1f
              && Giant.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= .55f 
            )
            {
                Attacking1 = true;
            }
            else
            {
                Attacking1 = false;
            }
        }
        if (Giant.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Monster Attack 1") &&
           (Giant.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1f
            || Giant.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime < .55f)
        )
        {
            CanAttack = true;
        }

        // ATTACK 2
        if (this.gameObject.name == "Right Foot" && CanAttack)
        {
            if (Giant.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Monster Attack 2")
              && Giant.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime <= .45f
              && Giant.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= .25f
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

        if (Giant.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Monster Attack 2") &&
           (Giant.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > .5f
           || Giant.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime < .25f)
         )
        {
            CanAttack = true;
        }

        // ATTACK 3
        if ((this.gameObject.name == "Right Hand" || this.gameObject.name == "Right Lower Arm") && CanAttack)
        {
            if (Giant.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Monster Attack 3")
              && Giant.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime <= .5f
              && Giant.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= .25f
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

        if (Giant.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Monster Attack 3") &&
        (Giant.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > .5f
        || Giant.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime < .25f)
        )
        {
            CanAttack = true;
        }




        // STOMP
        if ((this.gameObject.name == "Right Foot" || this.gameObject.name == "Left Foot") && CanStomp)
        {
            if (Giant.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("Run"))            
            {
                Stomping = true;
            }
            else
            {
                Stomping = false;
                CanStomp = true;
            }
        }

    }

    private void OnTriggerEnter(Collider Coll)
    {
        // SPLASH EFFECT ATTACK
        if ((Attacking1 || Attacking2 || Attacking3) && Coll.gameObject.transform.tag == "Player" 
            && Coll.gameObject.GetComponent<Collider>().GetType() != typeof(SphereCollider) &&
            (  Coll.gameObject.transform.name == "Hips" || Coll.gameObject.transform.name == "Spine"
            || Coll.gameObject.transform.name == "Chest"|| Coll.gameObject.transform.name == "Chest"
            || Coll.gameObject.transform.name == "Upper Chest")
        )
        {
            SplashSound.PlayOneShot(SplashSound.clip, 1f);
            SplashEffectClone = Instantiate(SplashEffect, transform.position, Quaternion.identity);
            SplashEffectClone.SetActive(true);
            StartCoroutine("DestroySplashEffect", SplashEffectClone);
            Player.GetComponent<Health>().HealthPoints -= 1;
            Player.GetComponent<Health>().KnockedBack = true;
            Player.GetComponent<Health>().Recovered = false;
            Player.GetComponent<Health>().StopCoroutine("Recovery");
            Player.GetComponent<Health>().StartCoroutine("Recovery");
            Player.GetComponent<Health>().KnockedBackCycle = Giant.GetComponent<Giant>().AttackCycle;
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


        // WALL EFFECT
        if ((Attacking1 || Attacking2 || Attacking3) && Coll.gameObject.transform.tag != "Player")
        {
            GiantWallSound.PlayOneShot(GiantWallSound.clip, 1f);
            GiantWallEffectClone = Instantiate(GiantWallEffect, transform.position, Quaternion.identity);
            GiantWallEffectClone.SetActive(true);

            GiantWallEffectClone.transform.eulerAngles = new Vector3(90, 0, 0);

            if (Giant.tag == "Green")
            {
                GiantWallEffectClone.transform.position = new Vector3(GiantWallEffectClone.transform.position.x, -.7f, GiantWallEffectClone.transform.position.z);
            }
            if (Giant.tag == "Yellow")
            {
                GiantWallEffectClone.transform.position = new Vector3(GiantWallEffectClone.transform.position.x, -.5f, GiantWallEffectClone.transform.position.z);
            }
            if (Giant.tag == "Red")
            {
                GiantWallEffectClone.transform.position = new Vector3(GiantWallEffectClone.transform.position.x, -.7f, GiantWallEffectClone.transform.position.z);
            }

            if (Giant.tag == "Blue")
            {
                GiantWallEffectClone.transform.position = new Vector3(GiantWallEffectClone.transform.position.x, -.7f, GiantWallEffectClone.transform.position.z);
            }

            if (Giant.tag == "Purple")
            {
                GiantWallEffectClone.transform.position = new Vector3(GiantWallEffectClone.transform.position.x, -.7f, GiantWallEffectClone.transform.position.z);
            }


            StartCoroutine("DestroyGiantWallEffect", GiantWallEffectClone);
            Attacking1 = false;
            Attacking2 = false;
            Attacking3 = false;
            CanAttack = false;
        }

        // STOMP EFFECT
        if (Stomping && Coll.gameObject.transform.tag != "Player" && Player.GetComponent<GraphicSettings>().StompEffects)
        {
            GiantStompSound.PlayOneShot(GiantStompSound.clip, 1f);
            GiantStompEffectClone = Instantiate(GiantStompEffect, transform.position, Quaternion.identity);
            GiantStompEffectClone.transform.eulerAngles = new Vector3(90,0,0);

            if (Giant.tag == "Green")
            {
                GiantStompEffectClone.transform.position = new Vector3(GiantStompEffectClone.transform.position.x, -.7f, GiantStompEffectClone.transform.position.z);
            }
            if (Giant.tag == "Yellow")
            {
                GiantStompEffectClone.transform.position = new Vector3(GiantStompEffectClone.transform.position.x, -.5f, GiantStompEffectClone.transform.position.z);
            }
            if (Giant.tag == "Red")
            {
                GiantStompEffectClone.transform.position = new Vector3(GiantStompEffectClone.transform.position.x, -.7f, GiantStompEffectClone.transform.position.z);
            }

            if (Giant.tag == "Blue")
            {
                GiantStompEffectClone.transform.position = new Vector3(GiantStompEffectClone.transform.position.x, -.7f, GiantStompEffectClone.transform.position.z);
            }

            if (Giant.tag == "Purple")
            {
                GiantStompEffectClone.transform.position = new Vector3(GiantStompEffectClone.transform.position.x, -.7f, GiantStompEffectClone.transform.position.z);
            }

            GiantStompEffectClone.SetActive(true);
            StartCoroutine("DestroyStompEffect", GiantStompEffectClone);
            Attacking1 = false;
            Attacking2 = false;
            Attacking3 = false;
            Stomping = false;
            CanStomp= false;
        }
 
    }

    private void OnTriggerExit(Collider Coll)
    {
        // WALL EFFECT
        if (!CanStomp && Coll.gameObject.transform.tag != "Player")
        {
            CanStomp = true;
        }
    }

    IEnumerator DestroyStompEffect(GameObject StompEffect)
    {
        yield return new WaitForSeconds(20);
        Destroy(StompEffect);
    }

    IEnumerator DestroySplashEffect(GameObject GiantSplashEffect)
    {
        yield return new WaitForSeconds(5);
        Destroy(GiantSplashEffect);
    }

    IEnumerator DestroyGiantWallEffect(GameObject GiantWallEffect)
    {
        yield return new WaitForSeconds(5);
        Destroy(GiantWallEffect);
    }
}
