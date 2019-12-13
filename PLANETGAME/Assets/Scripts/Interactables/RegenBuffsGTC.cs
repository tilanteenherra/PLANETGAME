﻿using System;
using System.IO;
using Photon.Pun;
using StatsScripts;
using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = System.Random;

namespace Interactables
{
    public class RegenBuffsGTC : MonoBehaviour
    {
        private PlayerStats stats;
        private GameObject player;
        //private FirstPersonController firstPersonController;
        private PlayerControllerGTC playerController;

        private PhotonView ourPlayer;

        private int nextLocation;
        public GameObject[] firePlaces;
        public GameObject[] cactusPlaces;
        public GameObject[] mushroomPlaces;
        public GameObject[] castleParts;
        private bool notLeft;
        public Material castleCaptured;
        public Material castleNotCaptured;
        public GameObject[] graveStones;
        public Animator anim;

        private CastleScript castleScript;
        
        private float castleCounter = 0;
        private float castleCounterReversed;
        public float castleCounterMax;
        public int castlesCaptured = 0;
        private bool castleCounting;
        private bool castleCapturedBool;
        public GameObject thisCastle;

        Vector3 playerPos;
        public bool keepPlace = false;
        
        private bool snowAngelPicked;
        private float snowAngelCounterValue;
        private bool SnowAngelCounter;
        public int SnowAngelMaxTime;

        public Material CharacterMaterial;
        public Material InvisibilityMaterial;
        public SkinnedMeshRenderer[] bodyPartsMeshRenderer;
        public MeshRenderer[] bodyPartsRenderers;

        public int bodypartsDone;
        public int bodypartsDone2;

        private GameObject bodypart;

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

        //Rolle addas yöllä:
        private float dmg;
       
        // Start is called before the first frame update
        void Awake()
        {
            //ROLLE WAS HERE
            dmg = cactusDamage;

            stats = GetComponent<PlayerStats>();

            anim = GetComponent<Animator>();
            bodypartsDone = 0;
            bodypartsDone2 = 0;
            playerController = GetComponent<PlayerControllerGTC>();

            //6 gameobjectia jolla on skinnedmeshrenderer, jos tulee lisää niin muuta valuee. 2 meshrenderer 
            bodyPartsMeshRenderer = new SkinnedMeshRenderer[6];
            bodyPartsRenderers = new MeshRenderer[2];
            SetLayer(transform.root, 1);
            
            firePlaces = GameObject.FindGameObjectsWithTag("Campfire");
            cactusPlaces = GameObject.FindGameObjectsWithTag("Cactus");
            mushroomPlaces = GameObject.FindGameObjectsWithTag("Mushroom");
            
            player = GetComponent<GameObject>();
            //firstPersonController = GetComponent<FirstPersonController>();
            
            counterForMushroom = 0;
            if (playerController != null)
            {
                origSpeed = playerController.moveSpeed;
            }
            
            cactusPicked = false;
            mushroomPicked = false;
            stats = GetComponent<PlayerStats>();
            curSpeed = origSpeed;
            cactusDoDamage = false;
            castleCounterReversed = 1;
            castleParts = GameObject.FindGameObjectsWithTag("CastlePart");
            castleCapturedBool = false;
            

            castleNotCaptured.SetColor("_Color",Color.white);

            graveStones = GameObject.FindGameObjectsWithTag("GraveStone");


        }

