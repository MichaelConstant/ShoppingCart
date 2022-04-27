using System;
using System.Collections;
using Photon.Pun;
using ShoppingCart.Scripts.Audio;
using UnityEngine;

namespace ShoppingCart.Scripts.Goods
{
    public class CourtesyCardComponent : MonoBehaviourPun
    {
        private AudioSource _audioSource;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();

            AudioInventory.Instance.PlayAudioClip(_audioSource, AudioInventory.AudioEnum.CouponAppear);
        }

        private void OnEnable()
        {
            CourtesyCardRegenerator.Instance.OnClearCourtesyCard += ClearCourtesyCard;
        }

        private void OnDisable()
        {
            CourtesyCardRegenerator.Instance.OnClearCourtesyCard -= ClearCourtesyCard;
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
            AudioInventory.Instance.PlayAudioClipAtLocation(transform.position, AudioInventory.AudioEnum.CouponGet);
            
            gameObject.SetActive(false);
        }
    }
}