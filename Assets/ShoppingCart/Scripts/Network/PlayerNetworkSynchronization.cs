using System;
using Photon.Pun;
using UnityEngine;

namespace ShoppingCart.Scripts.Network
{
    public class PlayerNetworkSynchronization : MonoBehaviour, IPunObservable
    {



        private PhotonView _photonView;

        private void Start()
        {
            _photonView = GetComponent<PhotonView>();
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            throw new System.NotImplementedException();
        }
    }
}