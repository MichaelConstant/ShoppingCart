using System;
using Photon.Realtime;
using UnityEngine;

namespace ShoppingCart.Scripts.Goods.Props
{
    public class ShootProp : PropBase
    {
        public float ShootSpeed = 10f;

        private Rigidbody _rigidbody;

        public void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.velocity += transform.forward * ShootSpeed;
        }

        private void OnTriggerEnter(Collider other)
        {
            var scoreComponent = other.GetComponent<ScoreComponent>();
            
            if (scoreComponent && scoreComponent.Guid != Guid)
            {
                scoreComponent.BeShoot();
                
                Destroy(gameObject);
            }
        }
    }
}