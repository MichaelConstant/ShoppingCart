using UnityEngine;

namespace ShoppingCart.Scripts.Goods
{
    public class ScoreComponent : MonoBehaviour
    {
        public int Score = 0;

        public int Exp = 0;
        
        private void Start()
        {
            Score = 0;
            Exp = 0;
        }

        public void GetScore(int score, int exp)
        {
            Score += score;
            exp += exp;
        }
    }
}