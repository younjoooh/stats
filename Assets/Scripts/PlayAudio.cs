using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    public AudioSource Male1, Male2, Male3, Male4, Male5, Female1, Female2, Female3, Female4, Female5;
    public GameObject MaleObj1, MaleObj2, MaleObj3, MaleObj4, MaleObj5, FemaleObj1, FemaleObj2, FemaleObj3, FemaleObj4, FemaleObj5;
    public GameObject Male3D1, Male3D2, Male3D3, Male3D4, Male3D5, Female3D1, Female3D2, Female3D3, Female3D4, Female3D5;
    public GameObject Male2D1, Male2D2, Male2D3, Male2D4, Male2D5, Female2D1, Female2D2, Female2D3, Female2D4, Female2D5;

    GameObject RebelCam, MainCam, HeadTracker;
    public GameObject MalePath1, MalePath2, MalePath3, MalePath4, MalePath5, FemalePath1, FemalePath2, FemalePath3, FemalePath4, FemalePath5;
    public bool Singing;
    public GameObject MechCanvas, ParasiteCanvas, MarkedCanvas;
    void Start()
    {
        AudioListener.volume = 0;
        MaleObj1 = GameObject.Find("Male Green");
        MaleObj2 = GameObject.Find("Male Yellow");
        MaleObj3 = GameObject.Find("Male Red");
        MaleObj4 = GameObject.Find("Male Blue");
        MaleObj5 = GameObject.Find("Male Purple");

        FemaleObj1 = GameObject.Find("Female Green");
        FemaleObj2 = GameObject.Find("Female Yellow");
        FemaleObj3 = GameObject.Find("Female Red");
        FemaleObj4 = GameObject.Find("Female Blue");
        FemaleObj5 = GameObject.Find("Female Purple");


        Male3D1 = MaleObj1.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).transform.GetChild(4).gameObject;
        Male3D2 = MaleObj2.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).transform.GetChild(4).gameObject;
        Male3D3 = MaleObj3.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).transform.GetChild(4).gameObject;
        Male3D4 = MaleObj4.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).transform.GetChild(4).gameObject;
        Male3D5 = MaleObj5.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).transform.GetChild(4).gameObject;

        Female3D1 = FemaleObj1.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).transform.GetChild(4).gameObject;
        Female3D2 = FemaleObj2.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).transform.GetChild(4).gameObject;
        Female3D3 = FemaleObj3.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).transform.GetChild(4).gameObject;
        Female3D4 = FemaleObj4.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).transform.GetChild(4).gameObject;
        Female3D5 = FemaleObj5.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).transform.GetChild(4).gameObject;

        Male3D1.SetActive(false);
        Male3D2.SetActive(false);
        Male3D3.SetActive(false);
        Male3D4.SetActive(false);
        Male3D5.SetActive(false);

        Female3D1.SetActive(false);
        Female3D2.SetActive(false);
        Female3D3.SetActive(false);
        Female3D4.SetActive(false);
        Female3D5.SetActive(false);


        Male2D1 = MaleObj1.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).transform.GetChild(3).gameObject;
        Male2D2 = MaleObj2.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).transform.GetChild(3).gameObject;
        Male2D3 = MaleObj3.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).transform.GetChild(3).gameObject;
        Male2D4 = MaleObj4.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).transform.GetChild(3).gameObject;
        Male2D5 = MaleObj5.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).transform.GetChild(3).gameObject;

        Female2D1 = FemaleObj1.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).transform.GetChild(3).gameObject;
        Female2D2 = FemaleObj2.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).transform.GetChild(3).gameObject;
        Female2D3 = FemaleObj3.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).transform.GetChild(3).gameObject;
        Female2D4 = FemaleObj4.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).transform.GetChild(3).gameObject;
        Female2D5 = FemaleObj5.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).transform.GetChild(3).gameObject;

        Male2D1.SetActive(false);
        Male2D2.SetActive(false);
        Male2D3.SetActive(false);
        Male2D4.SetActive(false);
        Male2D5.SetActive(false);

        Female2D1.SetActive(false);
        Female2D2.SetActive(false);
        Female2D3.SetActive(false);
        Female2D4.SetActive(false);
        Female2D5.SetActive(false);


        Male1 = gameObject.AddComponent<UnityEngine.AudioSource>();
        Male1.clip = Resources.Load<AudioClip>("Audio/Rebels/Male1");
        Male1.spatialBlend = 0;
        Male1.volume = .75f;
        Male1.playOnAwake = false;

        Male2 = gameObject.AddComponent<UnityEngine.AudioSource>();
        Male2.clip = Resources.Load<AudioClip>("Audio/Rebels/Male2");
        Male2.spatialBlend = 0;
        Male2.volume = .75f;
        Male2.playOnAwake = false;

        Male3 = gameObject.AddComponent<UnityEngine.AudioSource>();
        Male3.clip = Resources.Load<AudioClip>("Audio/Rebels/Male3");
        Male3.spatialBlend = 0;
        Male3.volume = .75f;
        Male3.playOnAwake = false;

        Male4 = gameObject.AddComponent<UnityEngine.AudioSource>();
        Male4.clip = Resources.Load<AudioClip>("Audio/Rebels/Male4");
        Male4.spatialBlend = 0;
        Male4.volume = .75f;
        Male4.playOnAwake = false;

        Male5 = gameObject.AddComponent<UnityEngine.AudioSource>();
        Male5.clip = Resources.Load<AudioClip>("Audio/Rebels/Male5");
        Male5.spatialBlend = 0;
        Male5.volume = .75f;
        Male5.playOnAwake = false;





        Female1 = gameObject.AddComponent<UnityEngine.AudioSource>();
        Female1.clip = Resources.Load<AudioClip>("Audio/Rebels/Female1");
        Female1.spatialBlend = 0;
        Female1.volume = .75f;
        Female1.playOnAwake = false;

        Female2 = gameObject.AddComponent<UnityEngine.AudioSource>();
        Female2.clip = Resources.Load<AudioClip>("Audio/Rebels/Female2");
        Female2.spatialBlend = 0;
        Female2.volume = .75f;
        Female2.playOnAwake = false;

        Female3 = gameObject.AddComponent<UnityEngine.AudioSource>();
        Female3.clip = Resources.Load<AudioClip>("Audio/Rebels/Female3");
        Female3.spatialBlend = 0;
        Female3.volume = .75f;
        Female3.playOnAwake = false;

        Female4 = gameObject.AddComponent<UnityEngine.AudioSource>();
        Female4.clip = Resources.Load<AudioClip>("Audio/Rebels/Female4");
        Female4.spatialBlend = 0;
        Female4.volume = .75f;
        Female4.playOnAwake = false;

        Female5 = gameObject.AddComponent<UnityEngine.AudioSource>();
        Female5.clip = Resources.Load<AudioClip>("Audio/Rebels/Female5");
        Female5.spatialBlend = 0;
        Female5.volume = .75f;
        Female5.playOnAwake = false;

        HeadTracker = GameObject.Find("Head Tracker");
        MainCam = GameObject.Find("Main Camera");
        RebelCam = GameObject.Find("Rebel Camera");
        RebelCam.SetActive(false);

        FemalePath1 = GameObject.Find("Female Green Path");
        FemalePath2 = GameObject.Find("Female Yellow Path");
        FemalePath3 = GameObject.Find("Female Red Path");
        FemalePath4 = GameObject.Find("Female Blue Path");
        FemalePath5 = GameObject.Find("Female Purple Path");

        MalePath1 = GameObject.Find("Male Green Path");
        MalePath2 = GameObject.Find("Male Yellow Path");
        MalePath3 = GameObject.Find("Male Red Path");
        MalePath4 = GameObject.Find("Male Blue Path");
        MalePath5 = GameObject.Find("Male Purple Path");

        
        FemalePath1.SetActive(false);
        FemalePath2.SetActive(false);
        FemalePath3.SetActive(false);
        FemalePath4.SetActive(false);
        FemalePath5.SetActive(false);

        MalePath1.SetActive(false);
        MalePath2.SetActive(false);
        MalePath3.SetActive(false);
        MalePath4.SetActive(false);
        MalePath5.SetActive(false);
    }
    void Update()
    {
        if (MechCanvas == null)
        {
            MechCanvas = GameObject.Find("Mech Canvas");
        }

        if (ParasiteCanvas == null)
        {
            ParasiteCanvas = GameObject.Find("Parasite Canvas");
        }

        if (MarkedCanvas == null)
        {
            MarkedCanvas = GameObject.Find("Marked Canvas");
        }

        if (Input.GetKeyDown("1"))
        {
            //Male1.PlayOneShot(Male1.clip, 1f);
            MainCam.SetActive(false);
            RebelCam.SetActive(true);
            MalePath1.SetActive(true);
            Male2D1.SetActive(true);
            Male3D1.SetActive(true);
            MaleObj1.GetComponent<Animator>().SetFloat("Multiplier", .2f);
            StopCoroutine(ReturnPlayer(0));
            StartCoroutine("ReturnPlayer", 42f);
            Singing = true;

            HeadTracker.transform.position = MaleObj1.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).transform.position;
            HeadTracker.transform.parent = MaleObj1.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).transform;

            FemalePath1.SetActive(false);
            FemalePath2.SetActive(false);
            FemalePath3.SetActive(false);
            FemalePath4.SetActive(false);
            FemalePath5.SetActive(false);

            MalePath1.SetActive(true);
            MalePath2.SetActive(false);
            MalePath3.SetActive(false);
            MalePath4.SetActive(false);
            MalePath5.SetActive(false);

            MalePath1.GetComponent<CPC_CameraPath>().PlayPath(30);
            //MechCanvas.SetActive(false);
            //MarkedCanvas.SetActive(false);
            //ParasiteCanvas.SetActive(false);
        }

        else if (Input.GetKeyDown("2") )
        {
            //Male2.PlayOneShot(Male2.clip, 1f);
            MainCam.SetActive(false);
            RebelCam.SetActive(true);
            MalePath2.SetActive(true);
            Male2D2.SetActive(true);
            Male3D2.SetActive(true);
            MaleObj2.GetComponent<Animator>().SetFloat("Multiplier", .2f);
            StopCoroutine(ReturnPlayer(0));
            StartCoroutine("ReturnPlayer", 63);
            Singing = true;

            HeadTracker.transform.position = MaleObj2.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).transform.position;
            HeadTracker.transform.parent = MaleObj2.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).transform;

            FemalePath1.SetActive(false);
            FemalePath2.SetActive(false);
            FemalePath3.SetActive(false);
            FemalePath4.SetActive(false);
            FemalePath5.SetActive(false);

            MalePath1.SetActive(false);
            MalePath2.SetActive(true);
            MalePath3.SetActive(false);
            MalePath4.SetActive(false);
            MalePath5.SetActive(false);

            MalePath2.GetComponent<CPC_CameraPath>().PlayPath(30);
            //MechCanvas.SetActive(false);
            //MarkedCanvas.SetActive(false);
            //ParasiteCanvas.SetActive(false);
        }

        if (Input.GetKeyDown("3") )
        {
            //Male3.PlayOneShot(Male3.clip, 1f);
            MainCam.SetActive(false);
            RebelCam.SetActive(true);
            MalePath3.SetActive(true);
            Male2D3.SetActive(true);
            Male3D3.SetActive(true);
            MaleObj3.GetComponent<Animator>().SetFloat("Multiplier", .2f);
            StopCoroutine(ReturnPlayer(0));
            StartCoroutine("ReturnPlayer", 35);
            Singing = true;

            HeadTracker.transform.position = MaleObj3.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).transform.position;
            HeadTracker.transform.parent = MaleObj3.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).transform;

            FemalePath1.SetActive(false);
            FemalePath2.SetActive(false);
            FemalePath3.SetActive(false);
            FemalePath4.SetActive(false);
            FemalePath5.SetActive(false);

            MalePath1.SetActive(false);
            MalePath2.SetActive(false);
            MalePath3.SetActive(true);
            MalePath4.SetActive(false);
            MalePath5.SetActive(false);

            MalePath3.GetComponent<CPC_CameraPath>().PlayPath(30);
            //MechCanvas.SetActive(false);
            //MarkedCanvas.SetActive(false);
            //ParasiteCanvas.SetActive(false);
        }

        if (Input.GetKeyDown("4"))
        {
            //Male4.PlayOneShot(Male4.clip, 1f);
            MainCam.SetActive(false);
            RebelCam.SetActive(true);
            MalePath4.SetActive(true);
            Male2D4.SetActive(true);
            Male3D4.SetActive(true);
            MaleObj4.GetComponent<Animator>().SetFloat("Multiplier", .2f);
            StopCoroutine(ReturnPlayer(0));
            StartCoroutine("ReturnPlayer", 51f);
            Singing = true;

            HeadTracker.transform.position = MaleObj4.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).transform.position;
            HeadTracker.transform.parent = MaleObj4.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).transform;

            FemalePath1.SetActive(false);
            FemalePath2.SetActive(false);
            FemalePath3.SetActive(false);
            FemalePath4.SetActive(false);
            FemalePath5.SetActive(false);

            MalePath1.SetActive(false);
            MalePath2.SetActive(false);
            MalePath3.SetActive(false);
            MalePath4.SetActive(true);
            MalePath5.SetActive(false);

            MalePath4.GetComponent<CPC_CameraPath>().PlayPath(30);
            //MechCanvas.SetActive(false);
            //MarkedCanvas.SetActive(false);
            //ParasiteCanvas.SetActive(false);
        }

        if (Input.GetKeyDown("5") )
        {
            //Male5.PlayOneShot(Male5.clip, 1f);
            MainCam.SetActive(false);
            RebelCam.SetActive(true);
            MalePath5.SetActive(true);
            Male2D5.SetActive(true);
            Male3D5.SetActive(true);
            MaleObj5.GetComponent<Animator>().SetFloat("Multiplier", .2f);
            StopCoroutine(ReturnPlayer(0));
            StartCoroutine("ReturnPlayer", 65);
            Singing = true;

            HeadTracker.transform.position = MaleObj5.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).transform.position;
            HeadTracker.transform.parent = MaleObj5.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).transform;


            FemalePath1.SetActive(false);
            FemalePath2.SetActive(false);
            FemalePath3.SetActive(false);
            FemalePath4.SetActive(false);
            FemalePath5.SetActive(false);

            MalePath1.SetActive(false);
            MalePath2.SetActive(false);
            MalePath3.SetActive(false);
            MalePath4.SetActive(false);
            MalePath5.SetActive(true);

            MalePath5.GetComponent<CPC_CameraPath>().PlayPath(30);
            //MechCanvas.SetActive(false);
            //MarkedCanvas.SetActive(false);
            //ParasiteCanvas.SetActive(false);
        }





        if (Input.GetKeyDown("6") )
        {
            //Female1.PlayOneShot(Female1.clip, 1f);
            MainCam.SetActive(false);
            RebelCam.SetActive(true);
            FemalePath1.SetActive(true);
            Female2D1.SetActive(true);
            Female3D1.SetActive(true);
            FemaleObj1.GetComponent<Animator>().SetFloat("Multiplier", .2f);
            StopCoroutine(ReturnPlayer(0));
            StartCoroutine("ReturnPlayer", 27);
            Singing = true;

            HeadTracker.transform.position = FemaleObj1.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).transform.position;
            HeadTracker.transform.parent = FemaleObj1.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).transform;


            FemalePath1.SetActive(true);
            FemalePath2.SetActive(false);
            FemalePath3.SetActive(false);
            FemalePath4.SetActive(false);
            FemalePath5.SetActive(false);

            MalePath1.SetActive(false);
            MalePath2.SetActive(false);
            MalePath3.SetActive(false);
            MalePath4.SetActive(false);
            MalePath5.SetActive(false);

            FemalePath1.GetComponent<CPC_CameraPath>().PlayPath(30);
            //MechCanvas.SetActive(false);
            //MarkedCanvas.SetActive(false);
            //ParasiteCanvas.SetActive(false);
        }

        if (Input.GetKeyDown("7") )
        {
            //Female2.PlayOneShot(Female2.clip, 1f);
            MainCam.SetActive(false);
            RebelCam.SetActive(true);
            FemalePath2.SetActive(true);
            Female2D2.SetActive(true);
            Female3D2.SetActive(true);
            FemaleObj2.GetComponent<Animator>().SetFloat("Multiplier", .2f);
            StopCoroutine(ReturnPlayer(0));
            StartCoroutine("ReturnPlayer",30);
            Singing = true;

            HeadTracker.transform.position = FemaleObj2.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).transform.position;
            HeadTracker.transform.parent = FemaleObj2.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).transform;


            FemalePath1.SetActive(false);
            FemalePath2.SetActive(true);
            FemalePath3.SetActive(false);
            FemalePath4.SetActive(false);
            FemalePath5.SetActive(false);

            MalePath1.SetActive(false);
            MalePath2.SetActive(false);
            MalePath3.SetActive(false);
            MalePath4.SetActive(false);
            MalePath5.SetActive(false);

            FemalePath2.GetComponent<CPC_CameraPath>().PlayPath(30);
            //MechCanvas.SetActive(false);
            //MarkedCanvas.SetActive(false);
            //ParasiteCanvas.SetActive(false);
        }

        if (Input.GetKeyDown("8") )
        {
            //Female3.PlayOneShot(Female3.clip, 1f);
            MainCam.SetActive(false);
            RebelCam.SetActive(true);
            FemalePath3.SetActive(true);
            Female2D3.SetActive(true);
            Female3D3.SetActive(true);
            FemaleObj3.GetComponent<Animator>().SetFloat("Multiplier", .2f);
            StopCoroutine(ReturnPlayer(0));
            StartCoroutine("ReturnPlayer", 33);
            Singing = true;

            HeadTracker.transform.position = FemaleObj3.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).transform.position;
            HeadTracker.transform.parent = FemaleObj3.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).transform;


            FemalePath1.SetActive(false);
            FemalePath2.SetActive(false);
            FemalePath3.SetActive(true);
            FemalePath4.SetActive(false);
            FemalePath5.SetActive(false);

            MalePath1.SetActive(false);
            MalePath2.SetActive(false);
            MalePath3.SetActive(false);
            MalePath4.SetActive(false);
            MalePath5.SetActive(false);

            FemalePath3.GetComponent<CPC_CameraPath>().PlayPath(30);
            //MechCanvas.SetActive(false);
            //MarkedCanvas.SetActive(false);
            //ParasiteCanvas.SetActive(false);
        }

        if (Input.GetKeyDown("9") )
        {
            //Female4.PlayOneShot(Female4.clip, 1f);
            MainCam.SetActive(false);
            RebelCam.SetActive(true);
            FemalePath4.SetActive(true);
            Female2D4.SetActive(true);
            Female3D4.SetActive(true);
            FemaleObj4.GetComponent<Animator>().SetFloat("Multiplier", .2f);
            StopCoroutine(ReturnPlayer(0));
            StartCoroutine("ReturnPlayer", 46);
            Singing = true;

            HeadTracker.transform.position = FemaleObj4.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).transform.position;
            HeadTracker.transform.parent = FemaleObj4.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).transform;


            FemalePath1.SetActive(false);
            FemalePath2.SetActive(false);
            FemalePath3.SetActive(false);
            FemalePath4.SetActive(true);
            FemalePath5.SetActive(false);

            MalePath1.SetActive(false);
            MalePath2.SetActive(false);
            MalePath3.SetActive(false);
            MalePath4.SetActive(false);
            MalePath5.SetActive(false);

            FemalePath4.GetComponent<CPC_CameraPath>().PlayPath(30);
            //MechCanvas.SetActive(false);
            //MarkedCanvas.SetActive(false);
            //ParasiteCanvas.SetActive(false);
        }

        if (Input.GetKeyDown("0") )
        {
            //Female5.PlayOneShot(Female5.clip, 1f);
            MainCam.SetActive(false);
            RebelCam.SetActive(true);
            FemalePath5.SetActive(true);
            Female2D5.SetActive(true);
            Female3D5.SetActive(true);
            FemaleObj5.GetComponent<Animator>().SetFloat("Multiplier", .2f);
            StopCoroutine(ReturnPlayer(0));
            StartCoroutine("ReturnPlayer", 64);
            Singing = true;

            HeadTracker.transform.position = FemaleObj5.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).transform.position;
            HeadTracker.transform.parent = FemaleObj5.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).transform;


            FemalePath1.SetActive(false);
            FemalePath2.SetActive(false);
            FemalePath3.SetActive(false);
            FemalePath4.SetActive(false);
            FemalePath5.SetActive(true);

            MalePath1.SetActive(false);
            MalePath2.SetActive(false);
            MalePath3.SetActive(false);
            MalePath4.SetActive(false);
            MalePath5.SetActive(false);

            FemalePath5.GetComponent<CPC_CameraPath>().PlayPath(30);
            //MechCanvas.SetActive(false);
            //MarkedCanvas.SetActive(false);
            //ParasiteCanvas.SetActive(false);
        }

        if (Singing)
        {
            Camera.main.fieldOfView = 10;
        }
    }


    IEnumerator ReturnPlayer(float Time)
    {
        yield return new WaitForSeconds(Time);

        Singing = false;
        Camera.main.fieldOfView = 30;

        Female2D1.SetActive(false);
        Female2D2.SetActive(false);
        Female2D3.SetActive(false);
        Female2D4.SetActive(false);
        Female2D5.SetActive(false);

        Male2D1.SetActive(false);
        Male2D2.SetActive(false);
        Male2D3.SetActive(false);
        Male2D4.SetActive(false);
        Male2D5.SetActive(false);


        Female3D1.SetActive(false);
        Female3D2.SetActive(false);
        Female3D3.SetActive(false);
        Female3D4.SetActive(false);
        Female3D5.SetActive(false);

        Male3D1.SetActive(false);
        Male3D2.SetActive(false);
        Male3D3.SetActive(false);
        Male3D4.SetActive(false);
        Male3D5.SetActive(false);



        MainCam.SetActive(true);
        RebelCam.SetActive(false);

        FemaleObj1.GetComponent<Animator>().SetFloat("Multiplier", 1);
        FemaleObj2.GetComponent<Animator>().SetFloat("Multiplier", 1);
        FemaleObj3.GetComponent<Animator>().SetFloat("Multiplier", 1);
        FemaleObj4.GetComponent<Animator>().SetFloat("Multiplier", 1);
        FemaleObj5.GetComponent<Animator>().SetFloat("Multiplier", 1);

        MaleObj1.GetComponent<Animator>().SetFloat("Multiplier", 1);
        MaleObj2.GetComponent<Animator>().SetFloat("Multiplier", 1);
        MaleObj3.GetComponent<Animator>().SetFloat("Multiplier", 1);
        MaleObj4.GetComponent<Animator>().SetFloat("Multiplier", 1);
        MaleObj5.GetComponent<Animator>().SetFloat("Multiplier", 1);

        FemalePath1.SetActive(false);
        FemalePath2.SetActive(false);
        FemalePath3.SetActive(false);
        FemalePath4.SetActive(false);
        FemalePath5.SetActive(false);

        MalePath1.SetActive(false);
        MalePath2.SetActive(false);
        MalePath3.SetActive(false);
        MalePath4.SetActive(false);
        MalePath5.SetActive(false);

        /*
        MechCanvas.SetActive(true);
        MarkedCanvas.SetActive(true);
        ParasiteCanvas.SetActive(true);
        */
    }
}
