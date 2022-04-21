using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace ShoppingCart.Scripts.Goods
{
    [RequireComponent(typeof(XRSimpleInteractable))]
    public class GoodComponent : MonoBehaviourPun
    {
        public float Score;
        public int Exp = 1;

        private const float _DISTANCE_TO_DESTROY = 0.5f;

        private bool _isSelected = false;
        private float _selectTimer = .0f;

        private Color _originalColor;

        private PurchaseHand _player;

        private void Start()
        {
            _originalColor = GetComponent<MeshRenderer>().material.color;
        }

        private void OnEnable()
        {
            GetComponent<XRSimpleInteractable>().selectExited.AddListener(OnSelectExit);
        }

        private void OnDisable()
        {
            GetComponent<XRSimpleInteractable>().selectExited.RemoveListener(OnSelectExit);
        }

        private void Update()
        {
            if (!_isSelected || !gameObject) return;

            _selectTimer += Time.deltaTime;

            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, _selectTimer);

            transform.position = Vector3.Lerp(transform.position, _player.transform.position, _selectTimer);

            if (_selectTimer <= 0.9f) return;
            
            _player.GetScore(gameObject);
            
            if (!(_selectTimer >= 0.99f)) return;
            
            this.photonView.RPC(nameof(SetSelfInvalidRPC), RpcTarget.AllBuffered);
        }

        private void OnSelectExit(SelectExitEventArgs eventArgs)
        {
            if (_isSelected) return;

            _player = eventArgs.interactorObject.transform.GetComponent<PurchaseHand>();
            
            if (_player == null || _player.IsPurchasing) return;

            _player.PurchaseGoods(Score, Exp);

            _isSelected = true;
        }

        public void OnHoverEnter()
        {
            GetComponent<MeshRenderer>().material.color = Color.red;
        }

        public void OnHoverExit()
        {
            GetComponent<MeshRenderer>().material.color = _originalColor;
        }

        [PunRPC]
        private void SetSelfInvalidRPC()
        {
            gameObject.SetActive(false);
        }
    }
}