using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBreak : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Transform[] allChildren = transform.GetChild(2).GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            child.transform.position = Vector3.MoveTowards(child.transform.position, Camera.main.transform.forward * 10, 5  * Time.deltaTime);
        }
        
    }
}
