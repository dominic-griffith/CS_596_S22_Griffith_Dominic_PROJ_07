using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EscapeMenu : MonoBehaviour {
    private GameObject mainWindow;
    
    private void Start() {
        mainWindow = GameObject.Find("Canvas/Escape Window");
        mainWindow.SetActive(false);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Cursor.lockState = (mainWindow.activeSelf) ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !mainWindow.activeSelf;
            mainWindow.SetActive(!mainWindow.activeSelf);
        }
    }
}
