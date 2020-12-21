using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostSpiritAttack : MonoBehaviour
{

    void Start()
    {
        
    }


    void Update()
    {


    }

     void OnTriggerEnter(Collider Coll)
    {
        if (this.gameObject.name == "Ice Explosion")
        {
            if (Coll.gameObject.name == "Parasite")
            {
                if (!Coll.gameObject.GetComponent<Parasite>().Disabled && !Coll.gameObject.GetComponent<Parasite>().KnockedOut)
                {
                    Coll.gameObject.GetComponent<Parasite>().Disabled = true;
                    Coll.gameObject.GetComponent<Parasite>().Frozen = true;
                    Coll.gameObject.GetComponent<Parasite>().Multiplier = 0;
                    Coll.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    GameObject IceCone = Instantiate(Resources.Load<GameObject>("Effects/Ice Cone"), Coll.gameObject.transform.position - new Vector3(0, 1, 0), Quaternion.identity);
                    IceCone.name = "Ice Cone";
                    IceCone.transform.parent = Coll.gameObject.transform;
                }
            }
        }

        if (this.gameObject.name == "Flames")
        {
            if (Coll.gameObject.name == "Parasite")
            {
                if (!Coll.gameObject.GetComponent<Parasite>().Disabled && !Coll.gameObject.GetComponent<Parasite>().KnockedOut)
                {
                    Coll.gameObject.GetComponent<Parasite>().Disabled = true;
                    Coll.gameObject.GetComponent<Parasite>().Burned = true;
                    Coll.gameObject.GetComponent<Parasite>().KnockedOutStyle = 0;
                    Coll.gameObject.GetComponent<Parasite>().ScreamSound.PlayOneShot(Coll.gameObject.GetComponent<Parasite>().ScreamSound.clip, 1f);
                    Coll.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    GameObject Flames = Instantiate(Resources.Load<GameObject>("Effects/Dripping Flames"), Coll.gameObject.transform.position + new Vector3(0, -.5f, .15f), Quaternion.identity);
                    Flames.name = "Flames";
                    Flames.transform.parent = Coll.gameObject.transform;
                }
            }
        }

        if (this.gameObject.name == "Poison Blast")
        {
            if (Coll.gameObject.name == "Parasite")
            {
                if (!Coll.gameObject.GetComponent<Parasite>().Disabled && !Coll.gameObject.GetComponent<Parasite>().KnockedOut)
                {
                    Coll.gameObject.GetComponent<Parasite>().Disabled = true;
                    Coll.gameObject.GetComponent<Parasite>().Poisoned = true;
                    Coll.gameObject.GetComponent<Parasite>().Range = 0f;
                    //Coll.gameObject.GetComponent<Parasite>().KnockedOutStyle = 0;
                    Coll.gameObject.GetComponent<Parasite>().ScreamSound.PlayOneShot(Coll.gameObject.GetComponent<Parasite>().ScreamSound.clip, 1f);
                    Coll.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    GameObject Flames = Instantiate(Resources.Load<GameObject>("Effects/Dripping Poison"), Coll.gameObject.transform.position + new Vector3(0, -1f, 0f), Quaternion.identity);
                    Flames.name = "Poison Blast";
                    //Flames.transform.parent = Coll.gameObject.transform;
                }
            }
        }

        if (this.gameObject.name == "Electric Blast Effect")
        {
            if (Coll.gameObject.name == "Parasite")
            {
                if (!Coll.gameObject.GetComponent<Parasite>().Disabled && !Coll.gameObject.GetComponent<Parasite>().KnockedOut)
                {
                    Coll.gameObject.GetComponent<Parasite>().Disabled = true;
                    Coll.gameObject.GetComponent<Parasite>().Electrocuted = true;
                    //Coll.gameObject.GetComponent<Parasite>().Multiplier = 0;
                    //Coll.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    GameObject IceCone = Instantiate(Resources.Load<GameObject>("Effects/Charge Effect Plus"), Coll.gameObject.transform.position, Quaternion.identity);
                    IceCone.transform.parent = Coll.gameObject.transform;
                    IceCone.transform.GetChild(0).transform.localScale = new Vector3(2, 2, 2);
                    IceCone.name = "Electric Blast Effect";
                    StartCoroutine(DestroyEffect(IceCone, 2));
                }
            }
        }
    }

    IEnumerator DestroyEffect(GameObject Effect, float time)
    {
        yield return new WaitForSeconds(time);
        if (Effect!= null)
        {
            Destroy(Effect);
        }
    }
}
