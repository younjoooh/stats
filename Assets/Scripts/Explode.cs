using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    GameObject Floor;
    bool MoveIn,AddCase;
    float Speed = 5;
    AudioSource SlowMoSound;
    public GameObject AIChipCase;
    public GameObject AIChip;
    GameObject Player;
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        AIChip = gameObject.transform.parent.transform.parent.gameObject;

        SlowMoSound = gameObject.AddComponent<UnityEngine.AudioSource>();
        SlowMoSound.clip = Resources.Load<AudioClip>("Audio/SlowMo Sound 2");
        SlowMoSound.spatialBlend = 0;
        SlowMoSound.volume = .75f;
        SlowMoSound.playOnAwake = false;

        Floor = GameObject.Find("Floor");
        GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-1.1f, 1f), Random.Range(-1.1f, 1f), Random.Range(-1.1f, 1f)) * 5f, ForceMode.VelocityChange);
        GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(-1.1f, 1f), Random.Range(-1.1f, 1f), Random.Range(-1.1f, 1f)) * 10f, ForceMode.VelocityChange);
        StartCoroutine("MoveInStart");
        StartCoroutine("PlaySlowMoSound");
        StartCoroutine("AddToCase");
    }


    void Update()
    {
        //Physics.IgnoreCollision(Floor.GetComponent<Collider>(), GetComponent<Collider>());
        if (MoveIn && transform.localPosition != new Vector3(0, 0, 0))
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(0, 0, 0), Speed/2 * Time.deltaTime);
            Speed+= 1 * Time.deltaTime;
        }

        if (AddCase)
        {
            AIChip.transform.position = Vector3.MoveTowards(AIChip.transform.position, AIChipCase.transform.GetChild(0).transform.position + AIChipCase.transform.GetChild(0).transform.forward * .0175f, .75f * Time.deltaTime);
            AIChip.transform.localScale = Vector3.MoveTowards(AIChip.transform.localScale, new Vector3(0.265f, 0.265f, .05f), .75f * Time.deltaTime);
        }

        if(AddCase)
        {
            if (AIChip.transform.position == AIChipCase.transform.GetChild(0).transform.position + AIChipCase.transform.GetChild(0).transform.forward * .0175f)
            {
                //AIChip.transform.eulerAngles = AIChipCase.transform.GetChild(0).transform.eulerAngles + new Vector3(0, 180, 0);
                AIChip.transform.parent = AIChipCase.transform.GetChild(0).transform;
                AIChip.transform.localEulerAngles = new Vector3(0, 180, 0);
                AIChip.transform.localScale = new Vector3(.45f, .45f, .1f);

            }
        }

        if (MoveIn && transform.localEulerAngles != new Vector3(-90, 0, 0))
        {
            GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(-90, 0, 0), Speed *.00075f * Time.time);
        }
        else
        {
            //transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(Random.Range(-1.1f, 1f), Random.Range(-1.1f, 1f), Random.Range(-1.1f, 1f)), 5f * Time.deltaTime);
            //transform.localEulerAngles = Vector3.MoveTowards(transform.localEulerAngles, new Vector3(Random.Range(-1.1f, 1f), Random.Range(-1.1f, 1f), Random.Range(-1.1f, 1f)), 5f * Time.deltaTime);
        }
    }

    IEnumerator MoveInStart()
    {
        yield return new WaitForSeconds(.75f);
        MoveIn = true;
        Player.GetComponent<CombineSouls>().AIChipCase.SetActive(true);
        AIChipCase = GameObject.Find("AI Chip Case");
        AIChipCase.transform.position = Player.transform.position + Camera.main.transform.forward * 2.5f;
        AIChipCase.transform.position = new Vector3(AIChipCase.transform.position.x, .5f, AIChipCase.transform.position.z);
        AIChipCase.transform.eulerAngles = Camera.main.transform.eulerAngles;

    }

    IEnumerator PlaySlowMoSound()
    {
        yield return new WaitForSeconds(.5f);
        SlowMoSound.PlayOneShot(SlowMoSound.clip, 1f);
    }

    IEnumerator AddToCase()
    {
        yield return new WaitForSeconds(2f);
        AddCase = true;
    }
}
