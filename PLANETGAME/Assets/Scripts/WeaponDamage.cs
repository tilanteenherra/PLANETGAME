using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{

    public GameObject thisParent;
    public GameObject otherPlayer;
    
    public int damage;
    
    // Start is called before the first frame update
    void Start()
    {
        thisParent = this.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // Damage doing script with trigger collider that detects who is using the weapon to not do damage to it but still can use "Player" tag.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != thisParent && other.gameObject.CompareTag("Player"))
        {
            otherPlayer = other.gameObject;
            other.gameObject.GetComponent<PlayerStats>().curHp -= damage;
        }
    }
}
