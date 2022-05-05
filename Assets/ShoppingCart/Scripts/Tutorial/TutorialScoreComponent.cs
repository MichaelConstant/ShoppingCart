using System;
using Photon.Pun;
using ShoppingCart.Scripts.Audio;
using ShoppingCart.Scripts.Goods;
using ShoppingCart.Scripts.Goods.Props;
using ShoppingCart.Scripts.Player;
using UnityEngine;

namespace ShoppingCart.Scripts.Tutorial
{
    public class TutorialScoreComponent : MonoBehaviour
    {
        public float Score = 0;
        public bool HasCourtesyCard = false;
        public float CourtesyPlus = 1.6f;

        public int MaxExp = 20;
        public int Exp = 0;

        public Guid Guid;
        
        [SerializeField] private GameObject BeHurtGameObject;
        [SerializeField] private GameObject GetCouponGameObject;
        
        private const float _RECOVER_TIME = 1f;

        private AudioSource _audioSource;
        
        public event Action OnGetScore;
        public event Action OnGetProp;

        private void OnEnable()
        {
            Score = 0;
            Exp = 0;
            Guid = Guid.NewGuid();
        }

        private void Awake()
        {
            BeHurtGameObject.SetActive(false);
            GetCouponGameObject.SetActive(false);
            _audioSource = GetComponent<AudioSource>();
        }

        public void GetScore(float score, int exp)
        {
            Exp += exp;

            if (Exp < MaxExp) return;

            OnGetProp?.Invoke();

            Exp -= MaxExp;
            
            var scoreGot = HasCourtesyCard ? CourtesyPlus * score : score;

            Score += scoreGot;

            OnGetScore?.Invoke();
        }
        
        public void InstantiateProp(TutorialPropBaseComponent prop, Transform trans)
        {
            var usedProp = Instantiate(prop, trans.position, trans.rotation);

            if (!usedProp) return;

            usedProp.Guid = new Guid();
            usedProp.Guid = Guid;
        }

        public void GetCoupon()
        {
            GetCouponGameObject.SetActive(true);
        }
    }
}