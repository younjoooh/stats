using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playEgg : MonoBehaviour
{
    public GameObject EggEffect;
    public GameObject Egg;

    public bool PurpleCave;
    public bool BlueCave;
    public bool RedCave;
    public bool YellowCave;
    public bool GreenCave;
    void Start()
    {
        Egg = GameObject.Find("Dragon Egg");
        //EggEffect = GameObject.Find("Egg Explosion");
        //EggEffect.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Egg == null)
        {
            Egg = GameObject.Find("Dragon Egg");
        }

        if (EggEffect == null)
        {
            EggEffect = GameObject.Find("Egg Explosion");
        }

        if (Input.GetKeyDown("p") && PurpleCave)
        {
            foreach (GameObject PurpleEgg in GameObject.FindObjectsOfType<GameObject>())
            {
                if (PurpleEgg.tag == "Purple Egg")
                {
                    EggEffect = Instantiate(Resources.Load<GameObject>("Effects/Egg Explosion"), PurpleEgg.transform.position, Quaternion.identity);
                    EggEffect.transform.parent = PurpleEgg.transform;
                    EggEffect.transform.localPosition = new Vector3(0.001160189f, -0.005998438f, -0.009709657f);
                    EggEffect.transform.localScale = new Vector3(0.01541776f, 0.01541772f, 0.01541777f);
                    StartCoroutine("EggShrink", PurpleEgg);
                }
            }
        }

        if (Input.GetKeyDown("p") && BlueCave)
        {
            foreach (GameObject BlueEgg in GameObject.FindObjectsOfType<GameObject>())
            {
                if (BlueEgg.tag == "Blue Egg")
                {
                    EggEffect = Instantiate(Resources.Load<GameObject>("Effects/Egg Explosion"), BlueEgg.transform.position, Quaternion.identity);
                    EggEffect.transform.parent = BlueEgg.transform;
                    EggEffect.transform.localPosition = new Vector3(0.001160189f, -0.005998438f, -0.009709657f);
                    EggEffect.transform.localScale = new Vector3(0.01541776f, 0.01541772f, 0.01541777f);
                    StartCoroutine("EggShrink", BlueEgg);
                }
            }
        }

        if (Input.GetKeyDown("p") && RedCave)
        {
            foreach (GameObject RedEgg in GameObject.FindObjectsOfType<GameObject>())
            {
                if (RedEgg.tag == "Red Egg")
                {
                    EggEffect = Instantiate(Resources.Load<GameObject>("Effects/Egg Explosion"), RedEgg.transform.position, Quaternion.identity);
                    EggEffect.transform.parent = RedEgg.transform;
                    EggEffect.transform.localPosition = new Vector3(0.001160189f, -0.005998438f, -0.009709657f);
                    EggEffect.transform.localScale = new Vector3(0.01541776f, 0.01541772f, 0.01541777f);
                    StartCoroutine("EggShrink", RedEgg);
                }
            }
        }

        if (Input.GetKeyDown("p") && YellowCave)
        {
            foreach (GameObject YellowEgg in GameObject.FindObjectsOfType<GameObject>())
            {
                if (YellowEgg.tag == "Yellow Egg")
                {
                    EggEffect = Instantiate(Resources.Load<GameObject>("Effects/Egg Explosion"), YellowEgg.transform.position, Quaternion.identity);
                    EggEffect.transform.parent = YellowEgg.transform;
                    EggEffect.transform.localPosition = new Vector3(0.001160189f, -0.005998438f, -0.009709657f);
                    EggEffect.transform.localScale = new Vector3(0.01541776f, 0.01541772f, 0.01541777f);
                    StartCoroutine("EggShrink", YellowEgg);
                }
            }
        }


        if (Input.GetKeyDown("p") && GreenCave)
        {
            foreach (GameObject GreenEgg in GameObject.FindObjectsOfType<GameObject>())
            {
                if (GreenEgg.tag == "Green Egg")
                {
                    EggEffect = Instantiate(Resources.Load<GameObject>("Effects/Egg Explosion"), GreenEgg.transform.position, Quaternion.identity);
                    EggEffect.transform.parent = GreenEgg.transform;
                    EggEffect.transform.localPosition = new Vector3(0.001160189f, -0.005998438f, -0.009709657f);
                    EggEffect.transform.localScale = new Vector3(0.01541776f, 0.01541772f, 0.01541777f);
                    StartCoroutine("EggShrink", GreenEgg);
                }
            }
        }

    }

    IEnumerator EggShrink(GameObject Egg)
    {
        yield return new WaitForSeconds(2.5f);
        EggEffect.transform.parent = null;
        Destroy(Egg.gameObject);
    }

    void OnTriggerStay(Collider Coll)
    {
        if (Coll.gameObject.tag == "Purple Egg")
        {
            PurpleCave = true;
        }
        else
        {
            PurpleCave = false;
        }

        if (Coll.gameObject.tag == "Blue Egg")
        {
            BlueCave = true;
        }
        else
        {
            BlueCave = false;
        }

        if (Coll.gameObject.tag == "Red Egg")
        {
           RedCave = true;
        }
        else
        {
            RedCave = false;
        }

        if (Coll.gameObject.tag == "Yellow Egg")
        {
            YellowCave = true;
        }
        else
        {
            YellowCave = false;
        }

        if (Coll.gameObject.tag == "Green Egg")
        {
            GreenCave = true;
        }
        else
        {
            GreenCave = false;
        }
    }


}
