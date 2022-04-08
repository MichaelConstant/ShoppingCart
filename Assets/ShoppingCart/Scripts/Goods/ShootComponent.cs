using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

namespace ShoppingCart.Scripts.Goods
{
    [Serializable]
    public enum DeviceName
    {
        LeftHand,
        RightHand,
    }

    public class ShootComponent : MonoBehaviour
    {
        public DeviceName MyDevice;

        public PropInventory PropInventory;
        public float DestroyTime = 5f;
        public ScoreComponent ScoreComponent;
        
        private bool _isPressedButton;

        private bool _firstSwitch;
        private bool _secondSwitch;
        private bool _thirdSwitch;

        private float _timer;
        private const float _SHOOT_COOLDOWN = 0.5f;

        private void Update()
        {
            switch (MyDevice)
            {
                case DeviceName.LeftHand:
                    DeviceManager.Instance.LeftHandDevice.TryGetFeatureValue(CommonUsages.triggerButton,
                        out _isPressedButton);
                    break;
                case DeviceName.RightHand:
                    DeviceManager.Instance.RightHandDevice.TryGetFeatureValue(CommonUsages.triggerButton,
                        out _isPressedButton);
                    break;
                default:
                    return;
            }

            #region Quick Pressed Twice TriggerButton

            if (_thirdSwitch)
            {
                if (!_isPressedButton)
                {
                    _thirdSwitch = false;
                }

                return;
            }

            if (_isPressedButton && !_secondSwitch && !_firstSwitch)
            {
                _firstSwitch = true;
            }

            if (_firstSwitch)
            {
                _timer += Time.deltaTime;

                if (!_secondSwitch && !_isPressedButton)
                {
                    _secondSwitch = true;
                }
            }

            if (_timer <= _SHOOT_COOLDOWN)
            {
                if (!_secondSwitch || !_firstSwitch || !_isPressedButton) return;

                _firstSwitch = false;
                _secondSwitch = false;
                _thirdSwitch = true;
                _timer = 0;
                Shoot();
            }
            else
            {
                _firstSwitch = false;
                _secondSwitch = false;
                _timer = 0;
            }

            #endregion
        }

        private void Shoot()
        {
            if (!PropInventory || !PropInventory.CurrentProp) return;

            var shootProp = Instantiate(PropInventory.CurrentProp, transform.position, transform.rotation);
            
            shootProp.Guid = new Guid();
            if (ScoreComponent)
            {
                shootProp.Guid = ScoreComponent.Guid;
            }
            
            Destroy(shootProp, DestroyTime);

            PropInventory.ShootProp();
        }
    }
}