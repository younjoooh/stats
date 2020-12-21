using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePointer : MonoBehaviour
{
    public GameObject Icon;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Icon.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Camera.main.transform.forward *5;

    }
}
