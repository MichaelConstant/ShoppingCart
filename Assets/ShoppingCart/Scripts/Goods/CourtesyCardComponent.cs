using System;
using System.Collections;
using UnityEngine;

namespace ShoppingCart.Scripts.Goods
{
    public class CourtesyCardComponent : MonoBehaviour
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
            Destroy(gameObject);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            var playerScoreComponent = other.GetComponent<ScoreComponent>();
            
            if (!playerScoreComponent || playerScoreComponent.HasCourtesyCard) return;
            
            playerScoreComponent.HasCourtesyCard = true;
            
            Destroy(gameObject);
        }
    }
}