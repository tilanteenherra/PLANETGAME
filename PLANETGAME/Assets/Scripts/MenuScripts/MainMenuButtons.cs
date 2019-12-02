using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MenuScripts
{
    public class MainMenuButtons : MonoBehaviour
    {
        private Button settingsButton;
        private Button exitButton;
        private Button startButton;   


        // Start is called before the first frame update
        void Start()
        {
            settingsButton = GameObject.Find("SettingsButton").GetComponent<Button>();
            startButton = GameObject.Find("StartButton").GetComponent<Button>();
            exitButton = GameObject.Find("ExitButton").GetComponent<Button>();
            
            settingsButton.onClick.AddListener(SettingsButton);
            exitButton.onClick.AddListener(ExitButton);
            startButton.onClick.AddListener(StartButton);
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void StartButton()
        {
            //Please add the scene
            SceneManager.LoadScene("RolleTest");
        }

        private void SettingsButton()
        {
            //Please add the scene
            SceneManager.LoadScene("SettingsScene");
        }

        private void ExitButton()
        {
            //Quit the game
            Application.Quit();
        }
    }
}
