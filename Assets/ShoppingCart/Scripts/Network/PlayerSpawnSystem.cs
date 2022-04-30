using System.Collections.Generic;
using Photon.Pun;
using ShoppingCart.Scripts.Goods;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ShoppingCart.Scripts.Network
{
    public class PlayerSpawnSystem : MonoBehaviourPun
    {
        [SerializeField] GameObject GenericVRPlayerPrefab;

        private PlayerStartComponent[] _playerStartArray;
        private int _index;

        private void Awake()
        {
            _playerStartArray = FindObjectsOfType<PlayerStartComponent>();
        }

        private void Start()
        {
            if (PhotonNetwork.IsConnectedAndReady)
            {
                this.photonView.RPC(nameof(InitializeIndexRPC), RpcTarget.AllBuffered);

                var player = PhotonNetwork.Instantiate(GenericVRPlayerPrefab.name,
                    _playerStartArray[_index].transform.position,
                    _playerStartArray[_index].transform.rotation);

                player.GetComponentInChildren<PlayerModelSelectionComponent>().UseModel(_index);
                
                this.photonView.RPC(nameof(RemoveIndexRPC), RpcTarget.AllBuffered);

                PlayersInitializeSystem.Instance.InitializeGameActors();

                ScoreComponent.ManuallyInvokePlayerUpdateScore();
            }
        }

        [PunRPC]
        private void RemoveIndexRPC()
        {
            Destroy(_playerStartArray[_index].gameObject);
            _playerStartArray = FindObjectsOfType<PlayerStartComponent>();
        }

        [PunRPC]
        private void InitializeIndexRPC()
        {
            _index = GetPlayerStartIndex();
        }

        private int GetPlayerStartIndex()
        {
            var index = Random.Range(0, _playerStartArray.Length);
            return index;
        }
    }
}