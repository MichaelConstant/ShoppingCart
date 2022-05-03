using System;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using ShoppingCart.Scripts.Goods;
using ShoppingCart.Scripts.Network;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ShoppingCart.Scripts.UI
{
    public class LocalPlayerTraceUIComponent : MonoBehaviour
    {
        public ScoreComponent MePlayer;

        private readonly Dictionary<Image, RectTransform>
            _image2RectTransforms = new Dictionary<Image, RectTransform>();

        private readonly Dictionary<ScoreComponent, Image> _player2Images = new Dictionary<ScoreComponent, Image>();

        [SerializeField] private List<Image> LocalImages;

        private List<ScoreComponent> _otherPlayers = new List<ScoreComponent>();

        public event Action<ScoreComponent> OnUpdateImageInfo;

        private void Start()
        {
            _otherPlayers = FindOtherPlayers();

            if (_otherPlayers == null) return;

            for (var i = 0; i < _otherPlayers.Count; i++)
            {
                _player2Images.Add(_otherPlayers[i], LocalImages[i]);
            }

            foreach (var image in LocalImages)
            {
                _image2RectTransforms.Add(image, image.GetComponent<RectTransform>());
            }
        }

        private void Update()
        {
            if (_otherPlayers == null) return;
            SetPlayersTagPositions(_otherPlayers);
        }

        private void SetPlayersTagPositions(List<ScoreComponent> players)
        {
            foreach (var player in players)
            {
                //var shouldHide = player.Mesh.isVisible;

                //SetPlayerTagHidden(player, shouldHide);

                //if (shouldHide) continue;

                AdjustTagPosition(player);

                OnUpdateImageInfo?.Invoke(player);
            }
        }

        private void AdjustTagPosition(ScoreComponent player)
        {
            var distanceVector = player.transform.position - MePlayer.transform.position;
            var unitVector = new Vector3(distanceVector.x, 0, distanceVector.z).normalized;

            var angleCos = Vector3.Dot(unitVector, Vector3.back);
            var angle = Mathf.Acos(angleCos) * Mathf.Rad2Deg * Mathf.Sign(unitVector.x);

            var screenPositionX = unitVector.x * 170f * Mathf.Sign(unitVector.x);
            var screenPositionY = unitVector.y * 85f;

            var image = _player2Images[player];
            var imageRectTransform = _image2RectTransforms[image];

            imageRectTransform.localPosition = new Vector3(screenPositionX, screenPositionY, 0);
            imageRectTransform.localRotation = Quaternion.Euler(0, 0, angle);
        }

        private void SetPlayerTagHidden(ScoreComponent player, bool isHide)
        {
            var image = _player2Images[player];
            image.gameObject.SetActive(!isHide);
        }

        private List<ScoreComponent> FindOtherPlayers()
        {
            var otherPlayers = PlayersInitializeSystem.Instance.Players.Where(player => player.Guid != MePlayer.Guid)
                .ToList();

            return otherPlayers;
        }
    }
}