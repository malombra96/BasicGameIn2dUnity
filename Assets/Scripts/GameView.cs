﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    public Text coinsText, scoreText, maxScoreText;
    private PlayerController _controller;
    void Start()
    {
        _controller = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.sharedInstance.CurrentGameState == GameState.inGame)
        {
            int coins = GameManager.sharedInstance.collecteObjetc;
            float score = _controller.GetTraveledDistance();
            float maxScore = PlayerPrefs.GetFloat("maxscore",0);

            coinsText.text = coins.ToString();
            scoreText.text = "Score: " + score.ToString("f1");
            maxScoreText.text = "Max Score: " + maxScore.ToString("f1");
        }
    }
    
    
}
