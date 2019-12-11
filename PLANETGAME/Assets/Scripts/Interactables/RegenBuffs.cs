using System;
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
    public class RegenBuffs : MonoBehaviour
    {
        private PlayerStats stats;
        private GameObject player;
        private FirstPersonController firstPersonController;
        private PlayerController playerController;

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
        private UIHealthChangeScript uiHealth;
        private float castleCounter = 0;
        private float castleCounterReversed;
        public float castleCounterMax;
        public int castlesCaptured = 0;
        private bool castleCounting;
        private bool castleCapturedBool;
        public GameObject thisCastle;

        private PhotonView mePlayer;

        Vector3 playerPos;
        public bool keepPlace = false;

        public UIHealthChangeScript healthScript;
        
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
        
        
       
        // Start is called before the first frame update
        void Awake()
        {
            anim = GetComponent<Animator>();
            uiHealth = GameObject.Find("HealthBarEdit").GetComponent<UIHealthChangeScript>();
            bodypartsDone = 0;
            bodypartsDone2 = 0;
            ourPlayer = GetComponent<PhotonView>();
            Debug.Log("Photon me" + ourPlayer + " Is mine?: " +  ourPlayer.IsMine);
            
            //tämä disablee kamerat muilta
            if (!ourPlayer.IsMine)
            {
                Destroy(gameObject.transform.Find("Main Camera").gameObject);
            }
            
            if (ourPlayer.IsMine)
            {
                mePlayer = ourPlayer;
                uiHealth.stats = GetComponent<PlayerStats>();
            }
            
            //6 gameobjectia jolla on skinnedmeshrenderer, jos tulee lisää niin muuta valuee. 2 meshrenderer 
            bodyPartsMeshRenderer = new SkinnedMeshRenderer[6];
            bodyPartsRenderers = new MeshRenderer[2];
            SetLayer(transform.root, 1);
            
            firePlaces = GameObject.FindGameObjectsWithTag("Campfire");
            cactusPlaces = GameObject.FindGameObjectsWithTag("Cactus");
            mushroomPlaces = GameObject.FindGameObjectsWithTag("Mushroom");
            
            player = GetComponent<GameObject>();
            firstPersonController = GetComponent<FirstPersonController>();
            
            counterForMushroom = 0;
            if (firstPersonController != null)
            {
                origSpeed = firstPersonController.walkSpeed;
            } else if (playerController != null)
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
            playerController = GetComponent<PlayerController>();

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
                    castleScript.capturedByUser = mePlayer.Owner.UserId;
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
                    if (firstPersonController != null)
                    {
                        firstPersonController.walkSpeed = origSpeed;
                    } else if (playerController != null)
                    {
                        playerController.moveSpeed = origSpeed;
                    }
                    curSpeed = origSpeed;
                }
            }

            if (cactusPicked)
            {
                if (cactusDoDamage && counterForCactus <= 0)
                {
                    mePlayer.RPC ("Damage", RpcTarget.All, cactusDamage);
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

            if (other.gameObject.CompareTag("CastlePart") && !castleCounting && !castleScript.castleCaptured && castleScript.capturedByUser != mePlayer.Owner.UserId)
            {
                castleCounting = true;
            }
            //shroom changes user walk speed from 8 to 15
            if (other.gameObject.CompareTag("Mushroom") && !mushroomPicked && Input.GetKeyDown(KeyCode.G))
            {
                //StartCoroutine(EatShrooms());


                //IEnumerator EatShrooms()
                //{
                    //in theory this should work, but the eat shrooms animation bugs out. also, player stops
                    //after the effect wears out.

                    //store weapons
                    //anim.SetInteger("condition", 85);
                    //yield return new WaitForSeconds(3.0f);

                    //eat shrooms
                    //anim.SetInteger("condition", 49);
                    //yield return new WaitForSeconds(10.7f);

                    //show weapons
                    //anim.SetInteger("condition", 86);
                    //yield return new WaitForSeconds(3.2f);
                    

                    mushroomPicked = true;
                    if (firstPersonController != null)
                    {
                        firstPersonController.walkSpeed = mushroomSpeed;
                    }
                    else if (playerController != null)
                    {
                        playerController.moveSpeed = mushroomSpeed;
                    }
                    curSpeed = mushroomSpeed;
                //}
            }

            if (other.gameObject.CompareTag("SnowAngelArea") && Input.GetKeyDown(KeyCode.N))
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
        [PunRPC]
        public void Damage(float dmg){
            Debug.Log ("Damaged amount: " + dmg);
            stats.curHp -= dmg;
        }
        private void OnTriggerExit(Collider other)
        {
            if (counterForCactus != 0 && other.gameObject.CompareTag("Cactus"))
            {
                counterForCactus = 0;
                cactusPicked = false;
                cactusDoDamage = false;
            }

            if (castleCounting && other.gameObject.CompareTag("CastlePart") && !castleScript.castleCaptured  && castleScript.capturedByUser != mePlayer.Owner.UserId)
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

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("CastlePart"))
            {
                thisCastle = other.gameObject;
                castleScript = other.gameObject.GetComponent<CastleScript>();
            }

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

        private void OnGUI()
        {
            GUI.Label(new Rect(50, 50, 400, 20),
                "Castles Captured: " + castlesCaptured);
            GUI.Label(new Rect(50, 80, 400, 20),
                "Capturing Castle: " + castleCounterReversed);
        }
    }
}
