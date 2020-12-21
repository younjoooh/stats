using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectScene : MonoBehaviour
{
    public GameObject Lesson1;
    public GameObject Lesson2;
    public GameObject Lesson3A;
    public GameObject Lesson3B;
    public GameObject Lesson4;
    public GameObject Lesson5and6;
    public GameObject Lesson7and8;
    public GameObject PiChartCam;

    void Start()
    {
        /*
        Lesson1 = GameObject.Find("Cyber Square (Giants)");
        Lesson2 = GameObject.Find("Cyber Water (Eggs)");
        Lesson3A = GameObject.Find("Cyber Square (Parasites)");
        Lesson3B = GameObject.Find("Cyber Pie (Pie Chart)");
        Lesson4 = GameObject.Find("Cyber Square (Median)");
        Lesson5and6 = GameObject.Find("Cyber Square (Lost Spirits)");
        Lesson7and8 = GameObject.Find("Cyber Street (Rebels)");
        */
    }



    void Update()
    {
        if (Input.GetKeyDown("z"))
        {
            SceneManager.LoadScene("Lesson 1", LoadSceneMode.Single);
        }

        if (Input.GetKeyDown("x"))
        {
            SceneManager.LoadScene("Lesson 2", LoadSceneMode.Single);
        }

        if (Input.GetKeyDown("c"))
        {
            SceneManager.LoadScene("Lesson 3A", LoadSceneMode.Single);
        }

        if (Input.GetKeyDown("v"))
        {
            SceneManager.LoadScene("Lesson 3B", LoadSceneMode.Single);
        }

        if (Input.GetKeyDown("b"))
        {
            SceneManager.LoadScene("Lesson 4", LoadSceneMode.Single);
        }

        if (Input.GetKeyDown("n"))
        {
            SceneManager.LoadScene("Lesson 5and6", LoadSceneMode.Single);
        }

        if (Input.GetKeyDown("m"))
        {
            SceneManager.LoadScene("Lesson 7and8", LoadSceneMode.Single);
        }
    }
}
