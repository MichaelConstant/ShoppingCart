using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverSystem : Singleton<GameOverSystem>
{
    private bool _isGameRunning;
    private bool _isCountDown;

    private float _gameTime;

    private float _countDownTimer = 3f;
    private int _countDownInt = 3;
    
    public event Action OnGameStart;

    private void Start()
    {
        
    }
    
    private void OnEnable()
    {
        OnGameStart += StartCountDown;
    }

    private void OnDisable()
    {
        OnGameStart -= StartCountDown;
    }
    
    private void Update()
    {
        if (_isCountDown)
        {
            _countDownTimer += Time.deltaTime;

            if (_countDownTimer >= _countDownInt)
            {
                
            }
        }
    }

    private void StartCountDown()
    {
        _isCountDown = true;
    }
    
    #region UI Callback Methods

    public void OnStartClick()
    {
        _isGameRunning = true;
        OnGameStart?.Invoke();
    }

    #endregion
    
    
}
