using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SummonMech : MonoBehaviour
{
    Animator Anim;
    [HideInInspector]
    public bool SummonMechAnimation;

    [HideInInspector]
    public GameObject GreenMech;
    public GameObject YellowMech;
    public GameObject RedMech;
    public GameObject BlueMech;
    public GameObject PurpleMech;

    public GameObject Mech;
    public GameObject MechClone;

    public GameObject MechSummonEffect;
    public GameObject MechSummonEffectClone;

    public float RunSpeed = 2f;
    public float Range = 15f;
    public float FireRate = 5f;
    public int Ammo = 25;
    public string ColorType = "Green";
    public Vector3 MechPosition;

    [HideInInspector]
    public bool Reload = true;

    public AudioSource MechPortalSound, ExplosionSound;
    void Start()
    {
        MechPortalSound = gameObject.AddComponent<UnityEngine.AudioSource>();
        MechPortalSound.clip = Resources.Load<AudioClip>("Audio/Mech Sounds/Mech Portal Sound");
        MechPortalSound.spatialBlend = 1f;
        MechPortalSound.volume = .25f;
        MechPortalSound.playOnAwake = false;

        ExplosionSound = gameObject.AddComponent<UnityEngine.AudioSource>();
        ExplosionSound.clip = Resources.Load<AudioClip>("Audio/Mech Sounds/Mech Explosion Sound");
        ExplosionSound.spatialBlend = 1;
        ExplosionSound.volume = .25f;
        ExplosionSound.playOnAwake = true;


        Anim = GetComponent<Animator>();
        GreenMech = Instantiate(Resources.Load<GameObject>("Prefabs/Mech Green"), transform.position + Camera.main.transform.forward * 7 - new Vector3(0, 1, 0), Quaternion.identity);
        GreenMech.SetActive(false);

        YellowMech = Instantiate(Resources.Load<GameObject>("Prefabs/Mech Yellow"), transform.position + Camera.main.transform.forward * 7 - new Vector3(0, 1, 0), Quaternion.identity);
        YellowMech.SetActive(false);


        RedMech = Instantiate(Resources.Load<GameObject>("Prefabs/Mech Red"), transform.position + Camera.main.transform.forward * 7 - new Vector3(0, 1, 0), Quaternion.identity);
        RedMech.SetActive(false);

        BlueMech = Instantiate(Resources.Load<GameObject>("Prefabs/Mech Blue"), transform.position + Camera.main.transform.forward * 7 - new Vector3(0, 1, 0), Quaternion.identity);
        BlueMech.SetActive(false);

        PurpleMech = Instantiate(Resources.Load<GameObject>("Prefabs/Mech Purple"), transform.position + Camera.main.transform.forward * 7 - new Vector3(0, 1, 0), Quaternion.identity);
        PurpleMech.SetActive(false);
    }

    void Update()
    {
        if (GetComponent<BodyGlow>().Color == "Green")
        {
            ColorType = "Green";
        }
        if (GetComponent<BodyGlow>().Color == "Yellow")
        {
            ColorType = "Yellow";
        }
        if (GetComponent<BodyGlow>().Color == "Red")
        {
            ColorType = "Red";
        }
        if (GetComponent<BodyGlow>().Color == "Blue")
        {
            ColorType = "Blue";
        }

        if (GetComponent<BodyGlow>().Color == "Purple")
        {
            ColorType = "Purple";
        }

        if (Input.GetKeyDown("u") && !Anim.GetCurrentAnimatorStateInfo(0).IsTag("Melee Attack") && GetComponent<Health>().Recovered)
        {
            MechPortalSound.PlayOneShot(MechPortalSound.clip, 1f);
            SummonMechAnimation = true;
            if (GetComponent<GraphicSettings>().TurretSummonEffects)
            {
                MechSummonEffect = Instantiate(Resources.Load<GameObject>("Effects/Mech Summon Effect " + ColorType), Camera.main.transform.position, Quaternion.identity);
                MechSummonEffect.SetActive(false);
                MechSummonEffect.transform.parent = Camera.main.transform;
                MechSummonEffect.transform.localPosition = new Vector3(0, 0, 0);
                MechSummonEffect.transform.localEulerAngles = new Vector3(0, 0, 0);

                MechSummonEffect.transform.position = new Vector3(MechSummonEffect.transform.position.x, 1, MechSummonEffect.transform.position.z);
                MechSummonEffect.transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
                MechSummonEffect.transform.parent = null;
                MechSummonEffect.SetActive(true);
            }

            StartCoroutine("SpawnMech");
            MechPosition = transform.position + Camera.main.transform.forward * 8 - new Vector3(0, 1, 0);
        }

       

        if (SummonMechAnimation)
        {
            Vector3 TargetDirection = transform.position - Camera.main.transform.position;
            TargetDirection.y = 0;
            transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, TargetDirection, 10 * Time.deltaTime, 0));
        }

        Anim.SetBool("Summon Mech", SummonMechAnimation);

        if (Anim.GetCurrentAnimatorStateInfo(0).IsName("Summon Mech"))
        {
            SummonMechAnimation = false;
            if (Anim.GetCurrentAnimatorStateInfo(0).normalizedTime <= .5f)
            {
                Vector3 TargetDirection = transform.position - Camera.main.transform.position;
                TargetDirection.y = 0;
                transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, TargetDirection, 10 * Time.deltaTime, 0));
            }
        }

        if (MechClone != null)
        {
            MechClone.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = MechClone.GetComponent<Mech>().Ammo.ToString();
            // FIND all GameObject With a Specification 
            foreach (GameObject SpecificObjectType in GameObject.FindObjectsOfType<GameObject>())
            {
                if (SpecificObjectType.name == "AI Chip Case")
                {
                    SpecificObjectType.transform.position = new Vector3(SpecificObjectType.transform.position.x, -500, SpecificObjectType.transform.position.z);
                }
            }
        }
    }

    IEnumerator SpawnMech()
    {
        yield return new WaitForSeconds(1.6f);
        if (GetComponent<BodyGlow>().Color == "Green" || ColorType == "Green")
        {
            Mech = GreenMech;
        }
        if (GetComponent<BodyGlow>().Color == "Yellow" || ColorType == "Yellow")
        {
            Mech = YellowMech;
        }
        if (GetComponent<BodyGlow>().Color == "Red" || ColorType == "Red")
        {
            Mech = RedMech;
        }
        if (GetComponent<BodyGlow>().Color == "Blue" || ColorType == "Blue")
        {
            Mech = BlueMech;
        }

        if (GetComponent<BodyGlow>().Color == "Purple" || ColorType == "Purple")
        {
            Mech = PurpleMech;
        }
        MechClone = Instantiate(Mech, MechPosition, Quaternion.identity);
        MechClone.transform.position = new Vector3(MechClone.transform.position.x, -1, MechClone.transform.position.z);
        MechClone.transform.eulerAngles = Camera.main.transform.eulerAngles;
        MechClone.SetActive(true);
        MechClone.GetComponent<Mech>().RunSpeed = RunSpeed;
        MechClone.GetComponent<Mech>().Range = Range;
        MechClone.GetComponent<Mech>().FireRate = FireRate;
        MechClone.GetComponent<Mech>().Ammo = Ammo;
        MechClone.GetComponent<Mech>().ColorType = ColorType;
    }
}
