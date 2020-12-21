using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeMutantExplode : MonoBehaviour
{
    float BreakForce = .2f;
    GameObject Player;
    void Start()
    {
        gameObject.AddComponent<Rigidbody>();
        GetComponent<Rigidbody>().useGravity = false;

        Player = GameObject.FindWithTag("Player");


        StartCoroutine("SpeedUp");
        StartCoroutine("DestroyPart");
    }


    void Update()
    {
        if (gameObject.name != "Merged Mutant Trans_Model" && gameObject.name != "Particle System")
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-BreakForce, BreakForce), Random.Range(-BreakForce, BreakForce), Random.Range(-BreakForce, BreakForce)) * BreakForce, ForceMode.VelocityChange);
            GetComponent<Rigidbody>().AddTorque(transform.up * Random.Range(-BreakForce, BreakForce));
            GetComponent<Rigidbody>().AddTorque(transform.right * Random.Range(-BreakForce, BreakForce));
            GetComponent<Rigidbody>().AddTorque(transform.forward * Random.Range(-BreakForce, BreakForce));
        }

        if (gameObject.name == "Particle System" && BreakForce == 3)
        {
            transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, 2 * Time.deltaTime);
        }
    }

    IEnumerator SpeedUp()
    {
        yield return new WaitForSeconds(1.5f);
        BreakForce = 3;

        if (gameObject.name == "Merged Mutant Trans_Model")
        {
            this.gameObject.transform.parent.GetChild(0).transform.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
    }

    IEnumerator DestroyPart()
    {
        yield return new WaitForSeconds(3);
        if (gameObject.name == "Particle System")
        {
            Destroy(this.gameObject.transform.parent.transform.gameObject);
        }
        //Destroy(this.gameObject);
    }


}
