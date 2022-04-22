using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Photon.Pun;
using ShoppingCart.Scripts.Network;
using Unity.Mathematics;
using Random = UnityEngine.Random;

public class PlayerSpawnSystem : MonoBehaviour
{
    [SerializeField] GameObject _genericVRPlayerPrefab;

    private Dictionary<int, PlayerStartComponent> _playerStartDic = new Dictionary<int, PlayerStartComponent>();
    private PlayerStartComponent[] _playerStartArray;
    private int _index;

    private void Awake()
    {
        _playerStartArray = FindObjectsOfType<PlayerStartComponent>();

        for (var i = 0; i < _playerStartArray.Length; i++)
        {
            _playerStartDic.Add(i, _playerStartArray[i]);
        }
    }

    private void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            do
            {
                _index = Random.Range(0, _playerStartArray.Length + 1);
            } while (!_playerStartDic.ContainsKey(_index));

            var playerStart = _playerStartDic[_index];
            
            Debug.Log(_index);
            
            PhotonNetwork.Instantiate(_genericVRPlayerPrefab.name, playerStart.transform.position, playerStart.transform.rotation);

            _playerStartDic.Remove(_index);
            
            FindObjectOfType<PlayersInitializeSystem>().InitializeGameActors();
        }
    }
}