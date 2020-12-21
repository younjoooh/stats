using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapCollision : MonoBehaviour
{
    //[HideInInspector]
    public bool Shrink;
    public bool Grow;
    GameObject FireCaptureEffect;
    GameObject IceCaptureEffect;
    GameObject LostSpirit;
    GameObject Roots;
    GameObject Player;
    string TargetName = "Lost Spirit";
    void Start()
    {
        Roots = transform.GetChild(0).gameObject;
        Roots.SetActive(false);
        transform.localScale = new Vector3(0, 0, 0);
        Grow = true;
        Shrink = false;

        IceCaptureEffect = Instantiate(Resources.Load<GameObject>("Effects/Ice Capture Effect"), new Vector3(0, -500, 0), Quaternion.identity);
        IceCaptureEffect.SetActive(false);

        FireCaptureEffect = Instantiate(Resources.Load<GameObject>("Effects/Fire Capture Effect"), new Vector3(0, -500, 0), Quaternion.identity);
        FireCaptureEffect.SetActive(false);
        Player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if(Shrink)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(0, 0, 0), 5 * Time.deltaTime);
            if (LostSpirit != null)
            {
                LostSpirit.transform.localScale = Vector3.MoveTowards(LostSpirit.transform.localScale, new Vector3(0, 0, 0), 5 * Time.deltaTime);
                LostSpirit.transform.position = Vector3.MoveTowards(LostSpirit.transform.position, new Vector3(LostSpirit.transform.position.x, -5, LostSpirit.transform.position.z), 5 * Time.deltaTime);
            }
        }

        if (Grow && transform.localScale != new Vector3(2, .0001f, 2))
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(2, .0001f, 2), 5 * Time.deltaTime);
        }

        if (Shrink && transform.localScale == new Vector3 (0,0,0))
        {
            if (LostSpirit != null) 
            {
                Destroy(LostSpirit);
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider Coll)
    {
        if (Coll.gameObject.name == TargetName)
        {
            TargetName = "Null";
            Coll.gameObject.GetComponent<LostSpirit>().KnockedOut = true;
            Coll.gameObject.GetComponent<LostSpirit>().Captured = true;

            if (Coll.gameObject.tag == "Red" || Coll.gameObject.tag == "Yellow")
            {
                Player.GetComponent<PlaceTraps>().CaturedFireSpirits += 1;
            }

            if (Coll.gameObject.tag == "Blue" || Coll.gameObject.tag == "Green")
            {
                Player.GetComponent<PlaceTraps>().CaturedIceSpirits += 1;
            }

            if (Coll.gameObject.tag == "Blue" || Coll.gameObject.tag == "Green")
            {
                IceCaptureEffect.transform.position = transform.position;
                IceCaptureEffect.transform.localScale = Coll.gameObject.transform.localScale;
                IceCaptureEffect.SetActive(true);
            }

            if (Coll.gameObject.tag == "Red" || Coll.gameObject.tag == "Yellow")
            {
                FireCaptureEffect.transform.position = transform.position;
                FireCaptureEffect.transform.localScale = Coll.gameObject.transform.localScale;
                FireCaptureEffect.SetActive(true);
            }

            LostSpirit = Coll.gameObject;
            Roots.SetActive(true);
            StartCoroutine(DestroyTraps());
        }
        
    }

    private void OnTriggerStay(Collider Coll)
    {
        if (Coll.gameObject.name == "Lost Spirit")
        {
            Coll.gameObject.transform.position = Vector3.MoveTowards(Coll.gameObject.transform.position, new Vector3(this.gameObject.transform.position.x, Coll.gameObject.transform.position.y, this.gameObject.transform.position.z), 3 * Time.deltaTime);
         }

        if (!Shrink)
        {
            if (LostSpirit != null)
            {
                LostSpirit.transform.position = Vector3.MoveTowards(LostSpirit.transform.position, new Vector3(LostSpirit.transform.position.x, -.15f, LostSpirit.transform.position.z), .5f * Time.deltaTime);
            }
        }

    }

    IEnumerator DestroyTraps()
    {
        yield return new WaitForSeconds(6);
        Shrink = true;
        Grow = false;
    }
}
