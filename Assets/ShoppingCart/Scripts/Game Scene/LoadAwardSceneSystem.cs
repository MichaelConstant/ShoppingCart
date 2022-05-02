using System;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;
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
                //PhotonNetwork.LeaveRoom();
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