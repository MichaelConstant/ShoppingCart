using System;
using Photon.Pun;
using ShoppingCart.Scripts.Goods;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShoppingCart.Scripts.UI
{
    public class LocalTraceImageComponent : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI Name;
        [SerializeField] private TextMeshProUGUI Distance;
        [SerializeField] private TextMeshProUGUI PlayerRank;
        [SerializeField] private Image CourtesyCard;
        [SerializeField] private LocalPlayerTraceUIComponent MainTraceUI;

        private void OnEnable()
        {
            MainTraceUI.OnUpdateImageInfo += UpdateImageInfo;
        }

        private void OnDisable()
        {
            MainTraceUI.OnUpdateImageInfo -= UpdateImageInfo;
        }

        private void UpdateImageInfo(ScoreComponent player)
        {
            
        }
    }
}