using System;
using System.Collections.Generic;
using ShoppingCart.Scripts.Game_Scene;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ShoppingCart.Scripts.Goods
{
    public class ExpensiveShelfSpawnSystem : MonoBehaviour
    {
        private readonly List<GameObject> _expensiveShelves = new List<GameObject>();
        [SerializeField] private float RefreshTime;
        [SerializeField] private float LastTime;

        private int _currentIndex = -1;
        private float _timer;
        private bool _canSpawn;

        private void OnEnable()
        {
            GameOverSystem.OnCountdownEnd += SetCanSpawn;
        }

        private void OnDisable()
        {
            GameOverSystem.OnCountdownEnd -= SetCanSpawn;
        }

        private void Start()
        {
            for (var i = 0; i < transform.childCount; i++)
            {
                _expensiveShelves.Add(transform.GetChild(i).gameObject);
            }

            foreach (var expensiveShelf in _expensiveShelves)
            {
                expensiveShelf.SetActive(false);
            }
        }

        private void Update()
        {
            if (!_canSpawn) return;

            _timer += Time.deltaTime;

            if (_currentIndex == -1)
            {
                if (!(_timer >= RefreshTime)) return;

                SpawnExpensiveShelf();
            }
            else
            {
                if (!(_timer >= LastTime)) return;

                DestroyExpensiveShelf();
            }
        }

        private void SetCanSpawn()
        {
            _canSpawn = true;
        }

        private void SpawnExpensiveShelf()
        {
            _currentIndex = Random.Range(0, _expensiveShelves.Count);
            _expensiveShelves[_currentIndex].SetActive(true);
            _timer = 0f;
        }

        private void DestroyExpensiveShelf()
        {
            _expensiveShelves[_currentIndex].SetActive(false);
            _currentIndex = -1;
            _timer = 0f;
        }
    }
}