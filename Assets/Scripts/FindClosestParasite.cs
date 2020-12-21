using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindClosestParasite : MonoBehaviour {
    //[HideInInspector]
    public GameObject Parasite;
    void Update () 
    {
		FindClosest();
	}


	void FindClosest()
	{
		float DistanceToClosestParasite = Mathf.Infinity;
		Parasite ClosestParasite = null;
		Parasite[] EveryParasite = GameObject.FindObjectsOfType<Parasite>();

		foreach (Parasite CurrentParasite in EveryParasite) 
        {
			float DistanceToParasite = (CurrentParasite.transform.position - this.transform.position).sqrMagnitude;
            if (DistanceToParasite < DistanceToClosestParasite && CurrentParasite!= this.gameObject && !CurrentParasite.GetComponent<Parasite>().KnockedOut && (!CurrentParasite.GetComponent<Parasite>().Disabled || this.gameObject.tag == "Player"))           
            {
                DistanceToClosestParasite = DistanceToParasite;
                ClosestParasite = CurrentParasite;
            }

		}

        if (ClosestParasite != null)
        {
            Parasite = ClosestParasite.gameObject;
        }
        else 
        {
            Parasite = null;
        }

        if (ClosestParasite != null)
        {
            Debug.DrawLine(this.transform.position, ClosestParasite.transform.position);
        }
	}

}
