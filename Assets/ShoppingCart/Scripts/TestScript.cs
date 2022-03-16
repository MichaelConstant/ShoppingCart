using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;

public class TestScript : MonoBehaviour
{
    public Vector3 TransformOffset;

    private void Update()
    {
        var position = transform.position;
        position = new Vector3(position.x, TransformOffset.y, position.z);
        transform.position = position;
    }
}
