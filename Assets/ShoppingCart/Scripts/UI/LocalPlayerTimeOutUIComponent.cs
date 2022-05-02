using System;
using ShoppingCart.Scripts.Game_Scene;
using TMPro;
using UnityEngine;

namespace ShoppingCart.Scripts.UI
{
    public class LocalPlayerTimeOutUIComponent : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI TimeOutText;

        private void Start()
        {
            TimeOutText.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            GameOverSystem.OnGameOver += GameOver;
        }

        private void OnDisable()
        {
            GameOverSystem.OnGameOver -= GameOver;
        }

        private void GameOver()
        {
            TimeOutText.gameObject.SetActive(true);
        }
    }
}