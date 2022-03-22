using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class LoginSystem : MonoBehaviourPunCallbacks
{
    public TMP_InputField PlayerNameInputField;

    #region Unity Methods

    #endregion

    #region UI Methods

    public void TestConnect()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void ConnectToPhotonServer()
    {
        if (PlayerNameInputField == null) return;
        PhotonNetwork.NickName = PlayerNameInputField.text;
        PhotonNetwork.ConnectUsingSettings();
    }

    #endregion

    #region Photon Callbacks

    public override void OnConnected()
    {
        Debug.Log("Connecting...");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Successfully Connected");

        if (PhotonNetwork.NickName != null)
        {
            Debug.Log("Welcome, " + PhotonNetwork.NickName);
        }
        
        PhotonNetwork.LoadLevel("LobbyScene");
    }

    #endregion
}