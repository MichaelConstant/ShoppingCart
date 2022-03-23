using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

[System.Serializable]
public enum MapLibrary
{
    TestScene,
}

[System.Serializable]
public struct MapType
{
    public MapLibrary MapLibrary;
    public string SceneName;

    public MapType(MapLibrary mapLibrary, string sceneName)
    {
        MapLibrary = mapLibrary;
        SceneName = sceneName;
    }
}

public class PhotonRoomSystem : MonoBehaviourPunCallbacks
{
    public List<MapType> MapType = new List<MapType>();
    
    private string _mapType;

    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
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

    #endregion

    #region Custom Methods

    private void CreateRoom()
    {
        var randomRoomName = "Room_" + Random.Range(0, 100);
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