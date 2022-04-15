using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace ShoppingCart.Scripts.Game_Scene
{
    public class GameWorldSystem : MonoBehaviourPunCallbacks
    {
        public static GameWorldSystem Instance;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        public void LeaveRoomAndLoadHomeScene()
        {
            PhotonNetwork.LeaveRoom();
        }
    
        #region Photon Callback Methods

        public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
        {
            Debug.Log($"Player {newPlayer.NickName} entered the room.");
            Debug.Log("Current Room PlayerCount: " + PhotonNetwork.CurrentRoom.PlayerCount);
        }

        public override void OnLeftRoom()
        {
            PhotonNetwork.Disconnect();
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            PhotonNetwork.LoadLevel("LobbyScene");
        }

        #endregion
    }
}
