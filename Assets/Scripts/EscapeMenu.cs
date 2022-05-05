using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EscapeMenu : MonoBehaviour {
    private GameObject mainWindow;
    private Button mainMenuButton;
    private Button quitButton;
    private Button resumeButton;

    private void Awake() {
        mainWindow = transform.Find("Escape Window").gameObject;
        mainMenuButton = mainWindow.transform.Find("Main Menu Button").GetComponent<Button>();
        quitButton = mainWindow.transform.Find("Quit Button").GetComponent<Button>();
        resumeButton = mainWindow.transform.Find("Resume Button").GetComponent<Button>();
        
        mainMenuButton.onClick.AddListener(onMainMenu);
        quitButton.onClick.AddListener(onQuit);
        resumeButton.onClick.AddListener(onToggleWindow);
    }

    private void Start() {
        mainWindow.SetActive(false);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) onToggleWindow();
    }



    private void onToggleWindow() {
        Cursor.lockState = (mainWindow.activeSelf) ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !mainWindow.activeSelf;
        mainWindow.SetActive(!mainWindow.activeSelf);
    }

    private void onMainMenu() {
        SceneManager.LoadScene("Main Menu");
    }

    private void onQuit() {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
