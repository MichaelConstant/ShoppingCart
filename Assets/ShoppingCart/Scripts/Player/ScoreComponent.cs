using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreComponent : MonoBehaviour
{
    public int Score { get; set; }
    public delegate void GoodPurchased(GameObject player);
    public static event GoodPurchased OnGoodPurchased;
    
    private void Start()
    {
        Score = 0;
        GoodComponent.OnGoodPurchasing += GetGoods;
    }

    private void GetGoods(int score, GameObject good)
    {
        Score += score;
        Debug.Log(Score);
        
        // This is Tight coupling method... Maybe needed fix after...
        OnGoodPurchased += good.GetComponent<GoodComponent>().StartCalculateDistance;
        OnGoodPurchased?.Invoke(gameObject);
    }
}
