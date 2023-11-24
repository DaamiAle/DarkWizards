using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //[SerializeField] float duration;
    [SerializeField] AudioSource audio;
    //private float timeToLoop;
    void Awake()
    {
        audio.pitch = Random.Range(0.3f, 0.8f);
    }
    // Start is called before the first frame update
    void Start()
    {
        //timeToLoop = Time.time + duration / audio.pitch;
    }

    // Update is called once per frame
    void Update()
    {/*
        if (Time.time >= timeToLoop)
        {
            float randomPitch = 
            audio.pitch = randomPitch;
            timeToLoop = Time.time + duration / audio.pitch;
        }*/
    }
}
