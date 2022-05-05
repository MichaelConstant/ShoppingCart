using UnityEngine;

namespace ShoppingCart.Scripts.Tutorial
{
    public class TutorialCouponComponent : MonoBehaviour
    {
        private AudioSource _audioSource;
        [SerializeField] private GameObject SpawnedParticle;
        [SerializeField] private GameObject TakenParticle;
        
        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            SpawnedParticle.SetActive(true);
            TakenParticle.SetActive(false);
        }
        
        private void OnDisable()
        {
            SpawnedParticle.SetActive(false);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            var playerScoreComponent = other.GetComponent<TutorialScoreComponent>();
            
            if (!playerScoreComponent || playerScoreComponent.HasCourtesyCard) return;
            
            playerScoreComponent.GetCoupon();
            playerScoreComponent.HasCourtesyCard = true;
            
            Destroy(gameObject);
        }
    }
}