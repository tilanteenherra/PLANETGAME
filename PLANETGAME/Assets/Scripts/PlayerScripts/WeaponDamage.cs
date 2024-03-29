﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{

    private PlayerController pc;

    private GameObject thisParent;
    private GameObject bloodEffect;

    private const float time = 3;
    private GameObject instantiatedObj;

    public float damage;

    public bool hitOnce = false;
    public bool hitAgain = false;

    // Start is called before the first frame update
    void Awake()
    {
        thisParent = transform.root.gameObject;
        pc = thisParent.GetComponent<PlayerController>();
        bloodEffect = gameObject.transform.GetChild(0).gameObject;
    }

    private void OnTriggerStay(Collider other)
    {
        if(pc.anim.GetInteger("condition") == 2 || pc.anim.GetInteger("condition") == 30)
        {
            if (other.gameObject != thisParent && other.gameObject.CompareTag("Player"))
            {
                if(hitOnce == false)
                {
                    other.gameObject.GetComponent<PlayerStats>().curHp -= damage;
                    hitOnce = true;
                    instantiatedObj = (GameObject)Instantiate(bloodEffect, other.transform.position, transform.rotation);
                    Destroy(instantiatedObj, time);
                }
            }
        }

        if (hitOnce)
        {
            if (pc.anim.GetInteger("condition") == 3 || pc.anim.GetInteger("condition") == 31)
            {
                if (other.gameObject != thisParent && other.gameObject.CompareTag("Player"))
                {
                    if (hitAgain == false)
                    {
                        other.gameObject.GetComponent<PlayerStats>().curHp -= damage;
                        hitAgain = true;
                        instantiatedObj = (GameObject)Instantiate(bloodEffect, other.transform.position, transform.rotation);
                        Destroy(instantiatedObj, time);
                    }
                }
            }
        }
    }
}
