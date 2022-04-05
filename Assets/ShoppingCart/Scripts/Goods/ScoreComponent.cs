using UnityEngine;

namespace ShoppingCart.Scripts.Goods
{
    public class ScoreComponent : MonoBehaviour
    {
        public int Score = 0;


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

        public void GetScore(int score, int exp)
        {
            Score += score;
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