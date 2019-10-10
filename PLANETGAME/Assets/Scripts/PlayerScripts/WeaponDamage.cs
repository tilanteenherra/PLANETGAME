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
    public GameObject damageBurst;

    private float time = 3;
    private GameObject instantiatedObj;

    public int damage;

    public bool hitOnce = false;
    public bool inArea = false;
    
    // Start is called before the first frame update
    void Awake()
    {
        thisParent = transform.root.gameObject;
        pcm = thisParent.GetComponent<PlayerControllerMovement>();
        fpc = thisParent.GetComponent<FirstPersonController>();
        damageBurst = this.gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
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
                    instantiatedObj = (GameObject)Instantiate(damageBurst, other.transform.position, transform.rotation);
                    Destroy(instantiatedObj, time);
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
