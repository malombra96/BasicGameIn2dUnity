using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public Canvas menuPrincipal;
    public Canvas menuGame;
    public Canvas menuGameOver;
    public static MenuManager sharedInstance;

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }

    public void ShowMainMenu() // para mostrar el menu 
    {
        menuPrincipal.enabled = true;
        menuGame.enabled = false;
    }

    public void HideMainMenu() // para oculta el menu
    {
        menuPrincipal.enabled = false;
        menuGame.enabled = true;
    }

    public void RestartMenu() //reinicio menus
    {
        menuPrincipal.enabled = true;
        menuGameOver.enabled = false;
    }
    public void ExitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
