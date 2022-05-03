using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using ShoppingCart.Scripts.Goods;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ShoppingCart.Scripts.Network
{
    public class PlayerSpawnSystem : MonoBehaviourPun
    {
        [SerializeField] GameObject GenericVRPlayerPrefab;
        [SerializeField] private GameObject PlayerStartRPC;
        
        private PlayerStartComponent[] _playerStartArray;
        private List<GameObject> _playerStartRPCArray = new List<GameObject>();
        private int _index;

        private static PlayerSpawnSystem _instance;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
            }
            
            _playerStartRPCArray = GameObject.FindGameObjectsWithTag("PlayerStart").ToList();
            if (_playerStartRPCArray == null || _playerStartRPCArray.Count == 0)
            {
                _playerStartArray = FindObjectsOfType<PlayerStartComponent>();
                foreach (var playerStart in _playerStartArray)
                {
                    var newTransform = playerStart.transform;
                    var newStart = PhotonNetwork.Instantiate(PlayerStartRPC.name, newTransform.position, newTransform.rotation);
                    Destroy(playerStart);
                    _playerStartRPCArray.Add(newStart);
                }
            }
        }

        private void Start()
        {
            if (PhotonNetwork.IsConnectedAndReady)
            {
                this.photonView.RPC(nameof(InitializeIndexRPC), RpcTarget.AllBuffered);
    
                var player = PhotonNetwork.Instantiate(GenericVRPlayerPrefab.name,
                    _playerStartRPCArray[_index].transform.position,
                    _playerStartRPCArray[_index].transform.rotation);
    
                player.GetComponentInChildren<PlayerModelSelectionComponent>().UseModel(_index);
   
                this.photonView.RPC(nameof(RemoveIndexRPC), RpcTarget.AllBuffered);

                PhotonNetwork.Destroy(_playerStartRPCArray[_index].gameObject);

                PlayersInitializeSystem.Instance.InitializeGameActors();

                ScoreComponent.ManuallyInvokePlayerUpdateScore();
            }
        }

        [PunRPC]
        private void RemoveIndexRPC()
        {
            if (_playerStartRPCArray[_index])
            {
                _playerStartRPCArray = GameObject.FindGameObjectsWithTag("PlayerStart").ToList();
            }
        }

        [PunRPC]
        private void InitializeIndexRPC()
        {
            _index = GetPlayerStartIndex();
        }

        private int GetPlayerStartIndex()
        {
            var index = Random.Range(0, _playerStartRPCArray.Count);
            return index;
        }
    }
}