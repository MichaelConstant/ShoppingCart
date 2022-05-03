using System;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using UnityEngine;

namespace ShoppingCart.Scripts.Network
{
    public class PlayerModelSelectionComponent : MonoBehaviourPun
    {
        private List<GameObject> _models = new List<GameObject>();

        private void Awake()
        {
            for (var i = 0; i < transform.childCount; i++)
            {
                _models.Add(transform.GetChild(i).gameObject);
            }
        }

        public void UseModel(int index)
        {
            this.photonView.RPC(nameof(ChangeModelRPC), RpcTarget.AllBuffered, index);
        }

        public void LocalUseModel(int index)
        {
            for (var i = 0; i < _models.Count; i++)
            {
                _models[i].SetActive(i == index);
            }
        }

        public int GetModelIndex()
        {
            var usedModel = _models.FirstOrDefault(model => model.activeSelf);
            return _models.IndexOf(usedModel);
        }
        
        [PunRPC]
        private void ChangeModelRPC(int index)
        {
            for (var i = 0; i < _models.Count; i++)
            {
                _models[i].SetActive(i == index);
            }
        }
    }
}