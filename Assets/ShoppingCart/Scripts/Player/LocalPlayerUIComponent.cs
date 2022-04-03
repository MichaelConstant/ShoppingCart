using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalPlayerUIComponent : MonoBehaviour
{
    public  GameObject BackToLobbyButton;

    private void Start()
    {
        BackToLobbyButton.GetComponent<Button>().onClick.AddListener(GameWorldSystem.Instance.LeaveRoomAndLoadHomeScene);
    }
}
