using System;
using Photon.Pun;
using UnityEngine;

namespace ShoppingCart.Scripts.Goods.Props
{
    public class PropBase : MonoBehaviourPun
    {
        public Guid Guid;

        public float DestroySelfTime = 10f;

        [PunRPC]
        protected void SetSelfInactiveRPC()
        {
            gameObject.SetActive(false);
        }
    }
}