﻿using UnityEngine;

namespace Interactables
{
    public class RegenBuffs : MonoBehaviour
    {
        private PlayerStats stats;
    

        public float healthMultiplier;
        // Start is called before the first frame update
        void Start()
        {
            stats = GameObject.Find("DemonSlayer").GetComponent<PlayerStats>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Campfire") && stats.curHp < stats.maxHp)
            {
                stats.curHp += (1 * healthMultiplier);
            }
    
        }
    }
}
