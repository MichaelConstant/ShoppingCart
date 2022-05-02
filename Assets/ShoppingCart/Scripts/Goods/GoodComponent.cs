using System;
using Photon.Pun;
using Photon.Realtime;
using ShoppingCart.Scripts.Audio;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace ShoppingCart.Scripts.Goods
{
    [RequireComponent(typeof(XRSimpleInteractable))]
    public class GoodComponent : MonoBehaviourPun
    {
        public enum GoodType
        {
            Low,
            Middle,
            Expensive
        }

        public GoodType Good;
        public float Score;
        public int Exp = 1;

        private const float _DISTANCE_TO_DESTROY = 0.5f;

        private bool _isSelected = false;
        private float _selectTimer = .0f;

        private float _originalRimPower;
        private MeshRenderer _selfMeshRenderer;

        private PurchaseHand _player;
        private static readonly int RimPower = Shader.PropertyToID("_RimPower");

        private void Start()
        {
            _selfMeshRenderer = GetComponent<MeshRenderer>();
            _originalRimPower = _selfMeshRenderer.material.GetFloat(RimPower);
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

            var audioEnum = Good switch
            {
                GoodComponent.GoodType.Low => AudioInventory.AudioEnum.LowGoodGet,
                GoodComponent.GoodType.Middle => AudioInventory.AudioEnum.MiddleGoodGet,
                GoodComponent.GoodType.Expensive => AudioInventory.AudioEnum.ExpensiveGoodGet,
                _ => throw new ArgumentOutOfRangeException()
            };
            AudioInventory.Instance.PlayAudioClip(_player.AudioSource, audioEnum);
            
            _isSelected = true;
        }

        public void OnHoverEnter()
        {
            _selfMeshRenderer.material.SetFloat(RimPower, 1);
        }

        public void OnHoverExit()
        {
            _selfMeshRenderer.material.SetFloat(RimPower, _originalRimPower);
        }

        [PunRPC]
        private void SetSelfInvalidRPC()
        {
            gameObject.SetActive(false);
        }
    }
}