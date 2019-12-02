using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MenuScripts
{
    public class CharacterSelect : MonoBehaviour
    {
        private Button demonSlayerButton;

        public float timer = 0;
        public GameObject model;
        private bool count = false;
        // Start is called before the first frame update
        void Start()
        {
            timer = 0;
            demonSlayerButton = GameObject.Find("DemonSlayerButton").GetComponent<Button>();
        
            demonSlayerButton.onClick.AddListener(DemonSlayerSelected);
        
        }

        // Update is called once per frame
        void Update()
        {

            if (count)
            {
                timer += Time.deltaTime;
                model.gameObject.transform.Rotate(0,timer*2,0);
            }

            if (timer >= 3)
            {
                SceneManager.LoadScene("RolleTest2");
            }
            
        }

        void DemonSlayerSelected()
        {
            count = true;
        }
    }
}
