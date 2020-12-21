using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Load3b : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("p"))
        {
            SceneManager.LoadScene("Lesson 3B", LoadSceneMode.Single);
        }

        if (Input.GetKeyDown("1"))
        {
            SceneManager.LoadScene("Lesson 3A", LoadSceneMode.Single);
        }

        if (Input.GetKeyDown("2"))
        {
            SceneManager.LoadScene("Lesson 3A", LoadSceneMode.Single);
        }

    }
}
