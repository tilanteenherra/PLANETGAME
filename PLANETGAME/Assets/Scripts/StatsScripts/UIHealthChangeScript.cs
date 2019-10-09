using UnityEngine;
using UnityEngine.UI;

namespace StatsScripts
{
    public class UIHealthChangeScript : MonoBehaviour
    {
        private PlayerStats stats;
        public GameObject image;
        private float asd = 0;
    
        // Start is called before the first frame update
        void Start()
        {
            image = GetComponent<GameObject>();
            stats = GameObject.Find("DemonSlayer").GetComponent<PlayerStats>();
        }

        // Update is called once per frame
        void Update()
        {
            image.transform.localScale = new Vector3( (float)stats.curHp/stats.maxHp,1,1);
            //image.transform.localScale = new Vector3(asd,1,1);
        }

    
    }
}
