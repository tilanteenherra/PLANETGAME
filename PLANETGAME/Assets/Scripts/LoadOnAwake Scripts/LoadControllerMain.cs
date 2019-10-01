using UnityEngine;
using UnityEngine.SceneManagement;

namespace LoadOnAwake_Scripts
{
    public class LoadControllerMain : MonoBehaviour
    {
        private int thisScene;
        // Start is called before the first frame update
        void Start()
        {
            thisScene = SceneManager.GetActiveScene().buildIndex;
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
            //SceneManager.LoadScene(thisScene + 1);
            Debug.Log("Load the next scene");
        }
    }
}
