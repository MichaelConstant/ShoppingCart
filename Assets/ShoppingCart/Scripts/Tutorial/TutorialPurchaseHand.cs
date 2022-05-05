using UnityEngine;

namespace ShoppingCart.Scripts.Tutorial
{
    public class TutorialPurchaseHand : MonoBehaviour
    {
        [SerializeField] private TutorialScoreComponent ScoreComponent;

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