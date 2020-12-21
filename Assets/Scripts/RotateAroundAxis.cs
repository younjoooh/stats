using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundAxis : MonoBehaviour
{
    public GameObject Target;
    public float Speed = 500;
    public Vector3 Angle = new Vector3(1,1,1);
    void Start()
    {
    }

    void Update()
    {
        transform.RotateAround(Target.transform.position, Angle, Speed * Time.deltaTime);
    }
}
