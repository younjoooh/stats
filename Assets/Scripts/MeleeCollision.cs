using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MeleeCollision : MonoBehaviour
{
    public GameObject Player;
    public bool UnArmedAttacking;
    public bool WeaponAttacking;
    public bool DivideAttacking;

    public AudioSource SplashSound, SpinSound;
    public GameObject SplashEffect;
    public GameObject SplashEffectClone;

    public AudioSource WallSound;
    public GameObject WallEffect;
    public GameObject WallEffectClone;

    public GameObject AIChip;
    public GameObject AIChipClone;

    public GameObject SelectEffect;
    void Start()
    {
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


        SpinSound = gameObject.AddComponent<UnityEngine.AudioSource>();
        SpinSound.clip = Resources.Load<AudioClip>("Audio/Spin Sound");
        SpinSound.spatialBlend = 0;
        SpinSound.volume = .5f;
        SpinSound.playOnAwake = false;

        WallEffect = Instantiate(Resources.Load<GameObject>("Effects/Wall Effect"), new Vector3(0, -5000, 0), Quaternion.identity);
        WallEffect.SetActive(false);

        AIChip = Instantiate(Resources.Load<GameObject>("Prefabs/AI Chip"), new Vector3(0, -5000, 0), Quaternion.identity);
        AIChip.SetActive(false);

        SelectEffect = GameObject.Instantiate(Resources.Load<GameObject>("Effects/Select Effect"), new Vector3(0, -500, 0), Quaternion.identity);
        SelectEffect.SetActive(false);
    }

    void Update()
    {
        // LEFT FOOT
        if (this.gameObject.name == "Left Foot")
        {
            if (Player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Un Armed Attack")
              && Player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime <= .5f
              || Player.GetComponent<MeleeAttack>().Attack && !Player.GetComponent<MeleeAttack>().WeaponEquipped
             )
            {
                UnArmedAttacking = true;
            }
            else
            {
                UnArmedAttacking = false;
            }
        }


        // RIGHT HAND
        if (this.gameObject.name == "Right Hand")
        {
            if ((Player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Weapon Attack") || Player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Weapon Attack 2") || Player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Melee Attack End 3"))
                && Player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= .01f)
            {
                WeaponAttacking = true;
            }
            else
            {
                WeaponAttacking = false;
            }
        }


        // RIGHT HAND
        if (this.gameObject.name == "Right Hand")
        {
            if (Player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Divide Attack 3")
              && Player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime <= .5f
             )
            {
                DivideAttacking = true;
            }
            else
            {
                DivideAttacking = false;
            }
        }

    }

    private void OnTriggerEnter(Collider Coll)
    {
        // SPLASH EFFECT WEAPON PARASITE
        if ((WeaponAttacking || DivideAttacking) && Coll.gameObject.transform.name == "Parasite")
        {
            SplashSound.PlayOneShot(SplashSound.clip, 1f);
            SplashEffectClone = Instantiate(SplashEffect, transform.position, Quaternion.identity);
            SplashEffectClone.SetActive(true);
            StartCoroutine("DestroySplashEffect", SplashEffectClone);

            Coll.gameObject.transform.GetComponent<Rigidbody>().AddForce(Player.transform.forward * 10, ForceMode.VelocityChange);
            if (Coll.gameObject.transform.tag != "Red")
            {
                Coll.gameObject.transform.GetComponent<Parasite>().KnockedOut = true;
            }
            Coll.gameObject.transform.GetComponent<Parasite>().ScreamSound.PlayOneShot(Coll.gameObject.transform.GetComponent<Parasite>().ScreamSound.clip, 1f);
            Coll.gameObject.transform.GetComponent<Parasite>().GrowlSound.Stop();
            Coll.gameObject.transform.GetComponent<CapsuleCollider>().enabled = false;


            Player.GetComponent<Canvas>().TotalMonsters += 1;

            if (Coll.gameObject.transform.tag == "Blue")
            {
                Player.GetComponent<Canvas>().BlueMonsters += 1;
            }

            if (Coll.gameObject.transform.tag == "Green")
            {
                Player.GetComponent<Canvas>().GreenMonsters += 1;
            }

            if (Coll.gameObject.transform.tag == "Yellow")
            {
                Player.GetComponent<Canvas>().YellowMonsters += 1;
            }

            if (Coll.gameObject.transform.tag == "Red")
            {
                Player.GetComponent<Canvas>().RedMonsters += 1;
            }
        }

        // SPLASH EFFECT WEAPON MERGED MUTANT
        if (DivideAttacking && Coll.gameObject.transform.name == "Merge Mutant")
        {
            SplashSound.PlayOneShot(SplashSound.clip, 1f);
            SplashEffectClone = Instantiate(SplashEffect, transform.position, Quaternion.identity);
            SplashEffectClone.SetActive(true);
            StartCoroutine("DestroySplashEffect", SplashEffectClone);

            Coll.gameObject.transform.GetComponent<Rigidbody>().AddForce(Player.transform.forward * 10, ForceMode.VelocityChange);
            Coll.gameObject.transform.GetComponent<Parasite>().KnockedOut = true;
            Coll.gameObject.transform.GetComponent<Parasite>().ScreamSound.PlayOneShot(Coll.gameObject.transform.GetComponent<Parasite>().ScreamSound.clip, 1f);
            Coll.gameObject.transform.GetComponent<Parasite>().GrowlSound.Stop();
            Coll.gameObject.transform.GetComponent<CapsuleCollider>().enabled = false;
        }


        // WALL EFFECT
        if (WeaponAttacking && Coll.gameObject.transform.name != "Parasite")
        {
            //WallSound.PlayOneShot(WallSound.clip, 1f);
            //WallEffectClone = Instantiate(WallEffect, transform.position, Quaternion.identity);
            //WallEffectClone.SetActive(true);
            //StartCoroutine("DestroyWallEffect", WallEffectClone);
        }

        // SPLASH EFFECT DIVIDE
        if (WeaponAttacking && Coll.gameObject.transform.name == "Soul Chip")
        {
            SplashSound.PlayOneShot(SplashSound.clip, 1f);
            SplashEffectClone = Instantiate(SplashEffect, transform.position, Quaternion.identity);
            SplashEffectClone.SetActive(true);
            StartCoroutine("DestroySplashEffect", SplashEffectClone);
        }

        // DIVIDE EFFECT
        if (DivideAttacking && Coll.gameObject.transform.name == "Soul Chip" && !Player.GetComponent<CombineSouls>().Divided)
        {
            Player.GetComponent<CombineSouls>().Divided = true;

            SplashSound.PlayOneShot(SplashSound.clip, 1f);
            SplashEffectClone = Instantiate(SplashEffect, transform.position, Quaternion.identity);
            SplashEffectClone.SetActive(true);
            StartCoroutine("DestroySplashEffect", SplashEffectClone);


            AIChipClone = Instantiate(AIChip, Player.GetComponent<CombineSouls>().SoulChipClone.transform.position, Quaternion.identity);
            AIChipClone.name = "AI Chip";
            AIChipClone.SetActive(true);
            AIChipClone.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(AIChipClone.transform.forward, new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z), 9999 * Time.deltaTime, 0));
            AIChipClone.transform.GetChild(0).GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = Player.GetComponent<CombineSouls>().SoulChipClone.transform.GetChild(0).GetChild(0).transform.GetComponent<TextMeshProUGUI>().text;

            float Dividedvalue = float.Parse(AIChipClone.transform.GetChild(0).GetChild(0).transform.GetComponent<TextMeshProUGUI>().text);
            Dividedvalue /= Player.GetComponent<Shooting>().CurrentMarkedObject;
            Dividedvalue = Mathf.Round(Dividedvalue);
            AIChipClone.transform.GetChild(0).GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = Dividedvalue.ToString();
            AIChipClone.transform.GetChild(0).GetChild(0).transform.GetComponent<TextMeshProUGUI>().enabled = false;
            Player.GetComponent<CombineSouls>().TextEnabled = false;
            StartCoroutine("ShowAIChipText", AIChipClone);

            Destroy(Player.GetComponent<CombineSouls>().SoulChipClone);
            Player.GetComponent<CombineSouls>().AIChipClone = AIChipClone;
        }


        // MEDIAN GATE
        if (DivideAttacking && Coll.gameObject.transform.name == "Added Tech" && !Player.GetComponent<CombineSouls>().Divided)
        {
            SpinSound.PlayOneShot(SpinSound.clip, 1f);
            SelectEffect.SetActive(false);
            SelectEffect.SetActive(true);
            SelectEffect.transform.position = Coll.gameObject.transform.position;
            SplashSound.PlayOneShot(SplashSound.clip, 1f);

            Coll.gameObject.GetComponent<Collider>().enabled = false;
            Coll.gameObject.transform.parent.GetComponent<MedianGate>().ShowAddedTech = false;
            Coll.gameObject.AddComponent<RotateAroundAxis>();
            Coll.gameObject.GetComponent<RotateAroundAxis>().Speed = -1;
            Coll.gameObject.GetComponent<RotateAroundAxis>().Angle = new Vector3(0, 0, 1);
            Coll.gameObject.GetComponent<RotateAroundAxis>().Target = Coll.gameObject;

            Coll.gameObject.transform.parent.GetComponent<MedianGate>().StartCoroutine("Explode");
        }
    }


    IEnumerator DestroySplashEffect(GameObject SplashEffect)
    {
        yield return new WaitForSeconds(5);
        Destroy(SplashEffect);
    }

    IEnumerator DestroyWallEffect(GameObject WallEffect)
    {
        yield return new WaitForSeconds(5);
        Destroy(WallEffect);
    }


    IEnumerator ShowAIChipText()
    {
        yield return new WaitForSeconds(1.6f);
        Player.GetComponent<CombineSouls>().TextEnabled = true;
        AIChipClone.transform.GetChild(0).GetChild(0).transform.GetComponent<TextMeshProUGUI>().enabled = true;
    }
}
