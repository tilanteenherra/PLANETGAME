using UnityEngine;
using UnityEngine.SceneManagement;

namespace MenuScripts
{
    public class LoadControllerMain : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.anyKeyDown)
            {
                Continue();
            }
        }

        void Continue()
        {
        
            //Loading the next Scene
            SceneManager.LoadScene("MainMenu");
            
        }
    }
}
