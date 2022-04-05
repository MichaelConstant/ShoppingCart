using System;
using UnityEngine;

namespace ShoppingCart.Scripts.Goods
{
    public class PropBase : MonoBehaviour
    {
        public float ShootSpeed = 10f;

        private Rigidbody _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.velocity += transform.forward * ShootSpeed;
        }
    }
}