using System;
using UnityEngine;

namespace ShoppingCart.Scripts.Goods
{
    public class ScoreComponent : MonoBehaviour
    {
        public float Score = 0;
        public bool HasCourtesyCard = false;
        public float CourtesyPlus = 1.6f;
        
        public int MaxExp = 20;
        public int Exp = 0;

        public Guid Guid;

        public delegate void GetScoreHandler();
        public delegate void GetPropHandler();
        
        public static event GetScoreHandler OnGetScore;
        public static event GetPropHandler OnGetProp;

        private void OnEnable()
        {
            CourtesyCardRegenerator.OnClearCourtesyCard += ClearCourtesyCard;
        }

        private void OnDisable()
        {
            CourtesyCardRegenerator.OnClearCourtesyCard -= ClearCourtesyCard;
        } 

        private void Start()
        {
            Score = 0;
            Exp = 0;
            Guid = Guid.NewGuid();
        }

        private void ClearCourtesyCard()
        {
            HasCourtesyCard = false;
        }
        
        public void GetScore(float score, int exp)
        {
            var scoreGot = HasCourtesyCard ? CourtesyPlus * score : score;

            Score += scoreGot;
            Exp += exp;

            if (Exp >= MaxExp)
            {
                OnGetProp?.Invoke();

                Exp -= MaxExp;
            }
            
            OnGetScore?.Invoke();
        }

        public void BeShoot()
        {
            if (!HasCourtesyCard) return;
            HasCourtesyCard = false;
            var position = transform.position - Vector3.back * 2f;
            CourtesyCardRegenerator.Instance.SpawnCourtesyCardAtPosition(position);
        }
    }
}