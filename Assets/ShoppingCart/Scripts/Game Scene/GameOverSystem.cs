using System;
using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] private TextMeshProUGUI CountDownTime;
    [SerializeField] private TextMeshProUGUI StartGameTime;

    public float TotalTime = 360;
    private int _minutes;
    private int _seconds;

    public event Action OnGameStart;

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

        if (_isGameRunning)
        {
            _gameStartTimer += Time.deltaTime;

            _minutes = (int) (TotalTime - _gameStartTimer) / 60;
            
            _seconds = (int) (TotalTime - _minutes * 60f);
            
            StartGameTime.text = "" + _minutes + " : " + _seconds;

            if (_gameStartTimer >= TotalTime)
            {
                Time.timeScale = 0;
            }
        }
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
            CountDown(_countDownInt);
            _countDownInt--;
        }
    }

    private void StartGame()
    {
        _isGameRunning = true;
    }

    private void CountDown(int countDownInt)
    {
        CountDownTime.text = "" + countDownInt;
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