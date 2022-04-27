using System;
using System.Linq;
using Photon.Pun;
using ShoppingCart.Scripts.Audio;
using ShoppingCart.Scripts.Goods.Props;
using ShoppingCart.Scripts.Player;
using ShoppingCart.Scripts.UI;
using UnityEngine;

namespace ShoppingCart.Scripts.Goods
{
    public class ScoreComponent : MonoBehaviourPun
    {
        public float Score = 0;
        public bool HasCourtesyCard = false;
        public float CourtesyPlus = 1.6f;

        public int MaxExp = 20;
        public int Exp = 0;

        public Guid Guid;

        public GameObject Model;
        [HideInInspector] public SkinnedMeshRenderer Mesh;

        private AudioSource _audioSource;


        public event Action OnGetScore;
        public event Action OnGetProp;
        public static event Action PlayerUpdateScore;

        private void OnEnable()
        {
            CourtesyCardRegenerator.Instance.OnClearCourtesyCard += ClearCourtesyCard;
            Score = 0;
            Exp = 0;
            Guid = Guid.NewGuid();
            Debug.Log($"Player {gameObject.name} | GUID {Guid}");
        }

        private void OnDisable()
        {
            CourtesyCardRegenerator.Instance.OnClearCourtesyCard -= ClearCourtesyCard;
        }

        private void Awake()
        {
            Mesh = Model.GetComponentsInChildren<SkinnedMeshRenderer>().FirstOrDefault();
            _audioSource = GetComponent<AudioSource>();
        }

        private void ClearCourtesyCard()
        {
            if (HasCourtesyCard)
            {
                AudioInventory.Instance.PlayAudioClip(_audioSource, AudioInventory.AudioEnum.CouponLost);
            }

            HasCourtesyCard = false;
        }

        public void GetScore(float score, int exp)
        {
            this.photonView.RPC(nameof(SetNewExpRPC), RpcTarget.AllBuffered, exp);

            this.photonView.RPC(nameof(SetNewScoreRPC), RpcTarget.AllBuffered, score);

            PlayerUpdateScore?.Invoke();

            OnGetScore?.Invoke();
        }

        public void BeShoot()
        {
            this.photonView.RPC(nameof(PlayerBeShootRPC), RpcTarget.All);
        }

        public void InstantiateProp(PropBase prop, Transform trans)
        {
            var usedPropGameObject = PhotonNetwork.Instantiate(prop.name, trans.position, trans.rotation);

            var usedProp = usedPropGameObject.GetComponent<PropBase>();

            if (!usedProp) return;

            usedProp.Guid = new Guid();
            usedProp.Guid = Guid;
        }

        public static void ManuallyInvokePlayerUpdateScore()
        {
            PlayerUpdateScore?.Invoke();
        }

        #region RPC methods

        [PunRPC]
        private void SetNewExpRPC(int exp)
        {
            Exp += exp;

            if (Exp < MaxExp) return;

            OnGetProp?.Invoke();

            Exp -= MaxExp;
        }

        [PunRPC]
        private void SetNewScoreRPC(float score)
        {
            var scoreGot = HasCourtesyCard ? CourtesyPlus * score : score;

            Score += scoreGot;
        }

        [PunRPC]
        private void PlayerBeShootRPC()
        {
            GetComponent<InputComponent>().MutePlayerInput();

            if (!HasCourtesyCard) return;

            AudioInventory.Instance.PlayAudioClip(_audioSource, AudioInventory.AudioEnum.CouponLost);
            
            HasCourtesyCard = false;

            var position = transform.position - Vector3.back * 2f;

            CourtesyCardRegenerator.Instance.SpawnCourtesyCardAtPosition(position);
        }

        #endregion
    }
}