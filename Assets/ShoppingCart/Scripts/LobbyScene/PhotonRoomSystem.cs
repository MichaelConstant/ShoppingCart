using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

// [System.Serializable]
// public struct MapType
// {
//     public MapLibrary MapLibrary;
//     public string SceneName;
//
//     public MapType(MapLibrary mapLibrary, string sceneName)
//     {
//         MapLibrary = mapLibrary;
//         SceneName = sceneName;
//     }
// }

public class PhotonRoomSystem : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI PlayerCountTextMeshPro;

    private string _mapType;

    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

        if (!PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            PhotonNetwork.JoinLobby();
        }
    }

    #region UI Callback Methods

    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public void OnTestButtonClicked()
    {
        _mapType = ConstantLibrary.MAP_TYPE_VALUE_TEST;

        var roomProperties = new ExitGames.Client.Photon.Hashtable()
        {
            {ConstantLibrary.MAP_TYPE_KEY, _mapType}
        };

        PhotonNetwork.JoinRandomRoom(roomProperties, 0);
    }

    #endregion

    #region Photon Callback Methods

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected again.");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        CreateRoom();
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("A room is created with the name" + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Player" + PhotonNetwork.NickName + " enter the room.");

        if (!PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey(ConstantLibrary.MAP_TYPE_KEY)) return;

        if (!PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(ConstantLibrary.MAP_TYPE_KEY, out var mapType))
            return;

        if ((string) mapType == ConstantLibrary.MAP_TYPE_VALUE_TEST)
        {
            PhotonNetwork.LoadLevel("TestGameScene");
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log($"Player {newPlayer.NickName} entered the room.");
        Debug.Log("Current Room Players: " + PhotonNetwork.CurrentRoom.PlayerCount);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if (roomList.Count == 0)
        {
            // No Room At All
            PlayerCountTextMeshPro.text = 0 + "/" + 4;
        }

        foreach (var room in roomList)
        {
            Debug.Log(room.Name);
            if (room.Name.Contains(ConstantLibrary.MAP_TYPE_VALUE_TEST))
            {
                PlayerCountTextMeshPro.text = room.PlayerCount + "/" + 4;
            }
        }
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
    }

    #endregion

    #region Custom Methods

    private void CreateRoom()
    {
        var randomRoomName = "Room_" + _mapType + Random.Range(0, 100);
        var roomOptions = new RoomOptions
        {
            MaxPlayers = 4
        };

        string[] roomPropsInLobby = {ConstantLibrary.MAP_TYPE_KEY};

        var customRoomProperties = new ExitGames.Client.Photon.Hashtable()
        {
            {ConstantLibrary.MAP_TYPE_KEY, _mapType}
        };

        roomOptions.CustomRoomPropertiesForLobby = roomPropsInLobby;
        roomOptions.CustomRoomProperties = customRoomProperties;

        PhotonNetwork.CreateRoom(randomRoomName, roomOptions);
    }

    #endregion
}