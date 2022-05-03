using System;
using System.Collections.Generic;
using System.Linq;
using HackMan.Scripts.Systems;
using ShoppingCart.Scripts.Network;
using TMPro;
using UnityEngine;

namespace ShoppingCart.Scripts.Game_Scene
{
    public class VictorySceneSystem : Singleton<VictorySceneSystem>
    {
        [SerializeField] private PlayerModelSelectionComponent Winner;
        private Dictionary<string, float> _playerName2Score = new Dictionary<string, float>();
        private Dictionary<string, int> _playerName2MeshIndex = new Dictionary<string, int>();

        public List<TextMeshProUGUI> PlayerScoreTexts = new List<TextMeshProUGUI>();
        public List<TextMeshProUGUI> PlayerNameTexts = new List<TextMeshProUGUI>();

        private void Awake()
        {
            var playerSavingData = AppDataSystem.Load<PlayerSavingData>("PlayerData");
            _playerName2Score = playerSavingData.PlayerName2Score;
            _playerName2MeshIndex = playerSavingData.PlayerName2MeshIndex;

            var playerList = _playerName2Score.OrderByDescending(player => player.Value).ToList();
            var winnerName = playerList[0].Key;
            var winnerMeshIndex = _playerName2MeshIndex[winnerName];

            Winner.LocalUseModel(winnerMeshIndex);
            
            for (var i = 0; i < playerList.Count; i++)
            {
                PlayerNameTexts[i].text = "" + playerList[i].Key;
                PlayerScoreTexts[i].text = "" + playerList[i].Value;
            }
        }
    }
}