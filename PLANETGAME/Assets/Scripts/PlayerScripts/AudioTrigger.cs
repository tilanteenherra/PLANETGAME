using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{

    private PlayerController pc;
    
    public AudioClip snowSteps;
    public AudioClip grassSteps;
    public AudioClip sandSteps;

    public bool movingOnSnow = false;
    public bool movingOnGrass = false;
    public bool movingOnSand = false;
    
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
            movingOnGrass = false;
            movingOnSand = false;
            
            if (pc.anim.GetBool("running") == true || pc.anim.GetBool("walking") == true)
            {
                movingOnSnow = true;
                source.PlayOneShot(snowSteps);
            }
            else
            {
                movingOnSnow = false;
            }
        }

        if (other.gameObject.CompareTag("GrassArea"))
        {
            movingOnSand = false;
            movingOnSnow = false;
            
            if (pc.anim.GetBool("running") == true || pc.anim.GetBool("walking") == true)
            {
                movingOnGrass = true;
                source.PlayOneShot(grassSteps);
            }
            else
            {
                movingOnGrass = false;
            }
        }

        if (other.gameObject.CompareTag("SandArea"))
        {
            movingOnGrass = false;
            movingOnSnow = false;
            if (pc.anim.GetBool("running") == true || pc.anim.GetBool("walking") == true)
            {
                movingOnSand = true;
                source.PlayOneShot(sandSteps);
            }
            else
            {
                movingOnSand = false;
            }
        }
    }
}
