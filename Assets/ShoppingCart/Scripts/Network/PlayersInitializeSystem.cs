using System;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using ShoppingCart.Scripts.Goods;
using UnityEngine;

namespace ShoppingCart.Scripts.Network
{
    public class PlayersInitializeSystem : MonoBehaviourPun
    {
        public List<ScoreComponent> Players = new List<ScoreComponent>();

        public static PlayersInitializeSystem Instance;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        #region Public Methods

        public void InitializeGameActors()
        {
            this.photonView.RPC(nameof(InitializeGameActorsRPC), RpcTarget.AllBuffered);
        }
        
        #endregion

        #region RPC Methods

        [PunRPC]
        public void InitializeGameActorsRPC()
        {
            Players = FindObjectsOfType<ScoreComponent>().ToList();
        }
        #endregion
    }
}