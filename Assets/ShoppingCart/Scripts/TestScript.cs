using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class TestScript : MonoBehaviour
{
    public List<GameObject> GameObjects = new List<GameObject>();

    private Rect _screenRect = Rect.zero;
    
    public Image Image;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetPlayersTagPositions(GameObjects);
        }
    }

    private void SetPlayersTagPositions(List<GameObject> players)
    {
        foreach (var player in players)
        {
            var playerPosInScreen = Camera.main.WorldToScreenPoint(player.transform.position);
            // Debug.Log(playerPosInScreen);
            if (!IsPositionInView(playerPosInScreen))
            {
                AdjustPosition(player);
            }
        }
    }

    private void AdjustPosition(GameObject player)
    {
        var distanceVector = -transform.position + player.transform.position;
        if (distanceVector.magnitude < 1)
        {
            Image.gameObject.SetActive(false);
            return;
        }
        var unitVector = distanceVector.normalized;

        var targetWidth = Camera.main.pixelWidth/2 * unitVector.x;
        var targetHeight = Camera.main.pixelHeight/2 * unitVector.z;

        Image.gameObject.SetActive(true);
        Image.rectTransform.localPosition = new Vector3(targetWidth, targetHeight);
        
        //
        Debug.Log($"Unit Vector. X : {unitVector.x} || Y : {unitVector.y}");
        // Debug.Log($"Target : X : {targetWidth} || Y : {targetHeight}");
    }

    private bool IsPositionInView(Vector3 position)
    {
        // return (position.x <= 1) &&
        //         (position.y <= 1) && (position.x >= 0) &&
        //         (position.y >= 0);
        return _screenRect.Contains(position);
    }
}