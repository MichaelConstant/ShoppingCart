using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ShoppingCart.Scripts.Player;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class TestScript : MonoBehaviour
{
    public List<GameObject> GameObjects = new List<GameObject>();

    private Rect _screenRect = Rect.zero;

    public Image Image;
    public Canvas Canvas;

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
            SetPlayersTagPositions(GameObjects);
        // }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Image.GetComponent<RectTransform>().localPosition = new Vector3(1, 1, 1);
        }
    }

    private void SetPlayersTagPositions(List<GameObject> gameObjects)
    {
        foreach (var go in gameObjects)
        {
            var shouldHide = go.GetComponent<MeshRenderer>().isVisible;
            SetPlayerTagHidden(shouldHide);
            if (!shouldHide)
            {
                AdjustTagPosition(go);
            }
        }
    }

    private void AdjustTagPosition(GameObject player)
    {
        Debug.Log("Adjustposition");
        var distanceVector = -transform.position + player.transform.position;

        var unitDistanceVector = new Vector3(distanceVector.x, 0,distanceVector.z).normalized;
        var angleCos = Vector3.Dot(unitDistanceVector, Vector3.back);
        var angle = Mathf.Acos(angleCos) * Mathf.Rad2Deg * Mathf.Sign(unitDistanceVector.x);
        Debug.Log(angle);
        
        var screenPositionX = unitDistanceVector.x * Canvas.pixelRect.width / 2;
        var screenPositionY = unitDistanceVector.z* Canvas.pixelRect.height / 2 + 100;
        // Debug.Log($"Unit Vector. X : {unitDistanceVector.x} || Y : {unitDistanceVector.y}");
        Image.GetComponent<RectTransform>().localPosition = new Vector3(screenPositionX, screenPositionY, 0);
        Image.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, angle);
    }

    private void SetPlayerTagHidden(bool isHide)
    {
        Image.gameObject.SetActive(!isHide);
        Debug.Log(!isHide);
    }


    //
    // private void SetPlayersTagPositions(List<GameObject> players)
    // {
    //     foreach (var player in players)
    //     {
    //         var playerPosInScreen = Camera.main.WorldToScreenPoint(player.transform.position);
    //         // Debug.Log(playerPosInScreen);
    //         if (!IsPositionInView(playerPosInScreen))
    //         {
    //             AdjustPosition(player);
    //         }
    //     }
    // }
    //
    // private void AdjustPosition(GameObject player)
    // {
    //     var distanceVector = -transform.position + player.transform.position;
    //     if (distanceVector.magnitude < 1)
    //     {
    //         Image.gameObject.SetActive(false);
    //         return;
    //     }
    //     var unitVector = distanceVector.normalized;
    //
    //     var targetWidth = Camera.main.pixelWidth/2 * unitVector.x;
    //     var targetHeight = Camera.main.pixelHeight/2 * unitVector.z;
    //
    //     Image.gameObject.SetActive(true);
    //     Image.rectTransform.localPosition = new Vector3(targetWidth, targetHeight);
    //     
    //     //
    //     Debug.Log($"Unit Vector. X : {unitVector.x} || Y : {unitVector.y}");
    //     // Debug.Log($"Target : X : {targetWidth} || Y : {targetHeight}");
    // }
    //
    // private bool IsPositionInView(Vector3 position)
    // {
    //     // return (position.x <= 1) &&
    //     //         (position.y <= 1) && (position.x >= 0) &&
    //     //         (position.y >= 0);
    //     return _screenRect.Contains(position);
    // }
}