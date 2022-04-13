using System;
using Photon.Pun;
using ShoppingCart.Scripts.Goods.Props;
using UnityEngine;

namespace ShoppingCart.Scripts.Goods
{
    public class ScoreComponent : MonoBehaviourPun
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
            this.photonView.RPC(nameof(SetNewExpRPC), RpcTarget.AllBuffered, exp);

            this.photonView.RPC(nameof(SetNewScoreRPC), RpcTarget.AllBuffered, score);

            OnGetScore?.Invoke();
        }
        
        public void BeShoot()
        {
            this.photonView.RPC(nameof(PlayerBeShootRPC), RpcTarget.All);
        }

        public void InstantiateProp(PropBase prop, Transform trans)
        {
            this.photonView.RPC(nameof(InstantiateNewPropRPC), RpcTarget.All, prop, trans);
        }
        
        #region RPC methods

        [PunRPC]
        private void SetNewExpRPC(int exp)
        {
            Exp += exp;

            if (Exp < MaxExp) return;

            OnGetProp?.Invoke();

            Exp -= MaxExp;
        }

        [PunRPC]
        private void SetNewScoreRPC(float score)
        {
            var scoreGot = HasCourtesyCard ? CourtesyPlus * score : score;

            Score += scoreGot;
        }

        [PunRPC]
        private void InstantiateNewPropRPC(PropBase prop, Transform trans)
        {
            var shootProp = Instantiate(prop, trans);
            shootProp.Guid = new Guid();
            shootProp.Guid = Guid;
        }

        [PunRPC]
        private void PlayerBeShootRPC()
        {
            if (!HasCourtesyCard) return;
            
            HasCourtesyCard = false;
            
            var position = transform.position - Vector3.back * 2f;
            
            CourtesyCardRegenerator.Instance.SpawnCourtesyCardAtPosition(position);
        }
        
        #endregion


    }
}