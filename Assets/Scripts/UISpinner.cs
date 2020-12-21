using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISpinner : MonoBehaviour
{
    public Vector3 Angle = new Vector3 (0,1,0);
    public float Speed = 40;
    void Start()
    {
    }

    void Update()
    {
       transform.RotateAround(transform.position, Angle, Speed * Time.deltaTime);
    }
}
