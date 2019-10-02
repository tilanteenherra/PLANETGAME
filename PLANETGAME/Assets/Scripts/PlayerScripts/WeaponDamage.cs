using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{

    PlayerControllerMovement pcm;
    // Only temporary fix to damage work with mouse
    FirstPersonController fpc;

    public GameObject thisParent;
    
    public int damage;

    public bool hitOnce = false;
    public bool inArea = false;
    
    // Start is called before the first frame update
    void Awake()
    {
        thisParent = transform.root.gameObject;
        pcm = thisParent.GetComponent<PlayerControllerMovement>();
        fpc = thisParent.GetComponent<FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // Damage doing script with trigger collider that detects who is using the weapon to not do damage to it but still can use "Player" tag.
    //private void OnTriggerEnter(Collider other)
    //{
    //    if(pcm.attRoutineOn == true || fpc.attRoutineOn == true)
    //    {
    //        if (other.gameObject != thisParent && other.gameObject.CompareTag("Player"))
    //        {
    //            Debug.Log("Found player to hit!");
    //            if (hitOnce == false)
    //            {
    //                Debug.Log("Hit!");
    //                other.gameObject.GetComponent<PlayerStats>().curHp -= damage;
    //                hitOnce = true;
    //            }
    //        }
    //    }
    //}

    private void OnTriggerStay(Collider other)
    {
        if (pcm.attRoutineOn == true || fpc.attRoutineOn == true)
        {
            if (other.gameObject != thisParent && other.gameObject.CompareTag("Player"))
            {
                if (hitOnce == true)
                {
                    Debug.Log(":)");
                }

                Debug.Log("Found player to hit!");

                if (hitOnce == false)
                {
                    Debug.Log("Hit!");
                    other.gameObject.GetComponent<PlayerStats>().curHp -= damage;
                    hitOnce = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject != thisParent && other.gameObject.CompareTag("Player"))
        {
            inArea = false;
        }
    }
}
