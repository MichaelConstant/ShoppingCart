using System;
using Photon.Pun;
using UnityEngine;

namespace ShoppingCart.Scripts.Network
{
    public class PlayerNetworkSynchronization : MonoBehaviour, IPunObservable
    {
        private PhotonView _photonView;
        
        //Main VRPlayer Transform
        [Header("Main VRPlayer Transform")]
        public Transform GeneralVRPlayerTransform;

        //Position
        private float _genericPlayerDistance;
        private Vector3 _genericPlayerDirection;
        private Vector3 _genericPlayerNetworkPosition;
        private Vector3 _genericPlayerStoredPosition;

        //Rotation
        private Quaternion _genericNetworkRotation;
        private float _genericPlayerAngle;
        
        //Main Avatar Transform
        [Header("Main Avatar Transform")]
        public Transform MainAvatarTransform;
        
        //Position
        private float _mainAvatarDistance;
        private Vector3 _mainAvatarDirection;
        private Vector3 _mainAvatarNetworkPosition;
        private Vector3 _mainAvatarStoredPosition;

        //Rotation
        private Quaternion _mainAvatarNetworkRotation;
        private float _mainAvatarAngle;

        //Head
        //Rotation
        [Header("Avatar Head Transform ")]
        public Transform HeadTransform;

        private Quaternion _headNetworkRotation;
        private float _headAngle;

        //Body
        //Rotation
        [Header("Avatar Body Transform")]
        public Transform BodyTransform;

        private Quaternion _bodyNetworkRotation;
        private float _bodyAngle;
        
        //Hands
        [Header("Hands Transform")] 
        public Transform LeftHandTransform;
        public Transform RightHandTransform;

        //Left Hand Sync
        //Position
        private float _leftHandDistance;

        private Vector3 _leftHandDirection;
        private Vector3 _leftHandNetworkPosition;
        private Vector3 _leftHandStoredPosition;

        //Rotation
        private Quaternion _leftHandNetworkRotation;
        private float _leftHandAngle;
        
        //Right Hand
        //Position
        private float _rightHandDistance;

        private Vector3 _rightHandDirection;
        private Vector3 _rightHandNetworkPosition;
        private Vector3 _rightHandStoredPosition;

        //Rotation
        private Quaternion _rightHandNetworkRotation;
        private float _rightHandAngle;
        
        private bool _firstTake = false;

        public void Awake()
        {
            _photonView = GetComponent<PhotonView>();

            //Main VRPlayer Synch Init
            _genericPlayerStoredPosition = GeneralVRPlayerTransform.position;
            _genericPlayerNetworkPosition = Vector3.zero;
            _genericNetworkRotation = Quaternion.identity;

            //Main Avatar Synch Init
            _mainAvatarStoredPosition = MainAvatarTransform.localPosition;
            _mainAvatarNetworkPosition = Vector3.zero;
            _mainAvatarNetworkRotation = Quaternion.identity;

            //Head Synch Init
            _headNetworkRotation = Quaternion.identity;

            //Body Synch Init
            _bodyNetworkRotation = Quaternion.identity;

            //Left Hand Synch Init
            _leftHandStoredPosition = LeftHandTransform.localPosition;
            _leftHandNetworkPosition = Vector3.zero;
            _leftHandNetworkRotation = Quaternion.identity;

            //Right Hand Synch Init
            _rightHandStoredPosition = RightHandTransform.localPosition;
            _rightHandNetworkPosition = Vector3.zero;
            _rightHandNetworkRotation = Quaternion.identity;
        }

        void OnEnable()
        {
            _firstTake = true;
        }

