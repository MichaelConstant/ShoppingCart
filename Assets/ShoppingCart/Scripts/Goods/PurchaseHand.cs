using System;
using ShoppingCart.Scripts.Audio;
using UnityEngine;

namespace ShoppingCart.Scripts.Goods
{
    public class PurchaseHand : MonoBehaviour
    {
        [SerializeField] private ScoreComponent ScoreComponent;

        public bool IsPurchasing => _isPurchasing;
        private bool _isPurchasing = false;

        private float _currentScore;
        private int _currentExp;

        [HideInInspector]
        public AudioSource AudioSource;

        private void Start()
        {
            AudioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (Input.GetKeyDown("R"))
            {
                _isPurchasing = false;
            }
        }

        public void PurchaseGoods(float score, int exp)
        {
            _isPurchasing = true;
            _currentScore = score;
            _currentExp = exp;
        }

        public void GetScore(GameObject good)
        {
            if (!good) return;
            
            ScoreComponent.GetScore(_currentScore, _currentExp);
            _isPurchasing = false;
            _currentScore = 0;
            _currentExp = 0;
        }
    }
}