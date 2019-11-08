using System;
using TMPro;
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
        public GameObject[] castleParts;

        public Material castleCaptured;
        public Material castleNotCaptured;

        private float castleCounter;
        private float castleCounterReversed;
        public float castleCounterMax;
        private bool castleCounting;
        private bool castleCapturedBool;
        
        
        private bool cactusDoDamage;
        private float origSpeed;
        public float curSpeed;
        public float cactusJuiceSpeed;
        private float counterForMushroom = 0;
        private float counterForCactus = 0;
        private bool cactusPicked;
        public int cactusDamage;
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
            cactusDoDamage = false;
            castleCounterReversed = 1;
            castleParts = GameObject.FindGameObjectsWithTag("CastlePart");
            castleCapturedBool = false;

        }

        // Update is called once per frame
        void Update()
        {
            if (castleCounting)
            {
                castleCounter += Time.deltaTime;
                castleCounterReversed -= Time.deltaTime * 0.1f;
                castleNotCaptured.SetColor("_Color",(new Color(1,castleCounterReversed,1,1)));
                if (castleCounter >= castleCounterMax)
                {
                    castleCounterReversed = 1;
                    castleCounter = 0;

                    foreach (GameObject a in castleParts)
                    {
                        a.GetComponent<Renderer>().material = castleCaptured;
                    }

                    castleCapturedBool = true;
                    castleCounting = false;
                }
            }
            //Counter for cactus effect to last
            if (mushroomPicked)
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

            if (cactusPicked)
            {
                if (cactusDoDamage && counterForCactus <= 0)
                {
                    stats.curHp -= cactusDamage;
                }
                    
                    
                counterForCactus += Time.deltaTime;
                if (counterForCactus >= cactusPickUpMaxTime)
                {
                    cactusDoDamage = false;   
                    cactusPicked = false;
                    counterForCactus = 0;
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
                cactusDoDamage = true;
                //firstPersonController.walkSpeed = cactusJuiceSpeed;
            }

            if (other.gameObject.CompareTag("CastlePart") && !castleCounting && !castleCapturedBool)
            {
                castleCounting = true;
            }
            //cactus changes user walk speed from 8 to 15
            if (other.gameObject.CompareTag("Mushroom") && !mushroomPicked)
            {
                mushroomPicked = true;
                firstPersonController.walkSpeed = mushroomSpeed;
                curSpeed = mushroomSpeed;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (counterForCactus != 0 &&other.gameObject.CompareTag("Cactus"))
            {
                counterForCactus = 0;
                cactusPicked = false;
                cactusDoDamage = false;
            }

            if (castleCounting && other.gameObject.CompareTag("CastlePart") && !castleCapturedBool)
            {
                castleCounter = 0;
                castleCounting = false;
                castleCounterReversed = 1;
                castleNotCaptured.SetColor("_Color",Color.white);
                foreach (var castle in castleParts)
                {
                    castle.GetComponent<Renderer>().material = castleNotCaptured;
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
        }
    }
}
