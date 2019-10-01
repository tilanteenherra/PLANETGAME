using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MenuScripts
{
    public class SettingsButtons : MonoBehaviour
    {
        
        private Button audioButton;
        private Button graphicsButton;
        private Button backButton;
        private bool audio = true;
        

        // Start is called before the first frame update
        void Start()
        {
            
            //Find all the buttons in game
            audioButton = GameObject.Find("AudioButton").GetComponent<Button>();
            graphicsButton = GameObject.Find("GraphicsButton").GetComponent<Button>();
            backButton = GameObject.Find("BackButton").GetComponent<Button>();
            
            
            //add listeners for click
            audioButton.onClick.AddListener(AudioButton);
            graphicsButton.onClick.AddListener(GraphicsButton);
            backButton.onClick.AddListener(BackButton);
            
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void AudioButton()
        {
            //Change Audio Things
            if (audio)
            {
                audioButton.gameObject.GetComponentInChildren<Text>().text = "Audio: OFF";
                audio = false;
            }
            else
            {
                audioButton.gameObject.GetComponentInChildren<Text>().text = "Audio: ON";
                audio = true;
            }
        }

        private void GraphicsButton()
        {
            string[] names = QualitySettings.names;

            if (QualitySettings.GetQualityLevel() >= names.Length)
            {
                QualitySettings.SetQualityLevel(0, false);
            }
            else
            {
                QualitySettings.SetQualityLevel(QualitySettings.GetQualityLevel() + 1, false);
            }

            graphicsButton.gameObject.GetComponentInChildren<Text>().text =
                "Graphics: " + names[QualitySettings.GetQualityLevel()];
        }

        private void BackButton()
        {
            //Load Main Menu
            SceneManager.LoadScene("MainMenu");
        }
    }
}
