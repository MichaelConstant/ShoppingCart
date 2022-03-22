using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginUISystem : MonoBehaviour
{
    public GameObject ConnectOptionsPanelGameObject;
    public GameObject SignInPanelGameObject;

    private void Start()
    {
        ConnectOptionsPanelGameObject.SetActive(true);
        SignInPanelGameObject.SetActive(false);
    }
    
    
}
