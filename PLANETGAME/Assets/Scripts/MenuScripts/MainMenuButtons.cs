using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MenuScripts
{
    public class MainMenuButtons : MonoBehaviour
    {

        public GameObject mainMenu;
        public GameObject settingMenu;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                FuckGoBack();
            }
        }

        public void Play()
        {
            // Please add the scene
            SceneManager.LoadScene("RolleTest");
        }

        private void FuckGoBack()
        {
            // Close MainMenu window and open Settings
            if (mainMenu.activeSelf == false)
            {
                settingMenu.SetActive(false);
                mainMenu.SetActive(true);
            }
            else
            {
                Debug.Log("The fuck are you trying to do?!?!?!?!?!");
            }
        }

        public void RageQuit()
        {
            // Quit the game
            Application.Quit();
        }
    }
}
