﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioVisualizer : MonoBehaviour
{
    public GameObject Player;
    public GameObject[] Objects;
    public float Scale = 10f;
    public enum EnumType {X, Y, Z}
    public EnumType Type = EnumType.Y;

    private float[] Samples = new float[1024];

    FFTWindow fftWindow;
    AudioSource thisAudioSource;

    void Start()
    {
        thisAudioSource = GetComponent<AudioSource>();
        fftWindow = FFTWindow.BlackmanHarris;
        Player = GameObject.FindWithTag("Player");
    }


    void Update()
    {

        if (this.gameObject.name == "Pink Robot Visualizer 2D" ||this.gameObject.name == "Pink Robot Visualizer 3D")
        {
            thisAudioSource = Player.GetComponent<LevelTriggers>().PinkAudio;
            thisAudioSource.clip = Player.GetComponent<LevelTriggers>().PinkAudio.clip;
        }

        thisAudioSource.GetSpectrumData(Samples, 0, fftWindow);

        for (int i = 0; i < Objects.Length; i++)
        {
            var myScale = Samples[i] * Scale;

            if(Type == EnumType.X)
            {
                Objects[i].transform.localScale = new Vector3(myScale, Objects[i].transform.localScale.y, Objects[i].transform.localScale.z);
            }

            if (Type == EnumType.Y)
            {
                Objects[i].transform.localScale = new Vector3(Objects[i].transform.localScale.x, myScale, Objects[i].transform.localScale.z);
            }

            if (Type == EnumType.Z)
            {
                Objects[i].transform.localScale = new Vector3(Objects[i].transform.localScale.x, Objects[i].transform.localScale.y, myScale);
            }
        }
    }
}