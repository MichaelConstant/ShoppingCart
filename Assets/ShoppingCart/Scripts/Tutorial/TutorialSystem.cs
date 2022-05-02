using System;
using ShoppingCart.Scripts.Goods;
using ShoppingCart.Scripts.Player;
using UnityEngine;

namespace ShoppingCart.Scripts.Tutorial
{
    public class TutorialSystem : Singleton<TutorialSystem>
    {
        private GameObject _player;
        private ScoreComponent _playerScoreComponent;
        private InputComponent _playerInputComponent;
        
        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            _playerScoreComponent = _player.GetComponentInChildren<ScoreComponent>();
            _playerInputComponent = _player.GetComponentInChildren<InputComponent>();

            _playerInputComponent.AllowPlayerInput();
        }
    }
}