using System;
using System.Collections;
using System.Collections.Generic;
using ShoppingCart.Scripts.Goods;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverSystem : Singleton<GameOverSystem>
{
    private bool _isGameRunning;
    private bool _isCountDown;

    private float _gameStartTimer;

    private float _countDownTimer = 3f;
    private int _countDownInt = 3;

    public float TotalTime = 360;
    private int _minutes;
    private int _seconds;

    public event Action OnGameStart;
    public event Action OnCountdownEnd;
    public event Action<int> OnCountTimerUpdate;
    public event Action<int, int> OnStartTimerUpdate;
    public event Action OnGameOver;

    private void Start()
    {
        Time.timeScale = 1;
        _countDownTimer = 3f;
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnGameStart?.Invoke();
        }

        UpdateCountDown();

        UpdateGameRunning();
    }

    private void UpdateGameRunning()
    {
        if (!_isGameRunning) return;

        _gameStartTimer += Time.deltaTime;
        
        _minutes = (int) (TotalTime - _gameStartTimer) / 60;
        _seconds = (int) (TotalTime - _minutes * 60f - _gameStartTimer);
        
        OnStartTimerUpdate?.Invoke(_minutes, _seconds);
        
        if (!(_gameStartTimer >= TotalTime)) return;

        Time.timeScale = 0;
        OnGameOver?.Invoke();
    }

    private void UpdateCountDown()
    {
        if (!_isCountDown) return;

        _countDownTimer -= Time.deltaTime;

        if (_countDownTimer < 0f)
        {
            StartGame();
            _isCountDown = false;
        }
        else if (_countDownTimer <= _countDownInt)
        {
            OnCountTimerUpdate?.Invoke(_countDownInt);
            _countDownInt--;
        }
    }

    private void StartGame()
    {
        _isGameRunning = true;
        CourtesyCardRegenerator.Instance.CanInstantiate = true;
        OnCountdownEnd?.Invoke();
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