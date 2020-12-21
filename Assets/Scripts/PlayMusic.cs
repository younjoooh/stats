using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    public AudioSource MusicSound;
    void Start()
    {
        MusicSound = gameObject.AddComponent<UnityEngine.AudioSource>();
        MusicSound.clip = Resources.Load<AudioClip>("Audio/Music/Song");
        MusicSound.spatialBlend = 0;
        MusicSound.volume = .01f;
        MusicSound.loop = true;
        MusicSound.playOnAwake = true;
        MusicSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("0") || Input.GetKeyDown("1") || Input.GetKeyDown("2") || Input.GetKeyDown("3") || Input.GetKeyDown("4") || Input.GetKeyDown("5") || Input.GetKeyDown("6") || Input.GetKeyDown("7") || Input.GetKeyDown("8") || Input.GetKeyDown("9"))
        {
            MusicSound.volume = 0f;
        }
    }
}
