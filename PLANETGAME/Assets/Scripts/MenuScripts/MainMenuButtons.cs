using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    public Button settingsButton;
    public Button exitButton;
    public Button startButton;   


    // Start is called before the first frame update
    void Start()
    {
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
             SceneManager.LoadScene("Game");
         }

    private void SettingsButton()
    {
        //Please add the scene
        SceneManager.LoadScene("Settings");
    }

    private void ExitButton()
    {
        //Quit the game
        Application.Quit();
    }
}
