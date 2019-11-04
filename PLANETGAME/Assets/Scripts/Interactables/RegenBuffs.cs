using System;
using UnityEngine;

namespace Interactables
{
    public class RegenBuffs : MonoBehaviour
    {
        private PlayerStats stats;
        private GameObject player;
        private FirstPersonController firstPersonController;
        
        private float origSpeed;
        public float curSpeed;
        public float cactusJuiceSpeed;
        private float counterForCactus = 0;
        private bool cactusPicked;
        public int cactusPickUpMaxTime;
        public float healthMultiplier;
        
        
        // Start is called before the first frame update
        void Start()
        {
            
            player = GetComponent<GameObject>();
            firstPersonController = GetComponent<FirstPersonController>();
            counterForCactus = 0;
            origSpeed = firstPersonController.walkSpeed;
            cactusPicked = false;
            stats = GetComponent<PlayerStats>();
            curSpeed = origSpeed;
        }

        // Update is called once per frame
        void Update()
        {
            
            //Counter for cactus effect to last
            if (cactusPicked)
            {
                counterForCactus += Time.deltaTime;
                if (counterForCactus >= cactusPickUpMaxTime)
                {
                    counterForCactus = 0;
                    cactusPicked = false;
                    firstPersonController.walkSpeed = origSpeed;
                    curSpeed = origSpeed;
                }
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Campfire") && stats.curHp < stats.maxHp)
            {
                stats.curHp += (1 * healthMultiplier);
            }
            //cactus changes user walk speed from 8 to 15
            if (other.gameObject.CompareTag("Cactus") && !cactusPicked)
            {
                cactusPicked = true;
                firstPersonController.walkSpeed = cactusJuiceSpeed;
                curSpeed = cactusJuiceSpeed;

            }
        }

        private void OnTriggerEnter(Collider other)
        {
        }
    }
}
