using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class GoodComponent : MonoBehaviour
{
    public int Score;
    public delegate void GoodPurchasing(int score, GameObject good);
    public static event GoodPurchasing OnGoodPurchasing;

    private bool _canCalculate;
    private GameObject _player;
    private const float _DISTANCETODESTORY = 2f; 
    
    private void Start()
    {
        _canCalculate = false;
    }

    private void Update()
    {
        if(!_canCalculate) return;
        var distance = Vector3.Distance(_player.transform.position, transform.position);
        if(distance >= _DISTANCETODESTORY) return;
        Destroy(gameObject);
    }

    public void StartCalculateDistance(GameObject player)
    {
        _player = player;
        _canCalculate = true;
    }
    
    public void BePurchased()
    {
        OnGoodPurchasing?.Invoke(Score, gameObject);
    }
}