        private void SetLayer (Transform trans, int layer){
            //Set the layer Of the parent
            trans.gameObject.layer = layer;

            if (trans.GetComponent<SkinnedMeshRenderer>() != null)
            {
                bodyPartsMeshRenderer[bodypartsDone] = trans.GetComponent<SkinnedMeshRenderer>();
                bodypartsDone++;
            }else if (trans.GetComponent<MeshRenderer>() != null)
            {
                bodyPartsRenderers[bodypartsDone2] = trans.GetComponent<MeshRenderer>();
                bodypartsDone2++;
            }
            
            // Call set layer on any children
            for (int i = 0; i < trans.childCount; i++) {
                SetLayer(trans.GetChild(i), layer);
            }
        }
        // Update is called once per frame
        void Update()
        {
            if (snowAngelPicked)
            {
                snowAngelCounterValue += Time.deltaTime;
                if (snowAngelCounterValue >= SnowAngelMaxTime)
                {
                    snowAngelPicked = false;
                    snowAngelCounterValue = 0;
                    SnowAngelCounter = false;
                    for (int i = 0; i < bodypartsDone; i++)
                    {
                        bodyPartsMeshRenderer[i].material = CharacterMaterial;
                    }
                    for (int s = 0; s < bodypartsDone2; s++)
                    {
                        bodyPartsRenderers[s].material = CharacterMaterial;
                    }

                    Debug.Log("done paint");
                    
                }
            }
            if (castleCounting)
            {
                castleCounter += Time.deltaTime;
                castleCounterReversed -= Time.deltaTime * 0.2f;
                //castleNotCaptured.SetColor("_Color",(new Color(1,castleCounterReversed,1,1)));
                if (castleCounter >= castleCounterMax)
                {
                    castleCounterReversed = 1;
                    castleCounter = 0;
                    thisCastle.GetComponent<Renderer>().material = castleCaptured;
                    castlesCaptured++;
                    //EI MENE POIS KOSKAAN
                    if (castlesCaptured >= 2)
                    {
                        stats.maxHp = 400;
                        stats.curHp = 400;
                    }
                    //castleNotCaptured.SetColor("_Color",Color.white);
                    castleScript.castleCaptured = true;
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
                    if (playerController != null)
                    {
                        //playerController.moveSpeed = origSpeed;
                        playerController.moveSpeed *= 1.5f;

                    }
                    //curSpeed = origSpeed;
                }
            }

            if (cactusPicked)
            {
                if (cactusDoDamage && counterForCactus <= 0)
                {
                    stats.curHp -= dmg;
                }


                counterForCactus += Time.deltaTime;
                if (counterForCactus >= cactusPickUpMaxTime)
                {
                    cactusDoDamage = false;   
                    cactusPicked = false;
                    counterForCactus = 0;
                }
                
                
                
            }

            if (keepPlace)
            {
                transform.position = playerPos;
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

            
            //shroom changes user walk speed from 8 to 15
            if (other.gameObject.CompareTag("Mushroom") && !mushroomPicked && playerController.interacting)
            {
                StartCoroutine(EatShrooms());


                IEnumerator EatShrooms()
                {
                    //in theory this should work, but the eat shrooms animation bugs out. also, player stops
                    //after the effect wears out.

                    playerPos = transform.position;
                    keepPlace = true;

                    //store weapons
                    anim.SetInteger("condition", 85);
                    yield return new WaitForSeconds(3.0f);

                    //eat shrooms
                    anim.SetInteger("condition", 9);
                    yield return new WaitForSeconds(10.7f);

                    //show weapons
                    anim.SetInteger("condition", 86);
                    yield return new WaitForSeconds(3.2f);

                    keepPlace = false;

                    mushroomPicked = true;
                    if (playerController != null)
                    {
                        playerController.moveSpeed = mushroomSpeed;
                    }
                    playerController.moveSpeed *= 0.66f;
                }
            }

            if (other.gameObject.CompareTag("SnowAngelArea") && playerController.interacting)
            {
                StartCoroutine(SnowAngels());


                IEnumerator SnowAngels()
                {
                    playerPos = transform.position;
                    keepPlace = true;

                    //store weapons
                    anim.SetInteger("condition", 85);
                    yield return new WaitForSeconds(2.9f);


                    //snow angel
                    anim.SetInteger("condition", 50);
                    yield return new WaitForSeconds(6.5f);

                    //show weapons
                    anim.SetInteger("condition", 86);
                    yield return new WaitForSeconds(3.167f);



                    snowAngelCounterValue = 0;
                    snowAngelPicked = true;
                    SnowAngelCounter = true;
                    for (int i = 0; i < bodypartsDone; i++)
                    {
                        bodyPartsMeshRenderer[i].material = InvisibilityMaterial;
                    }
                    for (int s = 0; s < bodypartsDone2; s++)
                    {
                        bodyPartsRenderers[s].material = InvisibilityMaterial;
                    }

                    keepPlace = false;
                }
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (counterForCactus != 0 && other.gameObject.CompareTag("Cactus"))
            {
                counterForCactus = 0;
                cactusPicked = false;
                cactusDoDamage = false;
            }

            if (castleCounting && other.gameObject.CompareTag("CastlePart") && !castleScript.castleCaptured)
            {
                castleCounter = 0;
                castleCounting = false;
                castleCounterReversed = 1;
                //castleNotCaptured.SetColor("_Color",Color.white);
                /*
                foreach (var castle in castleParts)
                {
                    castle.GetComponent<Renderer>().material = castleNotCaptured;
                }
                */
            }
/*
            if (other.gameObject.CompareTag("CastlePart"))
            {
                thisCastle = null;
                castleScript = null;
            }
*/
            if (other.gameObject.CompareTag("GraveStone") && other.gameObject == graveStones[nextLocation])
            {
                notLeft = false;
            }
        }



        public void Damage(float dmg)
        {
            Debug.Log("Damaged amount: " + dmg);
            stats.curHp -= dmg;
        }

            private void OnTriggerEnter(Collider other)
        {
            
        
            if (other.gameObject.CompareTag("GraveStone") && !notLeft)
            {
                //teleport to another gravestone randomly.
                nextLocation = UnityEngine.Random.Range(0, graveStones.Length);
                while (graveStones[nextLocation] == other.gameObject)
                {
                    nextLocation = UnityEngine.Random.Range(0, graveStones.Length);
                }

                notLeft = true;
                transform.position = graveStones[nextLocation].transform.position;
                
            }
        }

        
    }
}