        public void Update()
        {
            if (this._photonView.IsMine) return;
            
            GeneralVRPlayerTransform.position = Vector3.MoveTowards(GeneralVRPlayerTransform.position,
                this._genericPlayerNetworkPosition,
                this._genericPlayerDistance * (1.0f / PhotonNetwork.SerializationRate));
            GeneralVRPlayerTransform.rotation = Quaternion.RotateTowards(GeneralVRPlayerTransform.rotation,
                this._genericNetworkRotation,
                this._genericPlayerAngle * (1.0f / PhotonNetwork.SerializationRate));

            MainAvatarTransform.localPosition = Vector3.MoveTowards(MainAvatarTransform.localPosition,
                this._mainAvatarNetworkPosition,
                this._mainAvatarDistance * (1.0f / PhotonNetwork.SerializationRate));
            MainAvatarTransform.localRotation = Quaternion.RotateTowards(MainAvatarTransform.localRotation,
                this._mainAvatarNetworkRotation,
                this._mainAvatarAngle * (1.0f / PhotonNetwork.SerializationRate));


            HeadTransform.localRotation = Quaternion.RotateTowards(HeadTransform.localRotation,
                this._headNetworkRotation, this._headAngle * (1.0f / PhotonNetwork.SerializationRate));

            BodyTransform.localRotation = Quaternion.RotateTowards(BodyTransform.localRotation,
                this._bodyNetworkRotation, this._bodyAngle * (1.0f / PhotonNetwork.SerializationRate));


            LeftHandTransform.localPosition = Vector3.MoveTowards(LeftHandTransform.localPosition,
                this._leftHandNetworkPosition,
                this._leftHandDistance * (1.0f / PhotonNetwork.SerializationRate));
            LeftHandTransform.localRotation = Quaternion.RotateTowards(LeftHandTransform.localRotation,
                this._leftHandNetworkRotation, this._leftHandAngle * (1.0f / PhotonNetwork.SerializationRate));

            RightHandTransform.localPosition = Vector3.MoveTowards(RightHandTransform.localPosition,
                this._rightHandNetworkPosition,
                this._rightHandDistance * (1.0f / PhotonNetwork.SerializationRate));
            RightHandTransform.localRotation = Quaternion.RotateTowards(RightHandTransform.localRotation,
                this._rightHandNetworkRotation,
                this._rightHandAngle * (1.0f / PhotonNetwork.SerializationRate));
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                //////////////////////////////////////////////////////////////////
                //General VRPlayer Transform Synch

                //Send Main Avatar position data
                this._genericPlayerDirection =
                    GeneralVRPlayerTransform.position - this._genericPlayerStoredPosition;
                this._genericPlayerStoredPosition = GeneralVRPlayerTransform.position;

                stream.SendNext(GeneralVRPlayerTransform.position);
                stream.SendNext(this._genericPlayerDirection);

                //Send Main Avatar rotation data
                stream.SendNext(GeneralVRPlayerTransform.rotation);


                //////////////////////////////////////////////////////////////////
                //Main Avatar Transform Synch

                //Send Main Avatar position data
                this._mainAvatarDirection = MainAvatarTransform.localPosition - this._mainAvatarStoredPosition;
                this._mainAvatarStoredPosition = MainAvatarTransform.localPosition;

                stream.SendNext(MainAvatarTransform.localPosition);
                stream.SendNext(this._mainAvatarDirection);

                //Send Main Avatar rotation data
                stream.SendNext(MainAvatarTransform.localRotation);


                ///////////////////////////////////////////////////////////////////
                //Head rotation synch

                //Send Head rotation data
                stream.SendNext(HeadTransform.localRotation);


                ///////////////////////////////////////////////////////////////////
                //Body rotation synch

                //Send Body rotation data
                stream.SendNext(BodyTransform.localRotation);


                ///////////////////////////////////////////////////////////////////
                //Hands Transform Synch
                //Left Hand
                //Send Left Hand position data
                this._leftHandDirection = LeftHandTransform.localPosition - this._leftHandStoredPosition;
                this._leftHandStoredPosition = LeftHandTransform.localPosition;

                stream.SendNext(LeftHandTransform.localPosition);
                stream.SendNext(this._leftHandDirection);

                //Send Left Hand rotation data
                stream.SendNext(LeftHandTransform.localRotation);

                //Right Hand
                //Send Right Hand position data
                this._rightHandDirection = RightHandTransform.localPosition - this._rightHandStoredPosition;
                this._rightHandStoredPosition = RightHandTransform.localPosition;

                stream.SendNext(RightHandTransform.localPosition);
                stream.SendNext(this._rightHandDirection);

                //Send Right Hand rotation data
                stream.SendNext(RightHandTransform.localRotation);
            }
            else
            {
                ///////////////////////////////////////////////////////////////////
                //Ganeral VR Player Transform Synch

                //Get VR Player position data
                this._genericPlayerNetworkPosition = (Vector3) stream.ReceiveNext();
                this._genericPlayerDirection = (Vector3) stream.ReceiveNext();

                if (_firstTake)
                {
                    GeneralVRPlayerTransform.position = this._genericPlayerNetworkPosition;
                    this._genericPlayerDistance = 0f;
                }
                else
                {
                    float lag = Mathf.Abs((float) (PhotonNetwork.Time - info.SentServerTime));
                    this._genericPlayerNetworkPosition += this._genericPlayerDirection * lag;
                    this._genericPlayerDistance = Vector3.Distance(GeneralVRPlayerTransform.position,
                        this._genericPlayerNetworkPosition);
                }

                //Get Main Avatar rotation data
                this._genericNetworkRotation = (Quaternion) stream.ReceiveNext();
                if (_firstTake)
                {
                    this._genericPlayerAngle = 0f;
                    GeneralVRPlayerTransform.rotation = this._genericNetworkRotation;
                }
                else
                {
                    this._genericPlayerAngle = Quaternion.Angle(GeneralVRPlayerTransform.rotation,
                        this._genericNetworkRotation);
                }

                ///////////////////////////////////////////////////////////////////
                //Main Avatar Transform Synch

                //Get Main Avatar position data
                this._mainAvatarNetworkPosition = (Vector3) stream.ReceiveNext();
                this._mainAvatarDirection = (Vector3) stream.ReceiveNext();

                if (_firstTake)
                {
                    MainAvatarTransform.localPosition = this._mainAvatarNetworkPosition;
                    this._mainAvatarDistance = 0f;
                }
                else
                {
                    float lag = Mathf.Abs((float) (PhotonNetwork.Time - info.SentServerTime));
                    this._mainAvatarNetworkPosition += this._mainAvatarDirection * lag;
                    this._mainAvatarDistance = Vector3.Distance(MainAvatarTransform.localPosition,
                        this._mainAvatarNetworkPosition);
                }

                //Get Main Avatar rotation data
                this._mainAvatarNetworkRotation = (Quaternion) stream.ReceiveNext();
                if (_firstTake)
                {
                    this._mainAvatarAngle = 0f;
                    MainAvatarTransform.rotation = this._mainAvatarNetworkRotation;
                }
                else
                {
                    this._mainAvatarAngle =
                        Quaternion.Angle(MainAvatarTransform.rotation, this._mainAvatarNetworkRotation);
                }


                ///////////////////////////////////////////////////////////////////
                //Head rotation synch
                //Get Head rotation data 
                this._headNetworkRotation = (Quaternion) stream.ReceiveNext();

                if (_firstTake)
                {
                    this._headAngle = 0f;
                    HeadTransform.localRotation = this._headNetworkRotation;
                }
                else
                {
                    this._headAngle = Quaternion.Angle(HeadTransform.localRotation, this._headNetworkRotation);
                }

                ///////////////////////////////////////////////////////////////////
                //Body rotation synch
                //Get Body rotation data 
                this._bodyNetworkRotation = (Quaternion) stream.ReceiveNext();

                if (_firstTake)
                {
                    this._bodyAngle = 0f;
                    BodyTransform.localRotation = this._bodyNetworkRotation;
                }
                else
                {
                    this._bodyAngle = Quaternion.Angle(BodyTransform.localRotation, this._bodyNetworkRotation);
                }

                ///////////////////////////////////////////////////////////////////
                //Hands Transform Synch
                //Get Left Hand position data
                this._leftHandNetworkPosition = (Vector3) stream.ReceiveNext();
                this._leftHandDirection = (Vector3) stream.ReceiveNext();

                if (_firstTake)
                {
                    LeftHandTransform.localPosition = this._leftHandNetworkPosition;
                    this._leftHandDistance = 0f;
                }
                else
                {
                    float lag = Mathf.Abs((float) (PhotonNetwork.Time - info.SentServerTime));
                    this._leftHandNetworkPosition += this._leftHandDirection * lag;
                    this._leftHandDistance =
                        Vector3.Distance(LeftHandTransform.localPosition, this._leftHandNetworkPosition);
                }

                //Get Left Hand rotation data
                this._leftHandNetworkRotation = (Quaternion) stream.ReceiveNext();
                if (_firstTake)
                {
                    this._leftHandAngle = 0f;
                    LeftHandTransform.localRotation = this._leftHandNetworkRotation;
                }
                else
                {
                    this._leftHandAngle =
                        Quaternion.Angle(LeftHandTransform.localRotation, this._leftHandNetworkRotation);
                }

                //Get Right Hand position data
                this._rightHandNetworkPosition = (Vector3) stream.ReceiveNext();
                this._rightHandDirection = (Vector3) stream.ReceiveNext();

                if (_firstTake)
                {
                    RightHandTransform.localPosition = this._rightHandNetworkPosition;
                    this._rightHandDistance = 0f;
                }
                else
                {
                    float lag = Mathf.Abs((float) (PhotonNetwork.Time - info.SentServerTime));
                    this._rightHandNetworkPosition += this._rightHandDirection * lag;
                    this._rightHandDistance = Vector3.Distance(RightHandTransform.localPosition,
                        this._rightHandNetworkPosition);
                }

                //Get Right Hand rotation data
                this._rightHandNetworkRotation = (Quaternion) stream.ReceiveNext();
                if (_firstTake)
                {
                    this._rightHandAngle = 0f;
                    RightHandTransform.localRotation = this._rightHandNetworkRotation;
                }
                else
                {
                    this._rightHandAngle =
                        Quaternion.Angle(RightHandTransform.localRotation, this._rightHandNetworkRotation);
                }

                if (_firstTake)
                {
                    _firstTake = false;
                }
            }
        }
    }
}