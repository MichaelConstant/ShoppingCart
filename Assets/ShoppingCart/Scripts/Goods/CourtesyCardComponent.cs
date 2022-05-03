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
        [SerializeField] private GameObject SpawnedParticle;
        [SerializeField] private GameObject TakenParticle;

        public static event Action<string> OnCouponSpawnOrTakeAway;
        
        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();

            AudioInventory.Instance.PlayAudioClip(_audioSource, AudioInventory.AudioEnum.CouponAppear);
        }

        private void OnEnable()
        {
            CourtesyCardRegenerator.Instance.OnClearCourtesyCard += ClearCourtesyCard;
            SpawnedParticle.SetActive(true);
            TakenParticle.SetActive(false);
            
            OnCouponSpawnOrTakeAway?.Invoke("1230");
        }

        private void OnDisable()
        {
            // CourtesyCardRegenerator.Instance.OnClearCourtesyCard -= ClearCourtesyCard;
            SpawnedParticle.SetActive(false);
        }

        private void ClearCourtesyCard()
        {
            this.photonView.RPC(nameof(SetSelfInactiveRPC), RpcTarget.AllBuffered);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            var playerScoreComponent = other.GetComponent<ScoreComponent>();
            
            if (!playerScoreComponent || playerScoreComponent.HasCourtesyCard) return;
            
            playerScoreComponent.GetCoupon();
            playerScoreComponent.HasCourtesyCard = true;
            
            ClearCourtesyCard();

            var playerName = other.GetComponentInParent<PhotonView>().Owner.NickName;
            OnCouponSpawnOrTakeAway?.Invoke(playerName);
        }

        private IEnumerator CreateParticle(GameObject particle)
        {
            particle.SetActive(true);

            yield return new WaitForSeconds(1);
            
            particle.SetActive(false);

            yield return null;
        }

        [PunRPC]
        private void SetSelfInactiveRPC()
        {
            AudioInventory.Instance.PlayAudioClipAtLocation(transform.position, AudioInventory.AudioEnum.CouponGet);
            
            StopAllCoroutines();

            // StartCoroutine(CreateParticle(TakenParticle));
            
            gameObject.SetActive(false);
        }
    }
}