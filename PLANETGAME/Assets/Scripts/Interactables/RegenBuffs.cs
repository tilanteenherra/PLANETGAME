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
        private PlayerController playerController;

        public GameObject[] firePlaces;
        public GameObject[] cactusPlaces;
        public GameObject[] mushroomPlaces;
        public GameObject[] castleParts;

        public Material castleCaptured;
        public Material castleNotCaptured;

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
        }

        // Update is called once per frame
        void Update()
        {
            if (snowAngelPicked)
            {
                snowAngelCounterValue += Time.deltaTime;
                if (snowAngelCounterValue >= SnowAngelMaxTime)
                {
                    StandardShaderUtils.ChangeRenderMode(CharacterMaterial,StandardShaderUtils.BlendMode.Opaque);
                    CharacterMaterial.SetColor("Nakyva",new Color(CharacterMaterial.color.r,CharacterMaterial.color.g,CharacterMaterial.color.b,1f));
                    snowAngelPicked = false;
                    snowAngelCounterValue = 0;
                    SnowAngelCounter = false;
                    
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
                    StandardShaderUtils.ChangeRenderMode(CharacterMaterial,StandardShaderUtils.BlendMode.Fade);
                    CharacterMaterial.SetColor("Nakymaton",new Color(CharacterMaterial.color.r,CharacterMaterial.color.g,CharacterMaterial.color.b,0.2f));
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
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("CastlePart"))
            {
                
                thisCastle = other.gameObject;
                castleScript = other.gameObject.GetComponent<CastleScript>();
            }
        }

        private void OnGUI()
        {
            GUI.Label(new Rect(10, 10, 400, 20),
                "Castles Captured: " + castlesCaptured);
        }
    }
    
    
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
 }
}
