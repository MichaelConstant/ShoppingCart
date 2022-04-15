using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingCart.Scripts.Goods;
using ShoppingCart.Scripts.Network;
using ShoppingCart.Scripts.Player;
using UnityEngine;

namespace ShoppingCart.Scripts.UI
{
    public class LocalPlayerTraceUIComponent : MonoBehaviour
    {
        public ScoreComponent MePlayer;

        private Camera _myCamera;

        private List<ScoreComponent> _otherPlayers = new List<ScoreComponent>();

        private void Start()
        {
            _myCamera = Camera.main;
        }

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Space)) return;
            SetPlayersTagPositions(FindOtherPlayers());
        }

        private void SetPlayersTagPositions(List<ScoreComponent> players)
        {
            foreach (var player in players)
            {
                var playerTag = player.LocalPlayerTraceUIComponent;

                var playerTagPosInScreen = _myCamera.WorldToScreenPoint(playerTag.transform.position);

                if (!IsPositionInView(playerTagPosInScreen))
                {
                    ResetTagPosition(playerTag);
                }

                Debug.Log(
                    $"X: {playerTagPosInScreen.x}|{_myCamera.pixelWidth}, Y: {playerTagPosInScreen.y}|{_myCamera.pixelHeight}");
            }
        }

        private bool IsPositionInView(Vector3 playerTagPosInScreen)
        {
            return !(!(playerTagPosInScreen.x <= _myCamera.pixelWidth) &&
                     !(playerTagPosInScreen.y <= _myCamera.pixelHeight) && !(playerTagPosInScreen.x >= 0) &&
                     !(playerTagPosInScreen.y >= 0));
        }

        private void ResetTagPosition(LocalPlayerTraceUIComponent playerTag)
        {
            var minusVector = (transform.position - playerTag.transform.position).normalized;


            var tagWidth = minusVector.x * _myCamera.pixelWidth;
            var tagHeight = minusVector.y * _myCamera.pixelHeight;

            if (minusVector.x >= minusVector.y)
            {
                tagWidth = minusVector.x > 0 ? _myCamera.pixelWidth : 0;
            }
            else
            {
                tagHeight = minusVector.y > 0 ? _myCamera.pixelHeight : 0;
            }

            playerTag.transform.position =
                _myCamera.ScreenToWorldPoint(new Vector3(tagWidth, tagHeight,
                    _myCamera.transform.position.z + MePlayer.transform.forward.z));
        }

        private List<ScoreComponent> FindOtherPlayers()
        {
            _otherPlayers = PlayersInitializeSystem.Instance.Players.Where(player => player.Guid != MePlayer.Guid)
                .ToList();

            return _otherPlayers;
        }
    }
}