using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChipBuild : MonoBehaviour
{
    float Speed = 5;
    float Range = 7;
    Vector3 DefaultPosition;
    Vector3 RandomPosition;

    Vector3 DefaultRotation;
    Vector3 RandomRotation;

    bool HasBeenRandomized;
    GameObject AIChipCase;

    void Start()
    {
        AIChipCase = GameObject.Find("AI Chip Case");
        Randomize();
    }
    public void Randomize()
    {
        DefaultPosition = transform.localPosition;
        DefaultRotation = transform.eulerAngles;

        RandomPosition = transform.position + new Vector3(Random.Range(-Range, Range), Random.Range(-Range, Range), Random.Range(-Range, Range));
        RandomRotation = new Vector3(Random.Range(-180.0f, 180.0f), Random.Range(-180.0f, 180.0f), Random.Range(-180.0f, 180.0f));

        transform.position = RandomPosition;
        transform.eulerAngles = RandomRotation;

        HasBeenRandomized = true;
    }

    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            HasBeenRandomized = false;

            foreach (GameObject AIChipCases in GameObject.FindObjectsOfType<GameObject>())
            {
                if (AIChipCases.GetComponent<AIChipBuild>())
                {
                    AIChipCases.GetComponent<AIChipBuild>(). Randomize();
                }
            }

            AIChipCase.SetActive(false);
        }

        if (HasBeenRandomized)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, DefaultPosition, Speed * 1.5f * Time.deltaTime);

            if (transform.localEulerAngles != new Vector3(-90, 0, 0))
            {
                //GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
                transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0, 180, 0), Speed * .0075f * Time.time);
            }
        }
    }
}
