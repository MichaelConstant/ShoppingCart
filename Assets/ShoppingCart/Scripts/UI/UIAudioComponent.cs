using System;
using ShoppingCart.Scripts.Audio;
using UnityEngine;

namespace ShoppingCart.Scripts.UI
{
    public class UIAudioComponent : MonoBehaviour
    {
        private AudioSource _audioSource;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void OnHovered()
        {
            AudioInventory.Instance.PlayAudioClip(_audioSource, AudioInventory.AudioEnum.UISelect);
        }

        public void OnConfirm()
        {
            AudioInventory.Instance.PlayAudioClip(_audioSource, AudioInventory.AudioEnum.UIConfirm);
        }
    }
}