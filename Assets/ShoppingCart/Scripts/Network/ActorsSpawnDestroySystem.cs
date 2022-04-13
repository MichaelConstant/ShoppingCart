using System;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using ShoppingCart.Scripts.Goods;
using UnityEngine;

namespace ShoppingCart.Scripts.Network
{
    public class ActorsSpawnDestroySystem : MonoBehaviourPun
    {
        public List<ScoreComponent> Players { get; private set; }
        public List<GoodComponent> Goods { get; private set; }
        
        public static ActorsSpawnDestroySystem Instance;

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
            Players = new List<ScoreComponent>();
            Players = FindObjectsOfType<ScoreComponent>().ToList();

            Goods = new List<GoodComponent>();
            Goods = FindObjectsOfType<GoodComponent>().ToList();
        }
        #endregion
    }
}