using System;
using UnityEngine;

namespace Interactables
{
    public class RegenBuffs : MonoBehaviour
    {
        private PlayerStats stats;
        private GameObject player;
        private FirstPersonController firstPersonController;

        public GameObject[] firePlaces;
        public GameObject[] cactusPlaces;
        public GameObject[] mushroomPlaces;
        
        private float origSpeed;
        public float curSpeed;
        public float cactusJuiceSpeed;
        private float counterForMushroom = 0;
        private bool cactusPicked;
        private bool mushroomPicked;
        public int cactusPickUpMaxTime;
        public float mushroomSpeed;
        public int mushroomPickUpMaxTime;
        public float healthMultiplier;
        
        
        // Start is called before the first frame update
        void Start()
        {
            firePlaces = GameObject.FindGameObjectsWithTag("Campfire");
            cactusPlaces = GameObject.FindGameObjectsWithTag("Cactus");
            mushroomPlaces = GameObject.FindGameObjectsWithTag("Mushroom");
            player = GetComponent<GameObject>();
            firstPersonController = GetComponent<FirstPersonController>();
            counterForMushroom = 0;
            origSpeed = firstPersonController.walkSpeed;
            cactusPicked = false;
            mushroomPicked = false;
            stats = GetComponent<PlayerStats>();
            curSpeed = origSpeed;
        }

        // Update is called once per frame
        void Update()
        {
            
            //Counter for cactus effect to last
            if (cactusPicked)
            {
                counterForMushroom += Time.deltaTime;
                if (counterForMushroom >= mushroomPickUpMaxTime)
                {
                    counterForMushroom = 0;
                    mushroomPicked = false;
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
                //firstPersonController.walkSpeed = cactusJuiceSpeed;
                curSpeed = cactusJuiceSpeed;

            }

            if (other.gameObject.CompareTag("Mushroom") && !mushroomPicked)
            {
                mushroomPicked = true;
                firstPersonController.walkSpeed = mushroomSpeed;
                curSpeed = mushroomSpeed;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
        }
    }
}
