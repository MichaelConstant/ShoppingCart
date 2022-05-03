using System;
using System.Collections;
using System.Collections.Generic;
using HackMan.Scripts.Systems;
using Photon.Pun;
using Photon.Realtime;
using ShoppingCart.Scripts.Goods;
using ShoppingCart.Scripts.Network;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShoppingCart.Scripts.Game_Scene
{
    public class LoadAwardSceneSystem : MonoBehaviourPunCallbacks
    {
        private bool _isLeaving;

        private void OnEnable()
        {
            GameOverSystem.OnGameOver += LoadAwardScene;
        }

        private void OnDisable()
        {
            GameOverSystem.OnGameOver -= LoadAwardScene;
        }

        private void LoadAwardScene()
        {
            if (!_isLeaving)
            {
                StartCoroutine(LoadScene());

                var playerName2ScoreDictionary = new Dictionary<string, float>();
                var playerName2MeshIndex = new Dictionary<string, int>();

                foreach (var player in PlayersInitializeSystem.Instance.Players)
                {
                    var playerNickname = player.GetComponentInParent<PhotonView>().Owner.NickName;

                    var playerScore = player.HasCourtesyCard ? player.Score * 1.2f : player.Score;

                    var playerMeshIndex = player.Model.GetComponent<PlayerModelSelectionComponent>().GetModelIndex();
                    playerName2ScoreDictionary.Add(playerNickname, playerScore);
                    playerName2MeshIndex.Add(playerNickname, playerMeshIndex);
                }

                var playerSavingData = new PlayerSavingData()
                {
                    PlayerName2Score = playerName2ScoreDictionary,
                    PlayerName2MeshIndex = playerName2MeshIndex
                };
                
                AppDataSystem.Save(playerSavingData, "PlayerData");

                _isLeaving = true;
            }
        }

        private IEnumerator LoadScene()
        {
            yield return new WaitForSeconds(3f);

            PhotonNetwork.LoadLevel("VictoryAnimTest");

            yield return null;
        }

        public override void OnLeftRoom()
        {
            PhotonNetwork.LoadLevel("VictoryAnimTest");
            //PhotonNetwork.Disconnect();  
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            PhotonNetwork.LoadLevel("VictoryAnimTest");
        }
    }
}