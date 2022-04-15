using System;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using ShoppingCart.Scripts.Network;
using TMPro;
using UnityEngine;

namespace ShoppingCart.Scripts.Goods
{
    public class ScoreBoardUIComponent : MonoBehaviourPun
    {
        public List<TextMeshProUGUI> PlayerScoreTexts = new List<TextMeshProUGUI>();
        public PlayersInitializeSystem PlayersInitializeSystem;

        private void OnEnable()
        {
            ScoreComponent.PlayerUpdateScore += UpdateScoreBoard;
        }

        private void OnDisable()
        {
            ScoreComponent.PlayerUpdateScore -= UpdateScoreBoard;
        }
        
        public void UpdateScoreBoard()
        {
            this.photonView.RPC(nameof(UpdateScoreBoardRPC), RpcTarget.All);
        }

        [PunRPC]
        private void UpdateScoreBoardRPC()
        {
            if (PlayersInitializeSystem.Players == null) return;

            var scoreMostPlayerFirst =
                PlayersInitializeSystem.Players.OrderByDescending(player => player.Score).ToList();

            for (var i = 0; i < PlayerScoreTexts.Count; i++)
            {
                if (scoreMostPlayerFirst.Count < i + 1 || scoreMostPlayerFirst[i] == null) continue;

                PlayerScoreTexts[i].text = "" + scoreMostPlayerFirst[i].Score;
            }
        }
    }
}