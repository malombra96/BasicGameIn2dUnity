
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState  // enumerado para aceder desde cualquier script
{
    menu, inGame, gameOver
}
public class GameManager : MonoBehaviour
{
    public GameState CurrentGameState = GameState.menu;
    public static GameManager sharedInstance;
    private PlayerController controller;
    public int collecteObjetc;

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }

    void Start()
    {
        controller = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            StartGame();
        }
    }

    public void StartGame() // inicio de una pertida 
    {
        SetGameState(GameState.inGame);
    }

    public void GameOver() // finalizacion de una partida
    {
        SetGameState(GameState.gameOver);
    }

    public void BackToManu()  // volver al menu principal 
    {
        SetGameState(GameState.menu);
    }

    void SetGameState(GameState newGameState)
    {
        if (newGameState == GameState.menu)
        {
            MenuManager.sharedInstance.ShowMainMenu();
        }else if (newGameState == GameState.inGame)
        {
            LevelManager.shaderInstance.RemoveAllLevelBlocks(); /// OJO CON ESTOS CAMBIOS 
            LevelManager.shaderInstance.GenerateInicialBlock();    /// OJO CON ESTOS CAMBIOS
            MenuManager.sharedInstance.HideMainMenu();
            controller.StartGame();
        } else if (newGameState == GameState.gameOver)
        {
            LevelManager.shaderInstance.RemoveAllLevelBlocks(); /// OJO CON ESTOS CAMBIOS
            LevelManager.shaderInstance.GenerateInicialBlock(); /// OJO CON ESTOS CAMBIOS
            MenuManager.sharedInstance.menuGameOver.enabled = true;
            controller.StartGame();
            
        }

        this.CurrentGameState = newGameState;
    }

    public void CollectObject(Colectable colectable)
    {
        collecteObjetc += colectable.value;
    }

}
