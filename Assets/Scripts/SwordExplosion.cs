using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordExplosion : MonoBehaviour
{
    float BreakSpeed = 10;
    float MergeSpeed = 10;
    float Range = 7.5f;

    GameObject Player;
    public bool Explode;
    public bool Merge;

    void Start()
    {
        Player = GameObject.FindWithTag("Player");


        //StartCoroutine("SpeedUp");
        //StartCoroutine("DestroyPart");
    }

    void OnEnable()
    {
        //StopCoroutine("TriggerEnding");
        //StartCoroutine("TriggerEnding");

        MergeSpeed = 15;

        BreakSpeed = 10f;
        Explode = false;
        Merge = true;
        Range = 7.5f;

        transform.localPosition = new Vector3(Random.Range(-Range, Range), Random.Range(-Range, Range), Random.Range(-Range, Range));
        transform.localEulerAngles = new Vector3(Random.Range(-Range, Range), Random.Range(-Range, Range), Random.Range(-Range, Range));  
    }

    void Update()
    {

        if (GetComponent<Renderer>().material != transform.parent.GetComponent<Renderer>().material)
        {
            GetComponent<Renderer>().material = transform.parent.GetComponent<Renderer>().material;
        }

        if (Player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("Weapon Attack"))
        {
            //StopCoroutine("TriggerEnding");
        }


        if (Explode)
        {
            transform.localPosition += new Vector3(Random.Range(-Range, Range), Random.Range(-Range, Range), Random.Range(-Range, Range)) * Time.deltaTime;
            //transform.localEulerAngles = Vector3.MoveTowards(transform.localEulerAngles, new Vector3(Random.Range(-Range, Range), Random.Range(-Range, Range), Random.Range(-Range, Range)), BreakSpeed * Time.deltaTime);
            Range += 1 * Time.deltaTime;
        }


        if (Merge)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0, 0, 0), MergeSpeed/2 * Time.time);
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(0,0,0), MergeSpeed * Time.deltaTime);
        }

    }

    IEnumerator SpeedUp()
    {
        yield return new WaitForSeconds(.5f);
        BreakSpeed = 1;
        Range = 30;
    }


    IEnumerator TriggerEnding()
    {
        yield return new WaitForSeconds(4);
        BreakSpeed = 1;
        Explode = true;
        Merge = false;
        Range = 1f;
        //StopCoroutine("DestroyPart");
        //StopCoroutine("SpeedUp");

        //StartCoroutine("DestroyPart");
        //StartCoroutine("SpeedUp");
    }

    IEnumerator DestroyPart()
    {
        yield return new WaitForSeconds(1.25f);
        transform.parent.transform.parent.gameObject.SetActive(false);
        Explode = false;
    }
}