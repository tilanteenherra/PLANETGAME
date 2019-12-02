using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{

    PlayerController pc;

    private GameObject thisParent;
    private GameObject damageBurst;

    private const float time = 3;
    private GameObject instantiatedObj;

    public float damage;

    public bool hitOnce = false;
    public bool inArea = false;
    
    // Start is called before the first frame update
    void Awake()
    {
        thisParent = transform.root.gameObject;
        pc = thisParent.GetComponent<PlayerController>();
        damageBurst = this.gameObject.transform.GetChild(0).gameObject;
    }

    private void OnTriggerStay(Collider other)
    {
        //if (pc.attRoutineOn == true)
        //{
            if (other.gameObject != thisParent && other.gameObject.CompareTag("Player"))
            {
                if (hitOnce == false)
                {
                    other.gameObject.GetComponent<PlayerStats>().curHp -= damage;
                    hitOnce = true;
                    instantiatedObj = (GameObject)Instantiate(damageBurst, other.transform.position, transform.rotation);
                    Destroy(instantiatedObj, time);
                }
            }
        //}
    }
}
