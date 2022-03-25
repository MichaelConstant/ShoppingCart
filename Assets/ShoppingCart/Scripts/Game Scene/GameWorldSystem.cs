using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameWorldSystem : MonoBehaviourPunCallbacks
{
    #region Photon Callback Methods

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log($"Player {newPlayer.NickName} entered the room.");
        Debug.Log("Current Room PlayerCount: " + PhotonNetwork.CurrentRoom.PlayerCount);
    }

    #endregion
}
