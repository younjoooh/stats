using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    Vector3 TargetDirection;
    Vector3 NewDirection;
    public GameObject TargetObject;
    float TravelSpeed;
    float TurnSpeed = 1;



    void Start()
    {

    }


    void Update()
    {

        Debug.Log(transform.parent.gameObject.name);



    }

}



