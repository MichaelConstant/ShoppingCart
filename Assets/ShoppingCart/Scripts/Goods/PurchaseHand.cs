using UnityEngine;

namespace ShoppingCart.Scripts.Goods
{
    public class PurchaseHand : MonoBehaviour
    {
        [SerializeField] private ScoreComponent _scoreComponent;

        public bool IsPurchasing => _isPurchasing;
        private bool _isPurchasing = false;

        private float _currentScore;
        private int _currentExp;

        public void PurchaseGoods(float score, int exp)
        {
            _isPurchasing = true;
            _currentScore = score;
            _currentExp = exp;
        }

        public void GetScore(GameObject good)
        {
            if (!good) return;

            Destroy(good);
            _scoreComponent.GetScore(_currentScore, _currentExp);
            _isPurchasing = false;
            _currentScore = 0;
            _currentExp = 0;
        }
    }
}