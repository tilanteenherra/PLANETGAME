using System;
using Photon.Pun;
using TMPro;
using UnityEngine;
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

        private CastleScript castleScript;

        private float castleCounter;
        private float castleCounterReversed;
        public float castleCounterMax;
        public int castlesCaptured = 0;
        private bool castleCounting;
        private bool castleCapturedBool;
        public GameObject thisCastle;

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
        void Start()
        {
            bodypartsDone = 0;
            bodypartsDone2 = 0;
            ourPlayer = GetComponent<PhotonView>();
            PhotonView otherDude = GameObject.Find("PhotonPlayer(Clone)").GetComponent<PhotonView>();
            
            Debug.Log("Photon me" + ourPlayer + " Is mine?: " +  ourPlayer.IsMine);
            
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
                castleNotCaptured.SetColor("_Color",(new Color(1,castleCounterReversed,1,1)));
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
                    castleNotCaptured.SetColor("_Color",Color.white);
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

            if (other.gameObject.CompareTag("CastlePart") && !castleCounting && !castleScript.castleCaptured)
            {
                
                castleCounting = true;
            }
            //cactus changes user walk speed from 8 to 15
            if (other.gameObject.CompareTag("Mushroom") && !mushroomPicked)
            {
                mushroomPicked = true;
                if (firstPersonController != null)
                {
                    firstPersonController.walkSpeed = mushroomSpeed;
                } else if (playerController != null)
                {
                    playerController.moveSpeed = mushroomSpeed;
                }

                curSpeed = mushroomSpeed;
            }

            if (other.gameObject.CompareTag("SnowAngelArea") && Input.GetKeyDown(KeyCode.N))
            {
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
                castleNotCaptured.SetColor("_Color",Color.white);
                foreach (var castle in castleParts)
                {
                    castle.GetComponent<Renderer>().material = castleNotCaptured;
                }
            }

            if (other.gameObject.CompareTag("CastlePart"))
            {
                thisCastle = null;
                castleScript = null;
            }

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
            GUI.Label(new Rect(10, 10, 400, 20),
                "Castles Captured: " + castlesCaptured);
        }
    }
    
   /* 
    public static class StandardShaderUtils
 {
     public enum BlendMode
     {
         Opaque,
         Cutout,
         Fade,
         Transparent
     }
 
     public static void ChangeRenderMode(Material standardShaderMaterial, BlendMode blendMode)
     {
         switch (blendMode)
         {
             case BlendMode.Opaque:
                 standardShaderMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                 standardShaderMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                 standardShaderMaterial.SetInt("_ZWrite", 1);
                 standardShaderMaterial.DisableKeyword("_ALPHATEST_ON");
                 standardShaderMaterial.DisableKeyword("_ALPHABLEND_ON");
                 standardShaderMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                 standardShaderMaterial.renderQueue = -1;
                 break;
             case BlendMode.Cutout:
                 standardShaderMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                 standardShaderMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                 standardShaderMaterial.SetInt("_ZWrite", 1);
                 standardShaderMaterial.EnableKeyword("_ALPHATEST_ON");
                 standardShaderMaterial.DisableKeyword("_ALPHABLEND_ON");
                 standardShaderMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                 standardShaderMaterial.renderQueue = 2450;
                 break;
             case BlendMode.Fade:
                 standardShaderMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                 standardShaderMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                 standardShaderMaterial.SetInt("_ZWrite", 0);
                 standardShaderMaterial.DisableKeyword("_ALPHATEST_ON");
                 standardShaderMaterial.EnableKeyword("_ALPHABLEND_ON");
                 standardShaderMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                 standardShaderMaterial.renderQueue = 3000;
                 break;
             case BlendMode.Transparent:
                 standardShaderMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                 standardShaderMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                 standardShaderMaterial.SetInt("_ZWrite", 0);
                 standardShaderMaterial.DisableKeyword("_ALPHATEST_ON");
                 standardShaderMaterial.DisableKeyword("_ALPHABLEND_ON");
                 standardShaderMaterial.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                 standardShaderMaterial.renderQueue = 3000;
                 break;
         }
 
     }
 }*/
   
   
   
   
  
   
   
   
}
