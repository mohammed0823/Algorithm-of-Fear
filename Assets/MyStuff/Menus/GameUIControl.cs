using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using Unity.VisualScripting;
using System.Reflection.Emit;

public class GameUIControl : MonoBehaviour
{
    public GameObject HelpScreenUI;
    public GameObject SettingsUI;

    public GameObject PauseMenuUI;

    public GameObject WinscreenUI;

    public GameObject LoseScreenUI;

    public Toggle fullscreen;
    public Toggle windowed;

    private bool isFull;


    public static bool IsPaused;

    void Start()
    {
        QuizManage.won = false;
        HelpScreenUI.SetActive(false);
        SettingsUI.SetActive(false);
        PauseMenuUI.SetActive(false);
        WinscreenUI.SetActive(false);
        LoseScreenUI.SetActive(false);

        isFull = Screen.fullScreen;
        IsPaused = false;

        fullscreen.isOn = isFull;
        windowed.isOn = !isFull;

        fullscreen.onValueChanged.AddListener(FullscreenToggle);
        windowed.onValueChanged.AddListener(windowedToggle);

    }


    private void FullscreenToggle(bool isOn)
    {
        if (windowed.isOn && isOn)
        {
            windowed.isOn = false;
            Screen.fullScreen = true;
        }

        if (windowed.isOn == false)
        {
            fullscreen.isOn = true;
        }

        Text text = fullscreen.GetComponentInChildren<Text>();
            
        Debug.Log(text);
        
    }

    private void windowedToggle(bool isOn)
    {
        if (fullscreen.isOn && isOn)
        {
            fullscreen.isOn = false;
            Screen.fullScreen = false;
        }

        if (fullscreen.isOn == false)
        {
            windowed.isOn = true;
        }
        
    }

    public void Quit()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }

    public void Help()
    {
        HelpScreenUI.SetActive(true);
    }

    public void Back()
    {
        HelpScreenUI.SetActive(false);
        SettingsUI.SetActive(false);
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        IsPaused = false;
        SceneManager.LoadScene("Gameplay");
    }

    public void Settings()
    {
        SettingsUI.SetActive(true);
    }

    public void MenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void Update()
    {
        if (!QuizManage.won)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (IsPaused)
                Resume();
                if(SceneManager.GetSceneByName("Gameplay").isLoaded)
                Pause();
            }
        }
        else{
            Time.timeScale = 0f;
            IsPaused = true;
            WinscreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            QuizManage.count = 0;
        }
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
    }

    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
}
