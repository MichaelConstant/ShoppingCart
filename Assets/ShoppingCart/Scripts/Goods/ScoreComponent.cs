using UnityEngine;

namespace ShoppingCart.Scripts.Goods
{
    public class ScoreComponent : MonoBehaviour
    {
        public float Score = 0;
        public bool HasCourtesyCard = false;
        public int MaxExp = 20;
        public int Exp = 0;

        public delegate void GetScoreHandler();
        public delegate void GetPropHandler();
        
        public static event GetScoreHandler OnGetScore;
        public static event GetPropHandler OnGetProp;
        
        private void Start()
        {
            Score = 0;
            Exp = 0;
        }

        public void GetScore(float score, int exp)
        {
            var scoreGot = HasCourtesyCard ? 1.6f * score : score;

            Score += scoreGot;
            Exp += exp;

            if (Exp >= MaxExp)
            {
                OnGetProp?.Invoke();

                Exp -= MaxExp;
            }
            
            OnGetScore?.Invoke();
        }
    }
}