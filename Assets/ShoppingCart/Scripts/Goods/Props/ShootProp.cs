using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace ShoppingCart.Scripts.Goods.Props
{
    public class ShootProp : PropBase
    {
        public float ShootSpeed = 10f;
        
        private Rigidbody _rigidbody;

        private float _destroySelfTimer;

        public void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.velocity += transform.forward * ShootSpeed;
        }

        private void Update()
        {
            _destroySelfTimer += Time.deltaTime;

            if (_destroySelfTimer < DestroySelfTime || !gameObject) return;

            this.photonView.RPC(nameof(SetSelfInactiveRPC), RpcTarget.AllBuffered);
        }

        private void OnTriggerEnter(Collider other)
        {
            var scoreComponent = other.GetComponent<ScoreComponent>();
            
            if (scoreComponent && scoreComponent.Guid != Guid)
            {
                scoreComponent.BeShoot();
                
                this.photonView.RPC(nameof(SetSelfInactiveRPC), RpcTarget.AllBuffered);
            }
        }
        
    }
}