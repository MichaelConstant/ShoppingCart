using System;
using UnityEngine;

namespace ShoppingCart.Scripts.Goods
{
    public class ParticleEffectComponent : MonoBehaviour
    {
        private void Start()
        {
            Invoke(nameof(SetSelfInactive), 2f);
        }

        private void SetSelfInactive()
        {
            gameObject.SetActive(false);
        }
    }
}