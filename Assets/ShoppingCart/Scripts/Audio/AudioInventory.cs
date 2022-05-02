using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ShoppingCart.Scripts.Audio
{
    public class AudioInventory : Singleton<AudioInventory>
    {
        public enum AudioEnum
        {
            CouponAppear,
            CouponGet,
            CouponLost,
            ExpensiveGoodGet,
            MiddleGoodGet,
            LowGoodGet,
            PlayerBeingHurt,
            PlayerHitOthers,
            PropGet,
            PropShoot,
            UIConfirm,
            UISelect,
            UICountdown,
            PlayerRun,
        }
        
        [Serializable]
        public struct AudioStruct
        {
            public AudioClip AudioClip;
            public AudioEnum AudioEnum;
        }
        
        [SerializeField] private List<AudioStruct> AudioClips = new List<AudioStruct>();

        private AudioSource _audioSource;
        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }
        
        public void PlayAudioClip(AudioSource audioSource, AudioEnum audioClip)
        {
            var audioClipToPlay = AudioClips.FirstOrDefault(audioStruct => audioStruct.AudioEnum == audioClip);

            if (audioClipToPlay.AudioClip != null)
            {
                audioSource.PlayOneShot(audioClipToPlay.AudioClip);
            }
        }

        public void PlayAudioClipAtLocation(Vector3 position, AudioEnum audioClip)
        {
            var audioClipToPlay = AudioClips.FirstOrDefault(audioStruct => audioStruct.AudioEnum == audioClip);

            if (audioClipToPlay.AudioClip != null)
            {
                transform.position = position;
                _audioSource.PlayOneShot(audioClipToPlay.AudioClip);
            }
        }
    }
}