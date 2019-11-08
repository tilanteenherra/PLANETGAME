using UnityEngine;
using UnityEngine.UI;

namespace StatsScripts
{
    public class UIHealthChangeScript : MonoBehaviour
    {
        public PlayerStats stats;
        public GameObject image;
        private float asd = 0;
        public float damageMultiplier = 1;
        
        // Start is called before the first frame update
        void Start()
        {
            image = GetComponent<GameObject>();
            //kusee kun tulee photon /kusee jo nyt
            //stats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
            //stats = GameObject.Find("DemonSlayer").GetComponent<PlayerStats>();
        }

        // Update is called once per frame
        void Update()
        {

            if (Input.GetKey(KeyCode.X) && stats.curHp >= 0)
            {
                stats.curHp -= damageMultiplier;
            }
            transform.localScale = new Vector3( (float)stats.curHp/stats.maxHp,1,1);
            //image.transform.localScale = new Vector3(asd,1,1);
        }

    
    }
}
