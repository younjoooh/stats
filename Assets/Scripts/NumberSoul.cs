using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberSoul : MonoBehaviour
{
    public bool Marked;
    public bool Centered;
    public bool Combined;
    public GameObject Player;
    public GameObject MarkedRaysEffect;
    public GameObject MarkedRaysEffectClone;
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        //MarkedRaysEffect = Instantiate(Resources.Load<GameObject>("Effects/Marked Rays Effect"), new Vector3(0, -5000, 0), Quaternion.identity);
        //MarkedRaysEffect.SetActive(false);
    }

    void Update()
    {
        //MARKED
        if (Marked && transform.childCount > 0 && !Centered)
        {
            transform.GetChild(2).gameObject.transform.position = Vector3.MoveTowards(transform.GetChild(2).gameObject.transform.position, transform.position, 10 * Time.deltaTime);
        }

        if (!Centered)
        {
            if (Marked && transform.GetChild(2).gameObject.transform.position == transform.position)
            {
                transform.GetChild(2).gameObject.transform.parent = this.gameObject.transform;
                Centered = true;
                //MarkedRaysEffectClone = Instantiate(MarkedRaysEffect, transform.position, Quaternion.identity);
                //MarkedRaysEffectClone.SetActive(true);
                //MarkedRaysEffectClone.transform.parent = transform;
            }
        }

    }
}
