using System;
using System.Collections.Generic;
using ShoppingCart.Scripts.Audio;
using ShoppingCart.Scripts.Goods.Props;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using Random = UnityEngine.Random;

namespace ShoppingCart.Scripts.Goods
{
    public class PropInventory : MonoBehaviour
    {
        public List<PropBase> PropList = new List<PropBase>();

        public PropBase CurrentProp => _currentProp;
        private PropBase _currentProp;

        public delegate void OnChangePropHandler(int number);

        public event OnChangePropHandler OnChangeProp;

        private AudioSource _audioSource;
        
        private void OnEnable()
        {
            var scoreComponent = GetComponent<ScoreComponent>();
            _audioSource = GetComponent<AudioSource>();
            if (!scoreComponent) return;
            GetComponent<ScoreComponent>().OnGetProp += GetProp;
        }

        private void OnDisable()
        {
            var scoreComponent = GetComponent<ScoreComponent>();
            if (!scoreComponent) return;
            GetComponent<ScoreComponent>().OnGetProp -= GetProp;
        }

        private void GetProp()
        {
            if (PropList == null || PropList.Count < 1) return;

            var index = Random.Range(0, PropList.Count - 1);

            var newProp = PropList[index];

            AudioInventory.Instance.PlayAudioClip(_audioSource, AudioInventory.AudioEnum.PropGet);
            
            _currentProp = newProp;

            OnChangeProp?.Invoke(index + 1);
        }
        
        public void ShootProp()
        {
            _currentProp = null;
            OnChangeProp?.Invoke(0);
        }
    }
}