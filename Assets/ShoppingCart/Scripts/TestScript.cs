using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;

public class TestScript : MonoBehaviour
{

    public void OnTestEnter()
    {
        Debug.Log("Enter");
    }

    public void OnTestEnd()
    {
        Debug.Log(("Exit"));
    }
}
