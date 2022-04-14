using System;
using System.Collections;
using Photon.Pun;
using UnityEngine;

namespace ShoppingCart.Scripts.Goods
{
    public class CourtesyCardComponent : MonoBehaviourPun
    {
        private void OnEnable()
        {
            CourtesyCardRegenerator.OnClearCourtesyCard += ClearCourtesyCard;
        }

        private void OnDisable()
        {
            CourtesyCardRegenerator.OnClearCourtesyCard -= ClearCourtesyCard;
        }

        private void ClearCourtesyCard()
        {
            this.photonView.RPC(nameof(SetSelfInactiveRPC), RpcTarget.AllBuffered);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            var playerScoreComponent = other.GetComponent<ScoreComponent>();
            
            if (!playerScoreComponent || playerScoreComponent.HasCourtesyCard) return;
            
            playerScoreComponent.HasCourtesyCard = true;
            
            ClearCourtesyCard();
        }

        [PunRPC]
        private void SetSelfInactiveRPC()
        {
            gameObject.SetActive(false);
        }
    }
}