using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SnowSteps : MonoBehaviour
{
    private PlayerController pc;

    public AudioClip snowSteps;
    private AudioSource source;

    private void Start()
    {
        pc = GetComponent<PlayerController>();
        source = GetComponent<AudioSource>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("SnowAngelArea"))
        {
            if (pc.anim.GetBool("running") == true || pc.anim.GetBool("walking") == true)
            {
                
            }
        }
    }
}
