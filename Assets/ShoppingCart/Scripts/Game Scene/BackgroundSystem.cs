using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShoppingCart.Scripts.Game_Scene
{
    public class BackgroundSystem : MonoBehaviour
    {
        public List<AudioClip> BGMList = new List<AudioClip>();
        private bool _canPlay = false;
        private bool _isPlaying = false;
        private AudioSource _audioSource;
        private int _currentIndex = 0;

        private void OnEnable()
        {
            GameOverSystem.OnCountdownEnd += StartPlayMusic;
        }

        private void OnDisable()
        {
            GameOverSystem.OnCountdownEnd -= StartPlayMusic;
        }

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (_audioSource.clip)
            {
                Debug.Log(_audioSource.clip.name);
            }

            if (_canPlay)
            {
                _audioSource.clip = BGMList[_currentIndex];

                if (!_isPlaying)
                {
                    StartCoroutine(AudioPlay(_audioSource.clip.length));
                    _audioSource.Play();
                    _isPlaying = true;
                }
            }
        }

        private IEnumerator AudioPlay(float time)
        {
            yield return new WaitForSeconds(time);

            if (_currentIndex == BGMList.Count - 1)
            {
                _currentIndex = 0;
            }
            else
            {
                _currentIndex++;
            }

            _isPlaying = false;
        }

        private void StartPlayMusic()
        {
            _canPlay = true;
        }
    }
}