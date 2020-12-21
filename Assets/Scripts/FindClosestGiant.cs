using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindClosestGiant : MonoBehaviour {
    //[HideInInspector]
    public GameObject Giant;
    void Update () 
    {
		FindClosest();
	}


	void FindClosest()
	{
		float DistanceToClosestGiant = Mathf.Infinity;
		Giant ClosestGiant = null;
		Giant[] EveryGiant = GameObject.FindObjectsOfType<Giant>();

		foreach (Giant CurrentGiant in EveryGiant) 
        {
			float DistanceToGiant = (CurrentGiant.transform.position - this.transform.position).sqrMagnitude;
            if (DistanceToGiant < DistanceToClosestGiant && GetComponent<Mech>().tag == CurrentGiant.tag && !CurrentGiant.GetComponent<Giant>().KnockedOut)           
            {
                DistanceToClosestGiant = DistanceToGiant;
                ClosestGiant = CurrentGiant;
            }

		}

        if (ClosestGiant != null)
        {
            Giant = ClosestGiant.gameObject;
        }
        else 
        {
            Giant = null;
        }

        if (ClosestGiant != null)
        {
            Debug.DrawLine(this.transform.position, ClosestGiant.transform.position);
        }
	}

}
