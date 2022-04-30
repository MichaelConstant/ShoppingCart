using System;
using Photon.Pun;
using ShoppingCart.Scripts.Goods;
using UnityEngine;

namespace ShoppingCart.Scripts.Game_Scene
{
    public class GameOverSystem : MonoBehaviourPun
    {
        private bool _isGameRunning;
        private bool _isCountDown;

        private float _gameStartTimer;

        private float _countDownTimer = 3f;
        private int _countDownInt = 3;

        public float TotalTime = 360;
        private int _minutes;
        private int _seconds;

        public static event Action OnGameStart;
        public static event Action OnCountdownEnd;
        public static event Action<int> OnCountTimerUpdate;
        public static event Action<int, int> OnStartTimerUpdate;
        public static event Action OnGameOver;

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
                GetComponent<PhotonView>().RPC(nameof(StartGameRPC), RpcTarget.AllBuffered);
            }

            UpdateCountDown();

            UpdateGameRunning();
        }

        [PunRPC]
        private void StartGameRPC()
        {
            OnGameStart?.Invoke();
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
}