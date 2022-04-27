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

        private AudioSource _audioSource;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
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
            var goodType = good.GetComponent<GoodComponent>().Good;

            AudioInventory.AudioEnum audioEnum;
            switch (goodType)
            {
                case GoodComponent.GoodType.Low:
                    audioEnum = AudioInventory.AudioEnum.LowGoodGet;
                    break;
                case GoodComponent.GoodType.Middle:
                    audioEnum = AudioInventory.AudioEnum.MiddleGoodGet;
                    break;
                case GoodComponent.GoodType.Expensive:
                    audioEnum = AudioInventory.AudioEnum.ExpensiveGoodGet;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            AudioInventory.Instance.PlayAudioClip(_audioSource, audioEnum);
            
            ScoreComponent.GetScore(_currentScore, _currentExp);
            _isPurchasing = false;
            _currentScore = 0;
            _currentExp = 0;
        }
    }
}