using System;
using UnityEngine;

namespace ShoppingCart.Scripts.Tutorial
{
    public class TutorialPropBaseComponent : MonoBehaviour
    {
        public Guid Guid;

        public float DestroySelfTime = 10f;
        
        public float ShootSpeed = 10f;
        
        private Rigidbody _rigidbody;

        private float _destroySelfTimer;

        [SerializeField] private GameObject HitGameObject;

        public void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.velocity += transform.forward * ShootSpeed;
        }

        private void Update()
        {
            _destroySelfTimer += Time.deltaTime;

            if (_destroySelfTimer < DestroySelfTime || !gameObject) return;
            
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            var enemy = other.GetComponent<TutorialEnemyComponent>();
            
            if (enemy && enemy.Guid != Guid)
            {
                enemy.BeHurt();

                var go = Instantiate(HitGameObject, transform.position, transform.rotation);
                
                Destroy(go, 1);
            }
        }
    }
}